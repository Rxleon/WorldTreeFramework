﻿/****************************************

* 作者：闪电黑客
* 日期：2024/6/24 19:44

* 描述：

*/
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Text.RegularExpressions;
using WorldTree.SourceGenerator;

namespace WorldTree.Analyzer
{
	/// <summary>
	/// Node类型诊断
	/// </summary>
	public class NodeDiagnostic : DiagnosticGroupConfig
	{
		public override DiagnosticGroupConfig Init()
		{
			Screen = (Symbol) =>
			{
				if (Symbol is not ITypeSymbol TypeSymbol) return false;
				if (TypeSymbol.TypeKind != TypeKind.Class) return false;
				return NamedSymbolHelper.CheckInterface(TypeSymbol, "INode", out _);
			};
			SetNamingRule(DiagnosticKey.ConstNaming, new DiagnosticConfig()
			{
				Title = "Node常量命名规范诊断",
				MessageFormat = "常量命名都要大写",
				DeclarationKind = SyntaxKind.FieldDeclaration,
				KeywordKinds = new() { SyntaxKind.ConstKeyword, },
				Check = s => Regex.IsMatch(s, "^[A-Z]+(_[A-Z]+)*$"),
				FixCode = s => Regex.Replace(s, "([a-z])([A-Z])", "$1_$2").ToUpper()
			});

			SetNamingRule(DiagnosticKey.PublicFieldNaming, new DiagnosticConfig()
			{
				Title = "Node公开字段命名规范诊断",
				MessageFormat = "公开字段命名开头要大写",
				DeclarationKind = SyntaxKind.FieldDeclaration,
				Check = s => true,
				KeywordKinds = new() { SyntaxKind.PublicKeyword, },
				UnKeywordKinds = new() { SyntaxKind.ConstKeyword, },
			});
			SetNamingRule(DiagnosticKey.PrivateFieldNaming, new DiagnosticConfig()
			{
				Title = "Node私有字段命名规范诊断",
				MessageFormat = "私有字段命名开头要小写",
				DeclarationKind = SyntaxKind.FieldDeclaration,
				KeywordKinds = new() { SyntaxKind.PrivateKeyword, },
				UnKeywordKinds = new() { SyntaxKind.ConstKeyword, },
				Check = s => Regex.IsMatch(s, "^[a-z].*$"),
				FixCode = s => char.ToLower(s[0]) + s.Substring(1)
			});
			SetNamingRule(DiagnosticKey.ProtectedFieldNaming, new DiagnosticConfig()
			{
				Title = "Node保护字段命名规范诊断",
				MessageFormat = "保护字段命名开头要小写",
				DeclarationKind = SyntaxKind.FieldDeclaration,
				KeywordKinds = new() { SyntaxKind.ProtectedKeyword, },
				UnKeywordKinds = new() { SyntaxKind.ConstKeyword, },
				Check = s => Regex.IsMatch(s, "^[a-z].*$"),
				FixCode = s => char.ToLower(s[0]) + s.Substring(1)
			});
			SetNamingRule(DiagnosticKey.PublicPropertyNaming, new DiagnosticConfig()
			{
				Title = "Node公开属性命名规范诊断",
				MessageFormat = "公开属性命名开头要大写",
				DeclarationKind = SyntaxKind.PropertyDeclaration,
				KeywordKinds = new() { SyntaxKind.PublicKeyword, },
			});
			SetNamingRule(DiagnosticKey.PrivatePropertyNaming, new DiagnosticConfig()
			{
				Title = "Node私有属性命名规范诊断",
				MessageFormat = "私有属性命名开头要大写",
				DeclarationKind = SyntaxKind.PropertyDeclaration,
				KeywordKinds = new() { SyntaxKind.PrivateKeyword },
			});
			SetNamingRule(DiagnosticKey.ProtectedPropertyNaming, new DiagnosticConfig()
			{
				Title = "Node保护属性命名规范诊断",
				MessageFormat = "保护属性命名开头要大写",
				DeclarationKind = SyntaxKind.PropertyDeclaration,
				KeywordKinds = new() { SyntaxKind.ProtectedKeyword, },
			});
			SetNamingRule(DiagnosticKey.MethodNaming, new DiagnosticConfig()
			{
				Title = "Node方法命名规范诊断",
				MessageFormat = "方法命名开头要大写",
				DeclarationKind = SyntaxKind.MethodDeclaration,
			});

			return this;
		}


	}


}