
namespace Contracts.AEPS
{
    public class PaySprintOnboardingDetailsDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndtDate { get; set; }
        public string OrgCode { get; set; }
        public int SupplierId { get; set; }
        public string Bank { get; set; }
        public string? SearchBy { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? Status { get; set; }
        
    }


    public class PaySprintOnboardingDetails
    {
        
        public int? totalcount { get; set; }
        public string? orgcode { get; set; }
        public string? orgname { get; set; }
        public string? merchantname { get; set; }
        public string? mobilenumber { get; set; }
        public string? pancard { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? address { get; set; }
        public DateTime? creationdate { get; set; }
        public int onboardstatus { get; set; }
        public int? supplierid { get; set; }
        public int? orgid { get; set; }
        public string? ref_param1 { get; set; }
        public string? ref_param2 { get; set; }
        public string? ref_param3 { get; set; }
        public string? Remarks { get; set; }

        public async Task<PayTmOnboardingStatusCheckPacket> PreparePayTmOnboardingStatusCheckPacket()
        {
            return await Task.FromResult(new PayTmOnboardingStatusCheckPacket()
            {
                Mobile = mobilenumber ?? string.Empty,
                Pipe = "Bank5", // this pipe needs to dynamic.
                MerchantCode = orgcode ?? string.Empty
            });
        }
    }

    public class PayTmOnboardingStatusCheckPacket
    {
        public string Mobile { get; set; }
        public string Pipe { get; set; }
        public string MerchantCode { get; set; }
    }




}
