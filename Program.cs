using System.Globalization;
using Microsoft.AspNetCore.Localization;
using RangeDemo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new FractionJsonConverter()));
builder.Services.AddRequestLocalization(options => options.DefaultRequestCulture = new RequestCulture(CultureInfo.InvariantCulture));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRequestLocalization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
