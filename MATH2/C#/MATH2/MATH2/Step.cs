using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MATH2
{
    public class Step
    {
        public MathNet.Symbolics.Expression expression;
        public string notes;
       // public StepData.DataType dt = StepData.DataType.LaTeX;
        public object data;
        public StepData.DataType datatype = StepData.DataType.LaTeX;
        public Step() { }
        public Step(MathNet.Symbolics.Expression ex, string s) 
        {
            expression = ex;
            notes = s;
        }
        public Step(MathNet.Symbolics.Expression ex, string s, object dataa, StepData.DataType dt)
        {
            expression = ex;
            notes = s;
            data = dataa;
            datatype = dt;
        }
        public Step(MathNet.Symbolics.Expression ex, string s, object dataa)
        {
            expression = ex;
            notes = s;
            data = dataa;
            if (dataa.GetType() == typeof(StepData.LaTeXData))
            {
                datatype = StepData.DataType.LaTeX;
            }
            if (dataa.GetType() == typeof(StepData.GeneralData))
            {
                datatype = StepData.DataType.General;
            }
            if (dataa.GetType() == typeof(StepData.BranchData))
            {
                datatype = StepData.DataType.Branch;
            }
        }
        public Step(MathNet.Symbolics.Expression ex, string s, string latextext)
        {
            expression = ex;
            notes = s;
            //if (gendata)
            {
                StepData.LaTeXData ld = new StepData.LaTeXData(latextext);
                this.datatype = StepData.DataType.LaTeX;
            }
        }
        public Step(MathNet.Symbolics.Expression ex, string s, MathNet.Symbolics.Expression latextext)
        {
            expression = ex;
            notes = s;
            //if (gendata)
            {
                StepData.LaTeXData ld = new StepData.LaTeXData(MathNet.Symbolics.LaTeX.Print(latextext));
                this.datatype = StepData.DataType.LaTeX;
                this.data=ld;
            }
        }
        public Controls.StepUC GetControl()
        {
            Controls.StepUC UC = new Controls.StepUC(this.notes);
            UC.Append(data, datatype);
           // UC.Dock = System.Windows.Forms.DockStyle.Top;
            UC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            return UC;
        }
        public override string ToString()
        {
            try
            {
                if (string.IsNullOrEmpty(notes) == false)
                {
                    return MathNet.Symbolics.Infix.Print(expression) + " | " + notes;
                }
                else
                {
                    return MathNet.Symbolics.Infix.Print(expression);
                }
            }
            catch
            {
                try
                {
                    return notes;
                }
                catch {return ""; }
            }
        }
    }
}
