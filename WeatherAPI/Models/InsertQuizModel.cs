using System.Diagnostics.CodeAnalysis;

namespace WeatherAPI.Models
{
	public class InsertQuizModel
	{
		[NotNull]
		public string Topic { get; set; }

		[NotNull]
		public string Question { get; set; }
	}
}
