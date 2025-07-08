using API.Configurations;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureOptions()
       .ConfigureLogger()
       .AddApplication()
       .AddPersistence()
       .AddInfrastructure()
       .ConfigureAPI()
       .AddAutoMapper();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTokenValidation();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers().RequireAuthorization();

app.Run();
