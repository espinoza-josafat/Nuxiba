using System;

namespace Nuxiba.TestArch.Entities.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    [Serializable]
    public class MappingAttribute : Attribute
    {
        public string ColumnName { get; set; }
        
        public MappingAttribute(string columnName)
        {
            if (string.IsNullOrWhiteSpace(columnName))
                throw new ArgumentNullException("columnName");

            ColumnName = columnName;
        }
    }
}