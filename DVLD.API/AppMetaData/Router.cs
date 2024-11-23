namespace DVLD.API.AppMetaData
{
    public class Router
    {
        private const string Root = "Api";
        private const string Version1 = "v1";
        private const string Rule = Root + "/" + Version1 + "/";


        public static class PersonRouting
        {
            public const string prefix = Rule + "Person";

            public const string GetPeople = prefix + "/" + "GetPeople";

            public const string DeletePerson = prefix + "/" + "DeletePerson/{PersonId}";

            public const string AddPerson = prefix + "/" + "AddPerson";

            public const string GetPerson = prefix + "/" + "GetPerson";

            public const string UpdatePerson = prefix + "/" + "UpdatePerson";
        }

        public static class UserRouting
        {
            public const string prefix = Rule + "User";

            public const string GetUsers = prefix + "/" + "GetUsers";

            public const string GetUser = prefix + "/" + "GetUser";

            public const string DeleteUser = prefix + "/" + "DeleteUser/{UserId}";

            public const string AddUser = prefix + "/" + "AddUser";


            public const string UpdateUser = prefix + "/" + "UpdateUser";

        }

        public static class SharedRouting
        {
            public const string GetAllApplicationTypes = Rule + "GetAllApplicationTypes";

            public const string UpdateApplicationType = Rule + "UpdateApplicationType";

            public const string GetAllCountries = Rule + "GetAllCountries";
        }


    }


}
