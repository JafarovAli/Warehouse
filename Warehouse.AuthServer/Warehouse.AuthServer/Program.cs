using Warehouse.AuthServer.Configurations;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddConfigureJWT();
builder.Services.AddConfigureAuthServices(builder.Configuration);
builder.Services.AddConfigureSwaggerServices();
builder.Services.AddConfigureDbServices(builder.Configuration);
builder.Services.AddConfigureServices(builder.Configuration);
builder.Services.AddMassTransit(builder.Configuration);
builder.Services.AddRedis(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MY API");
});
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
