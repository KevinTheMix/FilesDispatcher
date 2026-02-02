namespace Dispatch.GUI
{
    partial class DispatchWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DispatchWindow));
            tbxInDirectory = new TextBox();
            btnBrowseInFolder = new Button();
            inDirectoryBrowser = new FolderBrowserDialog();
            btnNext = new Button();
            btnMoveDelete = new Button();
            tbxOutDirectory = new TextBox();
            btnBrowseOutFolder = new Button();
            outDirectoryBrowser = new FolderBrowserDialog();
            btnMoveOut = new Button();
            lblSessionCount = new Label();
            lblTodayCount = new Label();
            lblSession = new Label();
            lblToday = new Label();
            lblWeek = new Label();
            lblWeekCount = new Label();
            lblMonth = new Label();
            lblMonthCount = new Label();
            lblYear = new Label();
            lblYearCount = new Label();
            btnFindFile = new Button();
            lblAllCount = new Label();
            lblAll = new Label();
            cbxReadOnly = new CheckBox();
            SuspendLayout();
            // 
            // tbxInDirectory
            // 
            tbxInDirectory.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point);
            tbxInDirectory.Location = new Point(12, 12);
            tbxInDirectory.Name = "tbxInDirectory";
            tbxInDirectory.ReadOnly = true;
            tbxInDirectory.Size = new Size(150, 20);
            tbxInDirectory.TabIndex = 1;
            // 
            // btnBrowseInFolder
            // 
            btnBrowseInFolder.Location = new Point(168, 11);
            btnBrowseInFolder.Name = "btnBrowseInFolder";
            btnBrowseInFolder.Size = new Size(50, 23);
            btnBrowseInFolder.TabIndex = 2;
            btnBrowseInFolder.Text = "In";
            btnBrowseInFolder.UseVisualStyleBackColor = true;
            btnBrowseInFolder.Click += BtnBrowseInFolder_Click;
            // 
            // btnNext
            // 
            btnNext.Enabled = false;
            btnNext.Location = new Point(168, 95);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(102, 43);
            btnNext.TabIndex = 7;
            btnNext.Text = "Start!";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += BtnNext_Click;
            // 
            // btnMoveDelete
            // 
            btnMoveDelete.BackColor = Color.Coral;
            btnMoveDelete.Enabled = false;
            btnMoveDelete.ForeColor = Color.Orange;
            btnMoveDelete.Location = new Point(88, 95);
            btnMoveDelete.Name = "btnMoveDelete";
            btnMoveDelete.Size = new Size(70, 43);
            btnMoveDelete.TabIndex = 6;
            btnMoveDelete.Text = "Delete";
            btnMoveDelete.UseVisualStyleBackColor = false;
            btnMoveDelete.Click += BtnMoveDelete_Click;
            // 
            // tbxOutDirectory
            // 
            tbxOutDirectory.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point);
            tbxOutDirectory.Location = new Point(12, 41);
            tbxOutDirectory.Name = "tbxOutDirectory";
            tbxOutDirectory.ReadOnly = true;
            tbxOutDirectory.Size = new Size(150, 20);
            tbxOutDirectory.TabIndex = 3;
            // 
            // btnBrowseOutFolder
            // 
            btnBrowseOutFolder.Location = new Point(168, 39);
            btnBrowseOutFolder.Name = "btnBrowseOutFolder";
            btnBrowseOutFolder.Size = new Size(51, 23);
            btnBrowseOutFolder.TabIndex = 4;
            btnBrowseOutFolder.Text = "Out";
            btnBrowseOutFolder.UseVisualStyleBackColor = true;
            btnBrowseOutFolder.Click += BtnBrowseOutDirectory_Click;
            // 
            // btnMoveOut
            // 
            btnMoveOut.BackColor = Color.PaleGreen;
            btnMoveOut.Enabled = false;
            btnMoveOut.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnMoveOut.ForeColor = Color.MediumSeaGreen;
            btnMoveOut.Location = new Point(12, 95);
            btnMoveOut.Name = "btnMoveOut";
            btnMoveOut.Size = new Size(70, 43);
            btnMoveOut.TabIndex = 5;
            btnMoveOut.Text = "Very Good";
            btnMoveOut.UseVisualStyleBackColor = false;
            btnMoveOut.Click += BtnMoveOut_Click;
            // 
            // lblSessionCount
            // 
            lblSessionCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSessionCount.Location = new Point(346, 11);
            lblSessionCount.Name = "lblSessionCount";
            lblSessionCount.Size = new Size(130, 15);
            lblSessionCount.TabIndex = 9;
            lblSessionCount.Text = "10 (5+5) = 0,01%";
            lblSessionCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTodayCount
            // 
            lblTodayCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTodayCount.Location = new Point(346, 33);
            lblTodayCount.Name = "lblTodayCount";
            lblTodayCount.Size = new Size(130, 15);
            lblTodayCount.TabIndex = 11;
            lblTodayCount.Text = "40 (20+20) = 0,10%";
            lblTodayCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblSession
            // 
            lblSession.AutoSize = true;
            lblSession.Location = new Point(276, 11);
            lblSession.Name = "lblSession";
            lblSession.Size = new Size(49, 15);
            lblSession.TabIndex = 8;
            lblSession.Text = "Session:";
            // 
            // lblToday
            // 
            lblToday.AutoSize = true;
            lblToday.Location = new Point(276, 33);
            lblToday.Name = "lblToday";
            lblToday.Size = new Size(41, 15);
            lblToday.TabIndex = 10;
            lblToday.Text = "Today:";
            // 
            // lblWeek
            // 
            lblWeek.AutoSize = true;
            lblWeek.Location = new Point(276, 55);
            lblWeek.Name = "lblWeek";
            lblWeek.Size = new Size(39, 15);
            lblWeek.TabIndex = 12;
            lblWeek.Text = "Week:";
            // 
            // lblWeekCount
            // 
            lblWeekCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblWeekCount.Location = new Point(338, 55);
            lblWeekCount.Name = "lblWeekCount";
            lblWeekCount.Size = new Size(138, 15);
            lblWeekCount.TabIndex = 13;
            lblWeekCount.Text = "1000 (500+1000) = 5,00%";
            lblWeekCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblMonth
            // 
            lblMonth.AutoSize = true;
            lblMonth.Location = new Point(276, 77);
            lblMonth.Name = "lblMonth";
            lblMonth.Size = new Size(46, 15);
            lblMonth.TabIndex = 14;
            lblMonth.Text = "Month:";
            // 
            // lblMonthCount
            // 
            lblMonthCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblMonthCount.Location = new Point(329, 77);
            lblMonthCount.Name = "lblMonthCount";
            lblMonthCount.Size = new Size(147, 15);
            lblMonthCount.TabIndex = 15;
            lblMonthCount.Text = "1000 (500+1000) = 10,00%";
            lblMonthCount.TextAlign = ContentAlignment.TopRight;
            // 
            // lblYear
            // 
            lblYear.AutoSize = true;
            lblYear.Location = new Point(276, 99);
            lblYear.Name = "lblYear";
            lblYear.Size = new Size(32, 15);
            lblYear.TabIndex = 16;
            lblYear.Text = "Year:";
            // 
            // lblYearCount
            // 
            lblYearCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblYearCount.Location = new Point(322, 99);
            lblYearCount.Name = "lblYearCount";
            lblYearCount.Size = new Size(154, 15);
            lblYearCount.TabIndex = 17;
            lblYearCount.Text = "4000 (2000+2000) = 20,00%";
            lblYearCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnFindFile
            // 
            btnFindFile.Location = new Point(225, 11);
            btnFindFile.Name = "btnFindFile";
            btnFindFile.Size = new Size(45, 50);
            btnFindFile.TabIndex = 18;
            btnFindFile.Text = "Find File";
            btnFindFile.UseVisualStyleBackColor = true;
            btnFindFile.Click += BtnFindFile_Click;
            // 
            // lblAllCount
            // 
            lblAllCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblAllCount.Location = new Point(306, 123);
            lblAllCount.Name = "lblAllCount";
            lblAllCount.Size = new Size(170, 15);
            lblAllCount.TabIndex = 20;
            lblAllCount.Text = "10000 (5000+5000) = 50,00%";
            lblAllCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblAll
            // 
            lblAll.AutoSize = true;
            lblAll.Location = new Point(276, 123);
            lblAll.Name = "lblAll";
            lblAll.Size = new Size(24, 15);
            lblAll.TabIndex = 19;
            lblAll.Text = "All:";
            // 
            // cbxReadOnly
            // 
            cbxReadOnly.AutoSize = true;
            cbxReadOnly.Location = new Point(12, 67);
            cbxReadOnly.Name = "cbxReadOnly";
            cbxReadOnly.Size = new Size(247, 19);
            cbxReadOnly.TabIndex = 21;
            cbxReadOnly.Text = "Read-only Mode? (files cannot be moved)";
            cbxReadOnly.UseVisualStyleBackColor = true;
            cbxReadOnly.CheckedChanged += CbxReadOnly_CheckedChanged;
            // 
            // DispatchWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(488, 147);
            Controls.Add(cbxReadOnly);
            Controls.Add(lblAllCount);
            Controls.Add(lblAll);
            Controls.Add(btnFindFile);
            Controls.Add(lblYearCount);
            Controls.Add(lblYear);
            Controls.Add(lblMonthCount);
            Controls.Add(lblMonth);
            Controls.Add(lblWeekCount);
            Controls.Add(lblWeek);
            Controls.Add(lblToday);
            Controls.Add(lblSession);
            Controls.Add(lblTodayCount);
            Controls.Add(lblSessionCount);
            Controls.Add(btnMoveOut);
            Controls.Add(btnBrowseOutFolder);
            Controls.Add(tbxOutDirectory);
            Controls.Add(btnMoveDelete);
            Controls.Add(btnNext);
            Controls.Add(btnBrowseInFolder);
            Controls.Add(tbxInDirectory);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MaximizeBox = false;
            Name = "DispatchWindow";
            Text = "Dispatch";
            TopMost = true;
            FormClosed += Dispatch_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox tbxInDirectory;
        private Button btnBrowseInFolder;
        private FolderBrowserDialog inDirectoryBrowser;
        private Button btnNext;
        private Button btnMoveDelete;
        private TextBox tbxOutDirectory;
        private Button btnBrowseOutFolder;
        private FolderBrowserDialog outDirectoryBrowser;
        private Button btnMoveOut;
        private Label lblSessionCount;
        private Label lblTodayCount;
        private Label lblSession;
        private Label lblToday;
        private Label lblWeek;
        private Label lblWeekCount;
        private Label lblMonth;
        private Label lblMonthCount;
        private Label lblYear;
        private Label lblYearCount;
        private Button btnFindFile;
        private Label lblAllCount;
        private Label lblAll;
        private CheckBox cbxReadOnly;
    }
}