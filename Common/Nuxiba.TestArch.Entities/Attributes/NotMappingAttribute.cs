using System;

namespace Nuxiba.TestArch.Entities.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    [Serializable]
    public class NotMappingAttribute : Attribute
    {
    }
}