using System;
using UnityEngine;
using Util;

public class LanguageManager : Singleton<LanguageManager>
{
	public static bool m_canChange;

	public void init()
	{
		this.getLanguage();
	}

	public string getLanguage()
	{
		return Application.systemLanguage.ToString();
	}
}
