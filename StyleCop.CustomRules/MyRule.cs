using StyleCop.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleCop.CustomRules
{
    [SourceAnalyzer(typeof(CsParser))]
    class MyRule : SourceAnalyzer
    {

    }
}
