using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class MainGameView : MonoBehaviour
{
	private bool m_isEnd;

	private bool m_isStartGame;

	public PlayerView m_PlayerView;

	public BallView m_ballView;

	public AiView m_aiView;

	public Transform m_cameraInitPos;

	public Transform m_ballBg;

	public Transform m_playerBg;

	public Transform m_aiBg;

	public Transform m_effBg;

	public Transform m_vsBg;

	public Transform m_vsLogoBg;

	public Transform m_vsLeft;

	public Transform m_vsRight;

	public Text m_vsLeftTxt;

	public Text m_vsRightTxt;

	public bl_CameraOrbit m_bl_CameraOrbit;

	public Transform m_ballInitPos;

	public Transform m_playerInitPos;

	public Transform m_aiInitPos;

	public FailView m_failView;

	public WinView m_winView;

	public PauseView m_PauseView;

	public Transform m_planeWall;

	public Transform m_uiObjView;

	public SpriteRenderer m_playerScoTxt;

	public SpriteRenderer m_aiScoTxt;

	public SpriteRenderer m_gameLvTxt1;

	public SpriteRenderer m_gameLvTxt10;

	public Transform m_leagueBg;

	public SpriteRenderer m_leagueTxt1;

	public SpriteRenderer m_leagueTxt2;

	public SpriteRenderer m_leagueTxt3;

	public SpriteRenderer m_leagueTxt4;

	public SpriteRenderer m_leagueTxt5;

	public Transform m_room1;

	public Transform m_room_cup_1;

	public Transform m_room_cup_2;

	public Transform m_room_cup_3;

	public Transform m_room_cup_4;

	public Transform m_room_cup_5;

	public Transform m_room_cup_6;

	public int m_gameMode;

	public int m_aiInd;

	public Image m_efBg;

	public Transform m_obBg;

	public Animator m_ob;

	private int m_obState;

	public int m_moneyVal;

	public Transform m_LeftBtn1;

	private bool m_isDown;

	private Vector3 m_touchSPos;

	private float m_rotaValX;

	private float m_rotaValY;

	private void Start()
	{
	}

	private void OnDisable()
	{
		base.CancelInvoke("IsAdCatch");
	}

	private void OnEnable()
	{
		this.setBtnTp(false);
		base.InvokeRepeating("IsAdCatch", 1f, 6f);
	}

	private void IsAdCatch()
	{
		ControlsBase<AndroidControl>.Instance.CallAndroidFunc("isCatchAd", "", new AndroidControl.Callback(this.readyAd));
	}

	private void readyAd()
	{
		this.setBtnTp(true);
	}

	public void setBtnTp(bool adCatch = false)
	{
	}

	public void initGameView()
	{
		base.CancelInvoke();
		this.removeAllChild(this.m_ballBg);
		this.removeAllChild(this.m_playerBg);
		this.removeAllChild(this.m_aiBg);
		this.removeAllChild(this.m_effBg);
		this.m_vsBg.gameObject.SetActive(false);
		this.m_bl_CameraOrbit.setInitPos(this.m_cameraInitPos);
		this.addBall();
		this.addPlayer();
		this.addAi();
		this.m_uiObjView.gameObject.SetActive(false);
		this.m_isEnd = false;
		this.m_isStartGame = false;
		this.m_obState = 0;
		this.m_ob.CrossFade("ready", 0.01f);
		this.m_PauseView.gameObject.SetActive(false);
		this.m_efBg.gameObject.SetActive(false);
		Singleton<GameManager>.Instance.initGame();
		int num = Singleton<GameManager>.Instance.m_UserInfo.m_leagueLvStep + 1;
		if (num < 10)
		{
			this.m_gameLvTxt1.gameObject.SetActive(false);
			this.m_gameLvTxt10.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/public/num_level" + num);
		}
		else
		{
			this.m_gameLvTxt1.gameObject.SetActive(true);
			this.m_gameLvTxt1.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/public/num_level" + num % 10);
			this.m_gameLvTxt10.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/public/num_level" + num / 10);
		}
		this.flushScore();
		string leagueStr = Singleton<GameManager>.Instance.getLeagueStr();
		int length = leagueStr.Length;
		this.m_leagueBg.transform.localPosition = new Vector3(-0.8f - (float)(length - 1) * 0.24f, 15.1f, 9.73f);
		this.m_leagueTxt1.gameObject.SetActive(false);
		this.m_leagueTxt2.gameObject.SetActive(false);
		this.m_leagueTxt3.gameObject.SetActive(false);
		this.m_leagueTxt4.gameObject.SetActive(false);
		this.m_leagueTxt5.gameObject.SetActive(false);
		if (length == 1)
		{
			this.changeUi(leagueStr.ToCharArray()[0], this.m_leagueTxt1);
			this.m_leagueTxt1.gameObject.SetActive(true);
			return;
		}
		if (length == 2)
		{
			this.changeUi(leagueStr.ToCharArray()[1], this.m_leagueTxt2);
			this.m_leagueTxt2.gameObject.SetActive(true);
			return;
		}
		if (length == 3)
		{
			this.changeUi(leagueStr.ToCharArray()[2], this.m_leagueTxt3);
			this.m_leagueTxt3.gameObject.SetActive(true);
			return;
		}
		if (length == 4)
		{
			this.changeUi(leagueStr.ToCharArray()[3], this.m_leagueTxt4);
			this.m_leagueTxt4.gameObject.SetActive(true);
			return;
		}
		if (length >= 5)
		{
			this.changeUi(leagueStr.ToCharArray()[4], this.m_leagueTxt5);
			this.m_leagueTxt5.gameObject.SetActive(true);
		}
	}

	private void changeUi(char val, SpriteRenderer uis)
	{
		if (val.Equals('F'))
		{
			uis.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/shop/F");
			return;
		}
		if (val.Equals('E'))
		{
			uis.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/shop/E");
			return;
		}
		if (val.Equals('D'))
		{
			uis.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/shop/D");
			return;
		}
		if (val.Equals('C'))
		{
			uis.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/shop/C");
			return;
		}
		if (val.Equals('B'))
		{
			uis.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/shop/B");
			return;
		}
		if (val.Equals('A'))
		{
			uis.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/shop/A");
			return;
		}
		if (val.Equals('S'))
		{
			uis.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/shop/S");
		}
	}

	public void startGameView()
	{
		this.playStartEff();
		base.Invoke("startAni", 2f);
		this.playEf();
		this.addAi();
	}

	private void startGameViewPri()
	{
		base.Invoke("startAni", 0f);
	}

	public void playEf()
	{
		MainMenuView.m_this.m_PublicView.playEf();
		float x = this.m_LeftBtn1.localPosition.x;
		this.m_LeftBtn1.localPosition = new Vector3(-200f, this.m_LeftBtn1.localPosition.y, this.m_LeftBtn1.localPosition.z);
		this.m_LeftBtn1.DOLocalMoveX(x, 0.6f, false).SetEase(Ease.OutSine);
	}

	private void playStartEff()
	{
		this.m_vsBg.gameObject.SetActive(true);
		this.m_vsLogoBg.localScale = Vector3.one * 0f;
		this.m_vsLeft.localPosition = new Vector3(-600f, 0f, 0f);
		this.m_vsRight.localPosition = new Vector3(600f, 0f, 0f);
		Sequence expr_6E = DOTween.Sequence();
		expr_6E.Append(this.m_vsLogoBg.DOScale(1f, 0.3f));
		expr_6E.AppendInterval(1.4f);
		expr_6E.Append(this.m_vsLogoBg.DOScale(0f, 0.3f));
		Sequence expr_B6 = DOTween.Sequence();
		expr_B6.AppendInterval(0.4f);
		expr_B6.Append(this.m_vsLeft.DOLocalMoveX(-220f, 0.2f, false));
		expr_B6.AppendInterval(1f);
		expr_B6.Append(this.m_vsLeft.DOLocalMoveX(-600f, 0.2f, false));
		Sequence expr_10C = DOTween.Sequence();
		expr_10C.AppendInterval(0.4f);
		expr_10C.Append(this.m_vsRight.DOLocalMoveX(220f, 0.2f, false));
		expr_10C.AppendInterval(1f);
		expr_10C.Append(this.m_vsRight.DOLocalMoveX(600f, 0.2f, false));
		Sequence expr_162 = DOTween.Sequence();
		expr_162.AppendInterval(2f);
		expr_162.AppendCallback(delegate
		{
			this.m_vsBg.gameObject.SetActive(false);
		});
	}

	private void startAni()
	{
		this.m_uiObjView.gameObject.SetActive(true);
		this.m_isStartGame = true;
		this.m_ballView.starPlay();
		this.m_aiView.DoInit();
		this.m_PlayerView.DoInit();
		this.m_bl_CameraOrbit.SetTarget(this.m_ballView.transform);
		this.flushScore();
	}

	public void endGame(bool isWin)
	{
		if (this.m_isEnd)
		{
			return;
		}
		this.m_isEnd = true;
		if (isWin)
		{
			Sequence expr_18 = DOTween.Sequence();
			expr_18.AppendInterval(2f);
			expr_18.AppendCallback(delegate
			{
				this.m_winView.m_gameMode = this.m_gameMode;
				if (this.m_gameMode == 0)
				{
					float lvCoins = Singleton<GameManager>.Instance.getLvCoins();
					this.m_winView.m_coins = (int)lvCoins;
				}
				else
				{
					this.m_winView.m_coins = this.m_moneyVal;
				}
				this.m_winView.showView();
			});
			this.m_PlayerView.doWin();
			this.m_aiView.doFail();
			MainMenuView.m_this.m_MainGameView.m_bl_CameraOrbit.setInitPos(this.m_cameraInitPos);
			MainMenuView.m_this.m_MainGameView.m_bl_CameraOrbit.DoRotateWin();
			MainMenuView.m_this.m_winEf.gameObject.SetActive(true);
			return;
		}
		Sequence expr_95 = DOTween.Sequence();
		expr_95.AppendInterval(2f);
		expr_95.AppendCallback(delegate
		{
			this.m_failView.m_gameMode = this.m_gameMode;
			if (this.m_gameMode == 0)
			{
				float num = Singleton<GameManager>.Instance.getLvCoins() * 0.5f;
				this.m_failView.m_coins = (int)num;
			}
			else
			{
				this.m_failView.m_coins = this.m_moneyVal;
			}
			this.m_failView.showView();
		});
		this.m_PlayerView.doFail();
		this.m_aiView.doWin();
	}

	public void saveGame()
	{
		Singleton<GameManager>.Instance.saveGame();
		this.nextRound();
		MainMenuView.m_this.showFailExpress(0);
	}

	public void clickPause()
	{
		this.m_PauseView.m_gameMode = this.m_gameMode;
		this.m_PauseView.showView();
		Time.timeScale = 0f;
	}

	public void nextRound()
	{
		this.m_bl_CameraOrbit.setInitPos(this.m_cameraInitPos);
		this.m_efBg.gameObject.SetActive(true);
		this.m_efBg.transform.localScale = new Vector3(1f, 0f, 1f);
		Sequence expr_4B = DOTween.Sequence();
		expr_4B.Append(this.m_efBg.transform.DOScale(1f, 0.2f).SetEase(Ease.Linear));
		expr_4B.AppendInterval(0.2f);
		expr_4B.Append(this.m_efBg.DOFade(0f, 0.2f).SetEase(Ease.Linear));
		expr_4B.AppendCallback(delegate
		{
			this.m_efBg.gameObject.SetActive(false);
			this.removeAllChild(this.m_ballBg);
			this.removeAllChild(this.m_playerBg);
			this.removeAllChild(this.m_aiBg);
			this.removeAllChild(this.m_effBg);
			this.m_bl_CameraOrbit.setInitPos(this.m_cameraInitPos);
			this.addBall();
			this.addPlayer();
			this.addAi();
			this.m_uiObjView.gameObject.SetActive(false);
			this.m_isEnd = false;
			this.m_isStartGame = false;
			this.m_PauseView.gameObject.SetActive(false);
			this.flushScore();
			this.startGameViewPri();
			this.m_bl_CameraOrbit.SetTarget(this.m_ballView.transform);
			if (this.m_obState == 1)
			{
				this.m_ob.CrossFade("leftback", 0.01f);
			}
			else if (this.m_obState == 2)
			{
				this.m_ob.CrossFade("rightback", 0.01f);
			}
			else
			{
				this.m_ob.CrossFade("ready", 0.01f);
			}
			this.m_efBg.gameObject.SetActive(true);
			this.m_efBg.color = Color.white;
			this.m_efBg.transform.localScale = Vector3.zero;
		});
		expr_4B.SetEase(Ease.Linear);
	}

	private void flushScore()
	{
		this.m_playerScoTxt.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/public/num_score" + Singleton<GameManager>.Instance.m_userPoint);
		this.m_aiScoTxt.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/public/num_score" + Singleton<GameManager>.Instance.m_aiPoint);
	}

	public void addPlayer()
	{
		this.removeAllChild(this.m_playerBg);
		int roleInd = Singleton<GameManager>.Instance.m_UserInfo.m_roleInd;
		string path;
		if (roleInd < 10)
		{
			path = "Prefab/Game/player_new_" + roleInd;
		}
		else
		{
			path = "Prefab/Game/player_new_" + roleInd;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>(path));
		gameObject.transform.SetParent(this.m_playerBg);
		gameObject.transform.position = this.m_playerInitPos.position;
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		gameObject.transform.localEulerAngles = new Vector3(0f, -15f, 0f);
		this.m_PlayerView = gameObject.GetComponentInChildren<PlayerView>();
		this.m_PlayerView.DoInit();
	}

	public void addAi()
	{
		this.removeAllChild(this.m_aiBg);
		GameObject gameObject;
		if (this.m_gameMode == 0)
		{
			int num = Singleton<GameManager>.Instance.m_UserInfo.m_missInd;
			num %= 30;
			if (num == 0)
			{
				num = 30;
			}
			this.m_vsLeftTxt.text = Singleton<GameManager>.Instance.getAiName(num);
			if (num < 10)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>("Prefab/Game/ai_new_" + num));
			}
			else
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>("Prefab/Game/ai_new_" + num));
			}
			gameObject.transform.SetParent(this.m_aiBg);
			gameObject.transform.position = this.m_aiInitPos.position;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			gameObject.transform.localEulerAngles = new Vector3(0f, 10f, 0f);
		}
		else
		{
			int num2 = this.m_aiInd;
			num2 %= 30;
			if (num2 == 0)
			{
				num2 = 30;
			}
			this.m_vsLeftTxt.text = Singleton<GameManager>.Instance.getAiName(num2);
			if (num2 < 10)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>("Prefab/Game/ai_new_" + num2));
			}
			else
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>("Prefab/Game/ai_new_" + num2));
			}
			gameObject.transform.SetParent(this.m_aiBg);
			gameObject.transform.position = this.m_aiInitPos.position;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			gameObject.transform.localEulerAngles = new Vector3(0f, 10f, 0f);
		}
		this.m_aiView = gameObject.GetComponentInChildren<AiView>();
		this.m_aiView.DoInit();
	}

	public void addBall()
	{
		this.removeAllChild(this.m_ballBg);
		int num = Singleton<GameManager>.Instance.m_UserInfo.m_ballInd + 1;
		if (num < 10)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>("Prefab/Game/ball_0" + num));
			gameObject.transform.SetParent(this.m_ballBg);
			gameObject.transform.position = this.m_ballInitPos.position;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			this.m_ballView = gameObject.GetComponentInChildren<BallView>();
		}
		else
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>("Prefab/Game/ball_" + num));
			gameObject2.transform.SetParent(this.m_ballBg);
			gameObject2.transform.position = this.m_ballInitPos.position;
			gameObject2.transform.localScale = new Vector3(1f, 1f, 1f);
			this.m_ballView = gameObject2.GetComponentInChildren<BallView>();
		}
		this.m_isDown = false;
	}

	public void downBall(Transform ball)
	{
		if (this.m_isDown)
		{
			return;
		}
		this.m_isDown = true;
		int num;
		if (ball.position.x < 0f)
		{
			num = Singleton<GameManager>.Instance.addPoint(1, 0);
			this.m_PlayerView.doWin();
			this.m_aiView.doFail();
			this.m_ob.CrossFade("playleft", 0.01f);
			this.m_obState = 1;
		}
		else
		{
			num = Singleton<GameManager>.Instance.addPoint(0, 1);
			this.m_PlayerView.doFail();
			this.m_aiView.doWin();
			this.m_ob.CrossFade("playright", 0.01f);
			this.m_obState = 2;
		}
		AudioManager.PlayEffectAudio("wincrowd", false, false);
		if (num == 1)
		{
			this.endGame(true);
		}
		else if (num == 2)
		{
			this.endGame(false);
			MainMenuView.m_this.showFailExpress(1);
		}
		else
		{
			this.m_isEnd = true;
			base.Invoke("nextRound", 2f);
		}
		this.flushScore();
	}

	public void smashWin()
	{
		if (this.m_isDown)
		{
			return;
		}
		this.m_isDown = true;
		base.Invoke("doSmashWin", 1f);
	}

	private void doSmashWin()
	{
		int num = Singleton<GameManager>.Instance.addPoint(1, 0);
		this.m_PlayerView.doWin();
		this.m_aiView.doFail();
		this.m_ob.CrossFade("playleft", 0.01f);
		this.m_obState = 1;
		if (num == 1)
		{
			this.endGame(true);
		}
		else if (num == 2)
		{
			this.endGame(false);
			MainMenuView.m_this.showFailExpress(1);
		}
		else
		{
			this.m_isEnd = true;
			base.Invoke("nextRound", 2f);
		}
		this.flushScore();
	}

	private void removeAllChild(Transform tran)
	{
		int childCount = tran.childCount;
		for (int i = 0; i < childCount; i++)
		{
			if (tran.GetChild(i).transform != null)
			{
				UnityEngine.Object.Destroy(tran.GetChild(i).gameObject);
			}
		}
	}

	public bool canPlay()
	{
		bool result = false;
		if (!this.m_isEnd && this.m_isStartGame && !this.m_isDown)
		{
			result = true;
		}
		return result;
	}

	public void showSuperView()
	{
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			this.clickPause();
		}
	}

	public void onTouchEnter()
	{
		this.m_PlayerView.DoJump();
	}

	public void onTouchMove()
	{
	}

	public void onTouchClick()
	{
	}

	public void onTouchExit()
	{
	}
}
