using ProjetoRabbitMQ.Controllers;
using ProjetoRabbitMQ.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddMediatRConfiguration();
builder.Services.AddDependencyInjectionForServices();

builder.Services.AddRabbitMQService(builder.Configuration);
builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
