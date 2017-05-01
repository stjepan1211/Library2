using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Token
{
    public class Token
    {
        public string TokenString { get; internal set; }
        public double ExpirationTime { get; internal set; }
    }
}
