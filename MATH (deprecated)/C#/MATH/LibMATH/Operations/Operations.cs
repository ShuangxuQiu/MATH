using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMATH.Operations
{
    public class Operations
    {
        public static void Add(params Term[] vars)
        {
            Dictionary<string, List<Term>> symsorted = new Dictionary<string, List<Term>>();
            foreach (Term variable in vars)
            {
                if (!symsorted.ContainsKey(variable.symbol))
                {
                    //make a new list
                    symsorted.Add(variable.symbol, new List<Term>());
                }
                symsorted[variable.symbol].Add(variable);
            }
            foreach (var item in symsorted)
            {
                AddLike(item.Value.ToArray());
            }
        }
        public static void AddLike(params Term[] vars)
        {
            Dictionary<double, List<Term>> powersorted = new Dictionary<double, List<Term>>();
            Dictionary<double, Term> output = new Dictionary<double, Term>();
                   //   pow     var
            foreach (Term variable in vars)
            {
                if (!powersorted.ContainsKey(variable.power))
                {
                    //make a new list
                    powersorted.Add(variable.power, new List<Term>());
                }
                    powersorted[variable.power].Add(variable);
            }

            foreach (var pair in powersorted)
            {
                Console.WriteLine(pair.Key + "====================");
                double power = pair.Key;
                double totcoefficient = 0;
                foreach (var var in pair.Value)
                {
                    totcoefficient += var.coeficient;
                    Console.WriteLine(var);
                }
                Console.WriteLine("================================");
                Term tot = new Term();
                try
                {
                    tot.symbol = pair.Value[0].symbol;
                }
                catch { tot.symbol = "x"; }
                tot.power = pair.Key;
                tot.coeficient = totcoefficient;
                //tot.symvalue = pair.Value[0].symvalue;
                output.Add(power, tot);
            }
            foreach (var item in output)
            {
                Console.WriteLine(item.Value);
            }
        }
    }
}
