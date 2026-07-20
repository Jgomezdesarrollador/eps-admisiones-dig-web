using EpsAdmissions.Application.DependencyInjection;
using EpsAdmissions.Infrastructure.DependencyInjection;
using EpsAdmissions.Infrastructure.SignalR;

var builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddSignalR();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:7172")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

#endregion

var app = builder.Build();

#region Pipeline

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("BlazorPolicy");

app.UseAuthorization();

app.MapControllers();

app.MapHub<AdmissionHub>("/hubs/admissions");

#endregion

app.Run();