using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Bbps
{
    public class Biller
    {
        public string billerId { get; set; }
        public string billerName { get; set; }
        public string billerCategory { get; set; }
        public string billerAdhoc { get; set; }
        public string billerCoverage { get; set; }
        public string billerFetchRequiremet { get; set; }
        public string billerPaymentExactness { get; set; }
        public string billerSupportBillValidation { get; set; }
        public string supportPendingStatus { get; set; }
        public string supportDeemed { get; set; }
        public string billerTimeout { get; set; }

        //code added by biswa 
        public dynamic billerInputParams { get; set; }
        //code commented by biswa
        //public BillerInputParams billerInputParams { get; set; }

        public object billerAdditionalInfo { get; set; }
        public string billerAmountOptions { get; set; }
        public string billerPaymentModes { get; set; }
        public string billerDescription { get; set; }
        public string rechargeAmountInValidationRequest { get; set; }
        public BillerPaymentChannels billerPaymentChannels { get; set; }
        public object billerAdditionalInfoPayment { get; set; }
        public object planAdditionalInfo { get; set; }
        public string planMdmRequirement { get; set; }
    }
}
