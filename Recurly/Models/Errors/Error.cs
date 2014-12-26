using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace Recurly
{
    /// <summary>
    /// An individual error message.
    /// For more information, please visit http://docs.recurly.com/api/errors
    /// </summary>
    public class Error
    {
        [DeserializeAs(Name = "Value")]
        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Field causing the error, if appropriate.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Error code set for certain transaction failures.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Error symbol
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Error details
        /// </summary>
        public string Details { get; set; }

        internal static Error[] ReadResponseAndParseErrors(IRestResponse response)
        {
            if (string.IsNullOrWhiteSpace(response.Content))
                return new Error[0];

            var errors = new List<Error>();
            var ser = new RecurlyXmlDeserializer();

            var doc = XDocument.Parse(response.Content);
            if (doc.Root == null) return errors.ToArray();

            if (doc.Root.Name == "errors")
            {
                errors.AddRange(ser.Deserialize<List<Error>>(response));
            }
            else
            {
                errors.Add(ser.Deserialize<Error>(response));
            }

            return errors.ToArray();
        }
    }
}