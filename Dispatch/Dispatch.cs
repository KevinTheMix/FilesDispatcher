using Dispatch.Domain;
using Timer = System.Windows.Forms.Timer;

namespace Dispatch
{
    public partial class Dispatch : Form
    {
        #region Variables
        private Timer buttonsReenablingTimer = new Timer() { Interval = Settings.TreatmentThrottleMilliseconds };
        private IEngine bl;
        bool isStarted = false;
        #endregion

        #region Constructor
        public Dispatch()
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
            this.inDirectoryBrowser.SelectedPath = inDirectory;
            this.tbxInDirectory.Text = inDirectory;

            if (AreAllFieldsSet())
            {
                this.btnNext.Enabled = true;
                this.btnNext.Text = "Start!";
                BuildEngine();
            }
        }
        private void SetOutDirectory(string outDirectory)
        {
            this.outDirectoryBrowser.SelectedPath = outDirectory;
            this.tbxOutDirectory.Text = outDirectory;

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
            RefreshCountLabels();
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
            EnableButtons();
        }
        private void EnableButtons()
        {
            this.btnMoveOut.Enabled = true;
            this.btnMoveDelete.Enabled = true;
        }
        private void DisableButtons()
        {
            this.btnMoveOut.Enabled = false;
            this.btnMoveDelete.Enabled = false;

            //// Invoke back to UI thread (see https://stackoverflow.com/a/142069).
            //this.btnMoveOut.Invoke(new MethodInvoker(delegate { this.btnMoveOut.Enabled = false; }));
            //this.btnMoveDelete.Invoke(new MethodInvoker(delegate { this.btnMoveDelete.Enabled = false; }));
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
            RefreshCountLabels();
        }
        private void Engine_EndReached()
        {
            DisableButtons();
            MessageBox.Show("Last file being treated; quickly close the application where it is open so that it can be treated");
            this.btnNext.Text = "Finished";
        }

        private void RefreshCountLabels()
        {
            // Invoke back to UI thread (see https://stackoverflow.com/a/142069).
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    this.lblSessionCount.Text = $"{this.bl.SessionCount} = {String.Format("{0:0.00}%", 100.0 * this.bl.SessionCount.Count / this.bl.OriginalFilesCount)}";
                    this.lblTodayCount.Text = $"{this.bl.Counts[this.bl.Today]} = {String.Format("{0:0.00}%", 100.0 * this.bl.Counts[this.bl.Today].Count / this.bl.OriginalFilesCount)}";
                    this.lblWeekCount.Text = $"{this.bl.WeekCount} = {String.Format("{0:0.00}%", 100.0 * this.bl.WeekCount.Count / this.bl.OriginalFilesCount)}";
                    this.lblMonthCount.Text = $"{this.bl.MonthCount} = {String.Format("{0:0.00}%", 100.0 * this.bl.MonthCount.Count / this.bl.OriginalFilesCount)}";
                    this.lblYearCount.Text = $"{this.bl.YearCount} = {String.Format("{0:0.00}%", 100.0 * this.bl.YearCount.Count / this.bl.OriginalFilesCount)}";
                    this.Text = $"Dispatch ({this.bl.inFolderFilesCount - this.bl.SessionCount.Count}/{this.bl.OriginalFilesCount})";
                }));
            }
            else
            {
                this.lblSessionCount.Text = $"{this.bl.SessionCount} = {String.Format("{0:0.00}%", 100.0 * this.bl.SessionCount.Count / this.bl.OriginalFilesCount)}";
                this.lblTodayCount.Text = $"{this.bl.Counts[this.bl.Today]} = {String.Format("{0:0.00}%", 100.0 * this.bl.Counts[this.bl.Today].Count / this.bl.OriginalFilesCount)}";
                this.lblWeekCount.Text = $"{this.bl.WeekCount} = {String.Format("{0:0.00}%", 100.0 * this.bl.WeekCount.Count / this.bl.OriginalFilesCount)}";
                this.lblMonthCount.Text = $"{this.bl.MonthCount} = {String.Format("{0:0.00}%", 100.0 * this.bl.MonthCount.Count / this.bl.OriginalFilesCount)}";
                this.lblYearCount.Text = $"{this.bl.YearCount} = {String.Format("{0:0.00}%", 100.0 * this.bl.YearCount.Count / this.bl.OriginalFilesCount)}";
                this.Text = $"Dispatch ({this.bl.inFolderFilesCount - this.bl.SessionCount.Count}/{this.bl.OriginalFilesCount})";
            }
        }
        private void btnBrowseInFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = this.inDirectoryBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                SetInDirectory(this.inDirectoryBrowser.SelectedPath);
            }
        }
        private void btnBrowseOutDirectory_Click(object sender, EventArgs e)
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
        private async void btnNext_Click(object sender, EventArgs e)
        {
            await this.bl.Next();

            if(!this.isStarted)
            {
                EnableButtons();
                this.isStarted = true;
            }
        }
        private async void btnMoveOut_Click(object sender, EventArgs e)
        {
            DisableButtons();
            await this.bl.Move();
            this.buttonsReenablingTimer.Start();
        }
        private async void btnMoveDelete_Click(object sender, EventArgs e)
        {
            DisableButtons();
            await this.bl.Delete();
            this.buttonsReenablingTimer.Start();
        }
        #endregion

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            // Simplified from https://stackoverflow.com/a/13680458.
            if (File.Exists(this.bl.CurrentFilePath))
            {
                System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", this.bl.CurrentFilePath));
            }
        }
    }
}