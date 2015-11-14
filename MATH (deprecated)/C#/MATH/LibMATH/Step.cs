using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMATH
{
    public class Step
    {
        public Dictionary<Expression, Operator> rightexpressions = new Dictionary<Expression, Operator>();
        public bool equals = true;
        public Dictionary<Expression, Operator> leftexpressions = new Dictionary<Expression, Operator>();
        public string notes = string.Empty;
    }
}
