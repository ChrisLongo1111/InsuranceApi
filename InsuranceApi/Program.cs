/// <summary>
/// https://christian-schou.dk/how-to-use-dapper-with-asp-net-core/
/// </summary>
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<Repository.IQuoteRepository, Repository.QuoteRepository>();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
