using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MATH2
{
    public enum ArgType
    {
        integer,
        number,
        boolean,
        text,
    }
    public class Arg
    {
        public ArgType type = ArgType.text;
        public object defaultvalue = null;
        public object value = null;
        public string displayname = "option";
    }
}
