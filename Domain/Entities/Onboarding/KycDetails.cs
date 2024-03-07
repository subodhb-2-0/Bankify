using Domain.Entities.Common;

namespace Domain.Entities.Onboarding
{
    public class KycDetails : BaseEntity
    {
        public int org_kyc_details_Id { get; set; }
        public int orgId { get; set; }
        public int bankId { get; set; }
        public string ifsccode { get; set; }
        public string accountType { get; set; }
        public string accountNumber { get; set; }
        public string panCard { get; set; }
        public string panCardName { get; set; }
        public string aadhaarCard { get; set; }
        public string aadhaarCardName { get; set; }
        public string approvalDocketName { get; set; }
        public string approvalDocketNumber { get; set; }
        public int kyc_status { get; set; }
        public string aggrementCopy { get; set; }
        public string cancelledChequeImage { get; set; }
        public string shopImage { get; set; }
        public string selfImage { get; set; }
        public int onbooardStage { get; set; }
    }
}