namespace ClashW.View
{
    partial class GeneralConfigForm
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
            this.socksNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.socksPortLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.testUrlTextBox = new System.Windows.Forms.TextBox();
            this.urlTestLabel = new System.Windows.Forms.Label();
            this.externalSecretLabel = new System.Windows.Forms.Label();
            this.externalSecretTextbox = new System.Windows.Forms.TextBox();
            this.httpPortLabel = new System.Windows.Forms.Label();
            this.httpNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.allowLanLabel = new System.Windows.Forms.Label();
            this.allowLanCheckBox = new System.Windows.Forms.CheckBox();
            this.logLevelLabel = new System.Windows.Forms.Label();
            this.logLevelComboBox = new System.Windows.Forms.ComboBox();
            this.externalContrallLabel = new System.Windows.Forms.Label();
            this.externalContrallTextbox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ResetButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.socksNumericUpDown)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.httpNumericUpDown)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // socksNumericUpDown
            // 
            this.socksNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.socksNumericUpDown.Location = new System.Drawing.Point(178, 14);
            this.socksNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.socksNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.socksNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.socksNumericUpDown.Name = "socksNumericUpDown";
            this.socksNumericUpDown.Size = new System.Drawing.Size(356, 39);
            this.socksNumericUpDown.TabIndex = 1;
            this.socksNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // socksPortLabel
            // 
            this.socksPortLabel.AutoSize = true;
            this.socksPortLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.socksPortLabel.Location = new System.Drawing.Point(13, 10);
            this.socksPortLabel.Name = "socksPortLabel";
            this.socksPortLabel.Size = new System.Drawing.Size(159, 48);
            this.socksPortLabel.TabIndex = 0;
            this.socksPortLabel.Text = "Socks端口：";
            this.socksPortLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.3093F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.6907F));
            this.tableLayoutPanel1.Controls.Add(this.testUrlTextBox, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.urlTestLabel, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.externalSecretLabel, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.externalSecretTextbox, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.socksPortLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.socksNumericUpDown, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.httpPortLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.httpNumericUpDown, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.allowLanLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.allowLanCheckBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.logLevelLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.logLevelComboBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.externalContrallLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.externalContrallTextbox, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(547, 344);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // testUrlTextBox
            // 
            this.testUrlTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testUrlTextBox.Location = new System.Drawing.Point(178, 301);
            this.testUrlTextBox.Name = "testUrlTextBox";
            this.testUrlTextBox.Size = new System.Drawing.Size(356, 39);
            this.testUrlTextBox.TabIndex = 13;
            // 
            // urlTestLabel
            // 
            this.urlTestLabel.AutoSize = true;
            this.urlTestLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.urlTestLabel.Location = new System.Drawing.Point(13, 298);
            this.urlTestLabel.Name = "urlTestLabel";
            this.urlTestLabel.Size = new System.Drawing.Size(159, 48);
            this.urlTestLabel.TabIndex = 12;
            this.urlTestLabel.Text = "测速链接：";
            this.urlTestLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // externalSecretLabel
            // 
            this.externalSecretLabel.AutoSize = true;
            this.externalSecretLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.externalSecretLabel.Location = new System.Drawing.Point(13, 250);
            this.externalSecretLabel.Name = "externalSecretLabel";
            this.externalSecretLabel.Size = new System.Drawing.Size(159, 48);
            this.externalSecretLabel.TabIndex = 11;
            this.externalSecretLabel.Text = "API密匙：";
            this.externalSecretLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // externalSecretTextbox
            // 
            this.externalSecretTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.externalSecretTextbox.Location = new System.Drawing.Point(178, 253);
            this.externalSecretTextbox.Name = "externalSecretTextbox";
            this.externalSecretTextbox.Size = new System.Drawing.Size(356, 39);
            this.externalSecretTextbox.TabIndex = 10;
            // 
            // httpPortLabel
            // 
            this.httpPortLabel.AutoSize = true;
            this.httpPortLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.httpPortLabel.Location = new System.Drawing.Point(13, 58);
            this.httpPortLabel.Name = "httpPortLabel";
            this.httpPortLabel.Size = new System.Drawing.Size(159, 48);
            this.httpPortLabel.TabIndex = 2;
            this.httpPortLabel.Text = "HTTP端口：";
            this.httpPortLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // httpNumericUpDown
            // 
            this.httpNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.httpNumericUpDown.Location = new System.Drawing.Point(178, 61);
            this.httpNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.httpNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.httpNumericUpDown.Name = "httpNumericUpDown";
            this.httpNumericUpDown.Size = new System.Drawing.Size(356, 39);
            this.httpNumericUpDown.TabIndex = 3;
            this.httpNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // allowLanLabel
            // 
            this.allowLanLabel.AutoSize = true;
            this.allowLanLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allowLanLabel.Location = new System.Drawing.Point(13, 106);
            this.allowLanLabel.Name = "allowLanLabel";
            this.allowLanLabel.Size = new System.Drawing.Size(159, 48);
            this.allowLanLabel.TabIndex = 4;
            this.allowLanLabel.Text = "局域网访问：";
            this.allowLanLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // allowLanCheckBox
            // 
            this.allowLanCheckBox.AutoSize = true;
            this.allowLanCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allowLanCheckBox.Location = new System.Drawing.Point(178, 109);
            this.allowLanCheckBox.Name = "allowLanCheckBox";
            this.allowLanCheckBox.Size = new System.Drawing.Size(356, 42);
            this.allowLanCheckBox.TabIndex = 5;
            this.allowLanCheckBox.Text = "允许";
            this.allowLanCheckBox.UseVisualStyleBackColor = true;
            // 
            // logLevelLabel
            // 
            this.logLevelLabel.AutoSize = true;
            this.logLevelLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logLevelLabel.Location = new System.Drawing.Point(13, 154);
            this.logLevelLabel.Name = "logLevelLabel";
            this.logLevelLabel.Size = new System.Drawing.Size(159, 48);
            this.logLevelLabel.TabIndex = 6;
            this.logLevelLabel.Text = "日志等级：";
            this.logLevelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // logLevelComboBox
            // 
            this.logLevelComboBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.logLevelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.logLevelComboBox.FormattingEnabled = true;
            this.logLevelComboBox.Items.AddRange(new object[] {
            "INFO",
            "WARNING",
            "ERROR",
            "DEBUG"});
            this.logLevelComboBox.Location = new System.Drawing.Point(178, 157);
            this.logLevelComboBox.Name = "logLevelComboBox";
            this.logLevelComboBox.Size = new System.Drawing.Size(344, 39);
            this.logLevelComboBox.TabIndex = 7;
            // 
            // externalContrallLabel
            // 
            this.externalContrallLabel.AutoSize = true;
            this.externalContrallLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.externalContrallLabel.Location = new System.Drawing.Point(13, 202);
            this.externalContrallLabel.Name = "externalContrallLabel";
            this.externalContrallLabel.Size = new System.Drawing.Size(159, 48);
            this.externalContrallLabel.TabIndex = 8;
            this.externalContrallLabel.Text = "API地址：";
            this.externalContrallLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // externalContrallTextbox
            // 
            this.externalContrallTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.externalContrallTextbox.Location = new System.Drawing.Point(178, 205);
            this.externalContrallTextbox.Name = "externalContrallTextbox";
            this.externalContrallTextbox.Size = new System.Drawing.Size(356, 39);
            this.externalContrallTextbox.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ResetButton);
            this.panel1.Controls.Add(this.saveButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 344);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 20, 10, 0);
            this.panel1.Size = new System.Drawing.Size(547, 72);
            this.panel1.TabIndex = 1;
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(190, 16);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(160, 56);
            this.ResetButton.TabIndex = 1;
            this.ResetButton.Text = "恢复";
            this.ResetButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(375, 16);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(160, 56);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "保存";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // GeneralConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(547, 448);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "GeneralConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通用配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GeneralConfigForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GeneralConfigForm_FormClosed);
            this.Load += new System.EventHandler(this.GeneralConfigForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.socksNumericUpDown)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.httpNumericUpDown)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown socksNumericUpDown;
        private System.Windows.Forms.Label socksPortLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label httpPortLabel;
        private System.Windows.Forms.NumericUpDown httpNumericUpDown;
        private System.Windows.Forms.Label allowLanLabel;
        private System.Windows.Forms.CheckBox allowLanCheckBox;
        private System.Windows.Forms.Label logLevelLabel;
        private System.Windows.Forms.ComboBox logLevelComboBox;
        private System.Windows.Forms.Label externalContrallLabel;
        private System.Windows.Forms.TextBox externalContrallTextbox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label externalSecretLabel;
        private System.Windows.Forms.TextBox externalSecretTextbox;
        private System.Windows.Forms.TextBox testUrlTextBox;
        private System.Windows.Forms.Label urlTestLabel;
    }
}