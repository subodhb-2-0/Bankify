using Domain.Entities.Common;

namespace Domain.Entities.Onboarding
{
    public class Product: BaseEntity
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string productDesc { get; set; }
        public int channelId { get; set; }
        public int status { get; set; }
        public double enrollmentfee { get; set; }
        public int lotsize { get; set; }
    }
}