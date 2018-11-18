namespace ClashW.View
{
    partial class OnlineRuleForm
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
            this.onlineLabel = new System.Windows.Forms.Label();
            this.onlineLinkTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.updateCycleLabel = new System.Windows.Forms.Label();
            this.cycle1HRadioButton = new System.Windows.Forms.RadioButton();
            this.cycle6HRadioButton = new System.Windows.Forms.RadioButton();
            this.cycle12HRadioButton = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // onlineLabel
            // 
            this.onlineLabel.AutoSize = true;
            this.onlineLabel.Location = new System.Drawing.Point(31, 38);
            this.onlineLabel.Name = "onlineLabel";
            this.onlineLabel.Size = new System.Drawing.Size(134, 31);
            this.onlineLabel.TabIndex = 0;
            this.onlineLabel.Text = "规则链接：";
            // 
            // onlineLinkTextBox
            // 
            this.onlineLinkTextBox.Location = new System.Drawing.Point(36, 88);
            this.onlineLinkTextBox.Name = "onlineLinkTextBox";
            this.onlineLinkTextBox.Size = new System.Drawing.Size(665, 39);
            this.onlineLinkTextBox.TabIndex = 1;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(566, 256);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(135, 50);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "保存";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cycle12HRadioButton);
            this.panel1.Controls.Add(this.cycle6HRadioButton);
            this.panel1.Controls.Add(this.cycle1HRadioButton);
            this.panel1.Controls.Add(this.updateCycleLabel);
            this.panel1.Location = new System.Drawing.Point(37, 147);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(664, 103);
            this.panel1.TabIndex = 3;
            // 
            // updateCycleLabel
            // 
            this.updateCycleLabel.AutoSize = true;
            this.updateCycleLabel.Location = new System.Drawing.Point(-7, 0);
            this.updateCycleLabel.Name = "updateCycleLabel";
            this.updateCycleLabel.Size = new System.Drawing.Size(134, 31);
            this.updateCycleLabel.TabIndex = 0;
            this.updateCycleLabel.Text = "更新周期：";
            // 
            // cycle1HRadioButton
            // 
            this.cycle1HRadioButton.AutoSize = true;
            this.cycle1HRadioButton.Location = new System.Drawing.Point(4, 47);
            this.cycle1HRadioButton.Name = "cycle1HRadioButton";
            this.cycle1HRadioButton.Size = new System.Drawing.Size(107, 35);
            this.cycle1HRadioButton.TabIndex = 1;
            this.cycle1HRadioButton.TabStop = true;
            this.cycle1HRadioButton.Text = "1小时";
            this.cycle1HRadioButton.UseVisualStyleBackColor = true;
            this.cycle1HRadioButton.CheckedChanged += new System.EventHandler(this.cycle1HRadioButton_CheckedChanged);
            // 
            // cycle6HRadioButton
            // 
            this.cycle6HRadioButton.AutoSize = true;
            this.cycle6HRadioButton.Location = new System.Drawing.Point(117, 47);
            this.cycle6HRadioButton.Name = "cycle6HRadioButton";
            this.cycle6HRadioButton.Size = new System.Drawing.Size(107, 35);
            this.cycle6HRadioButton.TabIndex = 2;
            this.cycle6HRadioButton.TabStop = true;
            this.cycle6HRadioButton.Text = "6小时";
            this.cycle6HRadioButton.UseVisualStyleBackColor = true;
            this.cycle6HRadioButton.CheckedChanged += new System.EventHandler(this.cycle6HRadioButton_CheckedChanged);
            // 
            // cycle12HRadioButton
            // 
            this.cycle12HRadioButton.AutoSize = true;
            this.cycle12HRadioButton.Location = new System.Drawing.Point(230, 47);
            this.cycle12HRadioButton.Name = "cycle12HRadioButton";
            this.cycle12HRadioButton.Size = new System.Drawing.Size(121, 35);
            this.cycle12HRadioButton.TabIndex = 3;
            this.cycle12HRadioButton.TabStop = true;
            this.cycle12HRadioButton.Text = "12小时";
            this.cycle12HRadioButton.UseVisualStyleBackColor = true;
            this.cycle12HRadioButton.CheckedChanged += new System.EventHandler(this.cycle12HRadioButton_CheckedChanged);
            // 
            // OnlineRuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(735, 329);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.onlineLinkTextBox);
            this.Controls.Add(this.onlineLabel);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "OnlineRuleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "在线规则";
            this.Load += new System.EventHandler(this.OnlineRuleForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label onlineLabel;
        private System.Windows.Forms.TextBox onlineLinkTextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label updateCycleLabel;
        private System.Windows.Forms.RadioButton cycle12HRadioButton;
        private System.Windows.Forms.RadioButton cycle6HRadioButton;
        private System.Windows.Forms.RadioButton cycle1HRadioButton;
    }
}