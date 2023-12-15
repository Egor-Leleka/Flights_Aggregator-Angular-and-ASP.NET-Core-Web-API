using FlightsAggregator.DataAccess.Data;
using FlightsAggregator.DataAccess.Repositories.IRepositories;
using FlightsAggregator.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Flights")));

// Added
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowSpecificOrigin",
		builder => builder.WithOrigins("http://localhost:4200")
						  .AllowAnyMethod()
						  .AllowAnyHeader());
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");// Added

app.UseAuthorization();

app.MapControllers();

app.Run();
