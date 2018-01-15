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

        List<double> list = new List<double>();
        List<string> listx = new List<string>();

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
            list.Clear();
            listx.Clear();
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
            
            float eT = float.Parse(eTText.Text);
            float CigO = float.Parse(CigOText.Text);
            float TT = float.Parse(TTText.Text);
            float a1 = float.Parse(A1Text.Text);

            float lyam = float.Parse(STText.Text);
            float r = float.Parse(RTText.Text);
            float e1 = float.Parse(IText.Text);

            float fi1P = 1f / 4f;
            float A1P = 0.9f;
            float fi13 = 1f / 2f;
            float A13 = 0.9f;
            float A1d3 = 0.95f;

            float N1 = fi1P * Fn * (1 - A1P) + fi13 * Foz * (1 - A13) * Fdz * (1 - A1d3);
            

            for (int i = -60; i < 70; i++)
            {
                TT = i + 273.15f;
                //list.Add(Math.Round(
                //    +((Fn * FiTn * (1 - An)) / aT)
                //    + ((FiTz * ((Foz * (1 - AToz)) + (Fdz * (1 - ATdz)))) / aT)
                //    + ((FiTK * ((FPOK * (1 - ATPOK)) + (FOOK * (1 - ATOOK)) + (FdK * (1 - ATdK)))) / aT)
                //    + ((UT * UT) / (RT * aT * ST))
                //    - (5.36f * Math.Pow(10, -2))
                //    + ((eT * CigO * TT * TT * TT * TT) / a1)
                //    , 2));

                list.Add(Math.Round(- 10 + TT - 
                    (
                    + TT
                    - ((Fn * FiTn * (1 - An)) / aT)
                    - ((FiTz * ((Foz * (1 - AToz)) + (Fdz * (1 - ATdz)))) / aT)
                    - ((FiTK * ((FPOK * (1 - ATPOK)) + (FOOK * (1 - ATOOK)) + (FdK * (1 - ATdK)))) / aT)
                    //- ((UT * UT) / (RT * aT * ST))                    
                    + ((eT * CigO * TT * TT * TT * TT) / a1)
                    + (2 * TT * lyam * Math.PI * r * r * r * (a1 + 4 * e1 * CigO * TT * TT * TT))
                    - ((2 * N1 * lyam * Math.PI * r * r * r * (a1 + 4 * e1 * CigO * TT * TT * TT)) / (Math.PI * (a1 + 4 * e1 * CigO * TT * TT * TT)))
                    ) / (1 + 2 * lyam * Math.PI * r * r * r * (a1 + 4 * e1 * CigO * TT * TT * TT))
                    , 2));

                listx.Add(i.ToString() + " (°C)");
            }
            

            ChartValues<double> chart = new ChartValues<double>(list);
            
            LineSeries ls = new LineSeries();
            ls.Title = "∆T (°C)";
            ls.Values = chart;
            ls.PointGeometry = null;
            SeriesCollection.Add(ls);            
            DataContext = this;

            xAxis.Labels = listx;
            xAxis.Title = "Температура (°C)";
            yAxis.Title = "Погрешность (°C)";
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

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Register(list, listx));
        }
    }
}