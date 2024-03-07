namespace Contracts.AEPS
{
    public class AgentOnboardingRequestDto
    {
        public string merchantcode { get; set; }
        public string mobile { get; set; }
        public string is_new { get; set; }
        public string email { get; set; }
        public string firm { get; set; }
        public string callback { get; set; }
    }
}
