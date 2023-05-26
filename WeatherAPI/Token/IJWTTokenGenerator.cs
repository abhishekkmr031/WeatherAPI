using Microsoft.AspNetCore.Identity;

namespace WeatherAPI.Token
{
	public interface IJWTTokenGenerator
	{
		string GenerateToken(IdentityUser user);
	}
}