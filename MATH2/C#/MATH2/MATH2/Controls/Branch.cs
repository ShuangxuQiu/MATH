using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Symbolics;

namespace MATH2.Controls
{
    public partial class Branch : UserControl
    {
        [Description("The different lists of numbers to be displayed."), Category("Appearance")]
        public List<List<Expression>> branches = new List<List<Expression>>();
       // [Description("How far apart each number is from eachother."), Category("Appearance")]
        public int spreadx = 30;
        public int spready = 30;
        public int charspace = 5;
        public List<List<Point>> bottoms = new List<List<Point>>();
        public List<List<Point>> tops = new List<List<Point>>();
        public Size lastsize;

     //   public List<Point> bottomstemp = new List<Point>();
       // public List<Point> topstemp = new List<Point>();
        public Branch()
        {
            InitializeComponent();
            List<Expression> le = new List<Expression>();
            le.AddRange(new Expression[]{Expression.Symbol("t"),Expression.Symbol("e"),
            Expression.Symbol("s"),Expression.Symbol("t")});
            branches.Add(le);
            branches.Add(le.Take(3).ToList());
        }
        private void PaintBranch(List<Expression> branch, Point point,int spreadx, PaintEventArgs e)
        {
            List<Point> topstemp = new List<Point>();
            List<Point> bottomstemp = new List<Point>();
            foreach (var item in branch)
            {
                e.Graphics.DrawString(Infix.Print(item),Font,new SolidBrush(this.ForeColor),point);
                topstemp.Add(new Point(point.X,point.Y-(charspace+Font.Height/3)));
              // Console.Write(new Point(point.X, point.Y + charspace));
                bottomstemp.Add(new Point(point.X, point.Y + (charspace + Font.Height/3)));
                //Console.WriteLine();
             //Console.Write(new Point(point.X, point.Y - charspace));
                point.X += spreadx;
            }
            tops.Add(topstemp);
            bottoms.Add(bottomstemp);
        }
        private void Branch_Paint(object sender, PaintEventArgs e)
        {
           // e.Graphics.DrawLine(new Pen(new SolidBrush(ForeColor), 2), new Point(0,5), new Point(15,35));
           // int branchno = 0;
          //  int spread = initalspread;
           // Font.Size = ((this.Size.Height+this.Size.Width)/2) / 5;
            Point point = new Point(0, 0);
            foreach (var branch in branches)
            {
                PaintBranch(branch, point, spreadx, e);
               // Console.WriteLine();
                point.Y += spready*2;
             //   spread = spread/2;
                point.X += spreadx/2;
           //     bottoms.Add(bottomstemp);
             //   tops.Add(topstemp);

              //  bottomstemp.Clear();
                //topstemp.Clear();
            }

            int no = 0;
            foreach (var bottomm in bottoms)
            {

                int na = 0;
                foreach (var bottom in bottomm)
                {
                   // Console.Write(bottom.X+","+bottom.Y);
                    try
                    {
                        Point otherpoint = tops[no + 1][na];
                     //   e.Graphics.DrawLine(new Pen(new SolidBrush(ForeColor),2), bottom, otherpoint);
                    }
                    catch { }
                    na++; 
                }
              //  Console.WriteLine();
                no++;
            }
        }

        private void Branch_Resize(object sender, EventArgs e)
        {
            try
            {
                /*
                int no1 = lastsize.Height - this.Size.Height;
                int no2 = lastsize.Width - this.Size.Width;
                int no = (no1 + no2) / 2;
                if (no < 0)
                {
                    spread += 2;
                }
                else { spread -= 1; }//*/
                spreadx = this.Size.Width / branches[0].Count;
                spready = this.Size.Height / branches[0].Count;
            }
            catch { }
            lastsize = this.Size;
        }
    }
}
