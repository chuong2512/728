using System;
using UnityEngine;

namespace Engine
{
	public class AssetData
	{
		public string m_strCode = "";

		public AssetType m_assetType;

		public string m_strAssetPath;

		public bool m_bUpdateFromHTTP;

		public bool m_bCompress;

		public bool m_bEncrypt;

		public AssetBundle m_assetBundleLoad;

		public UnityEngine.Object m_mainAsset;

		public AssetStatus m_loadedStatus;

		public string GetSaveKey()
		{
			return AssetManager.Instance.GetAssetDataKey(this.m_assetType, this.m_strAssetPath);
		}

		public AssetData()
		{
			this.m_assetType = AssetType.Default;
			this.m_strAssetPath = "";
			this.m_assetBundleLoad = null;
			this.m_loadedStatus = AssetStatus.NotReady;
		}

		public bool IsSameVersion(AssetData data)
		{
			return this.m_strCode == data.m_strCode;
		}
	}
}
