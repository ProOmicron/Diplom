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
	public partial class MainMenu : UserControl, ISwitchable
	{

        //LoginWindowForm loginForm;
        //RegisterForm registerForm;
        public SeriesCollection SeriesCollection { get; set; }

        public MainMenu()
		{
            
			InitializeComponent();
            SeriesCollection = new SeriesCollection();

            Graph();
        }

		private void newGameButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Switcher.Switch(new Gameplay());
		}

		private void loadGameButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Switcher.Switch(new LoadGame());
		}
        
        private void Graph()
        {
            SeriesCollection.Clear();
            List<double> list = new List<double>();
            for (int i = 0; i < 100; i++)
            {
                list.Add(Math.Sin(i/10f));
            }
            ChartValues<double> chart = new ChartValues<double>(list);
            LineSeries ls = new LineSeries();
            ls.Title = "Дистанция вперёд";
            ls.Values = chart;
            ls.PointGeometry = null;
            SeriesCollection.Add(ls);
            DataContext = this;
        }

        
        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void loginTextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        	Switcher.Switch(new Login());
        }

        private void registerTextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        	Switcher.Switch(new Register());
        }
        #endregion
		
	}
}