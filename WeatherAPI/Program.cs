using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WeatherAPI.Database;
using WeatherAPI.Token;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IJWTTokenGenerator, JWTTokenGenerator>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<APIDbContext>(options =>
		options.UseSqlServer(builder.Configuration.GetConnectionString("ApiConnectionStringDev")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(
	opt =>
	{
		opt.Password.RequireDigit = false;
		opt.Password.RequiredLength = 4;
		opt.Password.RequiredUniqueChars = 1;
		opt.Password.RequireLowercase = false;
		opt.Password.RequireNonAlphanumeric = false;
		opt.Password.RequireUppercase = false;
	}).
	AddEntityFrameworkStores<APIDbContext>();

builder.Services.AddAuthentication(o =>
{
	o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
	o.RequireHttpsMetadata = false;
	o.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = false,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"])),
		ValidIssuer = builder.Configuration["Token:Issuer"],
		ValidateIssuer = true,
		ValidateAudience = false
	};
});


var app = builder.Build();
app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{	
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
