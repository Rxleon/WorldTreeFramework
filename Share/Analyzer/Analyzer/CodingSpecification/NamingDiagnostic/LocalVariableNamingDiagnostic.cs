﻿/****************************************

* 作者：闪电黑客
* 日期：2024/6/25 11:27

* 描述：

*/
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Generic;
using System.Composition;
using System.Threading;
using System.Threading.Tasks;
using WorldTree.SourceGenerator;

namespace WorldTree.Analyzer
{


	/// <summary>
	/// 局部变量命名规范诊断器
	/// </summary>
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public class LocalVariableNamingDiagnostic : NamingDiagnosticBase
	{
		public override SyntaxKind DeclarationKind => SyntaxKind.LocalDeclarationStatement;

		protected override void DiagnosticAction(SyntaxNodeAnalysisContext context)
		{
			if (!ProjectDiagnosticSetting.ProjectDiagnostics.TryGetValue(context.Compilation.AssemblyName, out List<DiagnosticConfigGroup> objectDiagnostics)) return;
			// 获取语义模型
			SemanticModel semanticModel = context.SemanticModel;
			LocalDeclarationStatementSyntax? localDeclaration = context.Node as LocalDeclarationStatementSyntax;
			foreach (DiagnosticConfigGroup objectDiagnostic in objectDiagnostics)
			{
				if (!objectDiagnostic.Diagnostics.TryGetValue(DiagnosticKey.LocalVariableNaming, out DiagnosticConfig codeDiagnostic)) continue;
				// 需要的修饰符
				if (!TreeSyntaxHelper.SyntaxKindContains(localDeclaration.Modifiers, codeDiagnostic.KeywordKinds)) continue;
				// 不需要检查的修饰符
				if (TreeSyntaxHelper.SyntaxKindContainsAny(localDeclaration.Modifiers, codeDiagnostic.UnKeywordKinds, false)) continue;

				//检查变量名
				foreach (VariableDeclaratorSyntax variable in localDeclaration.Declaration.Variables)
				{
					if (!codeDiagnostic.Check.Invoke(variable.Identifier.Text))
					{
						context.ReportDiagnostic(Diagnostic.Create(codeDiagnostic.Diagnostic, variable.GetLocation(), variable.Identifier.Text));
					}
				}

				if (codeDiagnostic.NeedComment && !TreeSyntaxHelper.CheckComment(localDeclaration))
				{
					context.ReportDiagnostic(Diagnostic.Create(codeDiagnostic.Diagnostic, localDeclaration.GetLocation()));
				}

				return;
			}
		}
	}
	[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(LocalVariableNamingCodeFixProvider)), Shared]
	public class LocalVariableNamingCodeFixProvider : NamingCodeFixProviderBase<LocalDeclarationStatementSyntax>
	{
		public override SyntaxKind DeclarationKind => SyntaxKind.LocalDeclarationStatement;
		protected override async Task<Document> CodeFix(DiagnosticConfig codeDiagnostic, Document document, LocalDeclarationStatementSyntax decl, CancellationToken cancellationToken)
		{
			var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
			var newRoot = root;
			foreach (VariableDeclaratorSyntax variable in decl.Declaration.Variables)
			{
				var fieldName = variable.Identifier.Text;
				fieldName = codeDiagnostic.FixCode?.Invoke(fieldName);

				// 创建新的字段名并替换旧的字段名
				var newFieldDecl = variable.WithIdentifier(SyntaxFactory.Identifier(fieldName));
				newRoot = newRoot.ReplaceNode(variable, newFieldDecl);
			}

			// 返回包含修改的文档
			return document.WithSyntaxRoot(newRoot);
		}
	}

}