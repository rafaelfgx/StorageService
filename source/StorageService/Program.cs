var builder = WebApplication.CreateBuilder();

var appSettings = builder.Configuration.Get<AppSettings>();

builder.Services.AddSingleton(appSettings);
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddResponseCompression();
builder.Services.AddControllers();

var application = builder.Build();

application.UseDeveloperExceptionPage();
application.UseHsts();
application.UseHttpsRedirection();
application.UseResponseCompression();
application.MapControllers();

application.Run();
