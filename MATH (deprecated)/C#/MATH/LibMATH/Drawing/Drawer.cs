using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace LibMATH.Drawing
{
    public class Drawing
    {
        public StringBuilder midscript;
        public StringBuilder superscript;
        public List<Drawing> over;
    }
    public class Drawer
    {
        private static List<Step> steps = new List<Step>();
        public static void AddStep(Step step)
        {
            steps.Add(step);
        }
        public static void AddStep(params Step[] step)
        {
            steps.AddRange(step);
        }
        public static string GetOperatorSymbol(Operator op)
        {
            switch (op)
            {
                case Operator.add:
                    return "+";
                case Operator.divide:
                    return "÷";
                case Operator.multiply:
                    return "x";
                case Operator.none:
                    return "";
                case Operator.sqroot:
                    return "√";
            }
            return "";
        }
        public static void DrawSteps()
        {
            foreach (var step in steps)
            {
                Console.Write("1) "+step.notes+Environment.NewLine);
                int no = 0;
                while (no < step.leftexpressions.Count)
                {
                    if (no == 0)
                    {
                        DrawExpression(step.leftexpressions.ElementAt(no).Key);
                    }
                    else
                    {
                        if (step.leftexpressions.ElementAt(no).Value == Operator.divide)
                        {
                        //    DrawDivideExpression(step.leftexpressions.ElementAt(no).Key, step.leftexpressions.ElementAt(no + 1).Key);
                      //      no++;
                        }
                        Console.Write(GetOperatorSymbol(step.leftexpressions.ElementAt(no).Value));
                        DrawExpression(step.leftexpressions.ElementAt(no).Key);

                    }
                    no++;
                }
            }
        }
        public static void DrawDivideExpression(Expression nominator, Expression denominator)
        {
          //  Console.Write("("+DrawExpression+")"+
        }
        public static Random _random = new Random();
        public static ConsoleColor GetRandomConsoleColor()
        {
            var consoleColors = Enum.GetValues(typeof(ConsoleColor));
            return (ConsoleColor)consoleColors.GetValue(_random.Next(consoleColors.Length));
        }
        public static void DrawSuperBasicSteps()
        {
            foreach (var step in steps)
            {
                List<ConsoleColor> usedcols = new List<ConsoleColor>();
                usedcols.Add(Console.ForegroundColor);
                foreach (var rexp in step.rightexpressions)
                {
                    Console.BackgroundColor = GetRandomConsoleColor();
                    while (usedcols.Contains(Console.BackgroundColor) == true)
                    {
                        Console.BackgroundColor = GetRandomConsoleColor();
                    }
                    usedcols.Add(Console.BackgroundColor);
                    Console.Write(GetOperatorSymbol(rexp.Value));
                    DrawSuperBasicExpression(rexp.Key);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if (step.equals)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("=");
                    Console.ForegroundColor = ConsoleColor.White;
                    foreach (var lexp in step.leftexpressions)
                    {
                        Console.BackgroundColor = GetRandomConsoleColor();
                        while (usedcols.Contains(Console.BackgroundColor) == true)
                        {
                            Console.BackgroundColor = GetRandomConsoleColor();
                        }
                        usedcols.Add(Console.BackgroundColor);
                      //  Console.Write(GetOperatorSymbol(lexp.Value));
                        DrawSuperBasicExpression(lexp.Key);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                Console.Write(step.notes);
                Console.Write(Environment.NewLine);
            }
        }
        public static void DrawSuperBasicExpression(Expression ex)
        {
            if (ex.coeficient > 0)
            {
                Console.Write("+");
            }
            else
            {
                Console.Write("-");
            }
            if (ex.coeficient != 1)
            {
                Console.Write(ex.coeficient);
            }
            Console.Write("(");
            int no = 0;
            foreach (var pair in ex.terms)
            {
                if (no > 0)
                {
                    Console.Write(GetOperatorSymbol(pair.Value));
                }
                    Console.Write(pair.Key);
                no++;
            }
            Console.Write(")");
            if (ex.power != 1)
            {
                Console.Write("^"+ex.power);
            }
        //    Console.Write(Environment.NewLine);
        }
        public static void DrawBasicExpression(Expression ex)
        {
            Console.WriteLine();
            Console.Write(ex.coeficient+"(");
            int top;
            int left;
            top = Console.CursorTop;
            left = Console.CursorLeft;
            foreach (var pair in ex.terms)
            {
                Drawing d = DrawTerm(pair.Key);
                Console.Write(d.midscript);
                Console.SetCursorPosition(left, top - 1);
                Console.Write(d.superscript);
                top += 2;
            }
            Console.WriteLine(")^" + ex.power);
        }
        public static List<Drawing> DrawExpression(Expression ex)
        {
            throw new InvalidOperationException();
            /*
            int no = 0;
            Console.Write(ex.coeficient+"(");
            Drawing ret = new Drawing();
            ret.midscript.Append("(");
            foreach (var pair in ex.terms)
            {
                Drawing termdrawing = DrawTerm(pair.Key);
                ret.midscript.Append(termdrawing.
                no++;
            }
            ret.midscript.Append(")");
            ret.superscript.Append(GetFill(Math.Abs(ret.midscript.Length - ret.superscript.Length), " "));
            ret.superscript.Append(ex.power);
            */
        }
        public static string GetFill(int amout, string fill)
        {
            StringBuilder sb = new StringBuilder();
            int no = amout;
            while (no != 0)
            {
                sb.Append(fill);
                no--;
            }
            return sb.ToString();
        }
        public static Drawing DrawTerm(Term term)
        {
        //   2
        //-4x 
           // int ch = 0;//obsolete
            StringBuilder midscript = new StringBuilder();
            if (term.coeficient > 0)
            {
                midscript.Append("-");
            }
            else
            {
                midscript.Append("+");
            }
            midscript.Append(term.coeficient);
            if (term.showsymbol)
            {
                midscript.Append(term.symbol);
            }
            else
            {
                try
                {
                    midscript.Append(term.symvalue);
                }
                catch { }
            }
            StringBuilder superscript = new StringBuilder();
            superscript.Append(GetFill(midscript.Length, " ") + term.power);
            List<Drawing> sub = new List<Drawing>();
            if (term.over != null)
            {
              //  sub = DrawExpression(term.over);
            }

            Drawing d = new Drawing();
            d.midscript = midscript;
            d.superscript = superscript;
            d.over = sub;
            return d;

        }
    }
}
