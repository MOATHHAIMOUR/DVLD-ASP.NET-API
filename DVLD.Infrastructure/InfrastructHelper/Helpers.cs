using Microsoft.Data.SqlClient;
using System.Reflection;

namespace DVLD.Infrastructure.InfrastructHelper
{
    internal static class Helpers
    {
        // Helper to create SQL parameters from a DTO
        public static SqlParameter[] CreateSqlParametersFromObject(object paramObject, object? includedDto = null)
        {
            var parameters = new List<SqlParameter>(); // This list collects all parameters

            // Recursive function to handle nested objects
            void ProcessProperties(object obj, object? includeObj = null, string prefix = "")
            {
                foreach (var prop in obj.GetType().GetProperties())
                {
                    // Attempt to find the property in the included DTO
                    var includeProp = includeObj?.GetType().GetProperty(prop.Name);


                    // If the property does not exist in the included DTO and is not a nested object, skip it
                    if (includeObj != null && includeProp == null && !(prop.PropertyType.IsClass && prop.PropertyType != typeof(string)))
                    {
                        Console.WriteLine($"Skipping {prop.Name} - Not found in includeObj and not a nested object.");
                        continue;
                    }

                    var value = prop.GetValue(obj);

                    // Check for default or null values
                    if (IsDefaultOrNull(value, prop))
                    {
                        value = DBNull.Value; // Use DBNull for null values
                    }

                    // Handle nested objects (classes)
                    if (includeObj != null && prop.PropertyType.IsClass && prop.PropertyType != typeof(string))
                    {
                        if (value != null)
                        {

                            Console.WriteLine($"Processing nested object: {prop.Name} with prefix {prefix}{prop.Name}_");

                            // Recursively call ProcessProperties for the nested object
                            ProcessProperties(value, includeObj);
                        }
                        else
                        {
                            Console.WriteLine($"Skipping nested object: {prop.Name} - Value is null.");
                        }
                    }
                    else
                    {
                        if (prop.PropertyType.IsEnum)
                        {
                            // Convert the enum value to its underlying integer type
                            value = Convert.ToInt32(value);
                        }

                        if (parameters.Any(p => p.ParameterName == "@" + prop.Name)) continue;

                        parameters.Add(new SqlParameter($"@{prop.Name}", value));
                    }
                }
            }

            // Start processing the main object
            ProcessProperties(paramObject, includedDto);

            return parameters.ToArray(); // Return the collected parameters
        }

        // Helper to check if a value is default or null
        public static bool IsDefaultOrNull(object? value, PropertyInfo propertyInfo)
        {
            if (value == null)
                return true; // Null values

            if (propertyInfo.PropertyType == typeof(string) && string.IsNullOrEmpty((string)value))
                return true; // Empty strings

            if (propertyInfo.PropertyType.IsValueType && value.Equals(Activator.CreateInstance(propertyInfo.PropertyType)))
                return true; // Default values for value types

            return false; // Not default or null
        }

    }
}
