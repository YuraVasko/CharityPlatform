namespace CharityPlatform.LinqPay.Integration.Models
{
    public class GetPaymentFormRequestModel
    {
        public string OrderId { get; set; }
        public string Description { get; set; } 
        public string Currency { get; set; } // UAH
        public int Amount { get; set; } // 150
        public string Action { get; set; } // pay, subscribe, hold
    }
}
