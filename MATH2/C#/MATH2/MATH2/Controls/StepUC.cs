using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MATH2.Controls
{
    public partial class StepUC : UserControl
    {
        public bool shown = true;
        public bool doo = false;
        public StepUC()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void StepUC_Click(object sender, EventArgs e)
        {
         //   Console.WriteLine("click");
         //   shown = !shown;
            doo = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (doo)
            {
              //  Console.WriteLine("tick");
                int increment = 1;
                if (!shown)
                {
                    if (Math.Abs(panel1.Size.Width - flowLayoutPanel1.Location.X) > 10)
                    {
                        increment = 5;
                    }
                    if (panel1.Size.Width != flowLayoutPanel1.Location.X - 2)
                    {
                        panel1.Size = new Size(panel1.Size.Width - increment, panel1.Size.Height);
                    }
                    else
                    {
                        doo = false;
                        shown = !shown;
                    //    Console.WriteLine("shown = " + shown);
                    }
                }
                else
                {
                    if (panel1.Size.Width != this.Size.Width)
                    {
                        if (Math.Abs(panel1.Size.Width - this.Size.Width) > 10)
                        {
                            increment = 5;
                        }
                        panel1.Size = new Size(panel1.Size.Width + increment, panel1.Size.Height);
                    }
                    else
                    {
                        doo = false;
                        shown = !shown;
                       // Console.WriteLine("shown = " + shown);
                    }
                }
            }
        }

        private void flowLayoutPanel1_Click(object sender, EventArgs e)
        {
            StepUC_Click(sender, e);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            StepUC_Click(sender, e);
        }

        private void Display_Click(object sender, EventArgs e)
        {
            StepUC_Click(sender, e);
        }
    }
}
