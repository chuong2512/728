using System;
using UnityEngine;
using Util;

public class SaveDataManager : Singleton<SaveDataManager>
{
	[Serializable]
	private class Data
	{
		public UserInfo UserData;

		public SigninInfo SigninData;

		public MissionInfo MissioninData;

		public MatchInfo MatchData;
	}

	private SaveDataManager.Data m_Data;

	protected override void Init()
	{
		this.m_Data = new SaveDataManager.Data();
		this.m_Data.UserData = new UserInfo();
		this.m_Data.SigninData = new SigninInfo();
		this.m_Data.MissioninData = new MissionInfo();
		this.m_Data.MatchData = new MatchInfo();
	}

	public bool ReadData()
	{
		string stringValue = Singleton<LocalDataManager>.Instance.getStringValue("SaveData", "");
		if (stringValue != "{}" && !string.IsNullOrEmpty(stringValue))
		{
			this.m_Data = JsonUtility.FromJson<SaveDataManager.Data>(stringValue);
			return true;
		}
		return false;
	}

	public void SaveDate()
	{
		JsonUtility.ToJson(this.m_Data);
		Singleton<LocalDataManager>.Instance.setStringValue("SaveData", JsonUtility.ToJson(this.m_Data));
	}

	public UserInfo getUserInfo()
	{
		return this.m_Data.UserData;
	}

	public void setUserInfo(UserInfo data)
	{
		this.m_Data.UserData = data;
	}

	public SigninInfo getSigninInfo()
	{
		return this.m_Data.SigninData;
	}

	public MissionInfo getMissionInfo()
	{
		return this.m_Data.MissioninData;
	}

	public MatchInfo getMatchInfo()
	{
		return this.m_Data.MatchData;
	}
}
