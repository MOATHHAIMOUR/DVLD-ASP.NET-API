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

            public const string GetLocalDrivingApplicationView = prefix + "/" + "GetLocalDrivingApplicationView";

            public const string GetLicenseClases = prefix + "/" + "GetLicenseClasses";

            public const string AddNewLocalDrivingApplication = prefix + "/" + "AddNewLocalDrivingLicenseApplication";

            public const string GetLicenseView = prefix + "/" + "GetLicenseView";

            public const string RenewLocalDrivingLicense = prefix + "/" + "RenewLocalDrivingLicense";

            public const string DetainLocalDrivingLicense = prefix + "/" + "DetainLocalDrivingLicense";


        }

        public static class InternationalLicenseRouting
        {

            private const string prefix = Rule + "InternationalLicense";

            public const string AddNewInternationalLicense = prefix + "/" + "AddNewInternationalLicense";

            public const string GetInternationalLicenseById = prefix + "/" + "GetInternationalLicenseById";

            public const string GetAllInternationalLicense = prefix + "/" + "GetAllInternationalLicense";

        }


        public static class TestRouting
        {

            private const string prefix = Rule + "Test";
            public const string GetTestLocalDrivingLicenseDetail = prefix + "/" + "GetTestLocalDrivingLicenseDetail/{localDrivingApplication}";
            public const string GetTestAppoimentView = prefix + "/" + "GetTestAppointmentView";
            public const string GetScheduleTestInfoView = prefix + "/" + "GetScheduleTestInfoView";
            public const string AddTestAppointment = prefix + "/" + "AddTestAppointment";
            public const string AddTestResult = prefix + "/" + "AddTestResult";

        }

        public static class SharedRouting
        {
            public const string GetAllApplicationTypes = Rule + "GetAllApplicationTypes";

            public const string UpdateApplicationType = Rule + "UpdateApplicationType";

            public const string GetAllCountries = Rule + "GetAllCountries";
        }


    }


}
