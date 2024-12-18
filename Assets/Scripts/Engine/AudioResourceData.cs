using System;

namespace Engine
{
	public class AudioResourceData
	{
		public string m_strID;

		public int m_iType;

		public float m_fVolume = 1f;

		public string m_strResourcePath;

		public float m_fDelay;

		public AudioResourceData(ReadCsvTools data, int col)
		{
			this.m_strID = data.GetDataByRowAndName(col, "UniqueID");
			this.m_strResourcePath = data.GetDataByRowAndName(col, "ResourcePath");
			this.m_iType = int.Parse(data.GetDataByRowAndName(col, "AudioType"));
			this.m_fDelay = float.Parse(data.GetDataByRowAndName(col, "DelayPlay"));
		}
	}
}
