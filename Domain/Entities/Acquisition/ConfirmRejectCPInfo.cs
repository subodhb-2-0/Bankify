using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Acquisition
{
    public class ConfirmRejectCPInfo
    {
        public ConfirmRejectCP confirmRejectCP { get; set; }
    }
    public class ConfirmRejectCP
    {
        public string orgId { get; set; }
        public string statusId { get; set; }
        public KycStatusDtls kycStatusDtls { get; set; }
        public string remarks { get; set; }
    }

    public class KycStatusDtls
    {
        public int onboardStage1 { get; set; }
        public int onboardStage2 { get; set; }
        public int onboardStage3 { get; set; }
        public int onboardStage4 { get; set; }
        public int onboardStage5 { get; set; }
        public int onboardStage6 { get; set; }
        public int onboardStage7 { get; set; }
    }

    public class ApproveRejectCPInfo
    {
        public ApproveRejectCP confirmRejectCP { get; set; }
    }
    public class ApproveRejectCP
    {
        public string orgId { get; set; }
        public string statusId { get; set; }
        public List<KycStatusDtl> kycStatusDtls { get; set; }
    }

    public class KycStatusDtl
    {
        public string onOnboardState { get; set; }
        public string statusId { get; set; }
        public string remarks { get; set; }
    }
}
