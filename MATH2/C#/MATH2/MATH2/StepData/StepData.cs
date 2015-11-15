using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MATH2.StepData
{
    public enum DataType
    {
        General,
        Branch,
        LaTeX,
    }
    public class GeneralData
    {
        public Font font = new Font("Arial", 9, FontStyle.Regular);
        public Color fore = Color.WhiteSmoke;
        public Color back = Color.DimGray;
    }
    public class BranchData
    {
        public BranchData(List<List<MathNet.Symbolics.Expression>> l,bool Default=true)
        {
            branches = l;
            if (Default)
            {
                gd.back = Color.WhiteSmoke;
                gd.fore = Color.Black;
                gd.font = new Font("Arial", 14, FontStyle.Regular);
            }
        }
        public List<List<MathNet.Symbolics.Expression>> branches = new List<List<MathNet.Symbolics.Expression>>();
        public GeneralData gd = new GeneralData();
    }
    public class LaTeXData
    {
        public string tex = "";
        public bool autosize = true;
        public int size = 100;
        public GeneralData gd = new GeneralData();
        public LaTeXData(string s,bool Default=true)
        {
            if(Default)
            {
                gd.back = Color.WhiteSmoke;
                gd.fore = Color.Black;
                gd.font=new Font("Arial", 9, FontStyle.Regular);
            }
            tex = s;
        }
    }
}
