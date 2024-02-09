using Dispatch.Domain;
using Timer = System.Windows.Forms.Timer;

namespace Dispatch.GUI
{
    public partial class DispatchWindow : Form
    {
        #region Variables
        private readonly Timer buttonsReenablingTimer = new() { Interval = Settings.ButtonsReleaseThrottleMilliseconds };
        private IEngine bl = null!; // See https://stackoverflow.com/a/60812813/3559724 (similar to Dart's _late_).
        bool isStarted = false;
        #endregion

        #region Constructor
        public DispatchWindow()
        {
            InitializeComponent();

            // Optional.
            SetInDirectory(Settings.InDirectory);
            SetOutDirectory(Settings.OutDirectory);

            this.buttonsReenablingTimer.Tick += ButtonsReenablingTimer_Tick;
        }

        private void Dispatch_FormClosed(object sender, FormClosedEventArgs e)
        {
            CleanupEngine();
        }
        private void SetInDirectory(string inDirectory)
        {
            this.inDirectoryBrowser.SelectedPath = this.tbxInDirectory.Text = inDirectory;
            ResetState();
            SetReadyToStart();
        }
        private void SetOutDirectory(string outDirectory)
        {
            this.outDirectoryBrowser.SelectedPath = this.tbxOutDirectory.Text = outDirectory;
            ResetState();
            SetReadyToStart();
        }
        private void ResetState()
        {
            this.cbxReadOnly.Enabled = true;
            this.isStarted = false;
        }
        private void SetReadyToStart()
        {
            if (AreAllFieldsSet())
            {
                this.btnNext.Enabled = true;
                this.btnNext.Text = "Start!";
                BuildEngine();
            }
        }
        private void BuildEngine()
        {
            CleanupEngine();

            this.bl = new Engine(this.tbxInDirectory.Text, this.tbxOutDirectory.Text);
            this.bl.SkipCountUpdated += Engine_SkipCountUpdated;
            this.bl.CountsUpdated += Engine_CountsUpdated;
            this.bl.WarningThrown += Engine_WarningThrown;
            this.bl.EndReached += Engine_EndReached;
            ThreadSafeRefreshCountLabels();
        }
        private void CleanupEngine()
        {
            if (this.bl != null)
            {
                this.bl.CountsUpdated -= Engine_CountsUpdated;
                this.bl.SkipCountUpdated -= Engine_SkipCountUpdated;
                this.bl.EndReached -= Engine_EndReached;
                this.bl.WarningThrown -= Engine_WarningThrown;
            }
        }
        #endregion

        #region Methods
        private void ButtonsReenablingTimer_Tick(object? sender, EventArgs e)
        {
            this.buttonsReenablingTimer.Stop();
            EnableMoveButtons();
        }
        private void EnableMoveButtons()
        {
            this.btnMoveOut.Enabled = true;
            this.btnMoveDelete.Enabled = true;
        }
        private void DisableMoveButtons()
        {
            this.btnMoveOut.Enabled = false;
            this.btnMoveDelete.Enabled = false;

            //// Invoke back to UI thread (see https://stackoverflow.com/a/142069).
            //this.btnMoveOut.Invoke(new MethodInvoker(delegate { this.btnMoveOut.Enabled = false; }));
            //this.btnMoveDelete.Invoke(new MethodInvoker(delegate { this.btnMoveDelete.Enabled = false; }));
        }
        private void ShowMoveButtons()
        {
            this.btnMoveOut.Visible = true;
            this.btnMoveDelete.Visible = true;
        }
        private void HideMoveButtons()
        {
            this.btnMoveOut.Visible = false;
            this.btnMoveDelete.Visible = false;
        }
        private bool AreAllFieldsSet()
        {
            return !String.IsNullOrEmpty(this.inDirectoryBrowser.SelectedPath) && !String.IsNullOrEmpty(this.outDirectoryBrowser.SelectedPath);
        }

        private void Engine_SkipCountUpdated(object? sender, SkipArgs e)
        {
            this.btnNext.Enabled = e.Count != 0;
            this.btnNext.Text = $"Next\n(skip: {e.Count})";
        }
        private void Engine_WarningThrown(object? sender, WarningArgs e)
        {
            MessageBox.Show(e.Message);
        }
        private void Engine_CountsUpdated()
        {
            ThreadSafeRefreshCountLabels();
        }
        private void Engine_EndReached()
        {
            DisableMoveButtons();
            MessageBox.Show("Last file being treated; quickly close the application where it is open so that it can be treated");
            this.btnNext.Text = "Finished";
        }

        private void RefreshCountLabels()
        {
            int allTimeCount = this.bl.DoneCount.Count + this.bl.RemainingCount;

            this.lblSessionCount.Text = $"{this.bl.SessionCount} = {String.Format("{0:0.00}%", 100.0 * this.bl.SessionCount.Count / this.bl.RemainingCount)}";
            this.lblTodayCount.Text = $"{this.bl.Counts[this.bl.Today]} = {String.Format("{0:0.00}%", 100.0 * this.bl.Counts[this.bl.Today].Count / allTimeCount)}";
            this.lblWeekCount.Text = $"{this.bl.WeekCount} = {String.Format("{0:0.00}%", 100.0 * this.bl.WeekCount.Count / allTimeCount)}";
            this.lblMonthCount.Text = $"{this.bl.MonthCount} = {String.Format("{0:0.00}%", 100.0 * this.bl.MonthCount.Count / allTimeCount)}";
            this.lblYearCount.Text = $"{this.bl.YearCount} = {String.Format("{0:0.00}%", 100.0 * this.bl.YearCount.Count / allTimeCount)}";
            this.lblAllCount.Text = $"{this.bl.DoneCount} = {String.Format("{0:0.00}%", 100.0 * this.bl.DoneCount.Count / allTimeCount)}";
            this.Text = $"Dispatch (Left: {this.bl.InFolderCount - this.bl.SessionCount.Count} root, {this.bl.RemainingCount} recursive)";
        }
        private void ThreadSafeRefreshCountLabels()
        {
            // Invoke back to UI thread (see https://stackoverflow.com/a/142069).
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(RefreshCountLabels));
            }
            else
            {
                RefreshCountLabels();
            }
        }
        private void BtnBrowseInFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = this.inDirectoryBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                SetInDirectory(this.inDirectoryBrowser.SelectedPath);
            }
        }
        private void BtnBrowseOutDirectory_Click(object sender, EventArgs e)
        {
            DialogResult result = this.outDirectoryBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                SetOutDirectory(this.outDirectoryBrowser.SelectedPath);
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // See https://stackoverflow.com/a/22788658
            switch (keyData)
            {
                case Keys.Back:
                case Keys.Left: this.bl.RunCurrent(); break;   // Play it again, Sam.
                case Keys.Right: this.btnNext.PerformClick(); break;
                case Keys.Add:
                //case Keys.Insert:
                case Keys.Y: this.btnMoveOut.PerformClick(); break;
                case Keys.Delete:
                case Keys.Subtract:
                case Keys.N: this.btnMoveDelete.PerformClick(); break;
                default: break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // `async void` is permitted here to make an event handler asynchronous (see https://stackoverflow.com/a/45449457/3559724).
        private async void BtnNext_Click(object sender, EventArgs e)
        {
            if (!this.isStarted)
            {
                EnableMoveButtons();
                this.cbxReadOnly.Enabled = false;
                this.isStarted = true;
            }

            await this.bl.Next();
        }
        private async void BtnMoveOut_Click(object sender, EventArgs e)
        {
            DisableMoveButtons();
            await this.bl.Move();
            this.buttonsReenablingTimer.Start();
        }
        private async void BtnMoveDelete_Click(object sender, EventArgs e)
        {
            DisableMoveButtons();
            await this.bl.Delete();
            this.buttonsReenablingTimer.Start();
        }
        #endregion

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            // Simplified from https://stackoverflow.com/a/13680458.
            if (File.Exists(this.bl.CurrentFilePath))
            {
                System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", this.bl.CurrentFilePath));
            }
        }

        private void CbxReadOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbxReadOnly.CheckState == CheckState.Checked)
            {
                HideMoveButtons();
            }
            else
            {
                ShowMoveButtons();
            }
        }
    }
}