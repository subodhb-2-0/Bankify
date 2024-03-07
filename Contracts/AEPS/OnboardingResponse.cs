namespace Contracts.AEPS
{
    public class OnboardingResponse
    {
        public bool status { get; set; }
        public int response_code { get; set; }
        public string redirecturl { get; set; }
        public int onboard_pending { get; set; }
        public string message { get; set; }
    }
}
