namespace Contracts.Acquisition
{
    public class GetCPAquisitionDto
    {
        public DateTime AcquisitionDate { get; set; }
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public int CPID { get; set; }
        public string CPCode { get; set; }
        public string CPName { get; set;}
        public string ShopName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public int ParentId { get; set; }
        public string ParentOrgCode { get; set; }
        public string ParentName { get; set; }
        public int SuperId { get; set; }
        public string SuperName { get; set; }
        public DateTime ActivationDate { get; set; }
        public string PinCode { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PAN { get; set; }
        public string Status { get; set; }
        public string ProductName { get; set; }
    }
}
