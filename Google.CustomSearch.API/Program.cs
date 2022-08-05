using Google.CustomSearch.API.Configuration;
using Google.CustomSearch.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Setup Google Custom Search API
builder.Services.Configure<GoogleCustomSearchApiConfiguration>(builder.Configuration.GetSection(GoogleCustomSearchApiConfiguration.Name));
builder.Services.AddTransient<IGoogleCustomSearchApiService, GoogleCustomSearchApiService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/search", async (string searchPhrase, IGoogleCustomSearchApiService service) =>
{
    return await service.SearchAsync(searchPhrase);
})
.WithName("GetSearchResults");

app.Run();