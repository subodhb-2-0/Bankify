using Domain.Entities.Common;

namespace Domain.Entities.Product
{
    public class SalesChannelGetProductDetails : BaseEntity
    {
        public int status { get; set; }
        public string mobilenumber {  get; set; }
        public string orgcode { get; set; }
        public int orgtype { get; set; }
        public string productname { get; set; }
        public int productid { get; set; }
        public string EmployeeAddress { get; set; }
        public string orgName { get; set; }
    }
}
