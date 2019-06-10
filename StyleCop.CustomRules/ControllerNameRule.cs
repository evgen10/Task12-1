using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StyleCop.CSharp;
using StyleCop;

namespace StyleCop.CustomRules
{
    [SourceAnalyzer(typeof(CsParser))]
    public class ControllerNameRule : SourceAnalyzer
    {
        const string ruleName = "ControllerNameRule";

        public override void AnalyzeDocument(CodeDocument document)
        {
            CsDocument csDocument = (CsDocument)document;
            csDocument.WalkDocument(
                new CodeWalkerElementVisitor<object>(this.VisitElemnt));
        }

        private bool VisitElemnt(CsElement element, CsElement parentElement, object context)
        {
            var cls = element as Class;           

            if (cls != null)
            {
                if (cls.BaseClass == "System.Web.Mvc.Controller" && !cls.Name.EndsWith("Controller", System.StringComparison.Ordinal))
                {
                    this.AddViolation(element, ruleName);                  

                }

                return false;
            }

            return true;
        }
    }
}
