namespace ClashW.View
{
    partial class TrafficForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.downloadInfoLabel = new System.Windows.Forms.Label();
            this.downLoadlabel = new System.Windows.Forms.Label();
            this.uploadInfolabel = new System.Windows.Forms.Label();
            this.uploadLabel = new System.Windows.Forms.Label();
            this.trafficChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trafficChart)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.downloadInfoLabel);
            this.panel1.Controls.Add(this.downLoadlabel);
            this.panel1.Controls.Add(this.uploadInfolabel);
            this.panel1.Controls.Add(this.uploadLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1142, 87);
            this.panel1.TabIndex = 0;
            // 
            // downloadInfoLabel
            // 
            this.downloadInfoLabel.AutoSize = true;
            this.downloadInfoLabel.Location = new System.Drawing.Point(489, 34);
            this.downloadInfoLabel.Name = "downloadInfoLabel";
            this.downloadInfoLabel.Size = new System.Drawing.Size(64, 31);
            this.downloadInfoLabel.TabIndex = 3;
            this.downloadInfoLabel.Text = "0B/s";
            // 
            // downLoadlabel
            // 
            this.downLoadlabel.AutoSize = true;
            this.downLoadlabel.Location = new System.Drawing.Point(397, 34);
            this.downLoadlabel.Name = "downLoadlabel";
            this.downLoadlabel.Size = new System.Drawing.Size(86, 31);
            this.downLoadlabel.TabIndex = 2;
            this.downLoadlabel.Text = "下载：";
            // 
            // uploadInfolabel
            // 
            this.uploadInfolabel.AutoSize = true;
            this.uploadInfolabel.Location = new System.Drawing.Point(121, 34);
            this.uploadInfolabel.Name = "uploadInfolabel";
            this.uploadInfolabel.Size = new System.Drawing.Size(64, 31);
            this.uploadInfolabel.TabIndex = 1;
            this.uploadInfolabel.Text = "0B/s";
            // 
            // uploadLabel
            // 
            this.uploadLabel.AutoSize = true;
            this.uploadLabel.Location = new System.Drawing.Point(29, 34);
            this.uploadLabel.Name = "uploadLabel";
            this.uploadLabel.Size = new System.Drawing.Size(86, 31);
            this.uploadLabel.TabIndex = 0;
            this.uploadLabel.Text = "上传：";
            // 
            // trafficChart
            // 
            chartArea2.AxisX.Title = "经过时间（秒）";
            chartArea2.AxisY.Title = "速率";
            chartArea2.Name = "TrafficChartArea";
            this.trafficChart.ChartAreas.Add(chartArea2);
            this.trafficChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.trafficChart.Legends.Add(legend2);
            this.trafficChart.Location = new System.Drawing.Point(0, 87);
            this.trafficChart.Name = "trafficChart";
            series3.ChartArea = "TrafficChartArea";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "上传";
            series4.ChartArea = "TrafficChartArea";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "下载";
            this.trafficChart.Series.Add(series3);
            this.trafficChart.Series.Add(series4);
            this.trafficChart.Size = new System.Drawing.Size(1142, 634);
            this.trafficChart.TabIndex = 1;
            this.trafficChart.Text = "chart1";
            // 
            // TrafficForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1142, 721);
            this.Controls.Add(this.trafficChart);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TrafficForm";
            this.Text = "TrafficForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TrafficForm_FormClosing);
            this.Load += new System.EventHandler(this.TrafficForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trafficChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label downloadInfoLabel;
        private System.Windows.Forms.Label downLoadlabel;
        private System.Windows.Forms.Label uploadInfolabel;
        private System.Windows.Forms.Label uploadLabel;
        private System.Windows.Forms.DataVisualization.Charting.Chart trafficChart;
    }
}