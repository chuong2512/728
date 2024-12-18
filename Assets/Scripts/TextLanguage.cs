using System;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class TextLanguage : MonoBehaviour
{
	private Text m_text;

	private string m_DefaultTxt = "";

	public string m_ChineseTxt = "";

	public string m_EnglishTxt = "";

	public string m_EstonianTxt = "";

	public string m_FrenchTxt = "";

	public string m_GermanTxt = "";

	private void Start()
	{
		this.m_text = base.gameObject.GetComponent<Text>();
		if (this.m_text != null)
		{
			this.m_DefaultTxt = this.m_text.text;
		}
		this.changeTxt();
		this.InitEventListener();
	}

	private void OnDestroy()
	{
		this.RemoveEventListener();
	}

	public void InitEventListener()
	{
		bool arg_05_0 = LanguageManager.m_canChange;
	}

	private void RemoveEventListener()
	{
		bool arg_05_0 = LanguageManager.m_canChange;
	}

	public void changeTxt()
	{
		if (Singleton<LanguageManager>.Instance.getLanguage().Equals("English"))
		{
			this.doChange(this.m_EnglishTxt);
			return;
		}
		if (Singleton<LanguageManager>.Instance.getLanguage().Equals("Chinese"))
		{
			this.doChange(this.m_ChineseTxt);
			return;
		}
		if (Singleton<LanguageManager>.Instance.getLanguage().Equals("Estonian"))
		{
			this.doChange(this.m_EstonianTxt);
			return;
		}
		if (Singleton<LanguageManager>.Instance.getLanguage().Equals("French"))
		{
			this.doChange(this.m_FrenchTxt);
			return;
		}
		if (Singleton<LanguageManager>.Instance.getLanguage().Equals("German"))
		{
			this.doChange(this.m_GermanTxt);
		}
	}

	private void doChange(string str)
	{
		if (str == null || str.Length <= 0)
		{
			return;
		}
		if (this.m_text != null)
		{
			this.m_text.text = str;
		}
	}
}
