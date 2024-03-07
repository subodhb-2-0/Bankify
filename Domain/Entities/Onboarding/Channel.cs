using Domain.Entities.Common;
namespace Domain.Entities.Onboarding
{
    public class Channel : BaseEntity
    {
        public int channelId { get; set; }
        public string channelCode { get; set; }
        public string channelName { get; set; }
        public string channelDesc { get; set; }
        public int status { get; set; }
        public int corpId { get; set; }
    }
}