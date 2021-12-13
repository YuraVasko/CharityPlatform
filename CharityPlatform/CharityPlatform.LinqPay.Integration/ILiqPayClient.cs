using CharityPlatform.LinqPay.Integration.Models;
using System.Threading.Tasks;

namespace CharityPlatform.LinqPay.Integration
{
    public interface ILiqPayClient
    {
        Task<PaymentFormDetails> GetPaymentForm(GetPaymentFormRequestModel request);
    }
}
