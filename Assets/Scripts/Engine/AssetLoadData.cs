using System;
using UnityEngine;

namespace Engine
{
	public class AssetLoadData
	{
		public string m_strAssetPath = string.Empty;

		public AssetBundle m_assetBundle;

		public UnityEngine.Object m_assetObject;

		public AssetStatus m_loadedStatus;

		public void Dispose()
		{
			this.m_strAssetPath = string.Empty;
			this.m_assetObject = null;
			this.m_loadedStatus = AssetStatus.NotReady;
		}
	}
}
