/****************************************

* 作者：闪电黑客
* 日期：2024/6/25 11:34

* 描述：

*/
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WorldTree.Analyzer
{
	/// <summary>
	/// 局部方法命名规范诊断器
	/// </summary>
	public abstract class LocalMethodNamingDiagnostic<C> : NamingDiagnosticBase<C>
		where C : ProjectDiagnosticConfig, new()
	{
		public override SyntaxKind DeclarationKind => SyntaxKind.LocalFunctionStatement;

		protected override void DiagnosticAction(SyntaxNodeAnalysisContext context)
		{
			if (!TryGetDiagnosticConfigGroup(context.Compilation.AssemblyName, out List<DiagnosticConfigGroup> DiagnosticGroups)) return;
			// 获取语义模型
			SemanticModel semanticModel = context.SemanticModel;
			LocalFunctionStatementSyntax? localFunction = context.Node as LocalFunctionStatementSyntax;
			foreach (DiagnosticConfigGroup objectDiagnostic in DiagnosticGroups)
			{
				if (objectDiagnostic.Diagnostics.TryGetValue(DiagnosticKey.LocalMethodNaming, out DiagnosticConfig codeDiagnostic))
				{
					// 需要的修饰符
					if (TreeSyntaxHelper.SyntaxKindContains(localFunction.Modifiers, codeDiagnostic.KeywordKinds))
					{
						// 不需要检查的修饰符
						if (!TreeSyntaxHelper.SyntaxKindContainsAny(localFunction.Modifiers, codeDiagnostic.UnKeywordKinds, false))
						{
							//检查方法名
							if (!codeDiagnostic.Check.Invoke(semanticModel, localFunction.Identifier))
							{
								context.ReportDiagnostic(Diagnostic.Create(codeDiagnostic.Diagnostic, localFunction.GetLocation(), localFunction.Identifier.Text));
							}
							else if (codeDiagnostic.NeedComment.Invoke(semanticModel, localFunction.Identifier) && !TreeSyntaxHelper.CheckComment(localFunction))
							{
								context.ReportDiagnostic(Diagnostic.Create(codeDiagnostic.Diagnostic, localFunction.GetLocation(), localFunction.Identifier.Text));
							}
						}
					}
					return;
				}
			}
		}
	}

	public abstract class LocalMethodNamingProvider<C> : NamingCodeFixProviderBase<LocalFunctionStatementSyntax, C>
		where C : ProjectDiagnosticConfig, new()
	{
		public override SyntaxKind DeclarationKind => SyntaxKind.LocalFunctionStatement;
		protected override async Task<Document> CodeFix(DiagnosticConfig codeDiagnostic, Document document, LocalFunctionStatementSyntax decl, CancellationToken cancellationToken)
		{
			var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
			var newRoot = root;
			var fieldName = decl.Identifier.Text;
			fieldName = codeDiagnostic.FixCode?.Invoke(fieldName);

			// 创建新的字段名并替换旧的字段名
			var newFieldDecl = decl.WithIdentifier(SyntaxFactory.Identifier(fieldName));
			newRoot = newRoot.ReplaceNode(decl, newFieldDecl);

			// 返回包含修改的文档
			return document.WithSyntaxRoot(newRoot);
		}
	}
}