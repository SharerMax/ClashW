﻿namespace ClashW.View
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.addButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.basicConfigTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.serverLabel = new System.Windows.Forms.Label();
            this.serverTextBox = new System.Windows.Forms.TextBox();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.serverPoxtNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ssConfigPanel = new System.Windows.Forms.Panel();
            this.ssConfigTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ssObfsHostLabel = new System.Windows.Forms.Label();
            this.ssObfsTypeTextBox = new System.Windows.Forms.TextBox();
            this.ssPasswordTextBox = new System.Windows.Forms.TextBox();
            this.ssObfsTypeLabel = new System.Windows.Forms.Label();
            this.ssPasswordLabel = new System.Windows.Forms.Label();
            this.ssCipherComboBox = new System.Windows.Forms.ComboBox();
            this.ssEncryptTypeLabel = new System.Windows.Forms.Label();
            this.ssObfsHostTextBox = new System.Windows.Forms.TextBox();
            this.proxyModePanel = new System.Windows.Forms.Panel();
            this.proxyModelLabel = new System.Windows.Forms.Label();
            this.directModeRadioButton = new System.Windows.Forms.RadioButton();
            this.globalModeRadioButton = new System.Windows.Forms.RadioButton();
            this.ruleModeRadioButton = new System.Windows.Forms.RadioButton();
            this.advanceConfigPanel = new System.Windows.Forms.Panel();
            this.saveConfigTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.saveConfigButton = new System.Windows.Forms.Button();
            this.socks5ConfigPanel = new System.Windows.Forms.Panel();
            this.socksTLScheckBox = new System.Windows.Forms.CheckBox();
            this.socksSkipVerifyCheckBox = new System.Windows.Forms.CheckBox();
            this.proxyListBox = new System.Windows.Forms.ListBox();
            this.basicConfigTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverPoxtNumericUpDown)).BeginInit();
            this.ssConfigPanel.SuspendLayout();
            this.ssConfigTableLayoutPanel.SuspendLayout();
            this.proxyModePanel.SuspendLayout();
            this.advanceConfigPanel.SuspendLayout();
            this.saveConfigTableLayoutPanel.SuspendLayout();
            this.socks5ConfigPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // addButton
            // 
            this.addButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addButton.Location = new System.Drawing.Point(26, 658);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(110, 42);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "添加";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.deleteButton.Location = new System.Drawing.Point(156, 658);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(110, 42);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "删除";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // basicConfigTableLayoutPanel
            // 
            this.basicConfigTableLayoutPanel.ColumnCount = 2;
            this.basicConfigTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.basicConfigTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.basicConfigTableLayoutPanel.Controls.Add(this.nameTextBox, 1, 0);
            this.basicConfigTableLayoutPanel.Controls.Add(this.portLabel, 0, 2);
            this.basicConfigTableLayoutPanel.Controls.Add(this.typeLabel, 0, 3);
            this.basicConfigTableLayoutPanel.Controls.Add(this.serverLabel, 0, 1);
            this.basicConfigTableLayoutPanel.Controls.Add(this.serverTextBox, 1, 1);
            this.basicConfigTableLayoutPanel.Controls.Add(this.typeComboBox, 1, 3);
            this.basicConfigTableLayoutPanel.Controls.Add(this.nameLabel, 0, 0);
            this.basicConfigTableLayoutPanel.Controls.Add(this.serverPoxtNumericUpDown, 1, 2);
            this.basicConfigTableLayoutPanel.Location = new System.Drawing.Point(319, 35);
            this.basicConfigTableLayoutPanel.Name = "basicConfigTableLayoutPanel";
            this.basicConfigTableLayoutPanel.RowCount = 4;
            this.basicConfigTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.basicConfigTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.basicConfigTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.basicConfigTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.basicConfigTableLayoutPanel.Size = new System.Drawing.Size(580, 192);
            this.basicConfigTableLayoutPanel.TabIndex = 3;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameTextBox.Location = new System.Drawing.Point(177, 3);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(400, 39);
            this.nameTextBox.TabIndex = 9;
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portLabel.Location = new System.Drawing.Point(3, 96);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(168, 48);
            this.portLabel.TabIndex = 6;
            this.portLabel.Text = "端口：";
            this.portLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeLabel.Location = new System.Drawing.Point(3, 144);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(168, 48);
            this.typeLabel.TabIndex = 4;
            this.typeLabel.Text = "类型：";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // serverLabel
            // 
            this.serverLabel.AutoSize = true;
            this.serverLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverLabel.Location = new System.Drawing.Point(3, 48);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(168, 48);
            this.serverLabel.TabIndex = 0;
            this.serverLabel.Text = "服务器：";
            this.serverLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // serverTextBox
            // 
            this.serverTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverTextBox.Location = new System.Drawing.Point(177, 51);
            this.serverTextBox.Name = "serverTextBox";
            this.serverTextBox.Size = new System.Drawing.Size(400, 39);
            this.serverTextBox.TabIndex = 1;
            // 
            // typeComboBox
            // 
            this.typeComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            "HTTP",
            "HTTPS",
            "SOCKS5",
            "Shadowsocks",
            "Vmess"});
            this.typeComboBox.Location = new System.Drawing.Point(177, 147);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(400, 39);
            this.typeComboBox.TabIndex = 7;
            this.typeComboBox.SelectedIndexChanged += new System.EventHandler(this.typeComboBox_SelectedIndexChanged);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameLabel.Location = new System.Drawing.Point(3, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(168, 48);
            this.nameLabel.TabIndex = 8;
            this.nameLabel.Text = "名称：";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // serverPoxtNumericUpDown
            // 
            this.serverPoxtNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverPoxtNumericUpDown.Location = new System.Drawing.Point(177, 99);
            this.serverPoxtNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.serverPoxtNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.serverPoxtNumericUpDown.Name = "serverPoxtNumericUpDown";
            this.serverPoxtNumericUpDown.Size = new System.Drawing.Size(400, 39);
            this.serverPoxtNumericUpDown.TabIndex = 10;
            this.serverPoxtNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ssConfigPanel
            // 
            this.ssConfigPanel.Controls.Add(this.ssConfigTableLayoutPanel);
            this.ssConfigPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ssConfigPanel.Location = new System.Drawing.Point(0, 0);
            this.ssConfigPanel.Name = "ssConfigPanel";
            this.ssConfigPanel.Size = new System.Drawing.Size(580, 201);
            this.ssConfigPanel.TabIndex = 4;
            this.ssConfigPanel.Visible = false;
            // 
            // ssConfigTableLayoutPanel
            // 
            this.ssConfigTableLayoutPanel.ColumnCount = 2;
            this.ssConfigTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.ssConfigTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.ssConfigTableLayoutPanel.Controls.Add(this.ssObfsHostLabel, 0, 3);
            this.ssConfigTableLayoutPanel.Controls.Add(this.ssObfsTypeTextBox, 1, 2);
            this.ssConfigTableLayoutPanel.Controls.Add(this.ssPasswordTextBox, 1, 1);
            this.ssConfigTableLayoutPanel.Controls.Add(this.ssObfsTypeLabel, 0, 2);
            this.ssConfigTableLayoutPanel.Controls.Add(this.ssPasswordLabel, 0, 1);
            this.ssConfigTableLayoutPanel.Controls.Add(this.ssCipherComboBox, 1, 0);
            this.ssConfigTableLayoutPanel.Controls.Add(this.ssEncryptTypeLabel, 0, 0);
            this.ssConfigTableLayoutPanel.Controls.Add(this.ssObfsHostTextBox, 1, 3);
            this.ssConfigTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ssConfigTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.ssConfigTableLayoutPanel.Name = "ssConfigTableLayoutPanel";
            this.ssConfigTableLayoutPanel.RowCount = 4;
            this.ssConfigTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.ssConfigTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.ssConfigTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.ssConfigTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ssConfigTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ssConfigTableLayoutPanel.Size = new System.Drawing.Size(580, 192);
            this.ssConfigTableLayoutPanel.TabIndex = 0;
            // 
            // ssObfsHostLabel
            // 
            this.ssObfsHostLabel.AutoSize = true;
            this.ssObfsHostLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ssObfsHostLabel.Location = new System.Drawing.Point(3, 144);
            this.ssObfsHostLabel.Name = "ssObfsHostLabel";
            this.ssObfsHostLabel.Size = new System.Drawing.Size(168, 48);
            this.ssObfsHostLabel.TabIndex = 14;
            this.ssObfsHostLabel.Text = "混淆域名：";
            this.ssObfsHostLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ssObfsTypeTextBox
            // 
            this.ssObfsTypeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ssObfsTypeTextBox.Location = new System.Drawing.Point(177, 99);
            this.ssObfsTypeTextBox.Name = "ssObfsTypeTextBox";
            this.ssObfsTypeTextBox.Size = new System.Drawing.Size(400, 39);
            this.ssObfsTypeTextBox.TabIndex = 13;
            // 
            // ssPasswordTextBox
            // 
            this.ssPasswordTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ssPasswordTextBox.Location = new System.Drawing.Point(177, 51);
            this.ssPasswordTextBox.Name = "ssPasswordTextBox";
            this.ssPasswordTextBox.Size = new System.Drawing.Size(400, 39);
            this.ssPasswordTextBox.TabIndex = 12;
            this.ssPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // ssObfsTypeLabel
            // 
            this.ssObfsTypeLabel.AutoSize = true;
            this.ssObfsTypeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ssObfsTypeLabel.Location = new System.Drawing.Point(3, 96);
            this.ssObfsTypeLabel.Name = "ssObfsTypeLabel";
            this.ssObfsTypeLabel.Size = new System.Drawing.Size(168, 48);
            this.ssObfsTypeLabel.TabIndex = 11;
            this.ssObfsTypeLabel.Text = "混淆类型：";
            this.ssObfsTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ssPasswordLabel
            // 
            this.ssPasswordLabel.AutoSize = true;
            this.ssPasswordLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ssPasswordLabel.Location = new System.Drawing.Point(3, 48);
            this.ssPasswordLabel.Name = "ssPasswordLabel";
            this.ssPasswordLabel.Size = new System.Drawing.Size(168, 48);
            this.ssPasswordLabel.TabIndex = 9;
            this.ssPasswordLabel.Text = "密码：";
            this.ssPasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ssCipherComboBox
            // 
            this.ssCipherComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ssCipherComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ssCipherComboBox.FormattingEnabled = true;
            this.ssCipherComboBox.Items.AddRange(new object[] {
            "AEAD_AES_128_GCM",
            "AEAD_AES_192_GCM",
            "AEAD_AES_256_GCM",
            "AEAD_CHACHA20_POLY1305",
            "AES-128-CTR",
            "AES-192-CTR",
            "AES-256-CTR",
            "AES-128-CFB",
            "AES-192-CFB",
            "AES-256-CFB",
            "CHACHA20",
            "CHACHA20-IETF",
            "XCHACHA20",
            "XCHACHA20-IETF-POLY1305",
            "RC4-MD5"});
            this.ssCipherComboBox.Location = new System.Drawing.Point(177, 3);
            this.ssCipherComboBox.Name = "ssCipherComboBox";
            this.ssCipherComboBox.Size = new System.Drawing.Size(400, 39);
            this.ssCipherComboBox.TabIndex = 8;
            // 
            // ssEncryptTypeLabel
            // 
            this.ssEncryptTypeLabel.AutoSize = true;
            this.ssEncryptTypeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ssEncryptTypeLabel.Location = new System.Drawing.Point(3, 0);
            this.ssEncryptTypeLabel.Name = "ssEncryptTypeLabel";
            this.ssEncryptTypeLabel.Size = new System.Drawing.Size(168, 48);
            this.ssEncryptTypeLabel.TabIndex = 5;
            this.ssEncryptTypeLabel.Text = "加密类型：";
            this.ssEncryptTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ssObfsHostTextBox
            // 
            this.ssObfsHostTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ssObfsHostTextBox.Location = new System.Drawing.Point(177, 147);
            this.ssObfsHostTextBox.Name = "ssObfsHostTextBox";
            this.ssObfsHostTextBox.Size = new System.Drawing.Size(400, 39);
            this.ssObfsHostTextBox.TabIndex = 15;
            // 
            // proxyModePanel
            // 
            this.proxyModePanel.Controls.Add(this.proxyModelLabel);
            this.proxyModePanel.Controls.Add(this.directModeRadioButton);
            this.proxyModePanel.Controls.Add(this.globalModeRadioButton);
            this.proxyModePanel.Controls.Add(this.ruleModeRadioButton);
            this.proxyModePanel.Location = new System.Drawing.Point(319, 658);
            this.proxyModePanel.Name = "proxyModePanel";
            this.proxyModePanel.Size = new System.Drawing.Size(577, 42);
            this.proxyModePanel.TabIndex = 5;
            // 
            // proxyModelLabel
            // 
            this.proxyModelLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.proxyModelLabel.Location = new System.Drawing.Point(0, 0);
            this.proxyModelLabel.Margin = new System.Windows.Forms.Padding(0);
            this.proxyModelLabel.Name = "proxyModelLabel";
            this.proxyModelLabel.Size = new System.Drawing.Size(298, 42);
            this.proxyModelLabel.TabIndex = 3;
            this.proxyModelLabel.Text = "代理模式：";
            this.proxyModelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // directModeRadioButton
            // 
            this.directModeRadioButton.AutoSize = true;
            this.directModeRadioButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.directModeRadioButton.Location = new System.Drawing.Point(298, 0);
            this.directModeRadioButton.Name = "directModeRadioButton";
            this.directModeRadioButton.Size = new System.Drawing.Size(93, 42);
            this.directModeRadioButton.TabIndex = 2;
            this.directModeRadioButton.TabStop = true;
            this.directModeRadioButton.Text = "直连";
            this.directModeRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.directModeRadioButton.UseVisualStyleBackColor = true;
            this.directModeRadioButton.CheckedChanged += new System.EventHandler(this.directModeRadioButton_CheckedChanged);
            // 
            // globalModeRadioButton
            // 
            this.globalModeRadioButton.AutoSize = true;
            this.globalModeRadioButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.globalModeRadioButton.Location = new System.Drawing.Point(391, 0);
            this.globalModeRadioButton.Name = "globalModeRadioButton";
            this.globalModeRadioButton.Size = new System.Drawing.Size(93, 42);
            this.globalModeRadioButton.TabIndex = 1;
            this.globalModeRadioButton.TabStop = true;
            this.globalModeRadioButton.Text = "全局";
            this.globalModeRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.globalModeRadioButton.UseVisualStyleBackColor = true;
            this.globalModeRadioButton.CheckedChanged += new System.EventHandler(this.globalModeRadioButton_CheckedChanged);
            // 
            // ruleModeRadioButton
            // 
            this.ruleModeRadioButton.AutoSize = true;
            this.ruleModeRadioButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.ruleModeRadioButton.Location = new System.Drawing.Point(484, 0);
            this.ruleModeRadioButton.Name = "ruleModeRadioButton";
            this.ruleModeRadioButton.Size = new System.Drawing.Size(93, 42);
            this.ruleModeRadioButton.TabIndex = 0;
            this.ruleModeRadioButton.TabStop = true;
            this.ruleModeRadioButton.Text = "规则";
            this.ruleModeRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ruleModeRadioButton.UseVisualStyleBackColor = true;
            this.ruleModeRadioButton.CheckedChanged += new System.EventHandler(this.ruleModeRadioButton_CheckedChanged);
            // 
            // advanceConfigPanel
            // 
            this.advanceConfigPanel.Controls.Add(this.saveConfigTableLayoutPanel);
            this.advanceConfigPanel.Controls.Add(this.socks5ConfigPanel);
            this.advanceConfigPanel.Controls.Add(this.ssConfigPanel);
            this.advanceConfigPanel.Location = new System.Drawing.Point(319, 233);
            this.advanceConfigPanel.Name = "advanceConfigPanel";
            this.advanceConfigPanel.Size = new System.Drawing.Size(580, 402);
            this.advanceConfigPanel.TabIndex = 6;
            // 
            // saveConfigTableLayoutPanel
            // 
            this.saveConfigTableLayoutPanel.ColumnCount = 2;
            this.saveConfigTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.96552F));
            this.saveConfigTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.03448F));
            this.saveConfigTableLayoutPanel.Controls.Add(this.saveConfigButton, 1, 0);
            this.saveConfigTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.saveConfigTableLayoutPanel.Location = new System.Drawing.Point(0, 262);
            this.saveConfigTableLayoutPanel.Name = "saveConfigTableLayoutPanel";
            this.saveConfigTableLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.saveConfigTableLayoutPanel.RowCount = 1;
            this.saveConfigTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.saveConfigTableLayoutPanel.Size = new System.Drawing.Size(580, 170);
            this.saveConfigTableLayoutPanel.TabIndex = 7;
            // 
            // saveConfigButton
            // 
            this.saveConfigButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.saveConfigButton.Location = new System.Drawing.Point(403, 13);
            this.saveConfigButton.Name = "saveConfigButton";
            this.saveConfigButton.Size = new System.Drawing.Size(174, 50);
            this.saveConfigButton.TabIndex = 7;
            this.saveConfigButton.Text = "保存配置";
            this.saveConfigButton.UseVisualStyleBackColor = true;
            this.saveConfigButton.Click += new System.EventHandler(this.saveConfigButton_Click);
            // 
            // socks5ConfigPanel
            // 
            this.socks5ConfigPanel.Controls.Add(this.socksTLScheckBox);
            this.socks5ConfigPanel.Controls.Add(this.socksSkipVerifyCheckBox);
            this.socks5ConfigPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.socks5ConfigPanel.Location = new System.Drawing.Point(0, 201);
            this.socks5ConfigPanel.Name = "socks5ConfigPanel";
            this.socks5ConfigPanel.Size = new System.Drawing.Size(580, 61);
            this.socks5ConfigPanel.TabIndex = 8;
            // 
            // socksTLScheckBox
            // 
            this.socksTLScheckBox.AutoSize = true;
            this.socksTLScheckBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.socksTLScheckBox.Location = new System.Drawing.Point(304, 0);
            this.socksTLScheckBox.Name = "socksTLScheckBox";
            this.socksTLScheckBox.Size = new System.Drawing.Size(86, 61);
            this.socksTLScheckBox.TabIndex = 1;
            this.socksTLScheckBox.Text = "TLS";
            this.socksTLScheckBox.UseVisualStyleBackColor = true;
            // 
            // socksSkipVerifyCheckBox
            // 
            this.socksSkipVerifyCheckBox.AutoSize = true;
            this.socksSkipVerifyCheckBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.socksSkipVerifyCheckBox.Location = new System.Drawing.Point(390, 0);
            this.socksSkipVerifyCheckBox.Name = "socksSkipVerifyCheckBox";
            this.socksSkipVerifyCheckBox.Size = new System.Drawing.Size(190, 61);
            this.socksSkipVerifyCheckBox.TabIndex = 0;
            this.socksSkipVerifyCheckBox.Text = "跳过证书验证";
            this.socksSkipVerifyCheckBox.UseVisualStyleBackColor = true;
            // 
            // proxyListBox
            // 
            this.proxyListBox.FormattingEnabled = true;
            this.proxyListBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.proxyListBox.ItemHeight = 31;
            this.proxyListBox.Location = new System.Drawing.Point(26, 35);
            this.proxyListBox.Name = "proxyListBox";
            this.proxyListBox.Size = new System.Drawing.Size(240, 593);
            this.proxyListBox.TabIndex = 7;
            this.proxyListBox.SelectedIndexChanged += new System.EventHandler(this.proxyListBox_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(934, 729);
            this.Controls.Add(this.proxyListBox);
            this.Controls.Add(this.advanceConfigPanel);
            this.Controls.Add(this.proxyModePanel);
            this.Controls.Add(this.basicConfigTableLayoutPanel);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.addButton);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ClashW";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.basicConfigTableLayoutPanel.ResumeLayout(false);
            this.basicConfigTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverPoxtNumericUpDown)).EndInit();
            this.ssConfigPanel.ResumeLayout(false);
            this.ssConfigTableLayoutPanel.ResumeLayout(false);
            this.ssConfigTableLayoutPanel.PerformLayout();
            this.proxyModePanel.ResumeLayout(false);
            this.proxyModePanel.PerformLayout();
            this.advanceConfigPanel.ResumeLayout(false);
            this.saveConfigTableLayoutPanel.ResumeLayout(false);
            this.socks5ConfigPanel.ResumeLayout(false);
            this.socks5ConfigPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.TableLayoutPanel basicConfigTableLayoutPanel;
        private System.Windows.Forms.Label serverLabel;
        private System.Windows.Forms.TextBox serverTextBox;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Panel ssConfigPanel;
        private System.Windows.Forms.TableLayoutPanel ssConfigTableLayoutPanel;
        private System.Windows.Forms.ComboBox ssCipherComboBox;
        private System.Windows.Forms.Label ssEncryptTypeLabel;
        private System.Windows.Forms.TextBox ssObfsTypeTextBox;
        private System.Windows.Forms.TextBox ssPasswordTextBox;
        private System.Windows.Forms.Label ssObfsTypeLabel;
        private System.Windows.Forms.Label ssPasswordLabel;
        private System.Windows.Forms.Label ssObfsHostLabel;
        private System.Windows.Forms.TextBox ssObfsHostTextBox;
        private System.Windows.Forms.Panel proxyModePanel;
        private System.Windows.Forms.Panel advanceConfigPanel;
        private System.Windows.Forms.TableLayoutPanel saveConfigTableLayoutPanel;
        private System.Windows.Forms.Button saveConfigButton;
        private System.Windows.Forms.Label proxyModelLabel;
        private System.Windows.Forms.RadioButton directModeRadioButton;
        private System.Windows.Forms.RadioButton globalModeRadioButton;
        private System.Windows.Forms.RadioButton ruleModeRadioButton;
        private System.Windows.Forms.ListBox proxyListBox;
        private System.Windows.Forms.NumericUpDown serverPoxtNumericUpDown;
        private System.Windows.Forms.Panel socks5ConfigPanel;
        private System.Windows.Forms.CheckBox socksTLScheckBox;
        private System.Windows.Forms.CheckBox socksSkipVerifyCheckBox;
    }
}