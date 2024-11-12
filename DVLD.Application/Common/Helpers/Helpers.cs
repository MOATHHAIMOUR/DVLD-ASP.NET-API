namespace DVLD.Application.Common.Helpers
{
    public static class Helpers
    {
        public static Dictionary<string, string> BuildQueryKeyValuePer(string queries)
        {
            //personId=1,Name="Moath"

            Dictionary<string, string> queryPerValue = [];

            string[] query = queries.Split(',');

            foreach (string queryKeyPerValue in query)
            {
                string[] queryPerValueString = queryKeyPerValue.Split("=");

                queryPerValue.Add(queryPerValueString[0], queryPerValueString[1]);
            }

            return queryPerValue;
        }

    }
}
