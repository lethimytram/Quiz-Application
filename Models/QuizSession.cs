using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class QuizSession
    {
        [Key]
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public int QuizId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalCorrect { get; set; }
        public bool Passed { get; set; }
        public User User { get; set; }
        public Quiz Quiz { get; set; }
        public ICollection<UserAnswer> UserAnswers { get; set; }
    }
}