using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly QuizDatabase _context;

        public AnswersController(QuizDatabase context)
        {
            _context = context;
        }

        [HttpPost("validate")]
        public IActionResult ValidateAnswer([FromBody] UserAnswerRequest request)
        {
            if (request == null)
                return BadRequest("Request body is required");

            if (request.SessionId <= 0 || request.QuestionId <= 0 || request.OptionId <= 0)
                return BadRequest("Invalid request parameters");

            var option = _context.Options.Find(request.OptionId);
            if (option == null) 
                return NotFound("Option not found");

            var existingAnswer = _context.UserAnswers
                .FirstOrDefault(ua => ua.SessionId == request.SessionId && 
                                     ua.QuestionId == request.QuestionId);

            if (existingAnswer != null)
                return BadRequest("Question has already been answered in this session");

            var userAnswer = new api.Models.UserAnswer
            {
                SessionId = request.SessionId,
                QuestionId = request.QuestionId,
                OptionId = request.OptionId
            };
            _context.UserAnswers.Add(userAnswer);
            _context.SaveChanges();

            bool isCorrect = option.IsCorrect;
            return Ok(new { 
                IsCorrect = isCorrect, 
                Feedback = isCorrect ? "Correct!" : "Incorrect, try again." 
            });
        }
    }

    public class UserAnswerRequest
    {
        public int SessionId { get; set; }
        public int QuestionId { get; set; }
        public int OptionId { get; set; }
    }
}
