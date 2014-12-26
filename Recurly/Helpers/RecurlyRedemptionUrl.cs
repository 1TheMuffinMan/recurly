using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp.Deserializers;

namespace Recurly
{
    public class RecurlyRedemptionUrl
    {
        [DeserializeAs(Name = "href",Attribute = true)]
        public string Url { get; set; }
    }
}
