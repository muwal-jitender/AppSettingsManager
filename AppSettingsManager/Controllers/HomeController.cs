using AppSettingsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace AppSettingsManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IOptions<TwilioSettings> _twilioOption;
        private readonly TwilioSettings _twilioSettings;
        private readonly SocialLoginSettings _socialLoginSettings;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration,
           IOptions<TwilioSettings> twilioOption, TwilioSettings twilioSettings, SocialLoginSettings socialLoginSettings)
        {
            _logger = logger;
            _configuration = configuration;
            _twilioOption = twilioOption;
            _twilioSettings = twilioSettings;
            _socialLoginSettings = socialLoginSettings;
            // _twilioSettings = new();
            // configuration.GetSection("Twilio").Bind(_twilioSettings);
        }

        public IActionResult Index()
        {
            ViewBag.SendGridKey = _configuration.GetValue<string>("SendGridKey");
            //ViewBag.AuthToken = _configuration.GetSection("Twilio").GetValue<string>("AuthToken");
            //ViewBag.AccountSid = _configuration.GetValue<string>("Twilio:AccountSid");
            //ViewBag.PhoneNumber = _twilioSettings.PhoneNumber; 

            // IOption
            //ViewBag.AuthToken = _twilioOption.Value.AuthToken;
            //ViewBag.AccountSid = _twilioOption.Value.AccountSid;
            //ViewBag.PhoneNumber = _twilioOption.Value.PhoneNumber;  

            ViewBag.AuthToken = _twilioSettings.AuthToken;
            ViewBag.AccountSid = _twilioSettings.AccountSid;
            ViewBag.PhoneNumber = _twilioSettings.PhoneNumber;

            ViewBag.FacebookKey = _socialLoginSettings.FacebookSettings.Key;
            ViewBag.GoogleKey = _socialLoginSettings.GoogleSettings.Key;
            ViewBag.ConnectionString = _configuration.GetConnectionString("AppSettingsManagerDb");
            //  ViewBag.AccountSid = _configuration.GetValue<string>("FirstLevel:SecondLevel:BottomLevel");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}