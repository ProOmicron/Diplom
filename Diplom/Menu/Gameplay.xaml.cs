using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
	public partial class Gameplay : UserControl, ISwitchable
	{
        private List<Question> questions = new List<Question>();

        private List<List<RadioButton>> radioButtonGroup = new List<List<RadioButton>>();

		public Gameplay()
		{
			InitializeComponent();
            NameGroup.Text = "Имя: " + User.Name + " Группа: " + User.Group;
            Import();
            CreateQuiz();
        }

        private void Import()
        {
            using (var fs = File.OpenRead("log.csv"))
            {
                using (var reader = new StreamReader(fs))
                {
                    reader.ReadLine(); //Пропускаем первую строку.
                    while (!reader.EndOfStream)
                    {
                        Question question = new Question();
                        var line = reader.ReadLine();                        
                        var values = line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        question.Name = values[0];
                        question.CorrectAnswer = Int32.Parse(values[1]);
                        for (int i = 2; i < values.Length; i++)
                        {
                            question.Answer.Add(values[i]); 
                        }
                        questions.Add(question);
                    }
                }
            }
        }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	Switcher.Switch(new Login());
        }
        #endregion
        
        private void CreateQuiz()
        {
            int questionNumber = 0;
            
            myStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            myStackPanel.VerticalAlignment = VerticalAlignment.Top;

            foreach (var question in questions)
            {
                TextBlock txt = new TextBlock();
                txt.Text = (questionNumber + 1) + " " + question.Name;
                txt.Margin = new Thickness(50, 0, 0, 0);// top + (topposition * questionNumber), 0, 0);
                txt.HorizontalAlignment = HorizontalAlignment.Left;
                txt.TextWrapping = TextWrapping.Wrap;
                txt.VerticalAlignment = VerticalAlignment.Top;
                myStackPanel.Children.Add(txt);

                List<RadioButton> radioButtons = new List<RadioButton>();

                foreach (var item in question.Answer)
                {
                    RadioButton radioButton = new RadioButton();
                    radioButton.GroupName = question.Name;
                    radioButton.Content = item;
                    radioButton.Margin = new Thickness(400, 0, 0, 0);// top + (topposition * questionNumber), 0, 0);
                    myStackPanel.Children.Add(radioButton);                    
                    radioButtons.Add(radioButton);
                }

                Separator separator = new Separator();
                separator.Height = 3;
                myStackPanel.Children.Add(separator);
                questionNumber++;
                radioButtonGroup.Add(radioButtons);
            }

            scrollViewer.Content = myStackPanel;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            float questionsCount = questions.Count;
            float questionsTrue = 0;

            for (int i = 0; i < questions.Count; i++)
            {
                for (int j = 0; j < radioButtonGroup[i].Count; j++)
                {
                    if (radioButtonGroup[i][j].IsChecked == true)
                    {
                        if (questions[i].CorrectAnswer - 1 == j)
                        {
                            questionsTrue++;
                        }
                    }
                }                
            }
            bool won = false;
            string text = "";
            if (questionsTrue / questionsCount >=  0.8f)
            {
                won = true;
                User.Count = (int)questionsCount;
                User.CountTrue = (int)questionsTrue;
                text = "Имя: " + User.Name + " Группа: " + User.Group + "\nПравильных ответов:\n " + User.CountTrue + " из " + User.Count + "\nВы прошли тест!";
            }
            else
                text = "Имя: " + User.Name + " Группа: " + User.Group + "\nПравильных ответов:\n " + questionsTrue + "из " + questionsCount + "\nВы не прошли тест!";

            Switcher.Switch(new LoadGame(won, text));
        }
    }
}