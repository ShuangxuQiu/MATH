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
        public static bool Clear = false;
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
            if (MATH2.MasterForm.Clear){Console.Clear();}
            comboBox1.Items.Clear();
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
        private void PUpdated(Arg a)
        {
      //      a.parent.UpdateArgs(flowLayoutPanel2.Controls);
        }
        private void LoadArgs(MATH2.IMathPlugin plugin)
        {
            List<Arg> args = plugin.GetArgs();
            foreach (Arg arg in args)
            {
                arg.parent = plugin;
                arg.r = new Arg.Return(PUpdated);
                //flowLayoutPanel2.Controls.Add(arg.GetControl());
                Console.WriteLine("added arg " + arg.displayname);
            }
            
        }
        private void OnTextChanged(string raw)//or whatever moooooo
        {
            List<int> probs = new List<int>();

            Expression ex = null;
            try
            {
                ex = Infix.ParseOrThrow(fastColoredTextBox1.Text);
            }
            catch { }
            try
            {
                foreach (var item in plugins)
                {
                    probs.Add(GetProb(Activator.CreateInstance(item) as MATH2.IMathPlugin, raw, ex));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            foreach (var plugin in plugins)
            {
                comboBox1.Items.Add(plugin.Name);
            }
            
            comboBox1.SelectedIndex = probs.IndexOf(probs.Max());
            comboBox1_SelectedIndexChanged(null, null);
            
            // Console.Clear();
        }

        private void MasterForm_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Using the " + plugins[comboBox1.SelectedIndex].Name + " solver to solve your question:");
            Expression ex = null;
            try
            {
                ex = Infix.ParseOrThrow(fastColoredTextBox1.Text);
            }
            catch { }
            MATH2.IMathPlugin pl = Activator.CreateInstance(plugins[comboBox1.SelectedIndex]) as MATH2.IMathPlugin;
            LoadArgs(pl);
            List<Step> steps = null;
            try
            {
                steps = pl.Solve(fastColoredTextBox1.Text, ex);
                Console.WriteLine("Answer: " + steps.Last());
            }
            catch (Exception exc)
            {
                if (MATH2.MasterForm.Clear)
                {
                    Console.Clear();
                }
                Console.WriteLine("A " + exc.GetType().Name + " error occured whilst the solver was calculating the answer.");
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
        }
    }
}
