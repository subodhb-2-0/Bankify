namespace Contracts.WorkingCapital
{
    public class PaymentResponseDTO
    {
        public int paymentModelId { get; set; }
        public string paymentModee { get; set; }
        public PaymentStatus paymentStatus { get; set; }
        public string paymentMode { get; set; }
    }
}