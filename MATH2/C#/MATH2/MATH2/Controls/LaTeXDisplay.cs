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
    public partial class LaTeXDisplay : UserControl
    {
        [Description("The LaTeX string to be rendered"), Category("Appearance")]
        public string LaTex = "";
        [Description("The size of the render"), Category("Appearance")]
        public int size=100;
        [Description("Automatically change the size of the render based on control size"), Category("Appearance")]
        public bool autosize = true;
        public LaTeXDisplay()
        {
            InitializeComponent();
        }
        public LaTeXDisplay(StepData.LaTeXData data)
        {
            InitializeComponent();
            size = data.size;
            autosize = data.autosize;
            LaTex = data.tex;
            Font = data.gd.font;
            BackColor = data.gd.back;
            ForeColor = data.gd.fore;
        }
        public LaTeXDisplay(string latex)
        {
            InitializeComponent();
            LaTex=latex;
        }
        public void LoadLatex(Uri url)
        {
            pictureBox1.Load(url.AbsoluteUri);
        }
        private void CalcSize()
        {
            if (autosize)
            {
                size = (pictureBox1.Size.Height / MasterForm.resizefactor) * 10;
            }
        }
        private void LaTeXDisplay_Resize(object sender, EventArgs e)
        {
            pictureBox1.Padding = this.Padding;
            CalcSize();
            LoadLatex();
        }
        public void LoadLatex(string text)
        {
            LaTex = text;
           // CalcSize();
            LoadLatex();
        }
        public void LoadLatex(int siz)
        {
            size = siz;
            LoadLatex();
        }
        public void LoadLatex(string text, int siz)
        {
            LaTex = text;
            size = siz;
            LoadLatex();
        }
        public void LoadLatex()
        {
            string url = "";
            try
            {
                url = @"http://www.texrendr.com/cgi-bin/mathtex.cgi?\dpi{" + size.ToString() + @"}" + LaTeX.Print(Infix.ParseOrThrow(LaTex));
                //Console.WriteLine(url);
            }
            catch
            {
                url = @"http://www.texrendr.com/cgi-bin/mathtex.cgi?\dpi{" + size.ToString() +@"}" + LaTex;
            }
            pictureBox1.Load(url);
        }
    }
}
