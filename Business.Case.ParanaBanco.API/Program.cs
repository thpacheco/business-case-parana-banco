using Business.Case.ParanaBanco.API.Domain.Static;
using Business.Case.ParanaBanco.API.Infra.Context;
using Business.Case.ParanaBanco.API.Infra.DependencyInjection;
using Business.Case.ParanaBanco.API.Infra.Repositories.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

var _IsLocal = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

RuntimeConfig.ConnectStringSqlServer = configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDatabase();
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Business Case Paraná Banco - V1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
