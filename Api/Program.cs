using Api.Components;
using Api.Routes;
using Application;
using Infraestructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using System.Text;




    var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

    try
    {

        var builder = WebApplication.CreateBuilder(args);

        IConfiguration configuration = builder.Configuration;

        builder.Services.AddApplication();

        builder.Services.AddInfrastructure(configuration);

        builder.Services.AddScoped<RouterBase, AuthRouter>();
        builder.Services.AddScoped<RouterBase, StoriesRouter>();

        builder.Logging.ClearProviders();
        builder.Host.UseNLog();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "_myAllowSpecificOrigins",
                              builder =>
                              {
                                    builder.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                              });
        });


    // Add services to the container.
    string hackerNewsUrl = configuration["hackernews:Uri"] ?? string.Empty;
        string serverSigningPassword = configuration["JwtSettings:serverSigningPassword"] ?? string.Empty;

        if (!string.IsNullOrEmpty(hackerNewsUrl))
            builder.Services.AddHttpClient("hackernews", client =>
            {
                client.BaseAddress = new Uri(hackerNewsUrl);
            });
        else
            throw new Exception("HackersNews not configured");

        if (!string.IsNullOrEmpty(serverSigningPassword))
            builder.Services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidIssuer = "nexttech",
                    ValidateIssuerSigningKey = true,    
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(serverSigningPassword)),
                    ValidateLifetime = true
                };
            });
        else
            throw new Exception("JwtSettings not configured");

        builder.Services.AddAuthorization();


        var securityScheme = new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JSON Web Token based security",
            };

        var securityReq = new OpenApiSecurityRequirement()
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
                         new string[] {}
                        }
                    };

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(o =>
        {
            o.AddSecurityDefinition("Bearer", securityScheme);
            o.AddSecurityRequirement(securityReq);
        });

        builder.Services.AddMemoryCache();


    var app = builder.Build();


        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseCustomExceptionHandler();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("_myAllowSpecificOrigins");

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider.GetServices<RouterBase>();
            foreach (var item in services)
                item.AddRoutes(app);

            app.Run();
        }

    }
    catch (Exception exception)
    {
        logger.Error(exception, "Stopped program because of exception");
        throw;
    }
    finally
    {
        NLog.LogManager.Shutdown();
    }

