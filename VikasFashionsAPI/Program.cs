using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VikasFashionsAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Logging.AddFile("Logs/VikasFashionLog-{Date}.txt");

builder.Services.AddControllers();
builder.Services.AddCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContextVikasFashion>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection"))
);

builder.Services.AddScoped<VikasFashionsAPI.APIServices.CountryService.ICountryService, VikasFashionsAPI.APIServices.CountryService.CountryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{

    // Do not add exception handler for dev environment. In dev,
    // we get the developer exception page with detailed error info.

    app.UseExceptionHandler(errorApp =>
    {
        // Logs unhandled exceptions. For more information about all the
        // different possibilities for how to handle errors see
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-5.0
        errorApp.Run(async context =>
        {
            // Return machine-readable problem details. See RFC 7807 for details.
            // https://datatracker.ietf.org/doc/html/rfc7807#page-6
            var pd = new ProblemDetails
            {
                Type = "https://google.com/",
                Title = "An unrecoverable error occurred",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "This is a crash, but dont worry we are working on this.",
            };
            pd.Extensions.Add("RequestId", context.TraceIdentifier);
            await context.Response.WriteAsJsonAsync(pd, pd.GetType(), null, contentType: "application/problem+json");
        });
    });
}


app.UseCors(builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
