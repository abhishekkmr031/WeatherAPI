using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Models;
using WeatherAPI.Token;

namespace WeatherAPI.Controllers
{
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJWTTokenGenerator _jWTTokenGenerator;

		public IdentityController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, 
            IJWTTokenGenerator jWTTokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jWTTokenGenerator = jWTTokenGenerator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(CreateUser createUser)
        {
            var userToCreate = new IdentityUser
            {
                Email = createUser.EmailId,
                UserName = createUser.UserName
            };

            var result = await _userManager.CreateAsync(userToCreate, createUser.Password);

            if (result.Succeeded) return Ok(result);

            return BadRequest(result);
        }

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginUser loginUser)
		{
            var userFromDB = await _userManager.FindByNameAsync(loginUser.UserName);

            if (userFromDB.UserName == null) return BadRequest();

            var result = await _signInManager.CheckPasswordSignInAsync(userFromDB, loginUser.Password, false);

            if (!result.Succeeded) return BadRequest();

            return Ok(new
            {
                result = result,
                username = userFromDB.UserName,
                email = userFromDB.Email,
                token = _jWTTokenGenerator.GenerateToken(userFromDB)
            }) ;
		}
	}
}
