using CharityPlatform.LinqPay.Integration.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CharityPlatform.LinqPay.Integration
{
    public class LiqPayProvider : ILiqPayClient
    {
        private readonly LinqPaySettings _settings;

        public LiqPayProvider(LinqPaySettings settings)
        { 
            _settings = settings;
        }

        public async Task<PaymentFormDetails> GetPaymentForm(GetPaymentFormRequestModel request) 
        {
            var serializedContent = JsonConvert.SerializeObject(new
            {
                public_key = _settings.PublicKey,
                version = LinqPaySettings.Version,
                action = request.Action,
                amount = request.Amount,
                currency = request.Currency,
                description = request.Description,
                order_id = request.OrderId,
                paytypes = "card"
            });

            var plainTextBytes = Encoding.UTF8.GetBytes(serializedContent);
            var data = Convert.ToBase64String(plainTextBytes);

            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var signString = _settings.PrivateKey + data + _settings.PrivateKey;
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(signString));
                var signature = Convert.ToBase64String(hash);

                var postData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("data", data),
                    new KeyValuePair<string, string>("signature", signature)
                };

                return new PaymentFormDetails
                {
                    Data = data,
                    Signature = signature
                };
            }
        }
    }
}
