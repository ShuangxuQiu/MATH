using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMATH
{
    public enum Operator
    {
        add,
        multiply,
        divide,
        sqroot,
        pow,
        none,
    }
    public class Expression
    {
        public Dictionary<Term, Operator> terms = new Dictionary<Term, Operator>();
        public double power = 1;
        public double coeficient = 1;
        public Expression over;
        public Expression()
        {
        }
        public Expression(params Term[] newterms)
        {
            foreach (var item in newterms)
            {
                terms.Add(item, Operator.add);
            }
        }
        public Expression(Term term, Operator op)
        {
            terms.Add(term, op);
        }
    }
}
