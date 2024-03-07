using Contracts.Common;

namespace Contracts.Onboarding
{
    public class ChannelPartnerDto : BaseEntityDto
    {
        public int orgId { get; set; }
        public int orgCode { get; set; }
        public int orgName { get; set; }
        public int orgType { get; set; }
        public int parentOrgId { get; set; }
        public int firstTrxDate { get; set; }
        public int lastTrxDate { get; set; }
        public int status { get; set; }
        public int productId { get; set; }
        public int enrolmentFeeCharged { get; set; }
        public int secDeposit { get; set; }
        public int latitude { get; set; }
        public int longitude { get; set; }
        public int pgsStatus { get; set; }
        public int prefferedLanguage { get; set; }
        public int classOfTrade { get; set; }
        public int activationDate { get; set; }
        public int fieldStaffId { get; set; }
        public int secDepositChequeNo { get; set; }
        public int secDepositBankId { get; set; }
        public int secDepositAccount { get; set; }
        public int paidBy { get; set; }
        public int payment_option { get; set; }
    }
}
