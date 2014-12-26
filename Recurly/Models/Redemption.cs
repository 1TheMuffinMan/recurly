using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace Recurly
{
    [DeserializeAs(Name = "redemption")]
    public class Redemption
    {
        public string AccountCode { get; set; }
        public string CouponCode { get; set; }

        public string Currency { get;  set; }

        public bool SingleUse { get;  set; }
        public int TotalDiscountedInCents { get; set; }

        public DateTime CreatedAt { get; set; }

        public string State { get; set; }
    }
}
