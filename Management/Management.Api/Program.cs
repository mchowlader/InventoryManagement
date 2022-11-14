using Management.Model.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Management.Api.Startup;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager Configuration = builder.Configuration;
(string? connectionString, string? migrationAssemblyName) GetConnectionStringAndAssemblyName()
{
    var connectionStringName = "DefaultConnection";
    var connectionString = Configuration.GetConnectionString(connectionStringName);
    var migrationAssemblyName = typeof(Program).Assembly.FullName;
    return (connectionString, migrationAssemblyName);
}
builder.Services.RegisterServices(builder);

var app = builder.Build();

app.ConfigureSwagger();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
