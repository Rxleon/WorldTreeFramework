﻿/****************************************

* 作者：闪电黑客
* 日期：2024/10/15 15:02

* 描述：

*/
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace WorldTree.SourceGenerator
{
	[Generator]
	internal class TreeDataSerializeGenerator : ISourceGenerator
	{

		public static Dictionary<INamedTypeSymbol, int> TypeFieldsCountDict = new Dictionary<INamedTypeSymbol, int>();


		public void Initialize(GeneratorInitializationContext context)
		{
			context.RegisterForSyntaxNotifications(() => new FindTreeDataSyntaxReceiver());
		}
		public void Execute(GeneratorExecutionContext context)
		{
			TypeFieldsCountDict.Clear();
			foreach (var tree in context.Compilation.SyntaxTrees)
			{
				var semanticModel = context.Compilation.GetSemanticModel(tree);
				var root = tree.GetRoot();
				var classDeclarations = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
				foreach (var classDeclaration in classDeclarations)
				{
					var classSymbol = semanticModel.GetDeclaredSymbol(classDeclaration) as INamedTypeSymbol;
					// 查找特性并获取类型的 INamedTypeSymbol 和 int 值
					FindTreeDataSpecialAttribute(classSymbol);
				}
			}

			if (!(context.SyntaxReceiver is FindTreeDataSyntaxReceiver receiver and not null)) return;
			StringBuilder Code = new StringBuilder();
			StringBuilder ClassCode = new StringBuilder();
			foreach (var TypeListItem in receiver.TypeDeclarationsDict)
			{
				Code.Clear();
				ClassCode.Clear();
				string? Namespace = null;
				string? Usings = null;
				string fileName = TypeListItem.Key;
				if (TypeListItem.Value.Count != 0)
				{
					var classDeclaration = TypeListItem.Value[0];
					Namespace ??= TreeSyntaxHelper.GetNamespace(classDeclaration);
					Usings ??= TreeSyntaxHelper.GetUsings(classDeclaration);
				}

				foreach (TypeDeclarationSyntax typeDeclaration in TypeListItem.Value)
				{
					TreeDataSerializePartialClassGenerator.Execute(context, ClassCode, typeDeclaration);
				}
				if (ClassCode.Length == 0) return;
				Code.AppendLine(
@$"/****************************************
* 生成序列化部分
*/
"
	);
				Code.AppendLine(Usings);
				Code.AppendLine($"namespace {Namespace}");
				Code.AppendLine("{");
				Code.Append(ClassCode.ToString());
				Code.Append("}");


				context.AddSource($"{fileName}TreeDataSerialize.cs", SourceText.From(Code.ToString(), Encoding.UTF8));
			}
		}


		private static void FindTreeDataSpecialAttribute(INamedTypeSymbol classSymbol)
		{
			// 1. 直接查找类上的特性
			foreach (var attribute in classSymbol.GetAttributes())
			{
				if (attribute.AttributeClass?.Name == GeneratorHelper.TreeDataSpecialAttribute &&
					attribute.ConstructorArguments.Length == 1 &&
					attribute.ConstructorArguments[0].Value is int intValue)
				{
					var baseType = classSymbol.BaseType;
					if (baseType != null && baseType.TypeArguments.Length > 1)//&& baseType.Name == "TreeDataSerializeRule"
					{
						var genericType = baseType.TypeArguments[0] as INamedTypeSymbol;
						if (genericType != null)
						{
							// 处理泛型类型和特性参数
							if (!TypeFieldsCountDict.ContainsKey(genericType))
								TypeFieldsCountDict.Add(genericType, intValue);
						}
					}
				}
			}
		}
	}

	/// <summary>
	/// 查找序列化标记类型
	/// </summary>
	public class FindTreeDataSyntaxReceiver : ISyntaxReceiver
	{
		public Dictionary<string, List<TypeDeclarationSyntax>> TypeDeclarationsDict = new();

		public void OnVisitSyntaxNode(SyntaxNode node)
		{
			// 判断是否是类或结构体或接口
			if (node is ClassDeclarationSyntax or StructDeclarationSyntax or InterfaceDeclarationSyntax)
			{
				var TypeDeclaration = node as TypeDeclarationSyntax;
				if (TreeSyntaxHelper.CheckAttribute(TypeDeclaration, GeneratorHelper.TreeDataSerializableAttribute))
				{
					string fileName = Path.GetFileNameWithoutExtension(TypeDeclaration.SyntaxTree.FilePath);
					if (!TypeDeclarationsDict.TryGetValue(fileName, out var list))
					{
						list = new();
						TypeDeclarationsDict.Add(fileName, list);
					}
					list.Add(TypeDeclaration);
				}
			}
		}
	}
}
