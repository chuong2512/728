using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class MainMenuView : MonoBehaviour
{
	[Serializable]
	private sealed class __c
	{
		public static readonly MainMenuView.__c __9 = new MainMenuView.__c();

		public static AndroidControl.Callback __9__39_0;

		public static AndroidControl.Callback __9__39_1;

		public static AndroidControl.Callback __9__45_0;

		public static AndroidControl.Callback __9__45_1;

		internal void _Update_b__39_0()
		{
		}

		internal void _Update_b__39_1()
		{
		}

		internal void _initGame_b__45_0()
		{
		}

		internal void _initGame_b__45_1()
		{
		}
	}

	public static MainMenuView m_this;

	public MainGameView m_MainGameView;

	public CupView m_CupView;

	public ShopView m_ShopView;

	public MissionView m_MissionView;

	public PublicView m_PublicView;

	public TurnView m_TurnView;

	public TipsView m_TipsView;

	private int m_viewInd;

	public SpriteRenderer m_bg1;

	public SpriteRenderer m_bg2;

	private int m_bgInd1;

	private int m_bgInd2 = 1;

	public SpriteRenderer m_expImg;

	public Transform m_expFailImg;

	public Transform m_winEf;

	public Transform m_missionFlog;

	public Transform m_shopFlog;

	public Transform m_upgradeView;

	public Image m_upgradeBtn;

	public SkipView m_skipView;

	public Image m_upgradeCupView;

	public Text m_leagueSliderTxt;

	public Slider m_leagurSlider;

	private static bool m_isInit = false;

	public bool m_isCanTouch = true;

	private bool m_isCanClick = true;

	public Transform m_audiRoot;

	private Animator[] m_anis;

	public Transform m_effUis;

	public MatchView m_MatchView;

	public Transform m_LeftBtn1;

	public Transform m_LeftBtn2;

	public Transform m_RightBtn1;

	public CameraShark m_CameraShark;

	public Transform m_ScaleView1;

	public Transform m_ScaleView2;

	public int m_scaneId;

	public static bool m_isFirst = true;

	private static string[] m_expName1 = new string[]
	{
		"geng_01",
		"geng_03",
		"geng_05",
		"geng_08"
	};

	private static string[] m_expName2 = new string[]
	{
		"geng_02",
		"geng_05",
		"geng_06",
		"geng_07"
	};

    public void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
	{
		this.m_upgradeView.gameObject.SetActive(false);
		MainMenuView.m_this = this;
		this.initGame();
		if (Singleton<GameManager>.Instance.m_UserInfo.SoundState)
		{
			AudioManager.SetBGMVolume(1f);
		}
		else
		{
			AudioManager.SetBGMVolume(0f);
		}
		AudioManager.PlayBGM("bgm");
		this.m_anis = this.m_audiRoot.gameObject.GetComponentsInChildren<Animator>();
		if (this.m_anis.Length != 0)
		{
			for (int i = 0; i < this.m_anis.Length; i++)
			{
				this.m_anis[i].GetComponentInChildren<SkinnedMeshRenderer>().receiveShadows = false;
				this.m_anis[i].GetComponentInChildren<SkinnedMeshRenderer>().allowOcclusionWhenDynamic = true;
			}
		}
		this.playEf();
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			AndroidControl arg_56_0 = ControlsBase<AndroidControl>.Instance;
			string arg_56_1 = "doback";
			string arg_56_2 = "";
			AndroidControl.Callback arg_56_3;
			if ((arg_56_3 = MainMenuView.__c.__9__39_0) == null)
			{
				arg_56_3 = (MainMenuView.__c.__9__39_0 = new AndroidControl.Callback(MainMenuView.__c.__9._Update_b__39_0));
			}
			AndroidControl.Callback arg_56_4;
			if ((arg_56_4 = MainMenuView.__c.__9__39_1) == null)
			{
				arg_56_4 = (MainMenuView.__c.__9__39_1 = new AndroidControl.Callback(MainMenuView.__c.__9._Update_b__39_1));
			}
			arg_56_0.CallAndroidFunc(arg_56_1, arg_56_2, arg_56_3, arg_56_4);
		}
	}

	public void showView()
	{
		this.playEf();
		base.transform.gameObject.SetActive(true);
	}

	private void OnDisable()
	{
		this.m_PublicView.setShowTp(1);
	}

	private void OnEnable()
	{
		this.m_PublicView.setShowTp(0);
	}

	public void reflushTp()
	{
		if (Singleton<GameManager>.Instance.canBuy())
		{
			this.m_shopFlog.gameObject.SetActive(true);
		}
		else
		{
			this.m_shopFlog.gameObject.SetActive(false);
		}
		if (Singleton<GameManager>.Instance.canMission())
		{
			this.m_missionFlog.gameObject.SetActive(true);
		}
		else
		{
			this.m_missionFlog.gameObject.SetActive(false);
		}
		int leagueMax = Singleton<GameManager>.Instance.getLeagueMax();
		int leagueNow = Singleton<GameManager>.Instance.getLeagueNow();
		Singleton<GameManager>.Instance.getLeagueStr();
		this.m_leagueSliderTxt.text = leagueNow + "/" + leagueMax;
		float endValue = (float)leagueNow * 1f / ((float)leagueMax * 1f);
		this.m_leagurSlider.DOValue(endValue, 0.5f, false);
	}

	public void initGame()
	{
		this.m_isCanClick = true;
		this.m_upgradeView.gameObject.SetActive(false);
		this.m_expFailImg.gameObject.SetActive(false);
		base.transform.gameObject.SetActive(true);
		this.m_PublicView.gameObject.SetActive(true);
		this.m_MainGameView.gameObject.SetActive(false);
		this.m_MainGameView.initGameView();
		this.m_MainGameView.m_bl_CameraOrbit.DoSet();
		this.m_MainGameView.m_obBg.localPosition = Vector3.zero;
		this.m_winEf.gameObject.SetActive(false);
		this.reflushTp();
		this.m_isCanTouch = true;
		this.reflushCup();
		if (MainMenuView.m_isFirst)
		{
			MainMenuView.m_isFirst = false;
			ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "启动页插屏");
            /*
			AdControl arg_119_0 = ControlsBase<AdControl>.Instance;
			string arg_119_1 = "0";
			AndroidControl.Callback arg_119_2;
			if ((arg_119_2 = MainMenuView.__c.__9__45_0) == null)
			{
				arg_119_2 = (MainMenuView.__c.__9__45_0 = new AndroidControl.Callback(MainMenuView.__c.__9._initGame_b__45_0));
			}
			AndroidControl.Callback arg_119_3;
			if ((arg_119_3 = MainMenuView.__c.__9__45_1) == null)
			{
				arg_119_3 = (MainMenuView.__c.__9__45_1 = new AndroidControl.Callback(MainMenuView.__c.__9._initGame_b__45_1));
			}
			arg_119_0.ShowIntAd(arg_119_1, arg_119_2, arg_119_3);
			*/
            AdsControl.Instance.showAds();          
		}
		this.m_MainGameView.m_room1.gameObject.SetActive(true);
		this.m_MainGameView.m_room_cup_1.gameObject.SetActive(false);
		this.m_MainGameView.m_room_cup_2.gameObject.SetActive(false);
		this.m_MainGameView.m_room_cup_3.gameObject.SetActive(false);
		this.m_MainGameView.m_room_cup_4.gameObject.SetActive(false);
		this.m_MainGameView.m_room_cup_5.gameObject.SetActive(false);
		this.m_MainGameView.m_room_cup_6.gameObject.SetActive(false);
	}

	private void playEf()
	{
		this.m_PublicView.playEf();
		float x = this.m_LeftBtn1.localPosition.x;
		float x2 = this.m_LeftBtn2.localPosition.x;
		float x3 = this.m_RightBtn1.localPosition.x;
		this.m_ScaleView1.localScale = Vector3.zero;
		this.m_ScaleView2.localScale = Vector3.zero;
		this.m_LeftBtn1.localPosition = new Vector3(-200f, this.m_LeftBtn1.localPosition.y, this.m_LeftBtn1.localPosition.z);
		this.m_LeftBtn2.localPosition = new Vector3(-200f, this.m_LeftBtn2.localPosition.y, this.m_LeftBtn2.localPosition.z);
		this.m_RightBtn1.localPosition = new Vector3(200f, this.m_RightBtn1.localPosition.y, this.m_RightBtn1.localPosition.z);
		this.m_LeftBtn1.DOLocalMoveX(x, 0.6f, false).SetEase(Ease.OutSine);
		this.m_LeftBtn2.DOLocalMoveX(x2, 0.6f, false).SetEase(Ease.OutSine);
		this.m_RightBtn1.DOLocalMoveX(x3, 0.6f, false).SetEase(Ease.OutSine);
		Sequence expr_14D = DOTween.Sequence();
		expr_14D.Insert(0f, this.m_ScaleView1.DOScale(1f, 0.2f));
		expr_14D.Insert(0.05f, this.m_ScaleView2.DOScale(1f, 0.2f));
	}

	private void reflushCup()
	{
		int leagueLv = Singleton<GameManager>.Instance.m_UserInfo.m_leagueLv;
		if (leagueLv == 0)
		{
			this.m_upgradeCupView.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/lron_01");
			return;
		}
		if (leagueLv == 1)
		{
			this.m_upgradeCupView.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/copper");
			return;
		}
		if (leagueLv == 2)
		{
			this.m_upgradeCupView.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/silver");
			return;
		}
		if (leagueLv == 3)
		{
			this.m_upgradeCupView.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/gold");
			return;
		}
		if (leagueLv == 4)
		{
			this.m_upgradeCupView.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/super");
			return;
		}
		this.m_upgradeCupView.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/legend");
	}

	public void showTips()
	{
		this.m_TipsView.showView();
	}

	public void showNext()
	{
		int leagueMax = Singleton<GameManager>.Instance.getLeagueMax();
		if (Singleton<GameManager>.Instance.addLeagueLv(1))
		{
			this.m_leagueSliderTxt.text = leagueMax + "/" + leagueMax;
			float endValue = 1f;
			this.m_leagurSlider.DOValue(endValue, 0.5f, false);
			this.m_upgradeView.gameObject.SetActive(true);
			this.m_upgradeBtn.gameObject.SetActive(true);
			this.m_upgradeBtn.color = new Color(1f, 1f, 1f, 0f);
			Sequence expr_A0 = DOTween.Sequence();
			expr_A0.AppendInterval(0.5f);
			expr_A0.Append(this.m_upgradeBtn.DOFade(1f, 0.5f));
			this.m_isCanTouch = false;
			return;
		}
		this.reflushTp();
		if (Singleton<GameManager>.Instance.m_UserInfo.m_missInd == 3)
		{
			MainMenuView.m_this.showSkip();
		}
	}

	public void clickUpGrade()
	{
		this.reflushTp();
		this.m_upgradeView.gameObject.SetActive(false);
		this.m_isCanTouch = true;
		this.m_CupView.showView();
		this.reflushCup();
	}

	public void clickStartGame()
	{
		if (!this.m_isCanClick)
		{
			return;
		}
		if (!this.m_isCanTouch)
		{
			return;
		}
		this.m_MainGameView.m_gameMode = 0;
		this.m_PublicView.gameObject.SetActive(false);
		base.transform.gameObject.SetActive(false);
		this.m_MainGameView.gameObject.SetActive(true);
		this.m_MainGameView.startGameView();
		Singleton<GameManager>.Instance.OnPause();
		if (!MainMenuView.m_isInit)
		{
			this.m_MainGameView.m_bl_CameraOrbit.moveDefault();
			MainMenuView.m_isInit = true;
		}
		this.m_MainGameView.m_bl_CameraOrbit.CancelInvoke();
		this.m_MainGameView.m_bl_CameraOrbit.LerpSpeed = 3f;
		ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "主页-点击联赛");
	}

	public void clickStartCupGame(int ind)
	{
		if (!this.m_isCanClick)
		{
			return;
		}
		if (!this.m_isCanTouch)
		{
			return;
		}
		this.m_MainGameView.m_gameMode = 1;
		this.m_MainGameView.m_obBg.localPosition = new Vector3(0f, 0f, 1.5f);
		Singleton<GameManager>.Instance.m_gameMode = 1;
		Singleton<GameManager>.Instance.m_sceneInd = this.m_scaneId;
		this.m_MainGameView.m_room1.gameObject.SetActive(false);
		if (this.m_scaneId == 1)
		{
			this.m_MainGameView.m_room_cup_2.gameObject.SetActive(true);
		}
		else if (this.m_scaneId == 2)
		{
			this.m_MainGameView.m_room_cup_3.gameObject.SetActive(true);
		}
		else if (this.m_scaneId == 3)
		{
			this.m_MainGameView.m_room_cup_4.gameObject.SetActive(true);
		}
		else if (this.m_scaneId == 4)
		{
			this.m_MainGameView.m_room_cup_5.gameObject.SetActive(true);
		}
		else if (this.m_scaneId == 5)
		{
			this.m_MainGameView.m_room_cup_6.gameObject.SetActive(true);
		}
		else
		{
			this.m_MainGameView.m_room_cup_1.gameObject.SetActive(true);
		}
		this.m_MainGameView.m_aiInd = ind;
		this.m_PublicView.gameObject.SetActive(false);
		base.transform.gameObject.SetActive(false);
		this.m_MainGameView.gameObject.SetActive(true);
		this.m_MainGameView.addAi();
		this.m_MainGameView.startGameView();
		Singleton<GameManager>.Instance.OnPause();
		if (!MainMenuView.m_isInit)
		{
			this.m_MainGameView.m_bl_CameraOrbit.moveDefault();
			MainMenuView.m_isInit = true;
		}
		this.m_MainGameView.m_bl_CameraOrbit.CancelInvoke();
		this.m_MainGameView.m_bl_CameraOrbit.LerpSpeed = 3f;
	}

	public void clickMatch()
	{
		this.m_MatchView.showView();
		base.gameObject.SetActive(false);
		ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "主页-点击杯赛");
	}

	public void clickMatchContinue()
	{
		this.m_MatchView.showView();
		base.gameObject.SetActive(false);
	}

	public void clickTurn()
	{
		this.m_TurnView.showView();
		base.gameObject.SetActive(false);
		ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "主页-点击转盘");
	}

	public void clickMission()
	{
		this.m_MissionView.showView();
		base.gameObject.SetActive(false);
		ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "主页-点击任务");
	}

	private void resetClick()
	{
		this.m_isCanClick = true;
	}

	private void showUiTp(bool isShow)
	{
	}

	public void clickShop()
	{
		if (!this.m_isCanClick)
		{
			return;
		}
		this.m_isCanClick = false;
		base.Invoke("resetClick", 1.1f);
		if (!this.m_isCanTouch)
		{
			return;
		}
		this.m_ShopView.ShowView();
		base.gameObject.SetActive(false);
		this.m_MainGameView.m_bl_CameraOrbit.ShowShopPlayer();
		ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "主页-点击商店");
	}

	public void changeView()
	{
		this.m_viewInd++;
		this.m_viewInd %= 2;
		if (this.m_viewInd == 0)
		{
			this.m_bgInd1 = (int)(UnityEngine.Random.value * 10f) + 1;
			if (this.m_bgInd1 == this.m_bgInd2)
			{
				this.m_bgInd1++;
				if (this.m_bgInd1 > 10)
				{
					this.m_bgInd1 = 1;
				}
			}
			this.m_bg1.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/bg/" + this.m_bgInd1);
			this.m_bg1.color = new Color(1f, 1f, 1f, 0f);
			this.m_bg2.color = new Color(1f, 1f, 1f, 1f);
			this.m_bg1.gameObject.SetActive(true);
			this.m_bg2.gameObject.SetActive(true);
			this.m_bg1.DOFade(1f, 1f);
			this.m_bg2.DOFade(0f, 1f);
			return;
		}
		this.m_bgInd2 = (int)(UnityEngine.Random.value * 10f) + 1;
		if (this.m_bgInd2 == this.m_bgInd1)
		{
			this.m_bgInd2++;
			if (this.m_bgInd2 > 10)
			{
				this.m_bgInd2 = 1;
			}
		}
		this.m_bg2.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/bg/" + this.m_bgInd2);
		this.m_bg1.color = new Color(1f, 1f, 1f, 1f);
		this.m_bg2.color = new Color(1f, 1f, 1f, 0f);
		this.m_bg1.gameObject.SetActive(true);
		this.m_bg2.gameObject.SetActive(true);
		this.m_bg1.DOFade(0f, 1f);
		this.m_bg2.DOFade(1f, 1f);
	}

	public void showExpress(int state)
	{
		this.m_expImg.gameObject.SetActive(true);
		this.m_expImg.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		this.m_expImg.transform.localPosition = new Vector3(0f, -2f, 0f);
		if (state == 1)
		{
			int num = (int)(UnityEngine.Random.value * 4f);
			string path = "Texture/Ui/manu/" + MainMenuView.m_expName1[num];
			this.m_expImg.sprite = ResourcesLoad.Load<Sprite>(path);
		}
		else
		{
			int num2 = (int)(UnityEngine.Random.value * 4f);
			string path2 = "Texture/Ui/manu/" + MainMenuView.m_expName2[num2];
			this.m_expImg.sprite = ResourcesLoad.Load<Sprite>(path2);
		}
		Sequence expr_C4 = DOTween.Sequence();
		expr_C4.Insert(0f, this.m_expImg.transform.DOLocalMoveY(0f, 0.1f, false));
		expr_C4.Insert(0f, this.m_expImg.transform.DOScale(2f, 0.1f).SetEase(Ease.OutSine));
		expr_C4.Insert(1.5f, this.m_expImg.transform.DOScale(0f, 0.1f));
		expr_C4.InsertCallback(1.6f, delegate
		{
			this.m_expImg.gameObject.SetActive(false);
		});
	}

	public void showFailExpress(int state)
	{
		if (state == 1)
		{
			if (this.m_MainGameView.m_aiView != null)
			{
				this.m_expFailImg.gameObject.SetActive(true);
				this.m_expFailImg.position = this.m_MainGameView.m_aiView.transform.position + new Vector3(-0f, 2.1f, 0f);
				return;
			}
		}
		else
		{
			this.m_expFailImg.gameObject.SetActive(false);
		}
	}

	public void skipMarket()
	{
		ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "评分跳转");
		Singleton<GameManager>.Instance.m_UserInfo.m_isClickSkip = true;
		Singleton<GameManager>.Instance.OnPause();
		ControlsBase<AndroidControl>.Instance.CallAndroidSkipMarketFunc("skipMarket");
	}

	public void showSkip()
	{
		this.m_skipView.showView();
	}

	public void reflushRole()
	{
		this.m_MainGameView.addPlayer();
	}

	public void reflushBall()
	{
		this.m_MainGameView.addBall();
	}
}
