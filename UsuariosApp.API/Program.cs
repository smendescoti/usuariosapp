using UsuariosApp.API.Extensions;
using UsuariosApp.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerDoc();
builder.Services.AddEntityFramework(builder.Configuration);
builder.Services.AddServices(builder.Configuration);
builder.Services.AddJwtBearer(builder.Configuration);
builder.Services.AddAutoMapper();

var app = builder.Build();

app.UseSwaggerDoc();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();

app.Run();

public partial class Program { }