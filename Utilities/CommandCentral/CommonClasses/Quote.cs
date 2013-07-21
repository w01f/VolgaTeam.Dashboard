using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandCentral.CommonClasses
{
    public class Quote
    {
        public string Value { get; set; }
        public string Author { get; set; }

        public Quote(string quote, string author)
        {
            this.Value = quote;
            this.Author = author;
        }
    }
}
