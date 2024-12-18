using System;
using UnityEngine;

public class ResourcesLoad
{
	public static T Load<T>(string path) where T : UnityEngine.Object
	{
		T result = default(T);
		try
		{
			result = (T)((object)Resources.Load<T>(path));
		}
		catch (Exception)
		{
		}
		return result;
	}

	public static UnityEngine.Object Load(string path)
	{
		UnityEngine.Object result = null;
		try
		{
			result = Resources.Load(path);
		}
		catch (Exception)
		{
		}
		return result;
	}

	public static UnityEngine.Object Load(string path, Type systemTypeInstance)
	{
		UnityEngine.Object result = null;
		try
		{
			result = Resources.Load(path, systemTypeInstance);
		}
		catch (Exception)
		{
		}
		return result;
	}
}
