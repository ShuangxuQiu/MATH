using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MATH2
{
    public interface IMathPlugin
    {
        int GetProb(string raw,MathNet.Symbolics.Expression e);
        List<MATH2.Step> Solve(string raw,MathNet.Symbolics.Expression e);
        List<Arg> GetArgs();
        void UpdateArgs(System.Windows.Forms.Control.ControlCollection a);
    }
}
