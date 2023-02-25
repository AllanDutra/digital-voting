using DigitalVoting.API.Extensions;
using DigitalVoting.API.Middlewares;
using DigitalVoting.Application.Commands.SignUp;
using DigitalVoting.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();

builder.Services.AddMiddlewares();

builder.Services.AddMediatR(typeof(SignUpCommand));

builder.Services.AddDependencyInjection();

var connectionString = builder.Configuration.GetConnectionString("DigitalVotingCs");

builder.Services.AddDbContext<DigitalVotingDbContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // ? REMOVEM O "/swagger":
        c.RoutePrefix = string.Empty;
        c.SwaggerEndpoint("./swagger/v1/swagger.json", "DigitalVoting.API");
    });
}

app.UseMiddleware<GlobalExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
