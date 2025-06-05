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
        public IActionResult GetQuestions(int quizId, [FromServices] ILogger<QuestionsController> logger)
        {
            logger.LogInformation($"Fetching questions for quiz ID {quizId}");
            if (quizId <= 0)
            {
                logger.LogWarning("Invalid quiz ID provided: {quizId}", quizId);
                return BadRequest("Invalid quiz ID");
            }

            var quiz = _context.Quizzes
                .Where(q => q.QuizId == quizId)
                .Select(q => new { q.PassingCriteria })
                .FirstOrDefault();

            if (quiz == null)
            {
                logger.LogWarning("Quiz ID {quizId} does not exist", quizId);
                return NotFound("Quiz does not exist");
            }

            var questions = _context.Questions
                .Where(q => q.QuizId == quizId)
                .Include(q => q.Options)
                .Select(q => new
                {
                    QuestionID = q.QuestionId,
                    QuizID = q.QuizId,
                    PassingCriteria = quiz.PassingCriteria, 
                    Content = q.Content,
                    Options = q.Options.Select(o => new { o.QuestionId, o.Content }).ToList() 
                })
                .ToList();
            var invalidQuestions = questions.Where(q => q.Options.Count != 4).ToList();
            if (invalidQuestions.Any())
            {
                logger.LogWarning("Some questions for quiz ID {quizId} do not have exactly 4 options", quizId);
                return BadRequest("Some questions do not have exactly 4 options");
            }
            if (!questions.Any())
            {
                logger.LogInformation("No questions found for quiz ID {quizId}", quizId);
                return NotFound("No questions found for this quiz");
            }

            logger.LogInformation("Successfully fetched {count} questions for quiz ID {quizId}", questions.Count, quizId);
            return Ok(questions);
        }
    }
}
