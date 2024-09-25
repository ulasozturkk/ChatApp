using ChatApp.SignalR.Hubs;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x => {
  x.UsingRabbitMq((context, cfg) => {
    cfg.Host("amqp://localhost:5672", host => {
      host.Username("guest");
      host.Password("guest");
    });
  });
});

builder.Services.AddCors(opt => opt.AddDefaultPolicy(policy => policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(origin => true)));

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints => {
  endpoints.MapHub<ChatHub>("/chathub");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
