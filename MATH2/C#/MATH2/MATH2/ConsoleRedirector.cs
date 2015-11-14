using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace MATH2
{
    public class ConsoleRedirector : TextWriter
    {
        private RichTextBox textbox;
          private string buf="";
        public ConsoleRedirector(RichTextBox textbox)
        {
            this.textbox = textbox;
        }
        public void ScrollToBottom()
        {
            textbox.Select(textbox.Text.Length, textbox.Text.Length);
            textbox.ScrollToCaret();
        }
        public override void Write(char value)
        {
          //  textbox.Text += value;
         //   new System.Threading.Thread(() =>
            {
                buf += value;
                if (buf.Length > 20)
                {
                    textbox.Text += buf;
                    buf = "";
                }
                ScrollToBottom();
            }
            //).Start();

        }
        public override void Flush()
        {
            textbox.Text = "";
        }

        public override void Write(string value)
        {
           // new System.Threading.Thread(() =>
            {
                buf += value;
                if (buf.Length > 20)
                {
                    textbox.Text += buf;
                    buf = "";
                }
                ScrollToBottom();
            }
            //).Start();
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }
}
