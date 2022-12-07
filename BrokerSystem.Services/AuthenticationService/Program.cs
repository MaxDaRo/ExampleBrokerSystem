using AuthenticationService.Common.Configuration;
using AuthenticationService.Common.Ioc;
using AuthenticationService.Middleware;

const string DefaultKey = "Default";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(c =>
{
    var settings = builder.Configuration.GetSection(CorsSettings.Key).Get<CorsSettings>();
    c.AddPolicy(DefaultKey,
        options => options
            .WithOrigins(settings.Origins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<SecuritySettings>(builder.Configuration.GetSection(SecuritySettings.Key));

builder.AddAppModules();

//TODO: remove hardcoded port initializaion
builder.WebHost.UseUrls("http://*:4000");

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors(DefaultKey);

//app.UseHttpsRedirection();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();

public partial class Program { }
