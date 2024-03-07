using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Acquisition
{
    //public class AddAcquisitionDto 
    //{
    //    //public int orgid { get; set; }
    //    //public string orgcode { get; set; }
    //    public string orgname { get; set; }
    //    public int orgtype { get; set; }
    //    public int parentOrgId { get; set; }
    //    public int fieldStaffId { get; set; }
    //    public string fName { get; set; }
    //    public string lName { get; set; }
    //    public string mName { get; set; }
    //    public string emailid { get; set; }
    //    public string mobileNumber { get; set; }
    //    public int productId { get; set; }
    //    public string secdepositchequeno { get; set; }
    //    public int secdeposit { get; set; }
    //    public string paidBy { get; set; }
    //    public string paymentmode { get; set; }
    //    public int secDepositBankId { get; set; }
    //    public string secdepositaccount { get; set; }
    //    //public string payment_option { get; set; }
    //    //public int creator { get; set; }
    //}

    public class AddAcquisitionDto
    {
        public int orgid { get; set; }
        public string orgcode { get; set; }
        public string password { get; set; }
        public string orgname { get; set; }
        public int orgtype { get; set; }
        public int parentOrgId { get; set; }
        public int fieldStaffId { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string mName { get; set; }
        public string emailid { get; set; }
        public string mobileNumber { get; set; }
        public string busi_sub_district { get; set; }
        public int productId { get; set; }

        public int channels { get; set; }
        
        public string secdepositchequeno { get; set; }
        public int secdeposit { get; set; }
        public string paidBy { get; set; }
        public string paymentmode { get; set; }
        public int secDepositBankId { get; set; }
        public string secdepositaccount { get; set; }
        public string payment_option { get; set; }
        public int creator { get; set; }
        public int cpafnumber { get; set; }

        //
        //public int status { get; set; }
        //public int onboardstage { get; set; }
        //public int kycstatus { get; set; }
        public int needspasschange { get; set; }
        public int roleid { get; set; }
        public int failedcount { get; set; }
        public int ispasscodeexists { get; set; }

        public int usertypeid { get; set; }
        



    }
}

