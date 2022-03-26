using Microsoft.AspNetCore;
using Microsoft.OpenApi.Models;
using Otus.SocialNetwork;
using Otus.SocialNetwork.Application;
using Otus.SocialNetwork.Infrastructure.Authorization;
using Otus.SocialNetwork.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddPresentationModule(builder.Configuration)
    .AddApplicationModule()
    .AddPersistenceModule();

builder.Services.AddSwaggerGen(opts =>
{
    opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Type = SecuritySchemeType.ApiKey
    });

    opts.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

const string allowSpecificOrigins = "_allowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: allowSpecificOrigins,
        cors =>
        {
            cors.WithOrigins("http://localhost:5077")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<CustomAuthorizationMiddleware>();

app.UseRouting();

app.UseCors(allowSpecificOrigins);

app.UseEndpoints(
    endpoints => { endpoints.MapControllers(); });

await app.RunAsync();