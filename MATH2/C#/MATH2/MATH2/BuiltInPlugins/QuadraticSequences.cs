using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Symbolics;

namespace MATH2.BuiltInPlugins
{
    public class QuadraticSequences:IMathPlugin
    {
        public List<Arg> GetArgs()
        {
            return new List<Arg>();
        }
        public void UpdateArgs(List<Arg> a)
        {
        }
        public List<Expression> sequence = new List<Expression>();
        public Expression t_n = Expression.Symbol("n");
        public List<Step> steps = new List<Step>();
        public Expression FindOffset(int atindex)
        {
            Expression cur = Utils.Sequences.GetValueInSequence(t_n, Expression.Symbol("n"), atindex);
            Expression actu = sequence[atindex-1];
          //  Console.WriteLine(Infix.Print(actu) + ":"+Infix.Print(cur));
            return actu - cur;

        }
        public List<Step> Solve(string raw, Expression e)
        {
            sequence = Utils.Sequences.ParseSequence(raw);
            Expression seconddifference = Utils.Sequences.GetDifference(Utils.Sequences.GetDifference(sequence)).Last();
            steps.Add(new Step(seconddifference, "get the second difference"));
            t_n = t_n * t_n;
            steps.Add(new Step(t_n, "must be n^2 in quadratic sequence"));
            t_n = t_n * ((seconddifference / 2));
            steps.Add(new Step(t_n, "Half the second difference and multiply n^2 with it"));
          //  Console.WriteLine(Infix.Print(FindOffset(1)));
            t_n += FindOffset(1);
            steps.Add(new Step(FindOffset(1), "work out the offset with our current expression and the given sequence"));
            steps.Add(new Step(t_n, "add the offset to our expression"));
            int na = 1;
            StringBuilder sb = new StringBuilder();
            while (na != 10)
            {
                sb.Append(Infix.Print(Utils.Sequences.GetValueInSequence(t_n, t_n, na)) + ", ");
                na++;
            }
            steps.Add(new Step(null, "Double check: " + sb.ToString()));
            steps.Add(new Step(t_n, ""));
            return steps;
        }
        public int GetProb(string raw, Expression e)
        {
         //   throw new StackOverflowException();
            //*
            sequence = Utils.Sequences.ParseSequence(raw);
            if (Utils.Sequences.CalculateEqualizationDifNo(sequence) == 2)
            {
                return 100;
            }
            else
            {
                return -100;
            }
            //*/
        }
    }
}
