using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Acquisition
{
    public class CPDetailsforApproval
    {
        public GetChannelPartnerDetails getChannelPartnerDetails { get; set; }
    }
    public class BankingInfo
    {
        public string bankid { get; set; }
        public string bankName { get; set; }
        public string ifsc { get; set; }
        public string accountNumber { get; set; }
        public string accountHolderName { get; set; }
        public string cancelledchequeimage { get; set; }
        public string status { get; set; }
    }

    public class BusinessInfo
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

    public class GetChannelPartnerDetails
    {
        public Status status { get; set; }
    }

    public class KycInfo
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
        public PermanentAddress permanentAddress { get; set; }
        public BankingInfo bankingInfo { get; set; }
        public BusinessInfo businessInfo { get; set; }
    }

    public class OrganizationIfo
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

    public class PermanentAddress
    {
        public string permHouseNo { get; set; }
        public string permDist { get; set; }
        public string permSubDst { get; set; }
        public string permPincode { get; set; }
        public string permLandmark { get; set; }
    }
    public class Status
    {
        //public Status status { get; set; }
        public string status { get; set; }
        public OrganizationIfo organizationIfo { get; set; }
        public KycInfo kycInfo { get; set; }
    }
}
