using System;
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
	public partial class LoadGame : UserControl, ISwitchable
	{
		public LoadGame(bool end, string msg)
		{
			InitializeComponent();
            MsgText.Text = msg;
            if (end)            
                NextBnt.IsEnabled = true;            
            else
                NextBnt.IsEnabled = false;

        }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	Switcher.Switch(new Gameplay());
        }
        #endregion

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }
    }
}