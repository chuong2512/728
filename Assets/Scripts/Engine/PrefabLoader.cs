using System;
using UnityEngine;

namespace Engine
{
	public class PrefabLoader
	{
		public static void LoadPrefab(string path, LoadOverCall callback = null, object data = null)
		{
			if (AssetLoadManager.Instance.HasDicAssetData(path))
			{
				new LoadPrefab(path, data, callback);
			}
		}

		private static void LoadTexture(string path, LoadOverCall callback = null, object data = null)
		{
			UnityEngine.Object @object = ResourcesLoad.Load(path);
			if (@object == null)
			{
				UnityEngine.Debug.LogError("未找到此ID" + path);
			}
			if (callback != null)
			{
				callback(@object, data);
			}
		}

		private static void LoadTextureAsync(string path, LoadOverCall callback = null, object data = null)
		{
			ResourceRequest resourceRequest = Resources.LoadAsync(path);
			if (callback != null)
			{
				callback(resourceRequest.asset, data);
			}
		}
	}
}
