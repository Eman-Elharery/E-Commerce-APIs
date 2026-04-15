using System.Text;
using CompanySystem.BLL;
using CompanySystem.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

namespace lab11
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddBLLServices();
            builder.Services.AddDALServices(builder.Configuration);


            var jwtSection = builder.Configuration.GetSection("JwtSettings");
            builder.Services.Configure<JwtSettings>(jwtSection);
            var jwtSettings = jwtSection.Get<JwtSettings>() ?? new JwtSettings();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Convert.FromBase64String(jwtSettings.SecretKey)), 
                        ClockSkew = TimeSpan.Zero
                    };
                });


            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthPolicies.AuthenticatedUser, policy =>
                    policy.RequireAuthenticatedUser());


                options.AddPolicy(AuthPolicies.AdminOnly, policy =>
                    policy.RequireRole("Admin"));


                options.AddPolicy(AuthPolicies.AdminOrUser, policy =>
                    policy.RequireRole("Admin", "User"));
            });


            var rootPath = builder.Environment.ContentRootPath;
            var staticFilePath = Path.Combine(rootPath, "Files");
            if (!Directory.Exists(staticFilePath))
                Directory.CreateDirectory(staticFilePath);

            builder.Services.Configure<StaticFileOptions>(cfg =>
            {
                cfg.FileProvider = new PhysicalFileProvider(staticFilePath);
                cfg.RequestPath = "/Files";
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseStaticFiles();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}