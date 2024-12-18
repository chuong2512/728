using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class LvView : MonoBehaviour
{
	public Text m_lvTxt1;

	public Text m_lvTxt2;

	public Text m_nickNameTxt;

	public Transform m_urBg;

	public Image m_bgView;

	private static string[] m_images = new string[]
	{
		"shop_bg1",
		"shop_bg2",
		"shop_bg3",
		"shop_bg4",
		"shop_bg5",
		"shop_bg6",
		"shop_bg7"
	};

	private GameObject m_objl;

	private GameObject m_objn;

	private GameObject m_objnn;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void showView()
	{
		base.Invoke("clickClose", 2f);
		base.transform.gameObject.SetActive(true);
		this.m_lvTxt1.text = "LEAGUE : " + Singleton<GameManager>.Instance.getLeagueStr();
		this.m_lvTxt2.text = "#" + Singleton<GameManager>.Instance.m_UserInfo.m_leagueLvStep;
		this.m_nickNameTxt.text = Singleton<GameManager>.Instance.getNickName();
		this.addUrView();
		int num = Singleton<GameManager>.Instance.m_UserInfo.m_leagueLv % 7;
		this.m_bgView.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/shop/" + LvView.m_images[num]);
	}

	private void addUrView()
	{
		int childCount = this.m_urBg.childCount;
		for (int i = 0; i < childCount; i++)
		{
			if (this.m_urBg.GetChild(i).transform != null)
			{
				UnityEngine.Object.Destroy(this.m_urBg.GetChild(i).gameObject);
			}
		}
		this.m_objl = null;
		this.m_objn = null;
		this.m_objnn = null;
		int num = Singleton<GameManager>.Instance.m_UserInfo.m_missInd - 1;
		int num2 = Singleton<GameManager>.Instance.m_UserInfo.m_missInd;
		int num3 = Singleton<GameManager>.Instance.m_UserInfo.m_missInd + 1;
		num %= 30;
		num2 %= 30;
		num3 %= 30;
		if (num <= 0)
		{
			num = 30;
		}
		if (num2 <= 0)
		{
			num2 = 30;
		}
		if (num3 <= 0)
		{
			num3 = 30;
		}
		int arg_1F1_0 = Singleton<GameManager>.Instance.getLeagueNow();
		int leagueMax = Singleton<GameManager>.Instance.getLeagueMax();
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>("Prefab/Ui/lv/u" + num));
		gameObject.transform.SetParent(this.m_urBg);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		this.m_objl = gameObject;
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>("Prefab/Ui/lv/u" + num2));
		gameObject2.transform.SetParent(this.m_urBg);
		gameObject2.transform.localPosition = new Vector3(-2f, 0f, -4f);
		gameObject2.transform.localScale = new Vector3(1f, 1f, 1f);
		gameObject2.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		this.m_objn = gameObject2;
		if (arg_1F1_0 < leagueMax - 1)
		{
			GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>("Prefab/Ui/lv/u" + num3));
			gameObject3.transform.SetParent(this.m_urBg);
			gameObject3.transform.localPosition = new Vector3(-4f, 0f, -8f);
			gameObject3.transform.localScale = new Vector3(1f, 1f, 1f);
			gameObject3.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			this.m_objnn = gameObject3;
		}
		base.Invoke("startMove", 0.5f);
	}

	private void startMove()
	{
		this.m_objl.transform.DOLocalMove(new Vector3(2f, 0f, 4f), 0.3f, false).SetEase(Ease.Linear);
		this.m_objn.transform.DOLocalMove(new Vector3(0f, 0f, 0f), 0.3f, false).SetEase(Ease.Linear);
		if (this.m_objnn)
		{
			this.m_objnn.transform.DOLocalMove(new Vector3(-2f, 0f, -4f), 0.3f, false).SetEase(Ease.Linear);
		}
		Sequence expr_A5 = DOTween.Sequence();
		expr_A5.AppendInterval(0.3f);
		expr_A5.AppendCallback(delegate
		{
			this.m_lvTxt1.text = "LEAGUE : " + Singleton<GameManager>.Instance.getLeagueStr();
			this.m_lvTxt2.text = "#" + Singleton<GameManager>.Instance.m_UserInfo.m_leagueLvStep;
			this.m_nickNameTxt.text = Singleton<GameManager>.Instance.getNickName();
		});
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
