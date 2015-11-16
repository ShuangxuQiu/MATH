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
        public static bool Clear = true;
        public static bool ShownDefault = false;
        public static int resizefactor = 8;
        //public string lasttext="";
        public MasterForm()
        {

            InitializeComponent();
           // Console.SetOut(new ConsoleRedirector(richTextBox1));
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
          //  lasttext = fastColoredTextBox1.Text;
            if (MATH2.MasterForm.Clear){Console.Clear();}
            comboBox1.Items.Clear();
            try
            {
                OnTextChanged(fastColoredTextBox1.Text);
            }
            catch { }
            try
            {
                // url = @"http://www.texrendr.com/cgi-bin/mathtex.cgi?\dpi{" + trackBar1.Value.ToString() + @"}" + LaTeX.Print(Infix.ParseOrThrow(fastColoredTextBox1.Text));
                //Console.WriteLine(url);
                laTeXDisplay1.LoadLatex(LaTeX.Print(Infix.ParseOrThrow(fastColoredTextBox1.Text)));
            }
            catch
            {
                //url = @"http://www.texrendr.com/cgi-bin/mathtex.cgi?\dpi{" + trackBar1.Value.ToString() + @"}" + fastColoredTextBox1.Text;
                laTeXDisplay1.LoadLatex(fastColoredTextBox1.Text);
            }
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
      //  private void PUpdated(Arg a)
      //  {
      ////      a.parent.UpdateArgs(flowLayoutPanel2.Controls);
      //  }
        //private void LoadArgs(MATH2.IMathPlugin plugin)
        //{
        //    List<Arg> args = plugin.GetArgs();
        //    foreach (Arg arg in args)
        //    {
        //        arg.parent = plugin;
        //        arg.r = new Arg.Return(PUpdated);
        //        //flowLayoutPanel2.Controls.Add(arg.GetControl());
        //        Console.WriteLine("added arg " + arg.displayname);
        //    }
            
        //}
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
            if(MasterForm.Clear){
            Console.Clear();
        }
            Answers.Controls.Clear();
            this.FinalAnswer.Controls.Clear();
            Console.WriteLine("Using the " + plugins[comboBox1.SelectedIndex].Name + " solver to solve your question:");
            Expression ex = null;
            try
            {
                ex = Infix.ParseOrThrow(fastColoredTextBox1.Text);
            }
            catch { }
            MATH2.IMathPlugin pl = Activator.CreateInstance(plugins[comboBox1.SelectedIndex]) as MATH2.IMathPlugin;
            //LoadArgs(pl);
            List<Step> steps = pl.Solve(fastColoredTextBox1.Text, ex);// ;
            try
            {
               // steps = pl.Solve(fastColoredTextBox1.Text, ex);
                while (steps == null)
                {
                }
                Console.WriteLine("Answer: " + steps.Last());
                int no = steps.Count;
                bool shouldtry = true;
            tryagain:
                if(no <= 0){
                    shouldtry = false;
                }
                Console.WriteLine("trying");
                try
                {
                    MATH2.Controls.StepUC uc = steps[no].GetControl();
                    //overide these vars
                    uc.needoffset = false;
                    uc.stepnotevisible = false;
                  //  uc.BackColor = FinalAnswer.BackColor;
                
                uc.Dock = DockStyle.None;
                FinalAnswer.Controls.Add(uc);
                }
                catch
                {
                    no--;
                    if(shouldtry){
                    goto tryagain;
                    }
                }
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
                    try
                    {
                        MATH2.Controls.StepUC uc = step.GetControl();
                        uc.Dock = DockStyle.None;
                        Answers.Controls.Add(uc);
                    }
                    catch { Console.WriteLine("No display defined"); }
                }
            }
        }


        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            string url = "";
            laTeXDisplay1.size = trackBar1.Value;
            try
            {
                // url = @"http://www.texrendr.com/cgi-bin/mathtex.cgi?\dpi{" + trackBar1.Value.ToString() + @"}" + LaTeX.Print(Infix.ParseOrThrow(fastColoredTextBox1.Text));
                //Console.WriteLine(url);
                laTeXDisplay1.LoadLatex(LaTeX.Print(Infix.ParseOrThrow(fastColoredTextBox1.Text)));
            }
            catch
            {
                //url = @"http://www.texrendr.com/cgi-bin/mathtex.cgi?\dpi{" + trackBar1.Value.ToString() + @"}" + fastColoredTextBox1.Text;
                laTeXDisplay1.LoadLatex(fastColoredTextBox1.Text);
               // Console.WriteLine(char.MinValue);
              //  int no = (int)char.MinValue;
               // while (no != char.MaxValue)
                //{
                  //  char c = (char)no;
                    //System.Console.WriteLine("CharID: " + no + " | Bin: " 
                      //  + string.Join(" ", System.Text.Encoding.UTF8.GetBytes(c.ToString()).Select(byt => 
                        //    System.Convert.ToString(byt, 2).PadLeft(8, '0'))) 
                        //+ " | Display: " + c);
                    //no++;
                //}
            }
         //   laTeXDisplay1.LoadLatex(new Uri(url));
        }

        private void Answers_Paint(object sender, PaintEventArgs e)

        {
            //*
            Answers.AutoScroll = true;
            Answers.HorizontalScroll.Enabled = false;
            Answers.HorizontalScroll.Visible = false;
            //*/
        }
        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            resizefactor = 10 - (int)trackBar2.Value;
            OnTextChanged(fastColoredTextBox1.Text);
        }
    }
}
