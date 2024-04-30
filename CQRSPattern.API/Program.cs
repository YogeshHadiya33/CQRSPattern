using CQRSPattern.API.CustomInstaller;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//custom installers
builder.Services.InstallCustomService();
builder.Services.InstallDatabase(builder.Configuration);
builder.Services.ConfigureCaching(builder.Configuration);
builder.Services.InstallAuthenticationService(builder.Configuration);
builder.Services.InstallAssemblyDependentServices();
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();