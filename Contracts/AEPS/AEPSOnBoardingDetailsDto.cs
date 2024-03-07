using Contracts.Response;

namespace Contracts.AEPS
{
    public class AEPSOnBoardingDetailsDto
    {
        public string orgCode { get; set; }
        public string orgName { get; set; }
        public string supplier_csp_id { get; set; }
        public int onboarding_status { get; set; }
        public string refparam1 { get; set; }
        public string refparam2 { get; set; }
    }

    public class AEPSOnBoardingDetailsResponse : PaginationResponseModel
    {
        public List<AEPSOnBoardingDetailsDto> aEPSOnBoardingDetailsDtos { get; set; }
    }
}
