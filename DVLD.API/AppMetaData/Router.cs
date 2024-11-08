namespace DVLD.API.AppMetaData
{
    public class Router
    {
        private const string Root = "Api";
        private const string Version1 = "v1";
        private const string Rule = Root + "/" + Version1 + "/";

        private static class CrudOperations
        {
            public const string GetById = "{Id}"; // Consider more specific naming
            public const string Create = "Create";
            public const string Update = "Update";
            public const string Delete = "Delete";
        }

        public static class PersonRouting
        {
            public const string prefix = Rule + "Person";

            public const string GetPeople = prefix + "/" + "GetAllProducts";
        }
    }
}
