using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherAPI.Database;
using WeatherAPI.Models;

namespace WeatherAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class QuizController : Controller
	{
		private APIDbContext _dbContext { get; set; }

		public QuizController(APIDbContext aPIDbContext)
		{
			_dbContext = aPIDbContext;

		}

		[HttpGet]
		public async Task<IActionResult> GetQuestion()
		{
			return Ok(await _dbContext.quizzes.ToListAsync());
		}

		[HttpPost]
		public async Task<IActionResult> InsertQuiz(InsertQuizModel insertQuizModel)
		{
			Quiz quiz = new Quiz
			{
				Topic = insertQuizModel.Topic,
				Question = insertQuizModel.Question
			};
			await _dbContext.quizzes.AddAsync(quiz);
			await _dbContext.SaveChangesAsync();

			return Ok(quiz);
		}
	}
}
