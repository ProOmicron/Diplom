using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Drawing.Printing;
using System.Windows.Controls;
using System.Drawing;
using System.Windows;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace WPFPageSwitch
{    
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : UserControl
	{
        public SeriesCollection SeriesCollection { get; set; }
        public ChartValues<float> MyValues { get; private set; }

        List<double> list = new List<double>();
        List<string> listx = new List<string>();

        Bitmap bmp;

        public Register(List<double> list, List<string> listx)
		{
			this.InitializeComponent();
            SeriesCollection = new SeriesCollection();
            this.list = list;
            this.listx = listx;
            GarphMain();

            //textBox.Text = User.Name + " " + User.Group + " " + User.Count;
        }

		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Switcher.Switch(new MainMenu());
		}

        protected void Button_Click_1(object sender, EventArgs e)
        {
            CaptureApplication();
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("mstucams@gmail.com");
            mail.To.Add("mstucams@gmail.com");
            mail.Subject = "Отчёт практической работы от студента " + User.Name + " " + User.Group + ".";
            mail.Body = "Отчёт практической работы от студента " + User.Name + " " + User.Group + ".\nВывод студента:" + textBox.Text + "\nПравильных ответов теста: " + User.CountTrue + " из " + User.Count;
            mail.Attachments.Add(new Attachment("graph.png"));

            SendMailMessage(mail);

            
        }

        public void CaptureApplication()
        {
            var proc = Process.GetCurrentProcess();
            var rect = new User32.Rect();
            User32.GetWindowRect(proc.MainWindowHandle, ref rect);

            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;

            bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.CopyFromScreen(rect.left, rect.top, -20, -180, new System.Drawing.Size(790, 400), CopyPixelOperation.SourceCopy);

            bmp.Save("graph.png", ImageFormat.Png);
        }

        private class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct Rect
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);
        }

        private void GarphMain()
        {            
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

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        
        //}

        private void SendMailMessage(MailMessage mail)
        {
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential("mstucams@gmail.com", "Mstuca123456") as ICredentialsByHost;
            smtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            try
            {
                smtpServer.Send(mail);
            }
            catch (Exception)
            {

            }
        }
    }
}