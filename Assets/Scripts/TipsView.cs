using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class TipsView : MonoBehaviour
{
	[Serializable]
	private sealed class __c
	{
		public static readonly TipsView.__c __9 = new TipsView.__c();

		public static AndroidControl.Callback __9__13_1;

		public static AndroidControl.Callback __9__13_2;

		internal void _clickAdGetMoney_b__13_1()
		{
		}

		internal void _clickAdGetMoney_b__13_2()
		{
		}
	}

	public int m_tipTp;

	public int m_rwInd;

	public int m_rwNum;

	public Transform m_tipsBg1;

	public Transform m_tipsBg2;

	public Text m_moneyTxt;

	public Image m_playerImg;

	private void Start()
	{
	}

	public void WinDoScale1()
	{
		this.m_tipsBg1.gameObject.SetActive(true);
		Vector3 endValue = new Vector3(1f, 1f, 1f);
		float duration = 0.3f;
		this.m_tipsBg1.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		this.m_tipsBg1.DOScale(endValue, duration).SetEase(Ease.OutBack);
	}

	public void WinDoScale2()
	{
		this.m_tipsBg2.gameObject.SetActive(true);
		Vector3 endValue = new Vector3(1f, 1f, 1f);
		float duration = 0.3f;
		this.m_tipsBg2.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		this.m_tipsBg2.DOScale(endValue, duration).SetEase(Ease.OutBack);
	}

	public void showView()
	{
		base.transform.gameObject.SetActive(true);
		this.m_tipsBg1.gameObject.SetActive(false);
		this.m_tipsBg2.gameObject.SetActive(false);
		if (this.m_tipTp == 0)
		{
			this.WinDoScale1();
			this.m_playerImg.sprite = ResourcesLoad.Load<Sprite>(TurnView.m_ballPics[this.m_rwInd]);
		}
		else
		{
			this.WinDoScale2();
			this.m_moneyTxt.text = string.Concat(this.m_rwNum);
		}
		Singleton<GameManager>.Instance.OnPause();
	}

	public void clickBack()
	{
		base.transform.gameObject.SetActive(false);
	}

	public void clickGetMoney()
	{
		this.clickBack();
		Singleton<GameManager>.Instance.addCoins(this.m_rwNum);
		Singleton<GameManager>.Instance.OnPause();
	}

	public void clickAdGetMoney()
	{
        /*
		AdControl arg_54_0 = ControlsBase<AdControl>.Instance;
		string arg_54_1 = "3";
		AndroidControl.Callback arg_54_2 = delegate
		{
			this.clickBack();
			Singleton<GameManager>.Instance.addCoins(this.m_rwNum * 3);
			ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "激励-奖励三倍");
			Singleton<GameManager>.Instance.OnPause();
		};
		AndroidControl.Callback arg_54_3;
		if ((arg_54_3 = TipsView.__c.__9__13_1) == null)
		{
			arg_54_3 = (TipsView.__c.__9__13_1 = new AndroidControl.Callback(TipsView.__c.__9._clickAdGetMoney_b__13_1));
		}
		AndroidControl.Callback arg_54_4;
		if ((arg_54_4 = TipsView.__c.__9__13_2) == null)
		{
			arg_54_4 = (TipsView.__c.__9__13_2 = new AndroidControl.Callback(TipsView.__c.__9._clickAdGetMoney_b__13_2));
		}
		arg_54_0.ShowRwAd(arg_54_1, arg_54_2, arg_54_3, arg_54_4);

        */

        if (AdsControl.Instance.GetRewardAvailable())
        {
           // AdsControl.Instance.PlayDelegateRewardVideo(delegate
            {
                this.clickBack();
                Singleton<GameManager>.Instance.addCoins(this.m_rwNum * 3);
                ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "激励-奖励三倍");
                Singleton<GameManager>.Instance.OnPause();
            };

        }
    }

	public void clickGetPlayer()
	{
		Singleton<GameManager>.Instance.addBallSkin(this.m_rwInd);
		Singleton<GameManager>.Instance.OnPause();
		this.clickBack();
	}

	private void Update()
	{
	}
}
