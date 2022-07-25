using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/date-conversion", ([FromQuery(Name = "date")] string date, [FromQuery(Name = "culture")] string culture) =>
{
    try
    {
        var dateResult = DateTime.Parse(date, CultureInfo.GetCultureInfo(culture), DateTimeStyles.None);
        return Results.Ok(new { DateParsed = dateResult });
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
})
.WithName("GetDateConversion")
.WithOpenApi();

app.Run();