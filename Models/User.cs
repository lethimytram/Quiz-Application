using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public ICollection<QuizSession> QuizSessions { get; set; }
    }
}