using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly QuizDatabase _context;
          
        public ResultsController(QuizDatabase context)
        {
            _context = context;
        }

        [HttpGet("{sessionId}")]
        public IActionResult GetResults(int sessionId)
        {
            if (sessionId <= 0)
                return BadRequest("Invalid session ID");

            var session = _context.QuizSessions
                .Include(s => s.UserAnswers)
                .ThenInclude(ua => ua.Option)
                .FirstOrDefault(s => s.SessionId == sessionId);

            if (session == null) return NotFound("Session not found");

            var totalTime = (session.EndTime - session.StartTime).TotalMinutes;
            var correctAnswers = session.UserAnswers.Count(ua => ua.Option.IsCorrect);
            var incorrectAnswers = session.UserAnswers.Count(ua => !ua.Option.IsCorrect);
            var passed = session.Passed;

            return Ok(new
            {
                TotalTime = totalTime,
                CorrectAnswers = correctAnswers,
                IncorrectAnswers = incorrectAnswers,
                Passed = passed
            });
        }
    }
}
