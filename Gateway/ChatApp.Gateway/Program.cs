

using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);



builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
   .AddJsonFile("configuration.json", optional: false, reloadOnChange: false).AddEnvironmentVariables();


builder.Services.AddOcelot();
builder.Services.AddControllers();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



app.UseCors(x => x
         .AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader());
app.MapControllers();
if (app.Environment.IsDevelopment()) {
  await app.UseSwaggerForOcelotUI(opt => { opt.DownstreamSwaggerEndPointBasePath = "/swagger/docs"; }).UseOcelot();
} else {
  await app.UseOcelot();
}


app.Run();
