namespace Dispatch
{
    partial class Dispatch
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
            this.SuspendLayout();
            // 
            // tbxInFolder
            // 
            this.tbxInDirectory.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbxInDirectory.Location = new System.Drawing.Point(12, 12);
            this.tbxInDirectory.Name = "tbxInFolder";
            this.tbxInDirectory.ReadOnly = true;
            this.tbxInDirectory.Size = new System.Drawing.Size(186, 20);
            this.tbxInDirectory.TabIndex = 1;
            // 
            // btnBrowseInFolder
            // 
            this.btnBrowseInFolder.Location = new System.Drawing.Point(204, 9);
            this.btnBrowseInFolder.Name = "btnBrowseInFolder";
            this.btnBrowseInFolder.Size = new System.Drawing.Size(63, 23);
            this.btnBrowseInFolder.TabIndex = 2;
            this.btnBrowseInFolder.Text = "In";
            this.btnBrowseInFolder.UseVisualStyleBackColor = true;
            this.btnBrowseInFolder.Click += new System.EventHandler(this.btnBrowseInFolder_Click);
            // 
            // btnRun
            // 
            this.btnNext.Enabled = false;
            this.btnNext.Location = new System.Drawing.Point(204, 69);
            this.btnNext.Name = "btnRun";
            this.btnNext.Size = new System.Drawing.Size(63, 43);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = "Start!";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnMoveDelete
            // 
            this.btnMoveDelete.BackColor = System.Drawing.Color.Gray;
            this.btnMoveDelete.Enabled = false;
            this.btnMoveDelete.ForeColor = System.Drawing.Color.Silver;
            this.btnMoveDelete.Location = new System.Drawing.Point(108, 69);
            this.btnMoveDelete.Name = "btnMoveDelete";
            this.btnMoveDelete.Size = new System.Drawing.Size(90, 44);
            this.btnMoveDelete.TabIndex = 6;
            this.btnMoveDelete.Text = "Delete";
            this.btnMoveDelete.UseVisualStyleBackColor = false;
            this.btnMoveDelete.Click += new System.EventHandler(this.btnMoveDelete_Click);
            // 
            // tbxOutFolder
            // 
            this.tbxOutDirectory.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbxOutDirectory.Location = new System.Drawing.Point(12, 41);
            this.tbxOutDirectory.Name = "tbxOutFolder";
            this.tbxOutDirectory.ReadOnly = true;
            this.tbxOutDirectory.Size = new System.Drawing.Size(186, 20);
            this.tbxOutDirectory.TabIndex = 3;
            // 
            // btnBrowseOutFolder
            // 
            this.btnBrowseOutFolder.Location = new System.Drawing.Point(204, 38);
            this.btnBrowseOutFolder.Name = "btnBrowseOutFolder";
            this.btnBrowseOutFolder.Size = new System.Drawing.Size(63, 23);
            this.btnBrowseOutFolder.TabIndex = 4;
            this.btnBrowseOutFolder.Text = "Out";
            this.btnBrowseOutFolder.UseVisualStyleBackColor = true;
            this.btnBrowseOutFolder.Click += new System.EventHandler(this.btnBrowseOutDirectory_Click);
            // 
            // btnMoveOut
            // 
            this.btnMoveOut.BackColor = System.Drawing.Color.Green;
            this.btnMoveOut.Enabled = false;
            this.btnMoveOut.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnMoveOut.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.btnMoveOut.Location = new System.Drawing.Point(12, 70);
            this.btnMoveOut.Name = "btnMoveOut";
            this.btnMoveOut.Size = new System.Drawing.Size(90, 43);
            this.btnMoveOut.TabIndex = 5;
            this.btnMoveOut.Text = "Very Good";
            this.btnMoveOut.UseVisualStyleBackColor = false;
            this.btnMoveOut.Click += new System.EventHandler(this.btnMoveOut_Click);
            // 
            // lblSessionCount
            // 
            this.lblSessionCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSessionCount.Location = new System.Drawing.Point(344, 11);
            this.lblSessionCount.Name = "lblSessionCount";
            this.lblSessionCount.Size = new System.Drawing.Size(130, 15);
            this.lblSessionCount.TabIndex = 9;
            this.lblSessionCount.Text = "10 (5+5) = 0,01%";
            this.lblSessionCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTodayCount
            // 
            this.lblTodayCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTodayCount.Location = new System.Drawing.Point(344, 32);
            this.lblTodayCount.Name = "lblTodayCount";
            this.lblTodayCount.Size = new System.Drawing.Size(130, 15);
            this.lblTodayCount.TabIndex = 11;
            this.lblTodayCount.Text = "40 (20+20) = 0,10%";
            this.lblTodayCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSession
            // 
            this.lblSession.AutoSize = true;
            this.lblSession.Location = new System.Drawing.Point(275, 11);
            this.lblSession.Name = "lblSession";
            this.lblSession.Size = new System.Drawing.Size(49, 15);
            this.lblSession.TabIndex = 8;
            this.lblSession.Text = "Session:";
            // 
            // lblToday
            // 
            this.lblToday.AutoSize = true;
            this.lblToday.Location = new System.Drawing.Point(275, 32);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(43, 15);
            this.lblToday.TabIndex = 10;
            this.lblToday.Text = "Today:";
            // 
            // lblWeek
            // 
            this.lblWeek.AutoSize = true;
            this.lblWeek.Location = new System.Drawing.Point(275, 53);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(39, 15);
            this.lblWeek.TabIndex = 12;
            this.lblWeek.Text = "Week:";
            // 
            // lblWeekCount
            // 
            this.lblWeekCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWeekCount.Location = new System.Drawing.Point(336, 53);
            this.lblWeekCount.Name = "lblWeekCount";
            this.lblWeekCount.Size = new System.Drawing.Size(138, 15);
            this.lblWeekCount.TabIndex = 13;
            this.lblWeekCount.Text = "1000 (500+1000) = 5,00%";
            this.lblWeekCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Location = new System.Drawing.Point(275, 74);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(46, 15);
            this.lblMonth.TabIndex = 14;
            this.lblMonth.Text = "Month:";
            // 
            // lblMonthCount
            // 
            this.lblMonthCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMonthCount.Location = new System.Drawing.Point(327, 74);
            this.lblMonthCount.Name = "lblMonthCount";
            this.lblMonthCount.Size = new System.Drawing.Size(147, 15);
            this.lblMonthCount.TabIndex = 15;
            this.lblMonthCount.Text = "1000 (500+1000) = 10,00%";
            this.lblMonthCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(275, 95);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(33, 15);
            this.lblYear.TabIndex = 16;
            this.lblYear.Text = "Year:";
            // 
            // lblYearCount
            // 
            this.lblYearCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblYearCount.Location = new System.Drawing.Point(320, 95);
            this.lblYearCount.Name = "lblYearCount";
            this.lblYearCount.Size = new System.Drawing.Size(154, 15);
            this.lblYearCount.TabIndex = 17;
            this.lblYearCount.Text = "4000 (2000+2000) = 50,00%";
            this.lblYearCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Dispatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 124);
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
            this.Name = "Dispatch";
            this.Text = "Dispatch";
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
    }
}