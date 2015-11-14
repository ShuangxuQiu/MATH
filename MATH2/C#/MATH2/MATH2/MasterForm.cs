using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Symbolics;
using System.Reflection;

namespace MATH2
{
    public partial class MasterForm : Form
    {
        public List<Type> plugins = new List<Type>();
        public MasterForm()
        {

            InitializeComponent();
          //  Console.SetOut(new ConsoleRedirector(consoleTextBox1));

            #region built in plugin handle
            var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == "MATH2.BuiltInPlugins"
                    select t;
            foreach (var t in q.ToList())
            {
                // Initialize(Activator.CreateInstance(t) as MATH2.IMathPlugin);
                plugins.Add(t);
            }
            #endregion
        }

        private void fastColoredTextBox1_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            try
            {
                OnTextChanged(fastColoredTextBox1.Text);
            }
            catch { }
        }
        private int GetProb(MATH2.IMathPlugin plugin, string raw, Expression e)
        {
            try
            {
                return plugin.GetProb(raw, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"[SOLVER """ + plugin.GetType().Name.ToUpper() + @""" INSTANTIATION ERROR!] " + ex.Message);
                return int.MinValue;
            }
        }
        private void OnTextChanged(string raw)//or whatever moooooo
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
                probs.Add(GetProb(Activator.CreateInstance(item) as MATH2.IMathPlugin, raw, e));
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
                Console.Clear(); Console.WriteLine("A " + exc.GetType().Name + " error occured whilst the solver was calculating the answer.");
             //   System.Threading.Thread.Sleep(2000); Console.Clear();// Main();
            }
            if (steps != null)
            {
                //Console.ReadKey();
                Console.WriteLine("Steps");
                foreach (var step in steps)
                {
                    Console.WriteLine(step);
                }
            }
            // Console.Clear();

        }
    }
}
