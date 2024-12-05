namespace DVLD.Domain.StoredProcdure
{
    public static class PersonStoredProcedures
    {
        public static string SP_GetPeople => "SP_GetPeople";
        public static string SP_UpdatePerson => "SP_UpdatePerson";
        public static string SP_AddPerson => "SP_AddPerson";
        public static string SP_DeletePersonById => "SP_DeletePersonById";

        public static string SP_GetPersonByPersonIdOrNationalNo = "SP_GetPersonByPersonIdOrNationalNo";

    }
}
