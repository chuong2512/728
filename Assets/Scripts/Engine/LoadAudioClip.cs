using System;

namespace Engine
{
	public class LoadAudioClip
	{
		private LoadOverCall m_onLoadOver;

		private object m_callBackData;

		private string m_resPath;

		public LoadAudioClip(string resPath, object data = null, LoadOverCall callback = null)
		{
			this.m_resPath = resPath;
			this.m_onLoadOver = callback;
			this.m_callBackData = data;
			AssetLoadManager.Instance.LoadAssetObject(resPath, new AssetLoadManager.AssetLoadOverCall(this.CallBackLoadAudio));
		}

		public void CallBackLoadAudio(AssetLoadData data)
		{
			if (this.m_onLoadOver != null)
			{
				this.m_onLoadOver(data.m_assetObject, this.m_callBackData);
			}
		}
	}
}
