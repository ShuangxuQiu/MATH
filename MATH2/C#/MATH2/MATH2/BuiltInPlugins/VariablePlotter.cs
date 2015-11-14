using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MATH2.BuiltInPlugins
{
    public class VariablePlotter: IMathPlugin
    {
        public int GetProb(string raw, MathNet.Symbolics.Expression e)
        {
            return -99;
        }
        public List<MATH2.Step> Solve(string raw, MathNet.Symbolics.Expression e)
        {
             MathNet.Symbolics.Expression seq = MathNet.Symbolics.Infix.ParseOrThrow(raw);
            int na = 1;
            List<Step> r = new List<Step>();
            Console.WriteLine("How many numbers to substitute x");
            StringBuilder sb = new StringBuilder();
            int no = int.Parse(Console.ReadLine());
            while (na != no)
            {
                r.Add(
                    new Step(
                        null,
                // sb.Append(
                    MathNet.Symbolics.Infix.Print(
                  //  MATH2.Utils.Sequences.GetValueInSequence(e, MathNet.Symbolics.Expression.Symbol("x"), na)
                  MathNet.Symbolics.Structure.Substitute(MathNet.Symbolics.Expression.Symbol("x"), na, seq)
                    )
                   // +", "
                   )
                    );
                na++;
            }
            
            r.Add(new Step(null, sb.ToString()));
            return r;
        }
    }
}
