using Action.Web.Backoffice.Api.Extensions;
using Fortes.Web.Challenge.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigureMapping();
builder.Services.AddContext(builder.Configuration);

builder.Services.RegisterServicesDependencies();
builder.Services.RegisterRepositoriesDependencies();
builder.Services.RegisterValidatorsDependencies();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
                     builder =>
                     {
                         builder.WithOrigins("http://localhost:5173")
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                     });
});

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

app.UseAuthorization();

app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();
