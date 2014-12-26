﻿using System.Xml;
﻿using RestSharp.Serializers;

namespace Recurly
{
    [SerializeAs(Name = "address")]
    public class RecurlyAddress
    {
        [SerializeAs(Name = "address1")]
        public string Address1 { get; set; }

        [SerializeAs(Name = "address2")]
        public string Address2 { get; set; }

        [SerializeAs(Name = "city")]
        public string City { get; set; }

        [SerializeAs(Name = "state")]
        public string State { get; set; }

        [SerializeAs(Name = "zip")]
        public string Zip { get; set; }

        [SerializeAs(Name = "country")]
        public string Country { get; set; }

        [SerializeAs(Name = "phone")]
        public string Phone { get; set; }

        public RecurlyAddress() { }
    }
}