using Nuxiba.TestArch.Entities.Attributes;
using System;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Nuxiba.TestArch.Data.Helpers
{
    internal class MappingDAOs
    {
        public static T MapToClass<T>(IDataReader reader) where T : class
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            T result = Activator.CreateInstance<T>();
            var properties = result.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];

                if (property.CanWrite)
                {
                    var notMappingAttributes = property.GetCustomAttributes<NotMappingAttribute>(true).ToArray();

                    if (notMappingAttributes == null || notMappingAttributes.Length == 0)
                    {
                        var type = (Type)null;

                        if (property.PropertyType == typeof(string))
                            type = typeof(string);
                        else
                            type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                        var safeValue = (object)null;

                        var mappingAttributes = property.GetCustomAttributes<MappingAttribute>(true).ToArray();
                        if (mappingAttributes.Length > 0 && mappingAttributes[0].ColumnName != null)
                            safeValue = reader[mappingAttributes[0].ColumnName] == (object)DBNull.Value ? null : Convert.ChangeType(reader[mappingAttributes[0].ColumnName], type, null);
                        else
                            safeValue = reader[property.Name] == (object)DBNull.Value ? null : Convert.ChangeType(reader[property.Name], type, null);

                        property.SetValue(result, safeValue, null);
                    }
                }
            }

            return result;
        }
    }
}