using Engine;
using System;
using UnityEngine;

public class LoadManager
{
	public static void LoadPrefab(string path, global::LoadOverCall callback = null, object data = null)
	{
		if (AssetLoadManager.Instance.HasDicAssetData(path))
		{
			new global::LoadPrefab(path, data, callback);
			return;
		}
		UnityEngine.Object obj = ResourcesLoad.Load(path);
		if (callback != null)
		{
			callback(obj, data);
		}
	}
}
