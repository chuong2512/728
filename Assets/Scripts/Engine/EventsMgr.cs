using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Engine
{
	public class EventsMgr
	{
		public delegate void CommonEvent(object data);

		[Serializable]
		private sealed class __c
		{
			public static readonly EventsMgr.__c __9 = new EventsMgr.__c();

			public static EventsMgr.CommonEvent __9__6_0;

			internal void _AddDelegate_b__6_0(object data)
			{
			}
		}

		private static EventsMgr m_instance;

		public Dictionary<string, EventsMgr.CommonEvent> m_dicEvents = new Dictionary<string, EventsMgr.CommonEvent>();

		public static EventsMgr Instance
		{
			get
			{
				if (EventsMgr.m_instance == null)
				{
					EventsMgr.m_instance = new EventsMgr();
				}
				return EventsMgr.m_instance;
			}
		}

		public void Init()
		{
		}

		public void AddDelegate(string key)
		{
			Dictionary<string, EventsMgr.CommonEvent> arg_26_0 = this.m_dicEvents;
			EventsMgr.CommonEvent arg_26_2;
			if ((arg_26_2 = EventsMgr.__c.__9__6_0) == null)
			{
				arg_26_2 = (EventsMgr.__c.__9__6_0 = new EventsMgr.CommonEvent(EventsMgr.__c.__9._AddDelegate_b__6_0));
			}
			arg_26_0.Add(key, arg_26_2);
		}

		public void TriggerEvent(string strEventKey, object param)
		{
			if (!this.m_dicEvents.ContainsKey(strEventKey))
			{
				this.AddDelegate(strEventKey);
			}
			this.m_dicEvents[strEventKey](param);
		}

		public void AttachEvent(string strEventKey, EventsMgr.CommonEvent attachEvent)
		{
			if (!this.m_dicEvents.ContainsKey(strEventKey))
			{
				this.AddDelegate(strEventKey);
			}
			Dictionary<string, EventsMgr.CommonEvent> dicEvents = this.m_dicEvents;
			dicEvents[strEventKey] = (EventsMgr.CommonEvent)Delegate.Combine(dicEvents[strEventKey], attachEvent);
		}

		public void DetachEvent(string strEventKey, EventsMgr.CommonEvent attachEvent)
		{
			if (this.m_dicEvents.ContainsKey(strEventKey))
			{
				Delegate[] invocationList = this.m_dicEvents[strEventKey].GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					Delegate @delegate = invocationList[i];
					if (attachEvent == (EventsMgr.CommonEvent)@delegate)
					{
						Dictionary<string, EventsMgr.CommonEvent> dicEvents = this.m_dicEvents;
						dicEvents[strEventKey] = (EventsMgr.CommonEvent)Delegate.Remove(dicEvents[strEventKey], attachEvent);
					}
				}
				return;
			}
			UnityEngine.Debug.LogWarning("没有这个定义的事件:" + strEventKey);
		}
	}
}
