using ProjetoRabbitMQ.Controllers;
using ProjetoRabbitMQ.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddRabbitMQService();
builder.AddDatabaseContext();
builder.AddJwtConfiguration();
builder.Services.AddMediatRConfiguration();
builder.Services.AddDependencyInjectionForServices();
builder.Host.AddSerilogConfiguration();

var app = builder.Build();

app.AddEndpoints();
app.EnsureDatabaseCreated();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
