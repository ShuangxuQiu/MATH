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
        public bool initialshown = false;
        private bool shown = true;
        private bool doo = false;
        public bool needoffset = true;
        public bool stepnotevisible = true;
        private bool first = true;
        public StepUC(string tex)
        {
            InitializeComponent();
            SetStepText(tex);
            
        }
        public void InstantSet(bool visible)
        {
            if (visible)
            {
                panel1.Size = new Size(flowLayoutPanel1.Location.X,panel1.Size.Height);
            }
            else
            {
                if (!needoffset)
                {
                    panel1.Size = new Size(this.Size.Width, panel1.Size.Height);
                }
                else
                {
                    panel1.Size = new Size(this.Size.Width, panel1.Size.Height);
                }
            }
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
           // Console.WriteLine("foo");
        }

        private void StepUC_Click(object sender, EventArgs e)
        {
         //   Console.WriteLine("click");
         //   shown = !shown;
            if (stepnotevisible)
            {
                doo = true;
                timer1.Start();
            }
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
            if (needoffset)
            {
                this.Width = this.Parent.Size.Width - 23;
            }
            else
            {
                this.Width = this.Parent.Size.Width;
            }
            if (first)
            {
                InstantSet(initialshown);
                shown = initialshown;
                first = false;
                StepUC_Click(null, null);
            }
        }

        private void StepUC_Load(object sender, EventArgs e)
        {
        //    InstantSet(initialshown);
          //  shown=initialshown;
        }
    }
}
