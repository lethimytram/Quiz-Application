using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int QuizId { get; set; }
        public string Content { get; set; }
        public Quiz Quiz { get; set; }
        public ICollection<Option> Options { get; set; }
        public ICollection<UserAnswer> UserAnswers { get; set; }
    }
}