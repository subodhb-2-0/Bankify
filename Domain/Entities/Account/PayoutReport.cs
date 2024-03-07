using Domain.Entities.Common;

namespace Domain.Entities.Account
{
    public class PayoutReport : BaseEntity
    {
        public int paymentid { get; set; }
        public int orgid { get; set; }
        public string orgcode { get; set; }
        public string orgname { get; set; }
        public string orgtype { get; set; }
        public string amount { get; set; }
        public int paymentmode { get; set; }
        public string instrumentnumber { get; set; }
        public int status { get; set; }
        public int creator { get; set; }
        public DateTime creationdate { get; set; }
        public string DepositingBank { get; set; }
        public string Narration { get; set; }
        public string SlipNarration { get; set; }
        public string AccountNumber { get; set; }
        public DateTime ActionDateTime { get; set; }
        public string IFSCCode { get; set; }
        public string UPI { get; set; }
        public double TransactionCharge { get; set; }
        public string BankRefNo { get; set; }
        public double servicecharge { get; set; }
        public string BeneficiaryAccNo { get; set; }
        public string BeneficiaryName { get; set; }
        public string Remarks { get; set; }
        public long totalrecord { get; set; }


    }
}
