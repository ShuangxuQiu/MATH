using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace MATH2
{
    public class ConsoleRedirector:TextWriter
    {
        private RichTextBox textbox;
        public ConsoleRedirector(RichTextBox textbox)
    {
        this.textbox = textbox;
    }

    public override void Write(char value)
    {
        textbox.Text += value;
    }

    public override void Write(string value)
    {
        textbox.Text += value;
    }

    public override Encoding Encoding
    {
        get { return Encoding.ASCII; }
    }
    }
}
