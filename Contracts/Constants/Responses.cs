
namespace Contracts.Constants
{
    public static class ResponseCode
    {
        public const string Success = "0";
        public const string Error = "1";
    }

    public static class CommonErrorCode
    {
        public const string ContactToAdmin = "1001";
    }

    public static class UserManagementResponse
    {
        public const string NeedResetPassword = "101";
        public const string PasswordExpired = "102";
        public const string PasswordGoingToBeExpire = "103";
    }

    public static class UserManagementError
    {
        public const string UserIsInActive = "2001";
        public const string UserIsIsLocked = "2002";
        public const string InvalidUserNamePassword = "2003";
        public const string UserIsDeleted = "2004";
        public const string UserNotFound = "2005";
        public const string UserAlreadyExist = "2006";
        public const string MobileNumberExist = "2007";
        public const string InValidStatus = "2008";
        public const string FirstTimeLoginUserNeedToChangeThePassword = "2009";
        public const string UserNeedToChangeThePassword = "2010";
        public const string SomeOnboardingDetailsHaveFailed = "2011";
        public const string SomeOnboardingDetailsArePending = "2012";
        public const string SomeOnboardingKYCsArePending = "2013";
        public const string FacingIssueInFatchingOnboarding = "2014";
        public const string UserBlockedPermanently = "2015";
        public const string UnableToFetchWalletBalance = "2016";
        public const string NoSubUserFound = "2017";
    }

    public static class ComissionManagementError
    {
        public const string ComissionIsInActive = "5001";
        public const string ComissionIsIsLocked = "5002";
        public const string InvalidComissionPassword = "5003";
        public const string ComissionIsDeleted = "5004";
        public const string ComissionNotFound = "5005";
        public const string ComissionAlreadyExist = "5006";
        public const string MobileNumberExist = "5007";
        public const string ComissionModelAlreadyExist = "5008";
    }

    public static class RoleErrorCode
    {
        public const string RoleAlreadyExists = "3001";
    }

    public static class OTPErrorCode
    {
        public const string OTPExpire = "4001";
        public const string InValidOTP = "4002";
    }

    public static class CPAcquisition
    {
        public const string OrgIdNotFound = "6001";
        public const string ORGNOTFOUND = "6002";
    }

    public static class ReportsError
    {
       public const string Reportnotfound = "9001";

    }

    public static class WorkingCapitalError
    {
        public const string Paymentnotfound = "10001";

    }
    public static class LocationError
    {
        public const string NoCountryFound = "4001";
        public const string NostateFound = "4002";
        public const string NoCityFound = "4003";
        public const string NoLocationFound = "4004";
    }
    public static class PaymentError
    {
        public const string NoPaymentModeFound = "7001";
        public const string NoVPAFound = "7002";
        public const string SomethingWentWrongWhileUpdatingVPA = "7003";
        public const string NoPaymentIdFound = "7004";
        public const string NoBanksFound = "7005";
    }


    public static class PaySprintOnboardingResponse
    {
        public const string OnboardingProcessComplete = "Onboarding Process is Done!";
        public const string OnboardingError = "$Error while processing Paytm Onboardig request.";
        public const string PaytmStatusCheckError = "$Error while Checking Paytm Onboardig Status.";
    }


    public static class PaySprintBanks
    {
        public const string PayTmBank = "PayTmBank";
        public const string NSDL_FINO = "NSDL_FINO";
        public const string UnKnownBank = "UnKnownBank";
        public const string Bank5 = "Bank5";
        public const string Bank3 = "Bank3";
        public const string Bank2 = "Bank2";
    }



}
