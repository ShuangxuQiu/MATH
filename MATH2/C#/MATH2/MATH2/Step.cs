using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MATH2
{
    public class Step
    {
        public MathNet.Symbolics.Expression expression;
        public string notes;
        public Step() { }
        public Step(MathNet.Symbolics.Expression ex, string s) 
        {
            expression = ex;
            notes = s;
        }
        public override string ToString()
        {
            try
            {
                if (string.IsNullOrEmpty(notes) == false)
                {
                    return MathNet.Symbolics.Infix.Print(expression) + " | " + notes;
                }
                else
                {
                    return MathNet.Symbolics.Infix.Print(expression);
                }
            }
            catch
            {
                try
                {
                    return notes;
                }
                catch {return ""; }
            }
        }
    }
}
