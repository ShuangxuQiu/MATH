using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MATH
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
       // [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
          //  Application.Run(new Form1());
            //Console.WriteLine("Enter the first number then a new line then the second number");
          //  QuadraticFactorization.SplitMiddleTermMethod.FindNumber(double.Parse(Console.ReadLine()), double.Parse(Console.ReadLine()));
            LibMATH.Step s = new LibMATH.Step();
            s.equals = true;
            Dictionary<LibMATH.Term, LibMATH.Operator> t = new Dictionary<LibMATH.Term, LibMATH.Operator>();
            t.Add(new LibMATH.Term { symbol = "x", coeficient = 3 },LibMATH.Operator.none);
            t.Add(new LibMATH.Term { showsymbol = false, symvalue = 9 },LibMATH.Operator.add);
            s.rightexpressions.Add(new LibMATH.Expression { terms = t },LibMATH.Operator.multiply);

            Dictionary<LibMATH.Term, LibMATH.Operator> ta = new Dictionary<LibMATH.Term, LibMATH.Operator>();
            ta.Add(new LibMATH.Term { symbol = "y", coeficient = 2 }, LibMATH.Operator.none);
            ta.Add(new LibMATH.Term { showsymbol = false, symvalue = 50 }, LibMATH.Operator.add);
            s.rightexpressions.Add(new LibMATH.Expression { terms = ta }, LibMATH.Operator.add);
            ta.Clear();

            ta.Add(new LibMATH.Term { symvalue = 69, showsymbol = false }, LibMATH.Operator.add);
            ta.Add(new LibMATH.Term { symbol = "y", coeficient = 1, power = 3 }, LibMATH.Operator.multiply);
            s.leftexpressions.Add(new LibMATH.Expression {  terms = ta}, LibMATH.Operator.sqroot);

            s.notes = "start with this equation";
            LibMATH.Drawing.Drawer.AddStep(s);

            s = new LibMATH.Step();
            s.equals = true;
            t.Clear();
            ta.Clear();
            ta.Add(new LibMATH.Term { symbol = "y", coeficient = 2 }, LibMATH.Operator.none);
            ta.Add(new LibMATH.Term { showsymbol = false, symvalue = 50 }, LibMATH.Operator.add);
            s.rightexpressions.Add(new LibMATH.Expression { terms = ta }, LibMATH.Operator.add);

            ta.Clear();
            ta.Add(new LibMATH.Term { symbol = "y", coeficient = 69 }, LibMATH.Operator.none);
            ta.Add(new LibMATH.Term { showsymbol = false, symvalue = 400 }, LibMATH.Operator.add);
            s.leftexpressions.Add(new LibMATH.Expression { terms = ta }, LibMATH.Operator.add);

            s.notes = "now we get to our result";
            LibMATH.Drawing.Drawer.AddStep(s);

            LibMATH.Drawing.Drawer.DrawSuperBasicSteps();
            }
    }
}
