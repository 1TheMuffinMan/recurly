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
    public class RecurlyCurrency 
    {
        public int? USD { get; set; }
        public int? AUD { get; set; }
        public int? CAD { get; set; }
        public int? EUR { get; set; }
        public int? GBP { get; set; }
        public int? CZK { get; set; }
        public int? DKK { get; set; }
        public int? HUF { get; set; }
        public int? NOK { get; set; }
        public int? NZD { get; set; }
        public int? PLN { get; set; }
        public int? SGD { get; set; }
        public int? SEK { get; set; }
        public int? CHF { get; set; }
        public int? ZAR { get; set; }
    }
}
