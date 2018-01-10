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
        public ChartValues<float> MyValues { get; private set; }

        public MainMenu()
		{            
			InitializeComponent();
            SeriesCollection = new SeriesCollection();

            //Graph(1);
            GarphMain();
        }

		private void newGameButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Switcher.Switch(new Gameplay());
		}

		private void loadGameButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            //float count = float.Parse(Counter.Text);
            //Graph(count);
            SeriesCollection.Clear();
            GarphMain();
        }
        
        private void Graph(float count)
        {
            //SeriesCollection.Clear();
            List<double> list = new List<double>();
            for (int i = 0; i < 100; i++)
            {
                list.Add(count * Math.Sin(i/10f));
            }
            ChartValues<double> chart = new ChartValues<double>(list);
            LineSeries ls = new LineSeries();
            ls.Title = "Дистанция вперёд";
            ls.Values = chart;
            ls.PointGeometry = null;
            SeriesCollection.Add(ls);
            DataContext = this;
        }

        private void GarphMain()
        {
            List<double> list = new List<double>();
            List<string> listx = new List<string>();
            float Fn = float.Parse(FnText.Text);
            float FiTn = float.Parse(FiTnText.Text);
            float An = float.Parse(AnText.Text);
            float aT = float.Parse(aTText.Text);
            float FiTz = float.Parse(FiTzText.Text);
            float Foz = float.Parse(FozText.Text);
            float AToz = float.Parse(ATozText.Text);
            float Fdz = float.Parse(FdzText.Text);
            float ATdz = float.Parse(ATdzText.Text);
            float FiTK = float.Parse(FiTKText.Text);
            float FPOK = float.Parse(FPOKText.Text);
            float ATPOK = float.Parse(ATPOKText.Text);
            float FOOK = float.Parse(FOOKText.Text);
            float ATOOK = float.Parse(ATOOKText.Text);
            float FdK = float.Parse(FdKText.Text);
            float ATdK = float.Parse(ATdKText.Text);
            float UT = 0;
            float RT = 0;
            float ST = float.Parse(STText.Text);
            float I = float.Parse(IText.Text);

            for (int i = 1000; i < 220000; i += 1000)
            {
                RT = i;
                UT = I * RT;

                list.Add(
                    +((Fn * FiTn * (1 - An)) / aT) 
                    +((FiTz * ((Foz * (1 - AToz)) + (Fdz * (1 - ATdz)))) / aT) 
                    +((FiTK * ((FPOK * (1 - ATPOK)) + (FOOK * (1 - ATOOK)) + (FdK * (1 - ATdK)))) / aT)
                    +((UT * UT) / (RT * aT * ST)) 
                    -(5.36f * Math.Pow(10, -2))
                    );

                listx.Add(i.ToString() + " (Ом)");
            }
            

            ChartValues<double> chart = new ChartValues<double>(list);
            
            LineSeries ls = new LineSeries();
            ls.Title = "Зависимость погрешности от сопротивления";
            ls.Values = chart;
            ls.PointGeometry = null;
            SeriesCollection.Add(ls);            
            DataContext = this;

            xAxis.Labels = listx;
            xAxis.Title = "Сопротивление (Ом)";
            yAxis.Title = "Погрешность (∆T)";
        }

        
        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }
    }
}