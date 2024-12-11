namespace DVLD.Domain.StoredProcdure
{
    public static class PersonStoredProcedures
    {
        public static string SP_GetPeople => "SP_GetPeopleView";
        public static string SP_UpdatePerson => "SP_UpdatePerson";
        public static string SP_AddPerson => "SP_AddPerson";
        public static string SP_DeletePersonById => "SP_DeletePersonById";
        public static string SP_GetPersonByPersonIdOrNationalNo => "SP_GetPersonByPersonIdOrNationalNo";
        public static string SP_CheckIsPersonExist => "SP_CheckIsPersonExist";

        public static string SP_IsPersonHasProfileImage => "SP_IsPersonHasProfileImage";

    }
}
