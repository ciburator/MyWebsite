using System.Text;
using DataAccess;
using DataAccess.Interfaces;
using Home_API;
using Home_API.Common;
using Home_API.Common.Configuration;
using Home_API.Interfaces;
using Home_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    var services = builder.Services;

    services.AddCors();

    builder.Logging.ClearProviders();

    var logger = new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger();

    builder.Logging.AddSerilog(logger);
    builder.Host.UseSerilog();

    builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

    services.AddControllers();

    services.AddDbContext<IMainContext, MainContext>(options =>
    {
        options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")!);
    });

    services.AddScoped<IUserService, UserService>();

    services.AddAutoMapper(typeof(MapperConfig));

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddAuthentication(config =>
    {
        config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(config =>
    {
        var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value!);
        config.RequireHttpsMetadata = false;
        config.SaveToken = true;
        config.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

    services.AddAuthorization(options =>
    {
        options.AddPolicy("manager", policy => policy.RequireRole("manager"));
        options.AddPolicy("operator", policy => policy.RequireRole("operator"));
    });
}

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
