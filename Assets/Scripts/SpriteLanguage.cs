using System;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class SpriteLanguage : MonoBehaviour
{
	private Image m_image;

	private SpriteRenderer m_render;

	private Sprite m_DefaultSp;

	public Sprite m_ChineseSp;

	public Sprite m_EnglishSp;

	public Sprite m_EstonianSp;

	public Sprite m_FrenchSp;

	public Sprite m_GermanSp;

	private void Start()
	{
		this.m_image = base.gameObject.GetComponent<Image>();
		this.m_render = base.gameObject.GetComponent<SpriteRenderer>();
		if (this.m_image != null)
		{
			this.m_DefaultSp = this.m_image.sprite;
		}
		else if (this.m_render != null)
		{
			this.m_DefaultSp = this.m_render.sprite;
		}
		this.changeSp();
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

	public void changeSp()
	{
		if (Singleton<LanguageManager>.Instance.getLanguage().Equals("English"))
		{
			this.doChange(this.m_EnglishSp);
			return;
		}
		if (Singleton<LanguageManager>.Instance.getLanguage().Equals("Chinese"))
		{
			this.doChange(this.m_ChineseSp);
			return;
		}
		if (Singleton<LanguageManager>.Instance.getLanguage().Equals("Estonian"))
		{
			this.doChange(this.m_EstonianSp);
			return;
		}
		if (Singleton<LanguageManager>.Instance.getLanguage().Equals("French"))
		{
			this.doChange(this.m_FrenchSp);
			return;
		}
		if (Singleton<LanguageManager>.Instance.getLanguage().Equals("German"))
		{
			this.doChange(this.m_GermanSp);
		}
	}

	private void doChange(Sprite sp)
	{
		if (sp == null)
		{
			return;
		}
		if (this.m_image != null)
		{
			this.m_image.overrideSprite = sp;
			this.m_image.SetNativeSize();
			return;
		}
		if (this.m_render != null)
		{
			this.m_render.sprite = sp;
		}
	}
}
