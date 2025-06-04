using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public string Title { get; set; }
        public int PassingCriteria { get; set; } 
        public ICollection<Question> Questions { get; set; }
        public ICollection<QuizSession> QuizSessions { get; set; }
    }
}
