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
                ControllerNameRule1(cls);
                ControllerAttributeRule(cls);


                return false;
            }

            return true;
        }

        private void ControllerNameRule1(Class element)
        {
            if (element.BaseClass == "System.Web.Mvc.Controller" && !element.Name.EndsWith("Controller", System.StringComparison.Ordinal))
            {
                this.AddViolation(element, ruleName);
            }

        }

        private void ControllerAttributeRule(Class element)
        {
            bool isController = element.BaseClass == "System.Web.Mvc.Controller";
                                               
            if (isController)
            {
                foreach (Method item in element.ChildElements.Where(i=>i is Method))
                {
                    if (item.AccessModifier == AccessModifierType.Public && item.Attributes.Where(i => i.Text == "[System.Web.Mvc.Authorize]").Count() == 0)
                    {
                        this.AddViolation(element, "ControllerAttributeRule");
                        return;
                    }
                }

                if (element.Attributes.FirstOrDefault(a => a.Text == "[System.Web.Mvc.Authorize]") != null)
                {
                    this.AddViolation(element, "ControllerAttributeRule");
                } 
                
            }
        }
    }
}
