using ClashW.Config;
using ClashW.Config.Api;
using ClashW.Config.Api.Dao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ClashW.Config.Api.ClashApi;

namespace ClashW.View
{
    public partial class TrafficForm : Form
    {
        private TrafficInfoHandler trafficInfoHandler;
        private LinkedList<TrafficInfo> trafficInfos;
        public TrafficForm()
        {
            InitializeComponent();
            trafficInfoHandler = new TrafficInfoHandler(trafficInfoChanged);
            trafficInfos = new LinkedList<TrafficInfo>();
        }

        private void TrafficForm_Load(object sender, EventArgs e)
        {
           
            var chartArea = trafficChart.ChartAreas[0];
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Maximum = 10;
            
            trafficInfos.Clear();
            ConfigController.Instance.StartLoadTrafficInfo(trafficInfoHandler);

        }

        private void trafficInfoChanged(ClashApi clashApi, TrafficInfo trafficInfo)
        {
            if(trafficChart.InvokeRequired && !IsDisposed)
            {
                Invoke(trafficInfoHandler, new object[] {clashApi, trafficInfo });
            } else
            {
                uploadInfolabel.Text = generateFriendlyTrafficInfo(trafficInfo.Up);
                downloadInfoLabel.Text = generateFriendlyTrafficInfo(trafficInfo.Down);
                var count = trafficInfos.Count();
                if(count >= 11)
                {
                    trafficInfos.RemoveLast();
                }
                trafficInfos.AddFirst(trafficInfo);
                updateChart();
            }
        }

        private void updateChart()
        {
            int x = 0;
            var upSeries = trafficChart.Series[0];
            var downSeries = trafficChart.Series[1];
            upSeries.Points.Clear();
            downSeries.Points.Clear();
            var maxY = 100d;

            foreach (TrafficInfo trafficInfo in trafficInfos)
            {
                maxY = Math.Max(trafficInfo.Down, maxY);
                maxY = Math.Max(trafficInfo.Up, maxY);
            }

            var unit = 0;
            var unitString = string.Empty;
            while (maxY > 1024 && unit < 2)
            {
                maxY /= 1024.0;
                unit++;
            }
            maxY = Math.Ceiling(maxY * 1.25);

            var chartArea = trafficChart.ChartAreas[0];
            chartArea.AxisY.Maximum = maxY;
            chartArea.AxisY.Interval = maxY / 5d;
            switch (unit)
            {
                case 0:
                    unitString = "B/s";
                    break;
                case 1:
                    unitString = "KB/s";
                    break;
                case 2:
                    unitString = "MB/s";
                    break;
                default:
                    break;
            }
            chartArea.AxisY.Title = $"速率（{unitString}）";
            foreach (TrafficInfo trafficInfo in trafficInfos)
            {
                upSeries.Points.AddXY(x, trafficInfo.Up / Math.Pow(1024, unit));
                downSeries.Points.AddXY(x, trafficInfo.Down / Math.Pow(1024, unit));
                x++;
            }
        }

        private string generateFriendlyTrafficInfo(long trafficNumber)
        {
            StringBuilder stringBuilder = new StringBuilder();
            double result = trafficNumber;
            var unit = 0;
            var unitString = string.Empty;
            while(result > 1024 && unit < 2)
            {
                result /= 1024.0;
                unit++;
            }
            switch(unit)
            {
                case 0:
                    unitString = "B/s";
                    break;
                case 1:
                    unitString = "KB/s";
                    break;
                case 2:
                    unitString = "MB/s";
                    break;
                default:
                    break;
            }
            stringBuilder.Append(String.Format("{0:F2}", result)).Append(unitString);
            return stringBuilder.ToString();
        }

        private void TrafficForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigController.Instance.StopLoadTrafficInfo(trafficInfoHandler);
        }
    }
}
