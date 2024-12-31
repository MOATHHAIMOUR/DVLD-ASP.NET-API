namespace DVLD.API.AppMetaData
{
    public class Router
    {
        private const string Root = "Api";
        private const string Version1 = "v1";
        private const string Rule = Root + "/" + Version1 + "/";


        public static class PersonRouting
        {
            private const string prefix = Rule + "Person";

            public const string GetPeople = prefix + "/" + "GetPeople";

            public const string DeletePerson = prefix + "/" + "DeletePerson/{PersonId}";

            public const string AddPerson = prefix + "/" + "AddPerson";

            public const string GetPerson = prefix + "/" + "GetPerson";

            public const string UpdatePerson = prefix + "/" + "UpdatePerson";
        }

        public static class UserRouting
        {
            private const string prefix = Rule + "User";

            public const string GetUsers = prefix + "/" + "GetUsers";

            public const string GetUser = prefix + "/" + "GetUser";

            public const string DeleteUser = prefix + "/" + "DeleteUser/{UserId}";

            public const string AddUser = prefix + "/" + "AddUser";


            public const string UpdateUser = prefix + "/" + "UpdateUser";

        }

        public static class LocalDrivingApplicationRouting
        {

            private const string prefix = Rule + "LocalDrivingApplication";

            public const string GetLocalDrivingApplicationView = prefix + "/" + "GetLocalDrivingApplicationsView";

            public const string GetLicenseClases = prefix + "/" + "GetLicenseClasses";

            public const string AddNewLocalDrivingApplication = prefix + "/" + "AddNewLocalDrivingLicenseApplication";

            public const string RenewLocalDrivingLicense = prefix + "/" + "RenewLocalDrivingLicense";

            public const string DetainLocalDrivingLicense = prefix + "/" + "DetainLocalDrivingLicense";

            public const string CancelLocalDivingApplication = prefix + "/" + "CancelLocalDivingApplication";

            public const string AddNewLocalLicense = prefix + "/" + "AddNewLocalLicense";

            public const string GetLicenseView = prefix + "/" + "GetLicenseView";

        }

        public static class InternationalLicenseRouting
        {

            private const string prefix = Rule + "InternationalLicense";

            public const string AddNewInternationalLicense = prefix + "/" + "AddNewInternationalLicense";

            public const string GetInternationalLicenseById = prefix + "/" + "GetInternationalLicenseById";

            public const string GetAllInternationalLicense = prefix + "/" + "GetAllInternationalLicense";

        }

        public static class DetainLicenseRouting
        {

            private const string prefix = Rule + "DetainLicense";

            public const string DetainLicense = prefix + "/" + "AddDetainLicense";

            public const string ReleaseLicense = prefix + "/" + "ReleaseLicense";

        }
        public static class ReplaceLostDamageRouter
        {

            private const string prefix = Rule + "ReplaceLostDamageController";

            public const string ReplaceLostLicense = prefix + "/" + "ReplaceLostLicense";

            public const string ReplaceDamageLicense = prefix + "/" + "ReplaceDamageLicense";

        }


        public static class AuthRouting
        {

            private const string prefix = Rule + "Auth";

            public const string login = prefix + "/" + "Login";
            public const string RefreshToken = prefix + "/" + "RefreshToken";

        }


        public static class TestRouting
        {

            private const string prefix = Rule + "Test";
            public const string GetTestLocalDrivingLicenseDetail = prefix + "/" + "GetTestLocalDrivingLicenseDetail/{localDrivingApplication}";
            public const string GetTestAppoiments = prefix + "/" + "GetTestAppointments";
            public const string GetScheduleTestInfoView = prefix + "/" + "GetScheduleTestInfoView";
            public const string AddTestAppointment = prefix + "/" + "AddTestAppointment";
            public const string AddTestResult = prefix + "/" + "AddTestResult";


        }

        public static class SharedRouting
        {
            private const string prefix = Rule + "Shared/";

            public const string GetAllApplicationTypes = prefix + "GetAllApplicationTypes";

            public const string UpdateApplicationType = prefix + "UpdateApplicationType";

            public const string GetAllCountries = prefix + "GetAllCountries";

            public const string GetLicenseClases = prefix + "GetLicenseClasses";


        }


    }


}
