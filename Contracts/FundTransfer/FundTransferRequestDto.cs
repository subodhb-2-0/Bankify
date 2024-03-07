using Contracts.Enums;
using System.Net.NetworkInformation;

namespace Contracts.FundTransfer
{
    public class FundTransferRequestDto
    {
        public int orgid { get; set; }
        public int paymentmode { get; set; }
        public decimal amount { get; set; }
        public string depositdate { get; set; }
        public int status { get; set; }
        public string remark { get; set; }
        public int creator { get; set; }
        public int transferbyorgid { get; set; }
    }
}
