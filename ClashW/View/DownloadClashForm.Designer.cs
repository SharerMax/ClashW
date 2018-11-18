namespace ClashW.View
{
    partial class DownloadClashForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.clashProgressBar = new System.Windows.Forms.ProgressBar();
            this.geoipProgressBar = new System.Windows.Forms.ProgressBar();
            this.cancelButton = new System.Windows.Forms.Button();
            this.clashLabel = new System.Windows.Forms.Label();
            this.geoipLabel = new System.Windows.Forms.Label();
            this.clashProgressValueLabel = new System.Windows.Forms.Label();
            this.geoipProgressValuelabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // clashProgressBar
            // 
            this.clashProgressBar.Location = new System.Drawing.Point(126, 84);
            this.clashProgressBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.clashProgressBar.Name = "clashProgressBar";
            this.clashProgressBar.Size = new System.Drawing.Size(646, 40);
            this.clashProgressBar.TabIndex = 0;
            // 
            // geoipProgressBar
            // 
            this.geoipProgressBar.Location = new System.Drawing.Point(126, 168);
            this.geoipProgressBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.geoipProgressBar.Name = "geoipProgressBar";
            this.geoipProgressBar.Size = new System.Drawing.Size(646, 40);
            this.geoipProgressBar.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(359, 268);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(168, 61);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // clashLabel
            // 
            this.clashLabel.AutoSize = true;
            this.clashLabel.Location = new System.Drawing.Point(120, 49);
            this.clashLabel.Name = "clashLabel";
            this.clashLabel.Size = new System.Drawing.Size(75, 31);
            this.clashLabel.TabIndex = 3;
            this.clashLabel.Text = "Clash";
            // 
            // geoipLabel
            // 
            this.geoipLabel.AutoSize = true;
            this.geoipLabel.Location = new System.Drawing.Point(120, 133);
            this.geoipLabel.Name = "geoipLabel";
            this.geoipLabel.Size = new System.Drawing.Size(87, 31);
            this.geoipLabel.TabIndex = 4;
            this.geoipLabel.Text = "GEOIP";
            // 
            // clashProgressValueLabel
            // 
            this.clashProgressValueLabel.AutoSize = true;
            this.clashProgressValueLabel.Location = new System.Drawing.Point(201, 49);
            this.clashProgressValueLabel.Name = "clashProgressValueLabel";
            this.clashProgressValueLabel.Size = new System.Drawing.Size(49, 31);
            this.clashProgressValueLabel.TabIndex = 5;
            this.clashProgressValueLabel.Text = "0%";
            // 
            // geoipProgressValuelabel
            // 
            this.geoipProgressValuelabel.AutoSize = true;
            this.geoipProgressValuelabel.Location = new System.Drawing.Point(213, 133);
            this.geoipProgressValuelabel.Name = "geoipProgressValuelabel";
            this.geoipProgressValuelabel.Size = new System.Drawing.Size(49, 31);
            this.geoipProgressValuelabel.TabIndex = 6;
            this.geoipProgressValuelabel.Text = "0%";
            // 
            // DownloadClashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(879, 405);
            this.ControlBox = false;
            this.Controls.Add(this.geoipProgressValuelabel);
            this.Controls.Add(this.clashProgressValueLabel);
            this.Controls.Add(this.geoipLabel);
            this.Controls.Add(this.clashLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.geoipProgressBar);
            this.Controls.Add(this.clashProgressBar);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "DownloadClashForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "下载运行依赖";
            this.Load += new System.EventHandler(this.DownloadClash_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar clashProgressBar;
        private System.Windows.Forms.ProgressBar geoipProgressBar;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label clashLabel;
        private System.Windows.Forms.Label geoipLabel;
        private System.Windows.Forms.Label clashProgressValueLabel;
        private System.Windows.Forms.Label geoipProgressValuelabel;
    }
}