using Microsoft.EntityFrameworkCore;
using WeatherAPI.Database;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();

builder.Services.AddDbContext<APIDbContext>(options =>
		options.UseSqlServer(builder.Configuration.GetConnectionString("ApiConnectionStringDev")));

//if (app.Environment.IsDevelopment())
//{
//	builder.Services.AddDbContext<APIDbContext>(options =>
//		options.UseSqlServer(builder.Configuration.GetConnectionString("ApiConnectionStringDev")));
//}
//else
//{
//	builder.Services.AddDbContext<APIDbContext>(options =>
//		options.UseSqlServer(builder.Configuration.GetConnectionString("ApiConnectionString")));

//	//builder.Services.BuildServiceProvider().GetService<APIDbContext>().Database.Migrate();
//}

app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{	
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
