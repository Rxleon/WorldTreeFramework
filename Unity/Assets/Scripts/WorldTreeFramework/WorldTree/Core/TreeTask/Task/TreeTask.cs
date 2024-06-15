﻿/****************************************

* 作者： 闪电黑客
* 日期： 2022/6/27 9:47

* 描述： 异步任务
* 
*/

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WorldTree.Internal;

namespace WorldTree
{
	/// <summary>
	/// 异步任务
	/// </summary>
	/// <remarks>因为继承Node所以可以挂在树上,请不要在Task身上挂东西</remarks>
	[AsyncMethodBuilder(typeof(TreeTaskMethodBuilder))]
	public class TreeTask : AwaiterBase
		, TempOf<INode>
		, AsAwake
		, AsTreeTaskSetResuIt
	{

		private async TreeTaskVoid InnerCoroutine()
		{
			await this;
		}

		/// <summary>
		/// 协程启动
		/// </summary>
		public void Coroutine()
		{
			//this.Log($"this任务[{this.Id}]");
			InnerCoroutine().Coroutine();
			this.FindSyncTaskSetCompleted();
		}
		/// <summary>
		/// 协程启动
		/// </summary>
		public void Coroutine(TreeTaskToken treeTaskToken)
		{
			//this.Log($"this任务[{this.Id}]");
			this.SetToken(treeTaskToken);
			InnerCoroutine().Coroutine();
			this.FindSyncTaskSetCompleted();
		}
	}




	/// <summary>
	/// 泛型异步任务
	/// </summary>
	/// <remarks>因为继承Node所以可以挂在树上,请不要在Task身上挂东西</remarks>
	[AsyncMethodBuilder(typeof(TreeTaskMethodBuilder<>))]
	public class TreeTask<T> : AwaiterBase<T>
		, TempOf<INode>
		, AsAwake
		, AsTreeTaskSetResuIt<T>
	{
		[DebuggerHidden]
		private async TreeTaskVoid InnerCoroutine()
		{
			await this;
		}

		/// <summary>
		/// 协程启动
		/// </summary>
		public void Coroutine()
		{
			//this.SetToken(null);
			InnerCoroutine().Coroutine();
			this.FindSyncTaskSetCompleted();
		}
		/// <summary>
		/// 协程启动
		/// </summary>
		public void Coroutine(TreeTaskToken treeTaskToken)
		{
			this.SetToken(treeTaskToken);
			InnerCoroutine().Coroutine();
			this.FindSyncTaskSetCompleted();

		}
	}

	public static class TreeTaskRule
	{

		///// <summary>
		///// 在await的同时可以换一个新的cancellationToken
		///// </summary>
		//[DebuggerHidden]
		//public static async TreeTask NewToken(this TreeTask self, TreeTaskToken treeTaskToken)
		//{
		//	self.SetToken(treeTaskToken);
		//	self.FindSyncTaskSetCompleted();
		//	await self;
		//}

		///// <summary>
		///// 在await的同时可以换一个新的cancellationToken
		///// </summary>
		//[DebuggerHidden]
		//public static async TreeTask<T> NewToken<T>(this TreeTask<T> self, TreeTaskToken treeTaskToken)
		//{
		//	self.SetToken(treeTaskToken);
		//	self.FindSyncTaskSetCompleted();
		//	return await self;
		//}

		/// <summary>
		/// 插入新令牌：可以用新令牌取消，也能被老令牌取消
		/// </summary>
		public static async TreeTask AddToken(this TreeTask self, TreeTaskToken treeTaskToken)
		{
			var token = await self.Parent.TreeTaskTokenCatch();
			token.tokenEvent.Add(treeTaskToken);
			self.SetToken(treeTaskToken);
			await self;
		}

		/// <summary>
		/// 插入新令牌：可以用新令牌取消，也能被老令牌取消
		/// </summary>
		public static async TreeTask<T> AddToken<T>(this TreeTask<T> self, TreeTaskToken treeTaskToken)
		{
			var token = await self.Parent.TreeTaskTokenCatch();
			token.tokenEvent.Add(treeTaskToken);
			self.SetToken(treeTaskToken);
			return await self;
		}

		class SetResultSendRule : TreeTaskSetResuItRule<TreeTask>
		{
			protected override void Execute(TreeTask self)
			{
				self.SetResult();
			}
		}

		class SetResultSendRule<T> : TreeTaskSetResuItRule<TreeTask<T>, T>
		{
			protected override void Execute(TreeTask<T> self, T result)
			{
				self.SetResult(result);
			}
		}
	}
}