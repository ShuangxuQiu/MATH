using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MATH2
{
    public class ConsoleRedirector:TextWriter
    {
        private MATH2.ConsoleTextBox textbox;
        public ConsoleRedirector(MATH2.ConsoleTextBox textbox)
    {
        this.textbox = textbox;
    }

    public override void Write(char value)
    {
        textbox.Write(value.ToString());
    }

    public override void Write(string value)
    {
        textbox.Write(value);
    }

    public override Encoding Encoding
    {
        get { return Encoding.ASCII; }
    }
    }
}
