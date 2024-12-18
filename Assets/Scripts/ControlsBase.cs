using System;
using UnityEngine;

public class ControlsBase<T> where T : new()
{
	protected static T _instance = default(T);

	private static UnityEngine.Object _objLock = new UnityEngine.Object();

	public static T Instance
	{
		get
		{
			if (ControlsBase<T>._instance == null)
			{
				UnityEngine.Object objLock = ControlsBase<T>._objLock;
				lock (objLock)
				{
					if (ControlsBase<T>._instance == null)
					{
						ControlsBase<T>._instance = Activator.CreateInstance<T>();
					}
				}
			}
			return ControlsBase<T>._instance;
		}
	}

	protected ControlsBase()
	{
	}

	public static void Depose()
	{
		ControlsBase<T>._instance = default(T);
	}
}
