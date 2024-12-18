using System;
using UnityEngine;
using Util;

public class LocalDataManager : Singleton<LocalDataManager>
{
	protected new void Init()
	{
	}

	public string getStringValue(string key, string defaultVal)
	{
		return PlayerPrefs.GetString(key, defaultVal);
	}

	public void setStringValue(string key, string val)
	{
		PlayerPrefs.SetString(key, val);
	}

	public int getIntValue(string key, int defaultVal)
	{
		return PlayerPrefs.GetInt(key, defaultVal);
	}

	public void setIntValue(string key, int val)
	{
		PlayerPrefs.SetInt(key, val);
	}

	public float getFloatValue(string key, float defaultVal)
	{
		return PlayerPrefs.GetFloat(key, defaultVal);
	}

	public void setFloatgValue(string key, float val)
	{
		PlayerPrefs.SetFloat(key, val);
	}

	public bool getBoolValue(string key, bool defaultVal)
	{
		int num = 0;
		if (defaultVal)
		{
			num = 1;
		}
		num = PlayerPrefs.GetInt(key, num);
		return num == 1;
	}

	public void setBoolValue(string key, bool val)
	{
		int value = 0;
		if (val)
		{
			value = 1;
		}
		PlayerPrefs.SetInt(key, value);
	}
}
