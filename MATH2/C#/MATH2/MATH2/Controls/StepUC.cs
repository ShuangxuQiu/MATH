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
        private bool shown = true;
        private bool doo = false;
        public StepUC(string tex)
        {
            InitializeComponent();
            SetStepText(tex);
        }
        public void SetStepText(string foo)
        {
            this.StepText.Text = foo;
        }
        public T convert<T>(object data)
        {
            return (T)Convert.ChangeType(data,typeof(T));
        }
        public void Append(object data, StepData.DataType dt)
        {
            if (dt == StepData.DataType.Branch)
            {
                Display.Controls.Add(new Branch(convert<StepData.BranchData>(data)));
            }
            if (dt == StepData.DataType.LaTeX)
            {
                Display.Controls.Add(new LaTeXDisplay(convert<StepData.LaTeXData>(data)));
            }
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

        private void StepUC_Paint(object sender, PaintEventArgs e)
        {
            this.Width = this.Parent.Size.Width;
        }
    }
}
