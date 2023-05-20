using Foreca.App.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.ConfigureOptions(builder.Configuration);
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(configurePolicy =>
{
    configurePolicy.AllowAnyOrigin();
    configurePolicy.AllowAnyHeader();
    configurePolicy.AllowAnyMethod();
});

await app.RunAsync();

// "ConnectionStrings": {
//     "ForecaContext": "User ID=designer;Password=swordfish;Host=localhost;Port=5432;Database=ForecaApp;Pooling=true;"
// },
// "YandexWeatherOptions": {
//     "ApiKey": "2e3bd0d2-fd9f-4e83-b5a4-1e8cdeee96b0",
//     "BaseUrl": "https://api.weather.yandex.ru"
// },
// "YandexGeocoderOptions": {
//     "ApiKey": "0f3ea225-6599-4d0a-bf81-5631a173e858",
//     "BaseUrl": "https://geocode-maps.yandex.ru"
// },
// "UnsplashProviderOptions": {
//     "AccessKey": "ayOUzcvvgF6H5xPGMyLUNUtQ6A33K02eAxDAGa70zDc",
//     "BaseUrl": "https://api.unsplash.com"
// },