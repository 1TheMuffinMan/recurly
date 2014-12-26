using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recurly
{
    public class RecurlyPagedSet<T>
    {
        public int TotalItems { get; private set; }

        public List<T> Items { get; private set; }

        public string Cursor { get; private set; }

        internal RecurlyPagedSet(List<T> items, int itemCount, string nextUrl)
        {
            Items = items;
            TotalItems = itemCount;
        }
    }
}
