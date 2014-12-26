using System;
using RestSharp.Extensions;
using System.Globalization;

namespace RestSharp.Serializers
{
    /// <summary>
    /// Allows control over whether null properties will be serialized.
    /// Currently not supported with the JsonSerializer
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class XmlSkipNull : Attribute
    {
        public XmlSkipNull()
        {
            
        }
    }
}