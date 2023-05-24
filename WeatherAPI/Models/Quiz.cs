using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WeatherAPI.Models
{
	public class Quiz
	{
		[Key]
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[NotNull]
		public string Topic { get; set; }
		[NotNull]
		public string Question { get; set; }
	}
}
