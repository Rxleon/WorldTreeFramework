﻿/****************************************

* 作者：闪电黑客
* 日期：2024/4/10 19:27

* 描述：

*/

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace WorldTree.SourceGenerator
{
	internal class NodeCallRuleGenerator
	{
		public static void Execute(GeneratorExecutionContext context)
		{
			int argumentCount = RuleGeneratorSetting.argumentCount;
			StringBuilder Code = new StringBuilder();
			Code.AppendLine(
@$"/****************************************
* 节点执行调用法则
*/
"
);
			Code.AppendLine("namespace WorldTree");
			Code.AppendLine("{");
			Code.AppendLine("	public static partial class NodeRuleHelper");
			Code.Append("	{");

			for (int i = 0; i <= argumentCount; i++)
			{
				string genericsType = GeneratorTemplate.GenericsTypes[i];
				string genericsTypeAfter = GeneratorTemplate.GenericsTypesAfter[i];
				string genericParameter = GeneratorTemplate.GenericsParameter[i];
				string genericTypeParameter = GeneratorTemplate.GenericsTypeParameter[i];
				Code.AppendLine
				($$"""

						/// <summary>
						/// 尝试执行调用法则
						/// </summary>
						public static bool TryCallRule<R{{genericsType}}, OutT>(INode self, R nullRule{{genericTypeParameter}}, out OutT outT)
							where R : ICallRule<{{genericsTypeAfter}}OutT>
						{
							if (self.Core.RuleManager.TryGetRuleList(self.Type, out IRuleList<R> ruleList))
							{
								ruleList.Call(self{{genericParameter}}, out outT);
								return true;
							}
							outT = TypeInfo<OutT>.Default;
							return false;
						}

						/// <summary>
						/// 执行调用法则
						/// </summary>
						public static OutT CallRule<N, R{{genericsType}}, OutT>(N self, R nullRule{{genericTypeParameter}}, out OutT outT)
							where N : class, INode, AsRule<R>
							where R : ICallRule<{{genericsTypeAfter}}OutT>
						{
							if (self.Core.RuleManager.TryGetRuleList(self.Type, out IRuleList<R> ruleList))
							{
								ruleList.Call(self{{genericParameter}}, out outT);
								return outT;
							}
							outT = TypeInfo<OutT>.Default;
							return outT;
						}

						/// <summary>
						/// 尝试执行调用法则
						/// </summary>
						public static bool TryCallsRule<R{{genericsType}}, OutT>(INode self, R nullRule{{genericTypeParameter}}, out UnitList<OutT> outT)
							where R : ICallRule<{{genericsTypeAfter}}OutT>
						{
							if (self.Core.RuleManager.TryGetRuleList(self.Type, out IRuleList<R> ruleList))
							{
								ruleList.Calls(self{{genericParameter}}, out outT);
								return true;
							}
							outT = null;
							return true;
						}

						/// <summary>
						/// 执行调用法则
						/// </summary>
						public static UnitList<OutT> CallsRule<N, R{{genericsType}}, OutT>(N self, R nullRule{{genericTypeParameter}}, out UnitList<OutT> outT)
							where N : class, INode, AsRule<R>
							where R : ICallRule<{{genericsTypeAfter}}OutT>
						{
							if (self.Core.RuleManager.TryGetRuleList(self.Type, out IRuleList<R> ruleList))
							{
								ruleList.Calls(self{{genericParameter}}, out outT);
								return outT;
							}
							outT = null;
							return outT;
						}
				""");
			}
			Code.AppendLine("	}");
			Code.Append("}");

			context.AddSource("NodeRuleHelperCallRule.cs", SourceText.From(Code.ToString(), Encoding.UTF8));
		}
	}
}