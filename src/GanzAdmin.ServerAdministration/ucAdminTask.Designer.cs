namespace GanzAdmin.ServerAdministration
{
    partial class ucAdminTask
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblName = new System.Windows.Forms.Label();
            this.lblRunInterval = new System.Windows.Forms.Label();
            this.lblTimeLeft = new System.Windows.Forms.Label();
            this.lblLastRun = new System.Windows.Forms.Label();
            this.btnRunNow = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lblLastRun, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTimeLeft, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblRunInterval, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnRunNow, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(813, 36);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Location = new System.Drawing.Point(3, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(156, 36);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "#name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRunInterval
            // 
            this.lblRunInterval.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRunInterval.Location = new System.Drawing.Point(165, 0);
            this.lblRunInterval.Name = "lblRunInterval";
            this.lblRunInterval.Size = new System.Drawing.Size(156, 36);
            this.lblRunInterval.TabIndex = 1;
            this.lblRunInterval.Text = "#runinterva";
            this.lblRunInterval.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTimeLeft
            // 
            this.lblTimeLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTimeLeft.Location = new System.Drawing.Point(327, 0);
            this.lblTimeLeft.Name = "lblTimeLeft";
            this.lblTimeLeft.Size = new System.Drawing.Size(156, 36);
            this.lblTimeLeft.TabIndex = 2;
            this.lblTimeLeft.Text = "#timeleft";
            this.lblTimeLeft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLastRun
            // 
            this.lblLastRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLastRun.Location = new System.Drawing.Point(489, 0);
            this.lblLastRun.Name = "lblLastRun";
            this.lblLastRun.Size = new System.Drawing.Size(156, 36);
            this.lblLastRun.TabIndex = 3;
            this.lblLastRun.Text = "lastrun";
            this.lblLastRun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRunNow
            // 
            this.btnRunNow.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRunNow.Location = new System.Drawing.Point(693, 6);
            this.btnRunNow.Name = "btnRunNow";
            this.btnRunNow.Size = new System.Drawing.Size(75, 23);
            this.btnRunNow.TabIndex = 4;
            this.btnRunNow.Text = "Run now";
            this.btnRunNow.UseVisualStyleBackColor = true;
            this.btnRunNow.Click += new System.EventHandler(this.btnRunNow_Click);
            // 
            // ucAdminTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ucAdminTask";
            this.Size = new System.Drawing.Size(813, 36);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnRunNow;
        protected System.Windows.Forms.Label lblLastRun;
        protected System.Windows.Forms.Label lblTimeLeft;
        protected System.Windows.Forms.Label lblRunInterval;
        protected System.Windows.Forms.Label lblName;
    }
}
