using FakeStoreProxy.Api.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var baseUrl = builder.Configuration.GetValue<string?>("FakeStore:BaseUrl")
    ?? "https://fakestoreapi.com/";
var timeoutSeconds = builder.Configuration.GetValue<int?>("FakeStore:TimeoutSeconds")
    ?? 10;

builder.Services.AddHttpClient<IProductsService, ProductsService>(client =>
{
    client.BaseAddress = new Uri(baseUrl);
    client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);

});


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Pulls /// XML docs into Swagger
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();


// Swagger UI enabled in production for presentation reasons.
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
