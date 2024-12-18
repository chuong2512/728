using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class ShopView : MonoBehaviour
{
	[Serializable]
	private sealed class __c
	{
		public static readonly ShopView.__c __9 = new ShopView.__c();

		public static AndroidControl.Callback __9__36_1;

		public static AndroidControl.Callback __9__36_2;

		internal void _clickBuyBall_b__36_1()
		{
		}

		internal void _clickBuyBall_b__36_2()
		{
		}
	}

	public Transform m_roleBg;

	public Transform m_ballBg;

	private int m_topInd = -1;

	private int m_roleInd;

	private int m_ballInd;

	public Image m_topImg1;

	public Image m_topImg2;

	public Transform m_roleBuyBtn;

	public Text m_roleBuyTxt;

	public Transform m_roleUsingBtn;

	public Transform m_roleEqpBtn;

	public Transform m_ballAdBtn;

	public Transform m_ballUsingBtn;

	public Transform m_ballEqpBtn;

	public ScrollViewListener m_ballScroll;

	public ScrollViewListener2 m_playerScroll;

	private int m_selectBallInd;

	private int m_selectPlayerInd;

	public Transform m_LeftBtn1;

	public Transform m_LeftBtn2;

	public Transform m_LeftBtn3;

	private GameObject m_roleObj;

	private GameObject m_ballObj;

	private void Start()
	{
		ScrollViewListener2.OnPageChange = new Action<int>(this.showRoleInd);
		ScrollViewListener.OnPageChange = new Action<int>(this.showBallInd);
	}

	private void Update()
	{
	}

	private void OnDisable()
	{
		MainMenuView.m_this.m_PublicView.setShowTp(0);
		MainMenuView.m_this.reflushTp();
	}

	private void OnEnable()
	{
		MainMenuView.m_this.m_PublicView.setShowTp(1);
	}

	public void ShowView()
	{
		this.m_roleInd = this.m_playerScroll.GetCurIndex();
		this.m_ballInd = this.m_ballScroll.GetCurIndex();
		base.gameObject.SetActive(true);
		this.showInd(0);
		this.reflushTp();
		this.m_selectBallInd = Singleton<GameManager>.Instance.m_UserInfo.m_ballInd;
		this.m_selectPlayerInd = Singleton<GameManager>.Instance.m_UserInfo.m_roleInd;
		this.playEf();
	}

	public void playEf()
	{
		MainMenuView.m_this.m_PublicView.playEf();
		float x = this.m_LeftBtn1.localPosition.x;
		float x2 = this.m_LeftBtn2.localPosition.x;
		float x3 = this.m_LeftBtn3.localPosition.x;
		this.m_LeftBtn1.localPosition = new Vector3(-200f, this.m_LeftBtn1.localPosition.y, this.m_LeftBtn1.localPosition.z);
		this.m_LeftBtn1.DOLocalMoveX(x, 0.6f, false).SetEase(Ease.OutSine);
		this.m_LeftBtn2.localPosition = new Vector3(-200f, this.m_LeftBtn2.localPosition.y, this.m_LeftBtn2.localPosition.z);
		this.m_LeftBtn2.DOLocalMoveX(x2, 0.6f, false).SetEase(Ease.OutSine);
		this.m_LeftBtn3.localPosition = new Vector3(-200f, this.m_LeftBtn3.localPosition.y, this.m_LeftBtn3.localPosition.z);
		this.m_LeftBtn3.DOLocalMoveX(x3, 0.6f, false).SetEase(Ease.OutSine);
	}

	public void showInd(int ind)
	{
		if (this.m_topInd == ind)
		{
			return;
		}
		this.m_topInd = ind;
		if (this.m_topInd == 0)
		{
			this.m_roleInd = this.m_playerScroll.GetCurIndex();
			this.reflushTp();
			this.m_ballBg.gameObject.SetActive(false);
			this.m_roleBg.gameObject.SetActive(true);
			this.m_topImg1.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/shop/new/gmkm_iocn");
			this.m_topImg2.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/shop/new/gfi_icon_01");
			MainMenuView.m_this.m_MainGameView.m_bl_CameraOrbit.ShowShopPlayer();
			return;
		}
		this.m_ballInd = this.m_ballScroll.GetCurIndex();
		this.reflushTp();
		this.m_ballBg.gameObject.SetActive(true);
		this.m_roleBg.gameObject.SetActive(false);
		this.m_topImg1.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/shop/new/gmkm_iocn_01");
		this.m_topImg2.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/shop/new/gfi_icon");
		MainMenuView.m_this.m_MainGameView.m_bl_CameraOrbit.ShowShopBall();
	}

	public void reflushTp()
	{
		if (this.m_topInd == 0)
		{
			this.m_roleBuyBtn.gameObject.SetActive(false);
			this.m_roleEqpBtn.gameObject.SetActive(false);
			this.m_roleUsingBtn.gameObject.SetActive(false);
			if (Singleton<GameManager>.Instance.m_UserInfo.m_roleInfos[this.m_roleInd] == 0)
			{
				this.m_roleBuyBtn.gameObject.SetActive(true);
				this.m_roleBuyTxt.text = string.Concat(Singleton<GameManager>.Instance.getRoleCost(this.m_roleInd));
			}
			else
			{
				Singleton<GameManager>.Instance.m_UserInfo.m_roleInd = this.m_roleInd;
				this.m_roleUsingBtn.gameObject.SetActive(true);
			}
			if (this.m_roleObj != null)
			{
				this.m_roleObj.transform.SetParent(null);
				UnityEngine.Object.Destroy(this.m_roleObj);
				this.m_roleObj = null;
				return;
			}
		}
		else
		{
			this.m_ballAdBtn.gameObject.SetActive(false);
			this.m_ballEqpBtn.gameObject.SetActive(false);
			this.m_ballUsingBtn.gameObject.SetActive(false);
			if (Singleton<GameManager>.Instance.m_UserInfo.m_ballInfos[this.m_ballInd] == 0)
			{
				this.m_ballAdBtn.gameObject.SetActive(true);
			}
			else
			{
				Singleton<GameManager>.Instance.m_UserInfo.m_ballInd = this.m_ballInd;
				this.m_ballUsingBtn.gameObject.SetActive(true);
			}
			if (this.m_ballObj != null)
			{
				this.m_ballObj.transform.SetParent(null);
				UnityEngine.Object.Destroy(this.m_ballObj);
				this.m_ballObj = null;
			}
		}
	}

	public void showRoleInd(int ind)
	{
		this.m_roleInd = ind;
		this.reflushTp();
		Singleton<GameManager>.Instance.m_UserInfo.m_roleInd = this.m_roleInd;
		MainMenuView.m_this.reflushRole();
	}

	public void showBallInd(int ind)
	{
		this.m_ballInd = ind;
		this.reflushTp();
		Singleton<GameManager>.Instance.m_UserInfo.m_ballInd = this.m_ballInd;
		MainMenuView.m_this.reflushBall();
	}

	public void clickEqpRole()
	{
		if (Singleton<GameManager>.Instance.m_UserInfo.m_roleInfos[this.m_roleInd] == 1)
		{
			Singleton<GameManager>.Instance.m_UserInfo.m_roleInd = this.m_roleInd;
			this.reflushTp();
			MainMenuView.m_this.reflushRole();
			Singleton<GameManager>.Instance.OnPause();
			this.m_selectPlayerInd = Singleton<GameManager>.Instance.m_UserInfo.m_roleInd;
		}
	}

	public void clickBuyRole()
	{
		int roleCost = Singleton<GameManager>.Instance.getRoleCost(this.m_roleInd);
		if (Singleton<GameManager>.Instance.addCoins(-roleCost))
		{
			ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "金币-买角色" + this.m_roleInd);
			if (Singleton<GameManager>.Instance.m_UserInfo.m_roleInfos[this.m_roleInd] == 0)
			{
				Singleton<GameManager>.Instance.m_UserInfo.m_roleInfos[this.m_roleInd] = 1;
				Singleton<GameManager>.Instance.m_UserInfo.m_roleInd = this.m_roleInd;
				this.reflushTp();
				MainMenuView.m_this.reflushRole();
				Singleton<GameManager>.Instance.OnPause();
				this.m_selectPlayerInd = Singleton<GameManager>.Instance.m_UserInfo.m_roleInd;
			}
		}
	}

	public void clickEqpBall()
	{
		if (Singleton<GameManager>.Instance.m_UserInfo.m_ballInfos[this.m_ballInd] == 1)
		{
			Singleton<GameManager>.Instance.m_UserInfo.m_ballInd = this.m_ballInd;
			this.reflushTp();
			MainMenuView.m_this.reflushBall();
			Singleton<GameManager>.Instance.OnPause();
			this.m_selectBallInd = Singleton<GameManager>.Instance.m_UserInfo.m_ballInd;
		}
	}

	public void clickBuyBall()
	{
        /*
		AdControl arg_54_0 = ControlsBase<AdControl>.Instance;
		string arg_54_1 = "3";
		AndroidControl.Callback arg_54_2 = delegate
		{
			Singleton<MissionManager>.Instance.addMissionVal(2, 1);
			if (Singleton<GameManager>.Instance.m_UserInfo.m_ballInfos[this.m_ballInd] == 0)
			{
				ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "激励-买球" + this.m_ballInd);
				Singleton<GameManager>.Instance.m_UserInfo.m_ballInfos[this.m_ballInd] = 1;
				Singleton<GameManager>.Instance.m_UserInfo.m_ballInd = this.m_ballInd;
				this.reflushTp();
				MainMenuView.m_this.reflushBall();
				Singleton<GameManager>.Instance.OnPause();
				this.m_selectBallInd = Singleton<GameManager>.Instance.m_UserInfo.m_ballInd;
			}
		};
		AndroidControl.Callback arg_54_3;
		if ((arg_54_3 = ShopView.__c.__9__36_1) == null)
		{
			arg_54_3 = (ShopView.__c.__9__36_1 = new AndroidControl.Callback(ShopView.__c.__9._clickBuyBall_b__36_1));
		}
		AndroidControl.Callback arg_54_4;
		if ((arg_54_4 = ShopView.__c.__9__36_2) == null)
		{
			arg_54_4 = (ShopView.__c.__9__36_2 = new AndroidControl.Callback(ShopView.__c.__9._clickBuyBall_b__36_2));
		}
		arg_54_0.ShowRwAd(arg_54_1, arg_54_2, arg_54_3, arg_54_4);
		*/
        if (AdsControl.Instance.GetRewardAvailable())
        {
           // AdsControl.Instance.PlayDelegateRewardVideo(delegate
            {

                Singleton<MissionManager>.Instance.addMissionVal(2, 1);
                if (Singleton<GameManager>.Instance.m_UserInfo.m_ballInfos[this.m_ballInd] == 0)
                {
                    ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "激励-买球" + this.m_ballInd);
                    Singleton<GameManager>.Instance.m_UserInfo.m_ballInfos[this.m_ballInd] = 1;
                    Singleton<GameManager>.Instance.m_UserInfo.m_ballInd = this.m_ballInd;
                    this.reflushTp();
                    MainMenuView.m_this.reflushBall();
                    Singleton<GameManager>.Instance.OnPause();
                    this.m_selectBallInd = Singleton<GameManager>.Instance.m_UserInfo.m_ballInd;
                }
            };

        }
    }

	public void clickClose()
	{
		MainMenuView.m_this.m_MainGameView.m_bl_CameraOrbit.ShowInit();
		base.gameObject.SetActive(false);
		MainMenuView.m_this.gameObject.SetActive(true);
		MainMenuView.m_this.showView();
		if (Singleton<GameManager>.Instance.m_UserInfo.m_ballInfos[this.m_ballInd] == 0)
		{
			Singleton<GameManager>.Instance.m_UserInfo.m_ballInd = this.m_selectBallInd;
			MainMenuView.m_this.reflushBall();
		}
		if (Singleton<GameManager>.Instance.m_UserInfo.m_roleInfos[this.m_roleInd] == 0)
		{
			Singleton<GameManager>.Instance.m_UserInfo.m_roleInd = this.m_selectPlayerInd;
			MainMenuView.m_this.reflushRole();
		}
	}
}
