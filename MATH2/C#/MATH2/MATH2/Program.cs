using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Symbolics;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Text;
using Expr = MathNet.Symbolics.Expression;


namespace MATH2
{
    static class Program
    {
       // [STAThread]
        private static List<Type> plugins = new List<Type>();
        [STAThread]
        static void Main()
        {
            //Expr e = Infix.ParseOrThrow(Console.ReadLine()) + Infix.ParseOrThrow(Console.ReadLine());
           // Console.WriteLine(Infix.Print(e));
           // System.Windows.Forms.Clipboard.SetText(LaTeX.Print(e));
          //  Console.WriteLine("Enter sequence");
            //BuiltInPlugins.LinearEquations p = new BuiltInPlugins.LinearEquations();
            //string raw = Console.ReadLine();
            //p.GetProb(raw, null);
            //p.Solve(raw, null);
            
            new MasterForm().ShowDialog();
            /*
            string s = Console.ReadLine();
            /*
            try
            {
                OnTextChanged(s);
            }
            catch (Exception e)
            {
                Console.WriteLine("Plugin Error " + e.Message+":");
                Console.WriteLine(e);
                Console.WriteLine(e.InnerException);
            }
             
            //Console.Clear();
                  Console.ReadKey();
                  Console.Clear();
            Main();
            //*/

        }
        private static void OnTextChanged(string raw)//or whatever moooooo
        {
            List<int> probs = new List<int>();
            
            Expression e = null;
            try
            {
                e = Infix.ParseOrThrow(raw);
            }
            catch { }
            foreach (var item in plugins)
            {
                probs.Add(GetProb(Activator.CreateInstance(item) as MATH2.IMathPlugin,raw,e));
            }
            MATH2.IMathPlugin pl = Activator.CreateInstance(plugins[probs.IndexOf(probs.Max())]) as MATH2.IMathPlugin;
            Console.WriteLine("Using the " + plugins[probs.IndexOf(probs.Max())].Name + " solver to solve your question:");
            List<Step> steps = null;
            try
            {
            steps = pl.Solve(raw, e);
                Console.WriteLine("Answer: " + steps.Last());
            }
            catch (Exception exc)
            {
                if (MATH2.MasterForm.Clear)
                {
                    Console.Clear();
                } Console.WriteLine("A " + exc.GetType().Name + " error occured whilst the solver was calculating the answer.");
                System.Threading.Thread.Sleep(2000); if (MATH2.MasterForm.Clear)
                {
                    Console.Clear();
                } Main();
            }
            if (steps != null)
            {
             //   Console.ReadKey();
                Console.WriteLine("Steps");
                foreach (var step in steps)
                {
                    Console.WriteLine(step);
                }
            }
           // Console.Clear();

        }
        private static int GetProb(MATH2.IMathPlugin plugin, string raw, Expression e)
        {
            try
            {
                return plugin.GetProb(raw, e);
            }
            catch(Exception ex )
            {
                Console.WriteLine(@"[SOLVER """+plugin.GetType().Name.ToUpper()+@""" INSTANTIATION ERROR!] "+ex.Message);
                return int.MinValue;
            }
        }
    }
}
