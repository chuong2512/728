using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class WinView : MonoBehaviour
{
	[Serializable]
	private sealed class __c
	{
		public static readonly WinView.__c __9 = new WinView.__c();

		public static AndroidControl.Callback __9__21_1;

		public static AndroidControl.Callback __9__21_2;

		public static AndroidControl.Callback __9__22_0;

		public static AndroidControl.Callback __9__22_1;

		public static AndroidControl.Callback __9__24_1;

		public static AndroidControl.Callback __9__24_2;

		internal void _adBtn_b__21_1()
		{
		}

		internal void _adBtn_b__21_2()
		{
		}

		internal void _getBtn_b__22_0()
		{
		}

		internal void _getBtn_b__22_1()
		{
		}

		internal void _clickSkin_b__24_1()
		{
		}

		internal void _clickSkin_b__24_2()
		{
		}
	}

	public Transform m_effBg;

	public Transform m_bg0;

	public Transform m_bg1;

	public Transform m_bg;

	public Transform m_view;

	public int m_coins;

	public Text m_coinTxt;

	public int m_tp;

	public Image m_SkinBtn;

	public TipsView m_TipsView;

	public int m_skinInd;

	public int m_gameMode;

	public Transform m_ScaleView1;

	public Transform m_ScaleView2;

	public Transform m_ScaleView3;

	public Transform m_ScaleView4;

	public Transform m_ScaleView5;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void showView()
	{
		this.m_effBg.gameObject.SetActive(true);
		base.transform.gameObject.SetActive(true);
		AudioManager.PlayEffectAudio("win", false, false);
		this.m_coinTxt.text = "+" + this.m_coins;
		this.m_skinInd = Singleton<GameManager>.Instance.randBallSkin();
		this.m_SkinBtn.gameObject.SetActive(false);
		if (this.m_skinInd > 0)
		{
			this.m_SkinBtn.gameObject.SetActive(true);
		}
		this.playEf();
	}

	public void playEf()
	{
		this.m_ScaleView1.localScale = Vector3.zero;
		this.m_ScaleView2.localScale = Vector3.zero;
		this.m_ScaleView3.localScale = Vector3.zero;
		this.m_ScaleView4.localScale = Vector3.zero;
		this.m_ScaleView5.localScale = Vector3.zero;
		Sequence expr_55 = DOTween.Sequence();
		expr_55.Insert(0f, this.m_ScaleView1.DOScale(1f, 0.2f));
		expr_55.Insert(0.05f, this.m_ScaleView2.DOScale(1f, 0.2f));
		expr_55.Insert(0.1f, this.m_ScaleView3.DOScale(1f, 0.2f));
		expr_55.Insert(0.15f, this.m_ScaleView4.DOScale(1f, 0.2f));
		expr_55.Insert(0.2f, this.m_ScaleView5.DOScale(1f, 0.2f));
	}

	public void adBtn()
	{
        /*
		AdControl arg_54_0 = ControlsBase<AdControl>.Instance;
		string arg_54_1 = "3";
		AndroidControl.Callback arg_54_2 = delegate
		{
			if (this.m_gameMode == 0)
			{
				ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "联赛胜利奖励");
			}
			else
			{
				ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "杯赛胜利奖励");
			}
			Singleton<MissionManager>.Instance.addMissionVal(2, 1);
			Singleton<GameManager>.Instance.addCoins(this.m_coins * 3);
			this.clickClose();
		};
		AndroidControl.Callback arg_54_3;
		if ((arg_54_3 = WinView.__c.__9__21_1) == null)
		{
			arg_54_3 = (WinView.__c.__9__21_1 = new AndroidControl.Callback(WinView.__c.__9._adBtn_b__21_1));
		}
		AndroidControl.Callback arg_54_4;
		if ((arg_54_4 = WinView.__c.__9__21_2) == null)
		{
			arg_54_4 = (WinView.__c.__9__21_2 = new AndroidControl.Callback(WinView.__c.__9._adBtn_b__21_2));
		}
		arg_54_0.ShowRwAd(arg_54_1, arg_54_2, arg_54_3, arg_54_4);
		*/

        if (AdsControl.Instance.GetRewardAvailable())
        {
            //AdsControl.Instance.PlayDelegateRewardVideo(delegate
            {
                if (this.m_gameMode == 0)
                {
                    ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "联赛胜利奖励");
                }
                else
                {
                    ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "杯赛胜利奖励");
                }
                Singleton<MissionManager>.Instance.addMissionVal(2, 1);
                Singleton<GameManager>.Instance.addCoins(this.m_coins * 3);
                this.clickClose();
            };

        }
    }

	public void getBtn()
	{
		this.clickClose();
		Singleton<GameManager>.Instance.addCoins(this.m_coins);
		if (this.m_gameMode == 0)
		{
			ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "联赛胜利插屏");
		}
		else
		{
			ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "杯赛胜利插屏");
		}
		Singleton<GameManager>.Instance.OnPause();
        /*
		AdControl arg_9B_0 = ControlsBase<AdControl>.Instance;
		string arg_9B_1 = "3";
		AndroidControl.Callback arg_9B_2;
		if ((arg_9B_2 = WinView.__c.__9__22_0) == null)
		{
			arg_9B_2 = (WinView.__c.__9__22_0 = new AndroidControl.Callback(WinView.__c.__9._getBtn_b__22_0));
		}
		AndroidControl.Callback arg_9B_3;
		if ((arg_9B_3 = WinView.__c.__9__22_1) == null)
		{
			arg_9B_3 = (WinView.__c.__9__22_1 = new AndroidControl.Callback(WinView.__c.__9._getBtn_b__22_1));
		}
		arg_9B_0.ShowIntAd(arg_9B_1, arg_9B_2, arg_9B_3);
		*/
        AdsControl.Instance.showAds();      
	}

	public void clickClose()
	{
		MainMenuView.m_this.initGame();
		if (this.m_gameMode == 0)
		{
			Singleton<MissionManager>.Instance.addMissionVal(0, 1);
			MainMenuView.m_this.showNext();
			Singleton<GameManager>.Instance.m_UserInfo.m_missInd++;
		}
		else
		{
			MainMenuView.m_this.m_MatchView.m_MatchCupView.matchResult(true);
		}
		base.transform.gameObject.SetActive(false);
		this.m_effBg.gameObject.SetActive(false);
	}

	public void clickSkin()
	{
        /*
		AdControl arg_54_0 = ControlsBase<AdControl>.Instance;
		string arg_54_1 = "3";
		AndroidControl.Callback arg_54_2 = delegate
		{
			if (this.m_gameMode == 0)
			{
				ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "联赛胜利皮肤-奖励");
			}
			else
			{
				ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "杯赛胜利皮肤-奖励");
			}
			this.m_SkinBtn.gameObject.SetActive(false);
			this.m_TipsView.m_tipTp = 0;
			this.m_TipsView.m_rwInd = this.m_skinInd;
			this.m_TipsView.showView();
		};
		AndroidControl.Callback arg_54_3;
		if ((arg_54_3 = WinView.__c.__9__24_1) == null)
		{
			arg_54_3 = (WinView.__c.__9__24_1 = new AndroidControl.Callback(WinView.__c.__9._clickSkin_b__24_1));
		}
		AndroidControl.Callback arg_54_4;
		if ((arg_54_4 = WinView.__c.__9__24_2) == null)
		{
			arg_54_4 = (WinView.__c.__9__24_2 = new AndroidControl.Callback(WinView.__c.__9._clickSkin_b__24_2));
		}
		arg_54_0.ShowRwAd(arg_54_1, arg_54_2, arg_54_3, arg_54_4);
		*/
        if (AdsControl.Instance.GetRewardAvailable())
        {
           
                if (this.m_gameMode == 0)
                {
                    ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "联赛胜利皮肤-奖励");
                }
                else
                {
                    ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "杯赛胜利皮肤-奖励");
                }
                this.m_SkinBtn.gameObject.SetActive(false);
                this.m_TipsView.m_tipTp = 0;
                this.m_TipsView.m_rwInd = this.m_skinInd;
                this.m_TipsView.showView();
           

        }
    }
}
