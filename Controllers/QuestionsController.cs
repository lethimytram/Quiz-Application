using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly QuizDatabase _context;

        public QuestionsController(QuizDatabase context)
        {
            _context = context;
        }

        [HttpGet("{quizId}")]
        public IActionResult GetQuestions(int quizId)
        {
            if (quizId <= 0)
                return BadRequest("Invalid quiz ID");

            var questions = _context.Questions
                .Where(q => q.QuizId == quizId)
                .Include(q => q.Options)
                .ToList();

            if (!questions.Any())
                return NotFound("No questions found for this quiz");

            return Ok(questions);
        }
    }
}