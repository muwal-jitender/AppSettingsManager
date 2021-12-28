using AppSettingsManager.Models;
using AppSettingsManager;
var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, builder) =>
{
    builder.Sources.Clear();
    builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);    
    builder.AddJsonFile($"appsettings{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
    builder.AddJsonFile("customJson.json", optional: true, reloadOnChange: true);
    if (hostingContext.HostingEnvironment.IsDevelopment())
    {
        builder.AddUserSecrets<Program>();
    }    
    builder.AddEnvironmentVariables();
    builder.AddCommandLine(args);
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection(TwilioSettings.Twilio));

builder.Services.AddConfiguration<TwilioSettings>(builder.Configuration, TwilioSettings.Twilio);
builder.Services.AddConfiguration<SocialLoginSettings>(builder.Configuration, SocialLoginSettings.SectionName);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
