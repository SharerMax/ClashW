namespace ClashW.View
{
    partial class ProxyDelayForm
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
            this.proxyDelayListView = new System.Windows.Forms.ListView();
            this.proxyNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.delayColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.closeButton = new System.Windows.Forms.Button();
            this.reTestButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // proxyDelayListView
            // 
            this.proxyDelayListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.proxyDelayListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.proxyNameColumnHeader,
            this.delayColumnHeader});
            this.proxyDelayListView.GridLines = true;
            this.proxyDelayListView.Location = new System.Drawing.Point(45, 53);
            this.proxyDelayListView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.proxyDelayListView.Name = "proxyDelayListView";
            this.proxyDelayListView.Size = new System.Drawing.Size(457, 589);
            this.proxyDelayListView.TabIndex = 0;
            this.proxyDelayListView.UseCompatibleStateImageBehavior = false;
            this.proxyDelayListView.View = System.Windows.Forms.View.Details;
            // 
            // proxyNameColumnHeader
            // 
            this.proxyNameColumnHeader.Text = "代理";
            this.proxyNameColumnHeader.Width = 172;
            // 
            // delayColumnHeader
            // 
            this.delayColumnHeader.Text = "延迟（ms）";
            this.delayColumnHeader.Width = 280;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Location = new System.Drawing.Point(380, 660);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(122, 48);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "退出";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // reTestButton
            // 
            this.reTestButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.reTestButton.Location = new System.Drawing.Point(239, 660);
            this.reTestButton.Name = "reTestButton";
            this.reTestButton.Size = new System.Drawing.Size(122, 48);
            this.reTestButton.TabIndex = 2;
            this.reTestButton.Text = "重新测试";
            this.reTestButton.UseVisualStyleBackColor = true;
            this.reTestButton.Click += new System.EventHandler(this.ReTestButton_Click);
            // 
            // ProxyDelayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 741);
            this.Controls.Add(this.reTestButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.proxyDelayListView);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ProxyDelayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "服务器延迟";
            this.Load += new System.EventHandler(this.ProxyDelayForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView proxyDelayListView;
        private System.Windows.Forms.ColumnHeader proxyNameColumnHeader;
        private System.Windows.Forms.ColumnHeader delayColumnHeader;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button reTestButton;
    }
}