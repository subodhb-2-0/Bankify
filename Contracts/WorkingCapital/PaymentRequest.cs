namespace Contracts.WorkingCapital
{
    public class PaymentRequest: PaymentDto
    {
        public int paymentModeId { get; set; }
        public string hoBankId { get; set; }
        public string hoBankAccount { get; set; }
        public string pgId { get; set; }
    }
}