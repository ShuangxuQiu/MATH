using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Symbolics;

namespace MATH2.Utils
{
    public static class Sequences
    {
        public static Expression GetValueInSequence(Expression seq, Expression nexpression, int index)
        {
            return Structure.Substitute(Expression.Symbol("n"), index, seq);
        }
        public static int CalculateEqualizationDifNo(List<Expression> seq)
        {
            //Console.WriteLine("calculating: ================================");
            int no = 0;
            bool found = false;

            List<Expression> last = seq;
            while (!found)
            {
             //   Console.Write("last = ");
                //foreach (var item in last)
                {
              //      Console.Write(Infix.Print(item)+",");
                }
                //Console.WriteLine();
                //*
                if (last.TrueForAll(x => Infix.PrintStrict(last.Last())==Infix.PrintStrict(x)))
                {
                   // Console.Write("  Last - x =" + Infix.Print(last.Last() - x));
                    found = true;
                    continue;
                }//*/
                last = GetDifference(last);
                no++;
            }
            return no;
        }
        public static List<Expression> GetDifference(List<Expression> seq)
        {
            List<Expression> ret = new List<Expression>();
            Expression lastexp = null;
            foreach (var item in seq)
            {
                if (lastexp != null)
                {
                    ret.Add((item - lastexp));
                }
                lastexp = item;
            }
            return ret;
        }
        public static List<Expression> ParseSequence(string raw)
        {
            try
            {
                List<Expression> ret = new List<Expression>();
                StringBuilder sb = new StringBuilder();
                foreach (var item in raw.Split(',').ToList())
                {
                    ret.Add(Infix.ParseOrThrow(item.Replace(",", "")));
                    sb.Append(LaTeX.Print(Infix.ParseOrThrow(item.Replace(",", ""))) + ",");
                }
                System.Windows.Forms.Clipboard.SetText(sb.ToString());
                // Console.WriteLine("Successfully parsed sequence: " + sb.ToString());
                return ret;
            }
            catch
            {
                Console.WriteLine("Sequence was in the incorrect format, remember to seperate items by a comma!");
                return null;
            }
        }
    }
}
