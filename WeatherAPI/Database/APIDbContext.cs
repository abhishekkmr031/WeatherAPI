using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeatherAPI.Models;

namespace WeatherAPI.Database
{
	public class APIDbContext : IdentityDbContext
	{
		public APIDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Quiz> quizzes { get; set; }
	}
}
