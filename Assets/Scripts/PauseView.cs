using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PauseView : MonoBehaviour
{
	[Serializable]
	private sealed class __c
	{
		public static readonly PauseView.__c __9 = new PauseView.__c();

		public static AndroidControl.Callback __9__14_0;

		public static AndroidControl.Callback __9__14_1;

		public static AndroidControl.Callback __9__15_0;

		public static AndroidControl.Callback __9__15_1;

		public static AndroidControl.Callback __9__16_0;

		public static AndroidControl.Callback __9__16_1;

		internal void _clickRestart_b__14_0()
		{
		}

		internal void _clickRestart_b__14_1()
		{
		}

		internal void _clickQuite_b__15_0()
		{
		}

		internal void _clickQuite_b__15_1()
		{
		}

		internal void _clickGiveUp_b__16_0()
		{
		}

		internal void _clickGiveUp_b__16_1()
		{
		}
	}

	public int m_gameMode;

	public Transform m_resBtn;

	public Transform m_quiteBtn;

	public Transform m_giveupBtn;

	public Transform m_ScaleView1;

	public Transform m_ScaleView2;

	public Transform m_ScaleView3;

	public Transform m_ScaleView4;

	public Transform m_ScaleView5;

	private void Start()
	{
	}

	public void showView()
	{
		base.transform.gameObject.SetActive(true);
		this.m_resBtn.gameObject.SetActive(false);
		this.m_quiteBtn.gameObject.SetActive(false);
		this.m_giveupBtn.gameObject.SetActive(false);
		if (this.m_gameMode == 0)
		{
			this.m_resBtn.gameObject.SetActive(true);
			this.m_quiteBtn.gameObject.SetActive(true);
			return;
		}
		this.m_giveupBtn.gameObject.SetActive(true);
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
		expr_55.Insert(0.1f, this.m_ScaleView4.DOScale(1f, 0.2f));
		expr_55.Insert(0.15f, this.m_ScaleView5.DOScale(1f, 0.2f));
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			this.clickClose();
		}
	}

	public void clickClose()
	{
		Time.timeScale = 1f;
		base.gameObject.SetActive(false);
	}

	public void clickRestart()
	{
		Time.timeScale = 1f;
		base.gameObject.SetActive(false);
		MainMenuView.m_this.m_MainGameView.initGameView();
		MainMenuView.m_this.m_MainGameView.startGameView();
        /*
		AdControl arg_7C_0 = ControlsBase<AdControl>.Instance;
		string arg_7C_1 = "3";
		AndroidControl.Callback arg_7C_2;
		if ((arg_7C_2 = PauseView.__c.__9__14_0) == null)
		{
			arg_7C_2 = (PauseView.__c.__9__14_0 = new AndroidControl.Callback(PauseView.__c.__9._clickRestart_b__14_0));
		}
		AndroidControl.Callback arg_7C_3;
		if ((arg_7C_3 = PauseView.__c.__9__14_1) == null)
		{
			arg_7C_3 = (PauseView.__c.__9__14_1 = new AndroidControl.Callback(PauseView.__c.__9._clickRestart_b__14_1));
		}
		arg_7C_0.ShowIntAd(arg_7C_1, arg_7C_2, arg_7C_3);
		*/
        AdsControl.Instance.showAds();      
	}

	public void clickQuite()
	{
		Time.timeScale = 1f;
		base.gameObject.SetActive(false);
		MainMenuView.m_this.initGame();
		MainMenuView.m_this.showView();
		ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "主页插屏");
        /*
		AdControl arg_86_0 = ControlsBase<AdControl>.Instance;
		string arg_86_1 = "4";
		AndroidControl.Callback arg_86_2;
		if ((arg_86_2 = PauseView.__c.__9__15_0) == null)
		{
			arg_86_2 = (PauseView.__c.__9__15_0 = new AndroidControl.Callback(PauseView.__c.__9._clickQuite_b__15_0));
		}
		AndroidControl.Callback arg_86_3;
		if ((arg_86_3 = PauseView.__c.__9__15_1) == null)
		{
			arg_86_3 = (PauseView.__c.__9__15_1 = new AndroidControl.Callback(PauseView.__c.__9._clickQuite_b__15_1));
		}
		arg_86_0.ShowIntAd(arg_86_1, arg_86_2, arg_86_3);
		*/
        AdsControl.Instance.showAds();      
	}

	public void clickGiveUp()
	{
		Time.timeScale = 1f;
		base.gameObject.SetActive(false);
		MainMenuView.m_this.initGame();
		MainMenuView.m_this.showView();
		MainMenuView.m_this.m_MatchView.m_MatchCupView.matchResult(false);
		ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "杯赛放弃插屏");
        /*
		AdControl arg_9B_0 = ControlsBase<AdControl>.Instance;
		string arg_9B_1 = "4";
		AndroidControl.Callback arg_9B_2;
		if ((arg_9B_2 = PauseView.__c.__9__16_0) == null)
		{
			arg_9B_2 = (PauseView.__c.__9__16_0 = new AndroidControl.Callback(PauseView.__c.__9._clickGiveUp_b__16_0));
		}
		AndroidControl.Callback arg_9B_3;
		if ((arg_9B_3 = PauseView.__c.__9__16_1) == null)
		{
			arg_9B_3 = (PauseView.__c.__9__16_1 = new AndroidControl.Callback(PauseView.__c.__9._clickGiveUp_b__16_1));
		}
		arg_9B_0.ShowIntAd(arg_9B_1, arg_9B_2, arg_9B_3);
		*/
        AdsControl.Instance.showAds();      
	}
}
