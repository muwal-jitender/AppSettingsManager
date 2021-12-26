namespace AppSettingsManager.Models
{
    public class TwilioSettings
    {
        public const string Twilio = "Twilio";
        public string AuthToken { get; set; }
        public string AccountSid { get; set; }
        public string PhoneNumber { get; set; }
    }
}
