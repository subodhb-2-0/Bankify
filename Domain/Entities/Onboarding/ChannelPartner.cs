using Domain.Entities.Common;

namespace Domain.Entities.Onboarding
{
    public class ChannelPartner : BaseEntity
    {
        public int orgId { get; set; }
        public string orgCode { get; set; }
        public string orgName { get; set; }
        public int orgType { get; set; }
        public int parentOrgId { get; set; }
        public DateTime firstTrxDate { get; set; }
        public DateTime lastTrxDate { get; set; }
        public int status { get; set; }
        public int productId { get; set; }
        public decimal enrolmentFeeCharged { get; set; }
        public decimal secDeposit { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public int pgsStatus { get; set; }
        public int prefferedLanguage { get; set; }
        public int classOfTrade { get; set; }
        public DateTime activationDate { get; set; }
        public int fieldStaffId { get; set; }
        public string secDepositChequeNo { get; set; }
        public int secDepositBankId { get; set; }
        public string secDepositAccount { get; set; }
        public string paidBy { get; set; }
        public int paymentOption { get; set; }
        public int onbooardStage { get; set; }
        public int kycStatus { get; set; }
        public string remarks { get; set; }
        public int cpaf { get; set; }
    }
}