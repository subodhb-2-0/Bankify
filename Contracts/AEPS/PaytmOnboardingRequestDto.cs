namespace Contracts.AEPS
{
    public class PaytmOnboardingRequestDto
    {
        public int SupplierId { get; set; }
        public string SupplierCspId { get; set; }
        public int OnboardingStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int OrgId { get; set; }
        public int ServiceId { get; set; }
        public string RefParam1 { get; set; }
        public string RefParam2 { get; set; }
        public string RefParam3 { get; set; }
        public string OrgCode { get; set; }
        public string OrgName { get; set; }
        public string TerminalId { get; set; }
        public string? Remarks { get; set; }

        public PaytmOnboardingPacket? PaytmOnboardingPacket { get; set; }

        public AgentOnboardingRequestDto? AgentOnboardingRequestDto { get; set; }

        public PaySprintOnboardingDetailsDto GetPaySprintOnboardingInput()
        {
            return new PaySprintOnboardingDetailsDto()
            {
                OrgCode = OrgCode,
                SupplierId = SupplierId,
                Bank = RefParam1
            };  
        }
    }


    public class PaytmOnboardingPacket
    {
        public string merchantmobilenumber { get; set; }

        public double latitude {  get; set; }
        public double longitude {  get; set; }
        public string submerchantid {  get; set; }
        public string statecode {  get; set; }
        public string city {  get; set; }
        public string merchant_name {  get; set; }
        public string address {  get; set; }
        public string pannumber {  get; set; }
        public string pincode {  get; set; }
    }

    public class PaytmOnboardingResponseDto
    {
        public int response_code { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
        public string error_code { get; set; }
    }
}
