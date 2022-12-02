using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MusicEventBinaryTree
{
	class EventNode
	{
		public MusicEvent InvokeEvent;

		public EventNode RightChild;

		public EventNode(MusicEvent invokeEvent = null, EventNode rightChild = null)
		{
			InvokeEvent = invokeEvent;
			RightChild = rightChild;
		}
	}
	
	class TimelineNode
	{
		public float InvokeTime;

		public TimelineNode LeftChild;
		public EventNode RightChild;

		public TimelineNode(float invokeTime = 0.0f, TimelineNode leftChild = null, EventNode rightChild = null)
		{
			InvokeTime = invokeTime;
			LeftChild = leftChild;
			RightChild = rightChild;
		}
	}

	private TimelineNode root;

	private TimelineNode executeNode;

	private float minInvokeTime;

	private BroGameModeBase gameModeBase;

	private static int ComparationBettwen(MusicEvent a, MusicEvent b)
	{
		if (a.BeginTime < b.BeginTime) return -1;
		if (Mathf.Abs(a.BeginTime - b.BeginTime) < BasicDatas.FloatTolerance) return 0;
		return 1;
	}
	
	public MusicEventBinaryTree(MusicEvent[] musicEventsArray, BroGameModeBase gameModeBase)
	{
		this.gameModeBase = gameModeBase;
		minInvokeTime = gameModeBase.PlayTime;
		
		List<MusicEvent> musicEventsList = new List<MusicEvent>(musicEventsArray.Length);
		foreach (var eve in musicEventsArray)
		{
			if (eve.BeginTime >= minInvokeTime)
			{
				musicEventsList.Add(eve);
			}
		}
		musicEventsList.Sort(ComparationBettwen);
		
		//AdvancedDebug.LogWarning(musicEventsList.Count);
		if (musicEventsList.Count == 0)
		{
			return;
		}

		root = new TimelineNode(musicEventsList[0].BeginTime, null, new EventNode(musicEventsList[0]));
		executeNode = root;
		
		TimelineNode currNode = root;
		EventNode rightTemp = currNode.RightChild;
		for (int i = 1; i < musicEventsList.Count; i++)
		{
			var eve = musicEventsList[i];
			if (eve.BeginTime == currNode.InvokeTime)
			{
				if (rightTemp == null)
				{
					currNode.RightChild = new EventNode(eve);
					rightTemp = currNode.RightChild;
				}
				else
				{
					rightTemp.RightChild = new EventNode(eve);
					rightTemp = rightTemp.RightChild;
				}
			}
			else
			{
				rightTemp = new EventNode(eve);
				currNode.LeftChild = new TimelineNode(eve.BeginTime, null, rightTemp);
				currNode = currNode.LeftChild;
			}
		}

		/*for (var nd = root; nd != null; nd = nd.LeftChild)
		{
			string display = "";
			for (var rc = nd.RightChild; rc != null; rc = rc.RightChild)
			{
				display += $"{rc.InvokeEvent.BeginTime},";
				//AdvancedDebug.Log();
			}
			AdvancedDebug.Log(display);
		}*/
		//AdvancedDebug.Log("root=" + root.InvokeTime);
	}

	private TimelineNode InsertTimelineNode(TimelineNode parent, float invokeTime)
	{
		var child = parent.LeftChild;
		var result = new TimelineNode(invokeTime, child, null);
		parent.LeftChild = result;
		return result;
	}

	private void Invoke(TimelineNode node)
	{
		if (node == null)
		{
			Debug.LogError("node == null");
			
		}
		for (var right = node.RightChild; right != null; right = right.RightChild)
		{
			if (right.InvokeEvent != null)
			{
				right.InvokeEvent.BeginPlay();
			}
		}
	}

	public MusicProperty Property;

	public async void BeginExecute()
	{
		Debug.Assert(root.InvokeTime >= 0.0f);

		//AdvancedDebug.LogWarning($"{executeNode.InvokeTime}	{minInvokeTime}");
		//AdvancedDebug.LogError(executeNode.InvokeTime - minInvokeTime);
		//AdvancedDebug.Log(minInvokeTime);
		await UniTask.Delay(TimeSpan.FromSeconds(executeNode.InvokeTime - minInvokeTime));
		Invoke(executeNode);
		while (executeNode.LeftChild != null)
		{
			//AdvancedDebug.LogWarning($"{Property.ToBeat(executeNode.LeftChild.InvokeTime)}");
			//AdvancedDebug.LogWarning(executeNode.LeftChild.InvokeTime - gameModeBase.PlayTime);
			if (executeNode.LeftChild.InvokeTime >= gameModeBase.PlayTime - Time.deltaTime * 2)
			{
				AdvancedDebug.LogWarning($"执行:{executeNode.LeftChild.InvokeTime - gameModeBase.PlayTime + Time.deltaTime * 2}");
				await UniTask.Delay(TimeSpan.FromSeconds(executeNode.LeftChild.InvokeTime - gameModeBase.PlayTime + Time.deltaTime * 2));
				executeNode = executeNode.LeftChild;
				Invoke(executeNode);
			}
			else
			{
				AdvancedDebug.LogWarning($"跳过:{executeNode.LeftChild.InvokeTime - gameModeBase.PlayTime + Time.deltaTime * 2}");
				executeNode = executeNode.LeftChild;
			}
			
		}
	}
}
