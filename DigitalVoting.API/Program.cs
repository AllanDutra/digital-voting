using DigitalVoting.API.Extensions;
using DigitalVoting.API.Filters;
using DigitalVoting.API.Middlewares;
using DigitalVoting.Application.Commands.SignUp;
using DigitalVoting.Application.Validators;
using DigitalVoting.Infrastructure.Persistence;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#pragma warning disable CS0618
builder.Services
    .AddControllers(options => options.Filters.Add(typeof(ValidationFilter)))
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    })
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SignUpCommandValidator>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();

builder.Services.AddMiddlewares();

builder.Services.AddMediatR(typeof(SignUpCommand));

builder.Services.AddDependencyInjection();

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddHttpContextAccessor();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
