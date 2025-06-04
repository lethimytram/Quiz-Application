using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    public class QuizDatabase : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<QuizSession> QuizSessions { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }

        public QuizDatabase(DbContextOptions<QuizDatabase> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAnswer>()
                .HasKey(ua => new { ua.SessionId, ua.QuestionId });

            modelBuilder.Entity<QuizSession>()
                .HasOne(qs => qs.User)
                .WithMany(u => u.QuizSessions)
                .HasForeignKey(qs => qs.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuizSession>()
                .HasOne(qs => qs.Quiz)
                .WithMany(q => q.QuizSessions)
                .HasForeignKey(qs => qs.QuizId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Quiz)
                .WithMany(qz => qz.Questions)
                .HasForeignKey(q => q.QuizId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Option>()
                .HasOne(o => o.Question)
                .WithMany(q => q.Options)
                .HasForeignKey(o => o.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAnswer>()
                .HasOne(ua => ua.QuizSession)
                .WithMany(qs => qs.UserAnswers)
                .HasForeignKey(ua => ua.SessionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAnswer>()
                .HasOne(ua => ua.Question)
                .WithMany(q => q.UserAnswers)
                .HasForeignKey(ua => ua.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAnswer>()
                .HasOne(ua => ua.Option)
                .WithMany(o => o.UserAnswers)
                .HasForeignKey(ua => ua.OptionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}