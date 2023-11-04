using JsonApiEndpoints;
using Microsoft.EntityFrameworkCore;
using SI_2.Client;
using SI_2.Entities;
using SI_2.Services.JsonApiControllerService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IJsonApiControllerService, JsonApiControllerService>();
builder.Services.AddScoped<JsonClient>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#region Connector Database migrations
app.UseDatabaseMigrations();
#endregion Connector Database migrations

app.Run();