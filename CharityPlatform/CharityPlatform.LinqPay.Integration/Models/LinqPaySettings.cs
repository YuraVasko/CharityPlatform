namespace CharityPlatform.LinqPay.Integration.Models
{
    public class LinqPaySettings
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string Url { get; set; }

        public const string Version = "3";
    }
}
