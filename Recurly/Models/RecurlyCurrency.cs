using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using RestSharp.Deserializers;

namespace Recurly
{
    public class RecurlyCurrency : IXmlSerializable
    {
        /// <summary>
        /// United States Dollars
        /// </summary>
        public int USD { get; set; }

        /// <summary>
        /// Australian Dollars
        /// </summary>
        public int AUD { get; set; }

        /// <summary>
        /// Canadian Dollars
        /// </summary>
        public int CAD { get; set; }

        /// <summary>
        /// Euros
        /// </summary>
        public int EUR { get; set; }

        /// <summary>
        /// British Pounds
        /// </summary>
        public int GBP { get; set; }

        /// <summary>
        /// Czech Korunas
        /// </summary>
        public int CZK { get; set; }

        /// <summary>
        /// Danish Krones
        /// </summary>
        public int DKK { get; set; }

        /// <summary>
        /// Hungarian Forints
        /// </summary>
        public int HUF { get; set; }

        /// <summary>
        /// Norwegian Krones
        /// </summary>
        public int NOK { get; set; }

        /// <summary>
        /// New Zealand Dollars
        /// </summary>
        public int NZD { get; set; }

        /// <summary>
        /// Polish Zloty
        /// </summary>
        public int PLN { get; set; }

        /// <summary>
        /// Singapore Dollars
        /// </summary>
        public int SGD { get; set; }

        /// <summary>
        /// Swedish Kronas
        /// </summary>
        public int SEK { get; set; }

        /// <summary>
        /// Swiss Francs
        /// </summary>
        public int CHF { get; set; }

        /// <summary>
        /// South African Rand
        /// </summary>
        public int ZAR { get; set; }
        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (var prop in typeof(RecurlyCurrency).GetProperties())
            {
                var propValue = (int)prop.GetValue(this, null);
                if (propValue != 0)
                    writer.WriteElementString(prop.Name, propValue.ToString());
            }
        }

        /// <summary>
        /// Returns a value that reflects whether any of the currencies have a value greater than 0.
        /// </summary>
        internal bool AnySpecified
        {
            get { return typeof (RecurlyCurrency).GetProperties().Any(x => (int)x.GetValue(this, null) > 0); }
        }
    }
}
