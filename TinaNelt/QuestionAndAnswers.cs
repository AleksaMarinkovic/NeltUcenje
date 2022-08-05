using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinaNelt
{
    public class QuestionAndAnswers
    {
        private string question;
        private string[] wrongAnswers;
        private string rightAnswer;

        public string Question { get; set; }
        public string[] WrongAnswers { get; set; }
        public string RightAnswer { get; set; }

        public QuestionAndAnswers(string question, string[] wrongAnswers, string rightAnswer)
        {
            Question = question;
            WrongAnswers = wrongAnswers;
            RightAnswer = rightAnswer;
        }
        public QuestionAndAnswers()
        {
            Question = "";
            WrongAnswers = new string[] { "", "" };
            RightAnswer = "";
        }
    }
}
