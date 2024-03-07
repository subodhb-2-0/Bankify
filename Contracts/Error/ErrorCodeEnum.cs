

namespace Contracts.Error
{
   public class ErrorCodeEnum
    {
        public enum CommonErrorEnum
        {
            ContactToAdmin = 1001,
        }
        public enum UserManagementErrorEnum
        {
            UserIsInActive = 2001,
            UserIsIsLocked = 2002,
            InvalidUserNamePassword = 2003,
            UserIsDeleted = 2004,
            UserNotFound = 2005,
            UserAlreadyExist = 2006,
            MobileNumberExist = 2007,
            InValidStatus = 2008,
            FirstTimeLoginUserNeedToChangeThePassword = 2009,
            UserNeedToChangeThePassword = 2010,

            SomeOnboardingDetailsHaveFailed = 2011,
            SomeOnboardingDetailsArePending = 2012,
            SomeOnboardingKYCsArePending = 2013,
            FacingIssueInFatchingOnboarding = 2014,
            UserBlockedPermanently = 2015,
            UnableToFetchWalletBalance = 2016,
            NoSubUserFound = 2017,
        }
        public enum UserManagementTransErrorEnum
        {
            UserIsInActive = 2001,
            UserIsIsLocked = 2002,
            InvalidUserNamePassword = 2003,
            UserIsDeleted = 2004,
            UserNotFound = 2005,
            UserAlreadyExist = 2006,
            FirstTimeLoginUserNeedToChangeThePassword = 2007,
            UserNeedToChangeThePassword = 2008,
            RecentlyUsedPasword = 2009,
            UnableToFetchWalletBalance = 2010,
            UnableToFetchUserProfile = 2011,
            NoSubUserFound = 2012,
            SomethingWentWrongWhileUpdatingToken = 2013,
            InValidStatus = 2014,
            FacingIssueInFatchingOnboarding = 2015,
            SomeOnboardingDetailsHaveFailed = 2016,
            SomeOnboardingDetailsArePending = 2017,
            SomeOnboardingKYCsArePending = 2018,
            UserBlockedPermanently = 2019
        }

        public enum reportsErrorEnum
        {
            Reportnotfound = 9001

        }
    }
}
