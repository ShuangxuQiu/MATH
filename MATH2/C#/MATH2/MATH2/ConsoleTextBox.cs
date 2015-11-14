using FastColoredTextBoxNS;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace MATH2
{
    public class ConsoleTextBox : FastColoredTextBox
    {
        private volatile bool isReadLineMode;

        private volatile bool isUpdating;

        private Place StartReadPlace
        {
            get;
            set;
        }

        public bool IsReadLineMode
        {
            get
            {
                return this.isReadLineMode;
            }
            set
            {
                this.isReadLineMode = value;
            }
        }

        public void Write(string text)
        {
            this.IsReadLineMode = false;
            this.isUpdating = true;
            try
            {
              //  this.AppendText(text);
                this.Text += text;
                base.GoEnd();
            }
            finally
            {
                this.isUpdating = false;
                base.ClearUndo();
            }
        }

        public string ReadLine()
        {
            base.GoEnd();
            this.StartReadPlace = base.Range.End;
            this.IsReadLineMode = true;
            try
            {
                while (this.IsReadLineMode)
                {
                    Application.DoEvents();
                    Thread.Sleep(5);
                }
            }
            finally
            {
                this.IsReadLineMode = false;
                base.ClearUndo();
            }
            return new Range(this, this.StartReadPlace, base.Range.End).Text.TrimEnd(new char[]
			{
				'\r',
				'\n'
			});
        }

        public override void OnTextChanging(ref string text)
        {
            if (!this.IsReadLineMode && !this.isUpdating)
            {
                text = "";
            }
            else
            {
                if (this.IsReadLineMode)
                {
                    if (base.Selection.Start < this.StartReadPlace || base.Selection.End < this.StartReadPlace)
                    {
                        base.GoEnd();
                    }
                    if ((base.Selection.Start == this.StartReadPlace || base.Selection.End == this.StartReadPlace) && text == "\b")
                    {
                        text = "";
                        return;
                    }
                    if (text != null && text.Contains('\n'))
                    {
                        text = text.Substring(0, text.IndexOf('\n') + 1);
                        this.IsReadLineMode = false;
                    }
                }
                base.OnTextChanging(ref text);
            }
        }
    }
}
