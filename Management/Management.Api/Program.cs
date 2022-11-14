using Management.Model.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Management.Api.Startup;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices(builder);

var app = builder.Build();

app.ConfigureSwagger();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();