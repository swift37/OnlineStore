using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using OnlineStore.Application;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Mapping;
using OnlineStore.DAL;
using OnlineStore.DAL.Context;
using OnlineStore.Identity;
using OnlineStore.Identity.Context;
using OnlineStore.WebAPI.Middleware;
using OnlineStore.WebAPI.OptionsSetup;
using Stripe;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

// Add services to the container.
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IApplicationDbContext).Assembly));
});

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddApplication();

builder.Services.AddIdentity(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddVersionedApiExplorer(opt => opt.GroupNameFormat = "'v'VVV");

builder.Services.ConfigureOptions<SwaggerGenOptionsSetup>();

builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning();

var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var identityContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
        IdentityDbInitializer.Initialize(identityContext);

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception exception)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, "An error occurred during app initialization.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var desc in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{desc.GroupName}/swagger.json",
                desc.GroupName.ToUpperInvariant());
        }
        options.RoutePrefix = string.Empty;
    });
}

app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseApiVersioning();

app.MapControllers();

app.Run();
