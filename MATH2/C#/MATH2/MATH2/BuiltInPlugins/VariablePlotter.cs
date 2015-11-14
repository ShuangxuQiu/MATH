using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MATH2.BuiltInPlugins
{
    public class VariablePlotter: IMathPlugin
    {
        public List<object> argvals = new List<object>();
        public List<Arg> GetArgs()
        {
            List<Arg> a = new List<Arg>();
            int no = 0;
            a.Add(new Arg("variable to substitute", "x"));
            a.Add(new Arg("times", 10));
            a.Add(new Arg("start", 0));
            a.Add(new Arg("increment", 1));
            return a;
        }
        public void UpdateArgs(System.Windows.Forms.Control.ControlCollection clist)
        {
            List<object> a = new List<object>();
            foreach (var item in clist)
            {
                a.Add(Arg.GetValue(item));
                //Console.WriteLine(Arg.GetValue(item));
            }
            try
            {
                argvals = a;//.ToArray();
            }
            catch { Console.WriteLine("err"); }
            //*
            Console.WriteLine("-----------------------");
            foreach (var item in argvals)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-----------------------");
            Console.WriteLine(argvals[0]);
            //*/
        }
        public int GetProb(string raw, MathNet.Symbolics.Expression e)
        {
            
            return -99;
        }
        public List<MATH2.Step> Solve(string raw, MathNet.Symbolics.Expression e)
        {
            //*
            Console.WriteLine("-----------------------");
            foreach (var item in argvals)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-----------------------");
            //*/
             MathNet.Symbolics.Expression seq = MathNet.Symbolics.Infix.ParseOrThrow(raw);
             int na =// 1//
                 Arg.convert(argvals[2])
                 ;
            List<Step> r = new List<Step>();
            Console.WriteLine("How many numbers to substitute x");
            StringBuilder sb = new StringBuilder();
           // int no = int.Parse(Console.ReadLine());
            int no= Arg.convert(argvals[1])
                ;
            while (na != no)
            {
                r.Add(
                    new Step(
                        null,
                // sb.Append(
                    MathNet.Symbolics.Infix.Print(
                  //  MATH2.Utils.Sequences.GetValueInSequence(e, MathNet.Symbolics.Expression.Symbol("x"), na)
                  MathNet.Symbolics.Structure.Substitute(MathNet.Symbolics.Expression.Symbol(
                  Arg.convert(argvals[0])
                //  "x"
                  ), na, seq)
                    )
                   // +", "
                   )
                    );
                na += //1//
                    Arg.convert(argvals[3])
                    ; 
            }
            
            r.Add(new Step(null, sb.ToString()));
            return r;
        }
    }
}
