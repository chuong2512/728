using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class MissionView : MonoBehaviour
{
	[Serializable]
	private sealed class __c
	{
		public static readonly MissionView.__c __9 = new MissionView.__c();

		public static AndroidControl.Callback __9__85_1;

		public static AndroidControl.Callback __9__85_2;

		internal void _clickAd_b__85_1()
		{
		}

		internal void _clickAd_b__85_2()
		{
		}
	}

	public Transform m_effUi;

	private bool m_isCanClick = true;

	public Text m_day1NameTxt;

	public Slider m_day1SliderTxt;

	public Text m_day1SliderNumTxt;

	public Text m_day1NumTxt;

	public Transform m_day1GoBtn;

	public Transform m_day1GetBtn;

	public Text m_day2NameTxt;

	public Slider m_day2SliderTxt;

	public Text m_day2SliderNumTxt;

	public Text m_day2NumTxt;

	public Transform m_day2GoBtn;

	public Transform m_day2GetBtn;

	public Text m_ach1NameTxt;

	public Slider m_ach1SliderTxt;

	public Text m_ach1SliderNumTxt;

	public Text m_ach1NumTxt;

	public Transform m_ach1GoBtn;

	public Transform m_ach1GetBtn;

	public Text m_ach2NameTxt;

	public Slider m_ach2SliderTxt;

	public Text m_ach2SliderNumTxt;

	public Text m_ach2NumTxt;

	public Transform m_ach2GoBtn;

	public Transform m_ach2GetBtn;

	public Text m_ach3NameTxt;

	public Slider m_ach3SliderTxt;

	public Text m_ach3SliderNumTxt;

	public Text m_ach3NumTxt;

	public Transform m_ach3GoBtn;

	public Transform m_ach3GetBtn;

	public Text m_ach4NameTxt;

	public Slider m_ach4SliderTxt;

	public Text m_ach4SliderNumTxt;

	public Text m_ach4NumTxt;

	public Transform m_ach4GoBtn;

	public Transform m_ach4GetBtn;

	public Text m_ach5NameTxt;

	public Slider m_ach5SliderTxt;

	public Text m_ach5SliderNumTxt;

	public Text m_ach5NumTxt;

	public Transform m_ach5GoBtn;

	public Transform m_ach5GetBtn;

	public Text m_ach6NameTxt;

	public Slider m_ach6SliderTxt;

	public Text m_ach6SliderNumTxt;

	public Text m_ach6NumTxt;

	public Transform m_ach6GoBtn;

	public Transform m_ach6GetBtn;

	public Text m_ach7NameTxt;

	public Slider m_ach7SliderTxt;

	public Text m_ach7SliderNumTxt;

	public Text m_ach7NumTxt;

	public Transform m_ach7GoBtn;

	public Transform m_ach7GetBtn;

	public Text m_ach8NameTxt;

	public Slider m_ach8SliderTxt;

	public Text m_ach8SliderNumTxt;

	public Text m_ach8NumTxt;

	public Transform m_ach8GoBtn;

	public Transform m_ach8GetBtn;

	public static int[] m_dayMoneys = new int[]
	{
		200,
		300
	};

	public static int[] m_achMoneys = new int[]
	{
		500,
		500,
		1000,
		3000,
		5000,
		7000,
		10000,
		30000
	};

	public TipsView m_TipsView;

	public Transform m_LeftBtn1;

	public Transform m_ScaleView1;

	public Transform m_ScaleView2;

	public Transform m_ScaleView3;

	public Transform m_ScaleView4;

	public Transform m_ScaleView5;

	public Transform m_ScaleView6;

	public Transform m_ScaleView7;

	public Transform m_ScaleView8;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void playEf()
	{
		MainMenuView.m_this.m_PublicView.playEf();
		float x = this.m_LeftBtn1.localPosition.x;
		this.m_LeftBtn1.localPosition = new Vector3(-200f, this.m_LeftBtn1.localPosition.y, this.m_LeftBtn1.localPosition.z);
		this.m_LeftBtn1.DOLocalMoveX(x, 0.6f, false).SetEase(Ease.OutSine);
		this.m_ScaleView1.localScale = Vector3.zero;
		this.m_ScaleView2.localScale = Vector3.zero;
		this.m_ScaleView3.localScale = Vector3.zero;
		this.m_ScaleView4.localScale = Vector3.zero;
		this.m_ScaleView5.localScale = Vector3.zero;
		this.m_ScaleView6.localScale = Vector3.zero;
		this.m_ScaleView7.localScale = Vector3.zero;
		this.m_ScaleView8.localScale = Vector3.zero;
		Sequence expr_F3 = DOTween.Sequence();
		expr_F3.Insert(0f, this.m_ScaleView1.DOScale(1f, 0.2f));
		expr_F3.Insert(0.05f, this.m_ScaleView2.DOScale(1f, 0.2f));
		expr_F3.Insert(0.1f, this.m_ScaleView3.DOScale(1f, 0.2f));
		expr_F3.Insert(0.15f, this.m_ScaleView4.DOScale(1f, 0.2f));
		expr_F3.Insert(0.2f, this.m_ScaleView5.DOScale(1f, 0.2f));
		expr_F3.Insert(0.25f, this.m_ScaleView6.DOScale(1f, 0.2f));
		expr_F3.Insert(0.3f, this.m_ScaleView7.DOScale(1f, 0.2f));
		expr_F3.Insert(0.35f, this.m_ScaleView8.DOScale(1f, 0.2f));
	}

	public void reflushView()
	{
		this.m_day1SliderTxt.value = (float)Singleton<MissionManager>.Instance.getDayMissionDeVal(0) * 1f / (float)Singleton<MissionManager>.Instance.getDayMissionDeMaxVal(0);
		if (Singleton<MissionManager>.Instance.getDayMissionTp(0) == 0)
		{
			if (Singleton<MissionManager>.Instance.getDayMissionDeVal(0) < Singleton<MissionManager>.Instance.getDayMissionDeMaxVal(0))
			{
				this.m_day1GetBtn.gameObject.SetActive(false);
				this.m_day1GoBtn.gameObject.SetActive(true);
			}
			else
			{
				this.m_day1GetBtn.gameObject.SetActive(true);
				this.m_day1GoBtn.gameObject.SetActive(false);
			}
		}
		else
		{
			this.m_day1GetBtn.gameObject.SetActive(false);
			this.m_day1GoBtn.gameObject.SetActive(false);
		}
		this.m_day1NameTxt.text = Singleton<MissionManager>.Instance.getDayMissionName(0);
		this.m_day1NumTxt.text = string.Concat(Singleton<MissionManager>.Instance.getDayMissionMoney(0));
		this.m_day1SliderNumTxt.text = Singleton<MissionManager>.Instance.getDayMissionDeVal(0) + "/" + Singleton<MissionManager>.Instance.getDayMissionDeMaxVal(0);
		this.m_day2SliderTxt.value = (float)Singleton<MissionManager>.Instance.getDayMissionDeVal(1) * 1f / (float)Singleton<MissionManager>.Instance.getDayMissionDeMaxVal(1);
		if (Singleton<MissionManager>.Instance.getDayMissionTp(1) == 0)
		{
			if (Singleton<MissionManager>.Instance.getDayMissionDeVal(1) < Singleton<MissionManager>.Instance.getDayMissionDeMaxVal(1))
			{
				this.m_day2GetBtn.gameObject.SetActive(false);
				this.m_day2GoBtn.gameObject.SetActive(true);
			}
			else
			{
				this.m_day2GetBtn.gameObject.SetActive(true);
				this.m_day2GoBtn.gameObject.SetActive(false);
			}
		}
		else
		{
			this.m_day2GetBtn.gameObject.SetActive(false);
			this.m_day2GoBtn.gameObject.SetActive(false);
		}
		this.m_day2NameTxt.text = Singleton<MissionManager>.Instance.getDayMissionName(1);
		this.m_day2NumTxt.text = string.Concat(Singleton<MissionManager>.Instance.getDayMissionMoney(1));
		this.m_day2SliderNumTxt.text = Singleton<MissionManager>.Instance.getDayMissionDeVal(1) + "/" + Singleton<MissionManager>.Instance.getDayMissionDeMaxVal(1);
		this.m_ach1SliderTxt.value = (float)Singleton<MissionManager>.Instance.getAchMissionDeVal(0) * 1f / (float)Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(0);
		if (Singleton<MissionManager>.Instance.getAchMissionTp(0) == 0)
		{
			if (Singleton<MissionManager>.Instance.getAchMissionDeVal(0) < Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(0))
			{
				this.m_ach1GetBtn.gameObject.SetActive(false);
				this.m_ach1GoBtn.gameObject.SetActive(true);
			}
			else
			{
				this.m_ach1GetBtn.gameObject.SetActive(true);
				this.m_ach1GoBtn.gameObject.SetActive(false);
			}
		}
		else
		{
			this.m_ach1GetBtn.gameObject.SetActive(false);
			this.m_ach1GoBtn.gameObject.SetActive(false);
		}
		this.m_ach1NameTxt.text = Singleton<MissionManager>.Instance.getAchMissionName(0);
		this.m_ach1NumTxt.text = string.Concat(Singleton<MissionManager>.Instance.getAchMissionMoney(0));
		this.m_ach1SliderNumTxt.text = Singleton<MissionManager>.Instance.getAchMissionDeVal(0) + "/" + Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(0);
		this.m_ach2SliderTxt.value = (float)Singleton<MissionManager>.Instance.getAchMissionDeVal(1) * 1f / (float)Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(1);
		if (Singleton<MissionManager>.Instance.getAchMissionTp(1) == 0)
		{
			if (Singleton<MissionManager>.Instance.getAchMissionDeVal(1) < Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(1))
			{
				this.m_ach2GetBtn.gameObject.SetActive(false);
				this.m_ach2GoBtn.gameObject.SetActive(true);
			}
			else
			{
				this.m_ach2GetBtn.gameObject.SetActive(true);
				this.m_ach2GoBtn.gameObject.SetActive(false);
			}
		}
		else
		{
			this.m_ach2GetBtn.gameObject.SetActive(false);
			this.m_ach2GoBtn.gameObject.SetActive(false);
		}
		this.m_ach2NameTxt.text = Singleton<MissionManager>.Instance.getAchMissionName(1);
		this.m_ach2NumTxt.text = string.Concat(Singleton<MissionManager>.Instance.getAchMissionMoney(1));
		this.m_ach2SliderNumTxt.text = Singleton<MissionManager>.Instance.getAchMissionDeVal(1) + "/" + Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(1);
		this.m_ach3SliderTxt.value = (float)Singleton<MissionManager>.Instance.getAchMissionDeVal(2) * 1f / (float)Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(2);
		if (Singleton<MissionManager>.Instance.getAchMissionTp(2) == 0)
		{
			if (Singleton<MissionManager>.Instance.getAchMissionDeVal(2) < Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(2))
			{
				this.m_ach3GetBtn.gameObject.SetActive(false);
				this.m_ach3GoBtn.gameObject.SetActive(true);
			}
			else
			{
				this.m_ach3GetBtn.gameObject.SetActive(true);
				this.m_ach3GoBtn.gameObject.SetActive(false);
			}
		}
		else
		{
			this.m_ach3GetBtn.gameObject.SetActive(false);
			this.m_ach3GoBtn.gameObject.SetActive(false);
		}
		this.m_ach3NameTxt.text = Singleton<MissionManager>.Instance.getAchMissionName(2);
		this.m_ach3NumTxt.text = string.Concat(Singleton<MissionManager>.Instance.getAchMissionMoney(2));
		this.m_ach3SliderNumTxt.text = Singleton<MissionManager>.Instance.getAchMissionDeVal(2) + "/" + Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(2);
		this.m_ach4SliderTxt.value = (float)Singleton<MissionManager>.Instance.getAchMissionDeVal(3) * 1f / (float)Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(3);
		if (Singleton<MissionManager>.Instance.getAchMissionTp(3) == 0)
		{
			if (Singleton<MissionManager>.Instance.getAchMissionDeVal(3) < Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(3))
			{
				this.m_ach4GetBtn.gameObject.SetActive(false);
				this.m_ach4GoBtn.gameObject.SetActive(true);
			}
			else
			{
				this.m_ach4GetBtn.gameObject.SetActive(true);
				this.m_ach4GoBtn.gameObject.SetActive(false);
			}
		}
		else
		{
			this.m_ach4GetBtn.gameObject.SetActive(false);
			this.m_ach4GoBtn.gameObject.SetActive(false);
		}
		this.m_ach4NameTxt.text = Singleton<MissionManager>.Instance.getAchMissionName(3);
		this.m_ach4NumTxt.text = string.Concat(Singleton<MissionManager>.Instance.getAchMissionMoney(3));
		this.m_ach4SliderNumTxt.text = Singleton<MissionManager>.Instance.getAchMissionDeVal(3) + "/" + Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(3);
		this.m_ach5SliderTxt.value = (float)Singleton<MissionManager>.Instance.getAchMissionDeVal(4) * 1f / (float)Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(4);
		if (Singleton<MissionManager>.Instance.getAchMissionTp(4) == 0)
		{
			if (Singleton<MissionManager>.Instance.getAchMissionDeVal(4) < Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(4))
			{
				this.m_ach5GetBtn.gameObject.SetActive(false);
				this.m_ach5GoBtn.gameObject.SetActive(true);
			}
			else
			{
				this.m_ach5GetBtn.gameObject.SetActive(true);
				this.m_ach5GoBtn.gameObject.SetActive(false);
			}
		}
		else
		{
			this.m_ach5GetBtn.gameObject.SetActive(false);
			this.m_ach5GoBtn.gameObject.SetActive(false);
		}
		this.m_ach5NameTxt.text = Singleton<MissionManager>.Instance.getAchMissionName(4);
		this.m_ach5NumTxt.text = string.Concat(Singleton<MissionManager>.Instance.getAchMissionMoney(4));
		this.m_ach5SliderNumTxt.text = Singleton<MissionManager>.Instance.getAchMissionDeVal(4) + "/" + Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(4);
		this.m_ach6SliderTxt.value = (float)Singleton<MissionManager>.Instance.getAchMissionDeVal(5) * 1f / (float)Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(5);
		if (Singleton<MissionManager>.Instance.getAchMissionTp(5) == 0)
		{
			if (Singleton<MissionManager>.Instance.getAchMissionDeVal(5) < Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(5))
			{
				this.m_ach6GetBtn.gameObject.SetActive(false);
				this.m_ach6GoBtn.gameObject.SetActive(true);
			}
			else
			{
				this.m_ach6GetBtn.gameObject.SetActive(true);
				this.m_ach6GoBtn.gameObject.SetActive(false);
			}
		}
		else
		{
			this.m_ach6GetBtn.gameObject.SetActive(false);
			this.m_ach6GoBtn.gameObject.SetActive(false);
		}
		this.m_ach6NameTxt.text = Singleton<MissionManager>.Instance.getAchMissionName(5);
		this.m_ach6NumTxt.text = string.Concat(Singleton<MissionManager>.Instance.getAchMissionMoney(5));
		this.m_ach6SliderNumTxt.text = Singleton<MissionManager>.Instance.getAchMissionDeVal(5) + "/" + Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(5);
		this.m_ach7SliderTxt.value = (float)Singleton<MissionManager>.Instance.getAchMissionDeVal(6) * 1f / (float)Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(6);
		if (Singleton<MissionManager>.Instance.getAchMissionTp(6) == 0)
		{
			if (Singleton<MissionManager>.Instance.getAchMissionDeVal(6) < Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(6))
			{
				this.m_ach7GetBtn.gameObject.SetActive(false);
				this.m_ach7GoBtn.gameObject.SetActive(true);
			}
			else
			{
				this.m_ach7GetBtn.gameObject.SetActive(true);
				this.m_ach7GoBtn.gameObject.SetActive(false);
			}
		}
		else
		{
			this.m_ach7GetBtn.gameObject.SetActive(false);
			this.m_ach7GoBtn.gameObject.SetActive(false);
		}
		this.m_ach7NameTxt.text = Singleton<MissionManager>.Instance.getAchMissionName(6);
		this.m_ach7NumTxt.text = string.Concat(Singleton<MissionManager>.Instance.getAchMissionMoney(6));
		this.m_ach7SliderNumTxt.text = Singleton<MissionManager>.Instance.getAchMissionDeVal(6) + "/" + Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(6);
		this.m_ach8SliderTxt.value = (float)Singleton<MissionManager>.Instance.getAchMissionDeVal(7) * 1f / (float)Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(7);
		if (Singleton<MissionManager>.Instance.getAchMissionTp(7) == 0)
		{
			if (Singleton<MissionManager>.Instance.getAchMissionDeVal(7) < Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(7))
			{
				this.m_ach8GetBtn.gameObject.SetActive(false);
				this.m_ach8GoBtn.gameObject.SetActive(true);
			}
			else
			{
				this.m_ach8GetBtn.gameObject.SetActive(true);
				this.m_ach8GoBtn.gameObject.SetActive(false);
			}
		}
		else
		{
			this.m_ach8GetBtn.gameObject.SetActive(false);
			this.m_ach8GoBtn.gameObject.SetActive(false);
		}
		this.m_ach8NameTxt.text = Singleton<MissionManager>.Instance.getAchMissionName(7);
		this.m_ach8NumTxt.text = string.Concat(Singleton<MissionManager>.Instance.getAchMissionMoney(7));
		this.m_ach8SliderNumTxt.text = Singleton<MissionManager>.Instance.getAchMissionDeVal(7) + "/" + Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(7);
	}

	public void showView()
	{
		this.m_isCanClick = false;
		base.Invoke("resetClick", 1.1f);
		base.transform.gameObject.SetActive(true);
		this.reflushView();
		this.playEf();
	}

	public void resetClick()
	{
		this.m_isCanClick = true;
	}

	public void clickBack()
	{
		base.Invoke("resetClick", 1.1f);
		base.transform.gameObject.SetActive(false);
		MainMenuView.m_this.gameObject.SetActive(true);
		MainMenuView.m_this.showView();
	}

	private void changeUi(bool isShow)
	{
	}

	public void clickGo(int ind)
	{
		ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "任务GO-" + ind);
		if (ind == 0)
		{
			this.clickBack();
			return;
		}
		if (ind == 1)
		{
			this.clickAd();
			return;
		}
		if (ind == 2)
		{
			this.clickBack();
			return;
		}
		if (ind == 3)
		{
			this.clickBack();
			MainMenuView.m_this.clickMatch();
			return;
		}
		if (ind == 4)
		{
			this.clickBack();
			MainMenuView.m_this.clickMatch();
			return;
		}
		if (ind == 5)
		{
			this.clickBack();
			MainMenuView.m_this.clickMatch();
			return;
		}
		if (ind == 6)
		{
			this.clickBack();
			MainMenuView.m_this.clickMatch();
			return;
		}
		if (ind == 7)
		{
			this.clickBack();
			return;
		}
		if (ind == 8)
		{
			this.clickBack();
			return;
		}
		if (ind == 9)
		{
			this.clickBack();
		}
	}

	public void clickGet(int ind)
	{
		this.showTips(ind);
		ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "任务完成" + ind);
	}

	private void showTips(int ind)
	{
		this.m_TipsView.m_tipTp = 1;
		if (ind < 2)
		{
			this.m_TipsView.m_rwNum = MissionView.m_dayMoneys[ind];
			Singleton<MissionManager>.Instance.m_MissionInfo.m_DayTp[ind] = 1;
			this.reflushView();
		}
		else
		{
			this.m_TipsView.m_rwNum = MissionView.m_achMoneys[ind - 2];
			Singleton<MissionManager>.Instance.m_MissionInfo.m_AchTp[ind - 2] = 1;
			this.reflushView();
		}
		this.m_TipsView.showView();
	}

	public void clickAd()
	{
        /*
		AdControl arg_54_0 = ControlsBase<AdControl>.Instance;
		string arg_54_1 = "3";
		AndroidControl.Callback arg_54_2 = delegate
		{
			ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "激励-每日任务");
			Singleton<MissionManager>.Instance.addMissionVal(2, 1);
			Singleton<MissionManager>.Instance.m_MissionInfo.m_DayTp[1] = 1;
			this.reflushView();
		};
		AndroidControl.Callback arg_54_3;
		if ((arg_54_3 = MissionView.__c.__9__85_1) == null)
		{
			arg_54_3 = (MissionView.__c.__9__85_1 = new AndroidControl.Callback(MissionView.__c.__9._clickAd_b__85_1));
		}
		AndroidControl.Callback arg_54_4;
		if ((arg_54_4 = MissionView.__c.__9__85_2) == null)
		{
			arg_54_4 = (MissionView.__c.__9__85_2 = new AndroidControl.Callback(MissionView.__c.__9._clickAd_b__85_2));
		}
		arg_54_0.ShowRwAd(arg_54_1, arg_54_2, arg_54_3, arg_54_4);
		*/
        if (AdsControl.Instance.GetRewardAvailable())
        {
            
                ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "激励-每日任务");
                Singleton<MissionManager>.Instance.addMissionVal(2, 1);
                Singleton<MissionManager>.Instance.m_MissionInfo.m_DayTp[1] = 1;
                this.reflushView();

        }
    }
}
