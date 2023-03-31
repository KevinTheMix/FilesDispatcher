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
            this.tbxInDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowseInFolder = new System.Windows.Forms.Button();
            this.inDirectoryBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnMoveDelete = new System.Windows.Forms.Button();
            this.tbxOutDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowseOutFolder = new System.Windows.Forms.Button();
            this.outDirectoryBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnMoveOut = new System.Windows.Forms.Button();
            this.lblSessionCount = new System.Windows.Forms.Label();
            this.lblTodayCount = new System.Windows.Forms.Label();
            this.lblSession = new System.Windows.Forms.Label();
            this.lblToday = new System.Windows.Forms.Label();
            this.lblWeek = new System.Windows.Forms.Label();
            this.lblWeekCount = new System.Windows.Forms.Label();
            this.lblMonth = new System.Windows.Forms.Label();
            this.lblMonthCount = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.lblYearCount = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.lblAllCount = new System.Windows.Forms.Label();
            this.lblAll = new System.Windows.Forms.Label();
            this.cbxReadOnly = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tbxInDirectory
            // 
            this.tbxInDirectory.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbxInDirectory.Location = new System.Drawing.Point(12, 12);
            this.tbxInDirectory.Name = "tbxInDirectory";
            this.tbxInDirectory.ReadOnly = true;
            this.tbxInDirectory.Size = new System.Drawing.Size(150, 20);
            this.tbxInDirectory.TabIndex = 1;
            // 
            // btnBrowseInFolder
            // 
            this.btnBrowseInFolder.Location = new System.Drawing.Point(168, 11);
            this.btnBrowseInFolder.Name = "btnBrowseInFolder";
            this.btnBrowseInFolder.Size = new System.Drawing.Size(50, 23);
            this.btnBrowseInFolder.TabIndex = 2;
            this.btnBrowseInFolder.Text = "In";
            this.btnBrowseInFolder.UseVisualStyleBackColor = true;
            this.btnBrowseInFolder.Click += new System.EventHandler(this.BtnBrowseInFolder_Click);
            // 
            // btnNext
            // 
            this.btnNext.Enabled = false;
            this.btnNext.Location = new System.Drawing.Point(168, 95);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(102, 43);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = "Start!";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // btnMoveDelete
            // 
            this.btnMoveDelete.BackColor = System.Drawing.Color.Coral;
            this.btnMoveDelete.Enabled = false;
            this.btnMoveDelete.ForeColor = System.Drawing.Color.Orange;
            this.btnMoveDelete.Location = new System.Drawing.Point(88, 95);
            this.btnMoveDelete.Name = "btnMoveDelete";
            this.btnMoveDelete.Size = new System.Drawing.Size(70, 43);
            this.btnMoveDelete.TabIndex = 6;
            this.btnMoveDelete.Text = "Delete";
            this.btnMoveDelete.UseVisualStyleBackColor = false;
            this.btnMoveDelete.Click += new System.EventHandler(this.BtnMoveDelete_Click);
            // 
            // tbxOutDirectory
            // 
            this.tbxOutDirectory.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbxOutDirectory.Location = new System.Drawing.Point(12, 41);
            this.tbxOutDirectory.Name = "tbxOutDirectory";
            this.tbxOutDirectory.ReadOnly = true;
            this.tbxOutDirectory.Size = new System.Drawing.Size(150, 20);
            this.tbxOutDirectory.TabIndex = 3;
            // 
            // btnBrowseOutFolder
            // 
            this.btnBrowseOutFolder.Location = new System.Drawing.Point(168, 39);
            this.btnBrowseOutFolder.Name = "btnBrowseOutFolder";
            this.btnBrowseOutFolder.Size = new System.Drawing.Size(51, 23);
            this.btnBrowseOutFolder.TabIndex = 4;
            this.btnBrowseOutFolder.Text = "Out";
            this.btnBrowseOutFolder.UseVisualStyleBackColor = true;
            this.btnBrowseOutFolder.Click += new System.EventHandler(this.BtnBrowseOutDirectory_Click);
            // 
            // btnMoveOut
            // 
            this.btnMoveOut.BackColor = System.Drawing.Color.PaleGreen;
            this.btnMoveOut.Enabled = false;
            this.btnMoveOut.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnMoveOut.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.btnMoveOut.Location = new System.Drawing.Point(12, 95);
            this.btnMoveOut.Name = "btnMoveOut";
            this.btnMoveOut.Size = new System.Drawing.Size(70, 43);
            this.btnMoveOut.TabIndex = 5;
            this.btnMoveOut.Text = "Very Good";
            this.btnMoveOut.UseVisualStyleBackColor = false;
            this.btnMoveOut.Click += new System.EventHandler(this.BtnMoveOut_Click);
            // 
            // lblSessionCount
            // 
            this.lblSessionCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSessionCount.Location = new System.Drawing.Point(346, 11);
            this.lblSessionCount.Name = "lblSessionCount";
            this.lblSessionCount.Size = new System.Drawing.Size(130, 15);
            this.lblSessionCount.TabIndex = 9;
            this.lblSessionCount.Text = "10 (5+5) = 0,01%";
            this.lblSessionCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTodayCount
            // 
            this.lblTodayCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTodayCount.Location = new System.Drawing.Point(346, 33);
            this.lblTodayCount.Name = "lblTodayCount";
            this.lblTodayCount.Size = new System.Drawing.Size(130, 15);
            this.lblTodayCount.TabIndex = 11;
            this.lblTodayCount.Text = "40 (20+20) = 0,10%";
            this.lblTodayCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSession
            // 
            this.lblSession.AutoSize = true;
            this.lblSession.Location = new System.Drawing.Point(276, 11);
            this.lblSession.Name = "lblSession";
            this.lblSession.Size = new System.Drawing.Size(49, 15);
            this.lblSession.TabIndex = 8;
            this.lblSession.Text = "Session:";
            // 
            // lblToday
            // 
            this.lblToday.AutoSize = true;
            this.lblToday.Location = new System.Drawing.Point(276, 33);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(43, 15);
            this.lblToday.TabIndex = 10;
            this.lblToday.Text = "Today:";
            // 
            // lblWeek
            // 
            this.lblWeek.AutoSize = true;
            this.lblWeek.Location = new System.Drawing.Point(276, 55);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(39, 15);
            this.lblWeek.TabIndex = 12;
            this.lblWeek.Text = "Week:";
            // 
            // lblWeekCount
            // 
            this.lblWeekCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWeekCount.Location = new System.Drawing.Point(338, 55);
            this.lblWeekCount.Name = "lblWeekCount";
            this.lblWeekCount.Size = new System.Drawing.Size(138, 15);
            this.lblWeekCount.TabIndex = 13;
            this.lblWeekCount.Text = "1000 (500+1000) = 5,00%";
            this.lblWeekCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Location = new System.Drawing.Point(276, 77);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(46, 15);
            this.lblMonth.TabIndex = 14;
            this.lblMonth.Text = "Month:";
            // 
            // lblMonthCount
            // 
            this.lblMonthCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMonthCount.Location = new System.Drawing.Point(329, 77);
            this.lblMonthCount.Name = "lblMonthCount";
            this.lblMonthCount.Size = new System.Drawing.Size(147, 15);
            this.lblMonthCount.TabIndex = 15;
            this.lblMonthCount.Text = "1000 (500+1000) = 10,00%";
            this.lblMonthCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(276, 99);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(33, 15);
            this.lblYear.TabIndex = 16;
            this.lblYear.Text = "Year:";
            // 
            // lblYearCount
            // 
            this.lblYearCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblYearCount.Location = new System.Drawing.Point(322, 99);
            this.lblYearCount.Name = "lblYearCount";
            this.lblYearCount.Size = new System.Drawing.Size(154, 15);
            this.lblYearCount.TabIndex = 17;
            this.lblYearCount.Text = "4000 (2000+2000) = 20,00%";
            this.lblYearCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(225, 11);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(45, 50);
            this.btnSelectFile.TabIndex = 18;
            this.btnSelectFile.Text = "Open File";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.BtnSelectFile_Click);
            // 
            // lblAllCount
            // 
            this.lblAllCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAllCount.Location = new System.Drawing.Point(306, 123);
            this.lblAllCount.Name = "lblAllCount";
            this.lblAllCount.Size = new System.Drawing.Size(170, 15);
            this.lblAllCount.TabIndex = 20;
            this.lblAllCount.Text = "10000 (5000+5000) = 50,00%";
            this.lblAllCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAll
            // 
            this.lblAll.AutoSize = true;
            this.lblAll.Location = new System.Drawing.Point(276, 123);
            this.lblAll.Name = "lblAll";
            this.lblAll.Size = new System.Drawing.Size(24, 15);
            this.lblAll.TabIndex = 19;
            this.lblAll.Text = "All:";
            // 
            // cbxReadOnly
            // 
            this.cbxReadOnly.AutoSize = true;
            this.cbxReadOnly.Location = new System.Drawing.Point(12, 67);
            this.cbxReadOnly.Name = "cbxReadOnly";
            this.cbxReadOnly.Size = new System.Drawing.Size(247, 19);
            this.cbxReadOnly.TabIndex = 21;
            this.cbxReadOnly.Text = "Read-only Mode? (files cannot be moved)";
            this.cbxReadOnly.UseVisualStyleBackColor = true;
            this.cbxReadOnly.CheckedChanged += new System.EventHandler(this.CbxReadOnly_CheckedChanged);
            // 
            // DispatchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 147);
            this.Controls.Add(this.cbxReadOnly);
            this.Controls.Add(this.lblAllCount);
            this.Controls.Add(this.lblAll);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.lblYearCount);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.lblMonthCount);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.lblWeekCount);
            this.Controls.Add(this.lblWeek);
            this.Controls.Add(this.lblToday);
            this.Controls.Add(this.lblSession);
            this.Controls.Add(this.lblTodayCount);
            this.Controls.Add(this.lblSessionCount);
            this.Controls.Add(this.btnMoveOut);
            this.Controls.Add(this.btnBrowseOutFolder);
            this.Controls.Add(this.tbxOutDirectory);
            this.Controls.Add(this.btnMoveDelete);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnBrowseInFolder);
            this.Controls.Add(this.tbxInDirectory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "DispatchWindow";
            this.Text = "Dispatch";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Dispatch_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private Button btnSelectFile;
        private Label lblAllCount;
        private Label lblAll;
        private CheckBox cbxReadOnly;
    }
}