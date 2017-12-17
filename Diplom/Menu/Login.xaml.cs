using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFPageSwitch
{
	/// <summary>
	/// Interaction logic for Login.xaml
	/// </summary>
	public partial class Login : UserControl, ISwitchable
	{
		public Login()
		{
			this.InitializeComponent();
            if (User.Name != null && User.Name != "")
            {
                usernameTextBox.Text = User.Name;
            }
            if (User.Group != null && User.Group != "")
            {
                passwordBox.Text = User.Group;
            }
        }        

		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			
		}

        #region ISwitchable Members

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        #endregion

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            User.Name = usernameTextBox.Text;
            User.Group = passwordBox.Text;

            if (User.Name == null || User.Name == "")
            {
                errorText.Text = "Введите Имя";
                return;
            }            
            if (User.Group == null || User.Group == "")
            {
                errorText.Text = "Введите Группу";
                return;
            }           
            Switcher.Switch(new Gameplay());
        }
    }
}