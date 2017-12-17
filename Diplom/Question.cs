using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFPageSwitch
{
    class Question
    {
        public string Name { get; set; }
        public List<string> Answer { get; set; }
        public int CorrectAnswer { get; set; }

        public Question()
        {
            Name = "Вопрос";
            Answer = new List<string>();
            CorrectAnswer = 0;
        }
    }
}
