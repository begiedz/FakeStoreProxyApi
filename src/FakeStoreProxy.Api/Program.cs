using FakeStoreProxyApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var baseUrl = builder.Configuration.GetValue<string?>("FakeStore:BaseUrl")
    ?? "https://fakestoreapi.com/";
var timeoutSeconds = builder.Configuration.GetValue<int?>("FakeStore:TimeoutSeconds")
    ?? 10;

builder.Services.AddHttpClient<ProductsService>(client =>
{
    client.BaseAddress = new Uri(baseUrl!);
    client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);

});


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Swagger UI enabled in production for presentation reasons.
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
