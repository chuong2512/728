using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ViewBase<T> : MonoBehaviour where T : ViewBase<T>
{
	private static T _instance;

	private static bool _Exists_k__BackingField;

	protected static GameObject prefGo;

	public static T Instance
	{
		get
		{
			return ViewBase<T>._instance;
		}
	}

	public static bool Exists
	{
		get;
		private set;
	}

	public static void OpenView(string path)
	{
		if (ViewBase<T>.Instance == null)
		{
			ViewBase<T>.prefGo = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load(path) as GameObject);
			ViewBase<T>.prefGo.AddComponent<T>();
			return;
		}
		if (ViewBase<T>.Instance.gameObject.activeSelf)
		{
			ViewBase<T>.Instance.gameObject.SetActive(true);
		}
	}

	protected virtual void Awake()
	{
		if (ViewBase<T>._instance == null)
		{
			ViewBase<T>._instance = (T)((object)this);
			ViewBase<T>.Exists = true;
			return;
		}
		if (ViewBase<T>._instance != this)
		{
			UnityEngine.Debug.Log(ViewBase<T>._instance);
			throw new InvalidOperationException("Can't have two instances of a view");
		}
	}
}
