
using ChatApp.MobileAPI.Consumers;
using ChatApp.MobileAPI.Database;
using ChatApp.Utils.Security.JWT;
using ChatApp.Utils.Security.Services.Abstract;
using ChatApp.Utils.Security.Services.Concrete;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<MessageDbContext>(opt => {
  opt.UseSqlServer(builder.Configuration.GetConnectionString("defaultconnection"), config => {
    config.MigrationsAssembly("ChatApp.MobileAPI");
  });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMassTransit(x => {
  x.AddConsumer<SaveMessageConsumer>();

  x.UsingRabbitMq((context, cfg) => {
    cfg.ReceiveEndpoint("save-message-queue", e => {
      e.ConfigureConsumer<SaveMessageConsumer>(context);
    });
  });
});

builder.Services.AddSwaggerGen(c => {
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mobile API", Version = "v1" });

  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
    Description = @"JWT Token giriniz",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer"
  });

  c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
    {
      new OpenApiSecurityScheme {
        Reference = new OpenApiReference {
          Type = ReferenceType.SecurityScheme,
          Id = "Bearer"
        },
        Scheme = "oauth2",
        Name = "Bearer",
        In = ParameterLocation.Header,
      },
      new List<string> ()
    }
  });
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IJwtTokenParse, JwtTokenParse>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.Configure<ChatApp.Utils.Security.JWT.JWTSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddHttpClient();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
  options.TokenValidationParameters = new TokenValidationParameters {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["JWT:issuer"],
    ValidAudience = builder.Configuration["JWT:audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"] ?? "ERROR"))
  };
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName.ToLower() == "spexcotest" || app.Environment.EnvironmentName.ToLower() == "staging" || app.Environment.EnvironmentName.ToLower() == "productiondebug") {
  app.UseSwagger();
  app.UseSwaggerUI(c => {
    c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", "API v1");
    c.RoutePrefix = string.Empty;
  });
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
