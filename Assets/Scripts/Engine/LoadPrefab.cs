using System;

namespace Engine
{
	public class LoadPrefab
	{
		private LoadOverCall m_onLoadOver;

		private object m_callBackData;

		public LoadPrefab(string resPath, object data = null, LoadOverCall callback = null)
		{
			this.m_onLoadOver = callback;
			this.m_callBackData = data;
			AssetLoadManager.Instance.LoadAssetObject(resPath, new AssetLoadManager.AssetLoadOverCall(this.CallBackLoadPrefab));
		}

		public void CallBackLoadPrefab(AssetLoadData data)
		{
			if (this.m_onLoadOver != null)
			{
				this.m_onLoadOver(data.m_assetObject, this.m_callBackData);
			}
		}
	}
}
