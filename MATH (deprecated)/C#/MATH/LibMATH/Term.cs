using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMATH
{
    public class Term
    {
        public bool showsymbol = true;
        public double coeficient = 1;
        public double power = 1;
        public string symbol = "";
        public double symvalue;
        public Expression over;
        public override string ToString()
        {
            if (coeficient >= 0)
            {
                return "+" + coeficient + symbol + "^" + power;
            }
            else
            {
                return "-" + coeficient + symbol + "^" + power;
            }
        }
    }
}
