using System;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class CupView : MonoBehaviour
{
	public Image m_leagueImg;

	private void Start()
	{
		this.reflushCup();
	}

	private void Update()
	{
	}

	public void showView()
	{
		base.gameObject.SetActive(true);
		this.reflushCup();
	}

	private void reflushCup()
	{
		int leagueLv = Singleton<GameManager>.Instance.m_UserInfo.m_leagueLv;
		if (leagueLv == 0)
		{
			this.m_leagueImg.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/lron_01");
			return;
		}
		if (leagueLv == 1)
		{
			this.m_leagueImg.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/copper");
			return;
		}
		if (leagueLv == 2)
		{
			this.m_leagueImg.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/silver");
			return;
		}
		if (leagueLv == 3)
		{
			this.m_leagueImg.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/gold");
			return;
		}
		if (leagueLv == 4)
		{
			this.m_leagueImg.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/super");
			return;
		}
		this.m_leagueImg.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/legend");
	}

	public void clickClose()
	{
		base.gameObject.SetActive(false);
		if (Singleton<GameManager>.Instance.m_UserInfo.m_missInd == 3)
		{
			MainMenuView.m_this.showSkip();
		}
	}
}
