using System;
using System.Collections.Generic;
using System.Text;

namespace ContryApp
{
    public class Country
    {
        public string Nimetus { get; set; }
        public string Kapitali { get; set; }
        public string Elanikkonnast { get; set; }
        public string lipp { get; internal set; }
    }
}
