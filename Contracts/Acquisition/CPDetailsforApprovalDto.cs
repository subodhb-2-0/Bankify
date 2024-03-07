namespace Contracts.Acquisition
{
    public class CPDetailsforApprovalDto
    {
        public GetChannelPartnerDetailsDto getChannelPartnerDetails { get; set; }
    }
    public class BankingInfoDto
    {
        public string bankid { get; set; }
        public string bankName { get; set; }
        public string ifsc { get; set; }
        public string accountNumber { get; set; }
        public string accountHolderName { get; set; }
        public string cancelledchequeimage { get; set; }
        public string status { get; set; }
    }

    public class BusinessInfoDto
    {
        public string businessHouseNo { get; set; }
        public string businessRoad { get; set; }
        public string businessDist { get; set; }
        public string businessSubDst { get; set; }
        public string businessPincode { get; set; }
        public string businessLandmark { get; set; }
        public string businessShopImage { get; set; }
        public string aadharImage1 { get; set; }
        public string aadharImage2 { get; set; }
        public string aadharImage3 { get; set; }
        public string status { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
    }

    public class GetChannelPartnerDetailsDto
    {
        public StatusDto status { get; set; }
    }

    public class KycInfoDto
    {
        public string dob { get; set; }
        public string gender { get; set; }
        public string pan { get; set; }
        public string panStatu { get; set; }
        public string aadhaar { get; set; }
        public string aadhaarStatus { get; set; }
        public string nameEntered { get; set; }
        public string nameInPAN { get; set; }
        public string panImage { get; set; }
        public string nameInAadhaar { get; set; }
        public string aadharImage { get; set; }
        public PermanentAddressDto permanentAddress { get; set; }
        public BankingInfoDto bankingInfo { get; set; }
        public BusinessInfoDto businessInfo { get; set; }
    }

    public class OrganizationIfoDto
    {
        public string orgType { get; set; }
        public string organizationName { get; set; }
        public string orgCode { get; set; }
        public string ownerName { get; set; }
        public string mobile { get; set; }
        public string mobileStatus { get; set; }
        public string mailId { get; set; }
        public string mailStatus { get; set; }
        public string selfImage { get; set; }
        public string selfImageVerficationStatus { get; set; }
    }

    public class PermanentAddressDto
    {
        public string permHouseNo { get; set; }
        public string permDist { get; set; }
        public string permSubDst { get; set; }
        public string permPincode { get; set; }
        public string permLandmark { get; set; }
    }
    public class StatusDto
    {
        //public StatusDto status { get; set; }
        public string status { get; set; }
        public OrganizationIfoDto organizationIfo { get; set; }
        public KycInfoDto kycInfo { get; set; }
    }


}
