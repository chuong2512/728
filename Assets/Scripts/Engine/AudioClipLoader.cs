using System;
using UnityEngine;

namespace Engine
{
	public class AudioClipLoader
	{
		public static void LoadAudioClip(string path, LoadOverCall callback = null, object data = null)
		{
			if (AssetLoadManager.Instance.HasDicAssetData(path))
			{
				new LoadAudioClip(path, data, callback);
				return;
			}
			UnityEngine.Object obj = ResourcesLoad.Load(path);
			if (callback != null)
			{
				callback(obj, data);
			}
		}
	}
}
