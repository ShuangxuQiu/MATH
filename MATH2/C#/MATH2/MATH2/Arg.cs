
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MATH2
{
    public enum ArgType
    {
        integer,
        number,
        boolean,
        text,
    }
    public class Arg
    {
        public static dynamic GetValue(object thing)
        {
            if (thing.GetType() == typeof(NumericUpDown))
            {
                NumericUpDown c = (NumericUpDown)thing;
                return c.Value;
            }
            if (thing.GetType() == typeof(CheckBox))
            {
                CheckBox c = (CheckBox)thing;
                return c.CheckState;
            }
            if (thing.GetType() == typeof(TextBox))
            {
                TextBox t = (TextBox)thing;
                return t.Text;
            }
            return thing;
        }
        public static dynamic convert(object thing)
        {
            return Convert.ChangeType(thing, thing.GetType());
        }
        public delegate void Return(Arg a);
        public Return r;
        public IMathPlugin parent;
        public ArgType type = ArgType.text;
        public object defaultvalue = null;
        public object value = null;
        public string displayname = "option";

        public void Update(object dowiufasliudgvqwlufikjvdsalibgkljgkqligwoklbgkiadgslbj, EventArgs qwiuegolqiuaskjgbkliugkqwkiulefuqfwigguiqwgiu)
        {
            try
            {
                r.Invoke(this); 
            }
            catch { }
        }
        public System.Windows.Forms.Control GetControl()
        {
            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Dock = DockStyle.Top;
            panel.Size = new System.Drawing.Size(140, 20);
            Label l = new Label();
            l.Text = displayname;
            panel.Controls.Add(l);

            System.Drawing.Size s = new System.Drawing.Size(70,20);

            try
            {
                if (type == ArgType.text)
                {
                    TextBox t = new TextBox();
                    t.Size = s;
                    t.Text = (string)value;
                    panel.Controls.Add(t);
                    t.TextChanged += new EventHandler(Update);
                }
                if (type == ArgType.number)
                {
                    NumericUpDown n = new NumericUpDown();
                    n.Size = s;
                    n.DecimalPlaces = 2;
                    n.Increment = 0.25m;
                    n.Value = (decimal)value;
                    n.Maximum = decimal.MaxValue;
                    n.Minimum = decimal.MinValue;
                    n.ValueChanged += new EventHandler(Update);
                    panel.Controls.Add(n);
                }
                if (type == ArgType.integer)
                {
                    NumericUpDown n = new NumericUpDown();
                    n.Size = s;
                    n.Value = (decimal)value;
                    n.Increment = 1;
                    n.Maximum = int.MaxValue;
                    n.Minimum = int.MinValue;
                    n.ValueChanged += new EventHandler(Update);
                    panel.Controls.Add(n);
                }
                if (type == ArgType.boolean)
                {
                    CheckBox c = new CheckBox();
                    c.Size = s;
                    c.Checked = (bool)value;
                    c.CheckedChanged += new EventHandler(Update);
                    panel.Controls.Add(c);
                }
            }
            catch (Exception e)
            {
                Label a = new Label();
                a.Text = e.GetType().Name+" error when loading arg";
            }
            return panel;
        }
        public Arg() { }
        public Arg(string name, string defaultval)
        {
            displayname = name; defaultvalue = defaultval; type = ArgType.text;
        }
        public Arg(string name, int defaultval)
        {
            displayname = name; defaultvalue = defaultval; type = ArgType.integer;
        }
        public Arg(string name, decimal defaultval)
        {
            displayname = name; defaultvalue = defaultval; type = ArgType.number;
        }
        public Arg(string name, bool defaultval)
        {
            displayname = name; defaultvalue = defaultval; type = ArgType.boolean;
        }
        public Arg(string name, object defaultval)
        {
            displayname = name; defaultvalue = defaultval;
            if (defaultval.GetType() == typeof(int))
            {
                type = ArgType.integer;
            }
            if (defaultval.GetType() == typeof(string))
            {
                type = ArgType.text;
            }
            if (defaultval.GetType() == typeof(decimal))
            {
                type = ArgType.number;
            }
            if (defaultval.GetType() == typeof(bool))
            {
                type = ArgType.boolean;
            }
        }
    }
}
