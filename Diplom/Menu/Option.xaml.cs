using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WPFPageSwitch
{
	public partial class Option : UserControl, ISwitchable
	{
        public SeriesCollection SeriesCollection { get; set; }
        public ChartValues<float> MyValues { get; private set; }

        List<double> list1 = new List<double>()
        {
            Math.Round(1.43f,2),
            Math.Round(2.91f,2),
            Math.Round(4.24f,2)
        };

        List<double> list2 = new List<double>()
        {
            Math.Round(0.45f,2),
            Math.Round(0.03f,2),
            Math.Round(-0.85f,2)
        };
        List<double> list3 = new List<double>()
        {
            Math.Round(1.52f,2),
            Math.Round(2.93f,2),
            Math.Round(4.25f,2)
        };
        List<string> listx = new List<string>()
        {
            "0.25 мм",
            "0.7 мм",
            "1.6 мм"
        };

        public Option()
		{
            InitializeComponent();
            SeriesCollection = new SeriesCollection();
            ChartValues<double> chart = new ChartValues<double>(list1);

            LineSeries ls = new LineSeries();
            ls.Title = "Алюминий";
            ls.Values = chart;
            ls.PointGeometry = null;
            SeriesCollection.Add(ls);

            ChartValues<double> chart1 = new ChartValues<double>(list2);

            LineSeries ls1 = new LineSeries();
            ls1.Title = "ВЛ-548";
            ls1.Values = chart1;
            ls1.PointGeometry = null;
            SeriesCollection.Add(ls1);

            ChartValues<double> chart2 = new ChartValues<double>(list3);

            LineSeries ls2 = new LineSeries();
            ls2.Title = "НЦ-25";
            ls2.Values = chart2;
            ls2.PointGeometry = null;
            SeriesCollection.Add(ls2);
            DataContext = this;
            
            xAxis.Labels = listx;
            xAxis.Title = "Диаметр (мм)";
            yAxis.Title = "∆T";
            
            
            
        }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	Switcher.Switch(new MainMenu());
        }
        #endregion
	}
}