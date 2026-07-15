using EpsAdmissions.Application.DependencyInjection;
using EpsAdmissions.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

#endregion

var app = builder.Build();

#region Pipeline

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();