using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RangeDemo;
using Swashbuckle.AspNetCore.SwaggerGen;

#if PARSE_RANGE_CONSTRAINTS_USING_CURRENT_CULTURE
var defaultRequestCulture = new RequestCulture("de-DE");
#else
var defaultRequestCulture = new RequestCulture(System.Globalization.CultureInfo.InvariantCulture);
#endif

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new FractionJsonConverter()));
builder.Services.AddRequestLocalization(options =>
{
    options.DefaultRequestCulture = defaultRequestCulture;
    options.AddSupportedCultures("de-DE");
});
builder.Services.AddSingleton<ISerializerDataContractResolver>(serviceProvider =>
{
    // Ensure that properties of type Fraction are considered a primitive type, so [Range] properties are applied in OAS document.
    var jsonOptions = serviceProvider.GetRequiredService<IOptions<JsonOptions>>().Value;
    return new FractionAwareSerializerDataContractResolver(jsonOptions.JsonSerializerOptions);
});
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRequestLocalization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
