using StyleCop.CSharp;

namespace StyleCop.CustomRules
{
	[SourceAnalyzer(typeof(CsParser))]
	public class EnumPostfixRule : SourceAnalyzer
	{
		const string ruleName = "EnumPostfixRule";

		public override void AnalyzeDocument(CodeDocument document)
		{
			CsDocument csDocument = (CsDocument)document;
			csDocument.WalkDocument(
				new CodeWalkerElementVisitor<object>(this.VisitElement));
		}

		private bool VisitElement(CsElement element, CsElement parentElement, object context)
		{
			var enumElement = element as Enum;
			if (enumElement != null)
			{
				if (!enumElement.Name.EndsWith("Enum", System.StringComparison.Ordinal))
				{
					this.AddViolation(element, ruleName);
				}

				return false;
			}

			return true;
		}


	}
}
