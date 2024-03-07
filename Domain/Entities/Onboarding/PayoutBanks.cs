using Domain.Entities.Common;

namespace Domain.Entities.Onboarding
{
    public class PayoutBanks : BaseEntity
    {
        public int payOutBankId { get; set; }
        public int orgId { get; set; }
        public int bankId { get; set; }
        public string ifsc { get; set; }
        public string accountNumber { get; set; }
        public string accountHolderName { get; set; }
        public int status { get; set; }
        public int isPrimary { get; set; }
        public int isVerified { get; set; }
        public DateTime verificationDate { get; set; }
    }
}