using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Symbolics;
using S = MATH2.Utils.Sequences;

namespace MATH2.BuiltInPlugins
{
    public class LinearSequences : MATH2.IMathPlugin
    {
        public List<Arg> GetArgs()
        {
            return new List<Arg>();
        }
        public void UpdateArgs(System.Windows.Forms.Control.ControlCollection clist)
        {
        }
        public List<Expression> sequence = new List<Expression>();
        public Expression t_n = Expression.Symbol("n");
        public List<Step> steps = new List<Step>();
        
        public int GetProb(string raw,MathNet.Symbolics.Expression e)
        {
            sequence = MATH2.Utils.Sequences.ParseSequence(raw);
            if (S.CalculateEqualizationDifNo(sequence) == 1)
            {
                return 100;
            }
            else
            {
                return -100;
            }
            return 0;
        }
        public Expression FindOffset()
        {
            //get first value by current t_n

            Expression step1 = Structure.Substitute(Expression.Symbol("n"), 1, t_n);
            steps.Add(new Step(step1, "Try to find the offset by substituting n with 1"));
           // Console.WriteLine(Infix.Print(step1));
           // Console.WriteLine("what is "+Infix.Print(step1)+" - "+Infix.Print(sequence[0]));//this is false info now
            return (sequence[0]-step1);
        }
        public List<Step> Solve(string raw, Expression exp)
        {
            try
            {
                // Expression e = new Expression();
                //   e += exp;
                sequence = MATH2.Utils.Sequences.ParseSequence(raw);
                steps.Add(new Step(null, raw + " - start off with the sequence"));
                //  Console.WriteLine("First difference:");
                Expression firstdif = S.GetDifference(sequence)[S.GetDifference(sequence).Count - 1];
                steps.Add(new Step(firstdif, "find the first difference"));
                //Console.WriteLine(Infix.Print(firstdif));
                t_n = t_n * firstdif;
                steps.Add(new Step(t_n, "n must be multiple of first difference"));
                //substitute(thing,thingwith,raw);
                // Console.WriteLine("Equation so far: " + Infix.Print(t_n));
                // Console.WriteLine("offset: " + Infix.Print(FindOffset()));
                steps.Add(new Step(FindOffset(), "subtract and calculate offset"));
                t_n = t_n + FindOffset();
                steps.Add(new Step(t_n, "add that offset to the t_n expression"));
                //steps.Add(new Step(t_n, "boom, we now have a full expression for n in a linear sequence"));
                //   Console.WriteLine("Answer: " + Infix.Print(t_n));
                //     Console.Write("Sequence as calculated: ");
                int na = 1;
                StringBuilder sb = new StringBuilder();
                while (na != 10)
                {
                    sb.Append(Infix.Print(S.GetValueInSequence(t_n,t_n, na)) + ", ");
                    na++;
                }
                steps.Add(new Step(null, "Double check: " + sb.ToString()));
                steps.Add(new Step(t_n, ""));
                //       Console.Write(Environment.NewLine);
                //     Console.ReadKey();
                //   Console.WriteLine("Steps");
                //foreach (var item in steps)
                {
                    //     Console.WriteLine(item);
                }

                return steps;
            }
            catch (Exception e)
            {
                Console.WriteLine("BANANA Error " + e.Message + ":");
                Console.WriteLine(e);
                Console.WriteLine(e.InnerException);
                return null;
            }
            
        }
    }
}
