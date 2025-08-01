﻿/****************************************

* 作者：闪电黑客
* 日期：2024/4/15 19:45

* 描述：

*/

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace WorldTree.SourceGenerator
{
	internal static class RuleExecutorSendRuleGeneratorHelper
	{
		public static void Execute(GeneratorExecutionContext context)
		{
			int argumentCount = ProjectGeneratorSetting.ArgumentCount;
			StringBuilder Code = new StringBuilder();
			Code.AppendLine(
@$"/****************************************
* 执行器执行通知法则
*/
"
);
			Code.AppendLine("namespace WorldTree");
			Code.AppendLine("{");
			Code.AppendLine("	public static class RuleExecutorSendRule");
			Code.Append("	{");

			for (int i = 0; i <= argumentCount; i++)
			{
				string genericsType = GeneratorTemplate.GenericsTypes[i];
				string genericsTypeAngle = GeneratorTemplate.GenericsTypesAngle[i];
				string genericParameter = GeneratorTemplate.GenericsParameter[i];
				string genericTypeParameter = GeneratorTemplate.GenericsTypeParameter[i];
				Code.AppendLine(
					$$"""

							/// <summary>
							/// 执行器执行通知法则
							/// </summary>
							public static void Send<R{{genericsType}}>(this IRuleExecutor<R> selfExecutor{{genericTypeParameter}})
								where R : ISendRule{{genericsTypeAngle}}
							{
								IRuleExecutorEnumerable self = (IRuleExecutorEnumerable)selfExecutor;
								if (!self.IsActive) return;
								self.RefreshTraversalCount();
								for (int i = 0; i < self.TraversalCount; i++)
								{
									if (self.TryDequeue(out INode node, out RuleList ruleList))
									{
										((IRuleList<R>)ruleList).Send(node{{genericParameter}});
									}
								}
							}

							/// <summary>
							/// 执行器执行异步通知法则
							/// </summary>
							public static async TreeTask SendAsync<R{{genericsType}}>(this IRuleExecutor<R> selfExecutor{{genericTypeParameter}})
								where R : ISendRuleAsync{{genericsTypeAngle}}
							{
								IRuleExecutorEnumerable self = (IRuleExecutorEnumerable)selfExecutor;
								if (!self.IsActive)
								{
									await self.TreeTaskCompleted();
									return;
								}
								self.RefreshTraversalCount();
								if (self.TraversalCount == 0)
								{
									await self.TreeTaskCompleted();
									return;			
								}
								for (int i = 0; i < self.TraversalCount; i++)
								{
									if (self.TryDequeue(out INode node, out RuleList ruleList))
									{
										await ((IRuleList<R>)ruleList).SendAsync(node{{genericParameter}});
									}
									else
									{
										await self.TreeTaskCompleted();
									}
								}
							}
					""");
			}
			Code.AppendLine("	}");
			Code.Append("}");

			context.AddSource("RuleExecutorSendRule.cs", SourceText.From(Code.ToString(), Encoding.UTF8));
		}
	}
}