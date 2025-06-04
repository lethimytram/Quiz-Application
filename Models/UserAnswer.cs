using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class UserAnswer
    {
        public int SessionId { get; set; }
        public int QuestionId { get; set; }
        public int OptionId { get; set; }
        public QuizSession QuizSession { get; set; }
        public Question Question { get; set; }
        public Option Option { get; set; }
    }
}