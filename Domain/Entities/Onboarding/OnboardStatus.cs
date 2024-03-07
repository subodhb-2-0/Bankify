namespace Domain.Entities.Onboarding
{
    public class OnboardStatus
    {
        public int OrgId { get; set; }
        public int OnboardState { get; set; }
        public int status { get; set; }
        public DateTime statusDate { get; set; }
    }
}