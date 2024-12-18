using DG.Tweening;
using Engine;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class PublicView : MonoBehaviour
{
	private sealed class __c__DisplayClass12_0
	{
		public PublicView __4__this;

		public int val;
	}

	private sealed class __c__DisplayClass12_1
	{
		public int tmpInd;

		public PublicView.__c__DisplayClass12_0 CS___8__locals1;

		internal void _moveMoney_b__0()
		{
			this.CS___8__locals1.__4__this.m_moveMoneyImg[this.tmpInd].gameObject.SetActive(false);
			if (this.tmpInd == 0)
			{
				Singleton<GameManager>.Instance.addCoins(this.CS___8__locals1.val);
			}
		}
	}

	public Transform m_btnBg;

	public Transform m_sysbtnBg;

	public Transform m_coinBg;

	public Image m_coinTips;

	public Transform[] m_moveMoneyImg;

	public Transform m_staticMoneyImg;

	public Text m_coins;

	public Image m_soundImg;

	public Image m_shakeImg;

	private void Start()
	{
		this.InitEventListener();
		this.m_coins.text = Singleton<GameManager>.Instance.changeValToStr(Singleton<GameManager>.Instance.m_UserInfo.m_coins);
	}

	private void Update()
	{
	}

	public void playEf()
	{
		float x = this.m_sysbtnBg.localPosition.x;
		float x2 = this.m_coinBg.localPosition.x;
		this.m_sysbtnBg.localPosition = new Vector3(-200f, this.m_sysbtnBg.localPosition.y, this.m_sysbtnBg.localPosition.z);
		this.m_coinBg.localPosition = new Vector3(200f, this.m_coinBg.localPosition.y, this.m_coinBg.localPosition.z);
		this.m_sysbtnBg.DOLocalMoveX(x, 0.6f, false).SetEase(Ease.OutSine);
		this.m_coinBg.DOLocalMoveX(x2, 0.6f, false).SetEase(Ease.OutSine);
	}

	public void moveMoney(int val, Vector3 pos)
	{
		for (int i = 0; i < this.m_moveMoneyImg.Length; i++)
		{
			int tmpInd = i;
			float x = UnityEngine.Random.value * 30f - 15f;
			float y = UnityEngine.Random.value * 30f - 15f;
			float duration = UnityEngine.Random.value * 0.2f + 0.6f;
			this.m_moveMoneyImg[i].gameObject.SetActive(true);
			this.m_moveMoneyImg[i].position = pos;
			Sequence expr_8D = DOTween.Sequence();
			expr_8D.Append(this.m_moveMoneyImg[i].DOMove(pos + new Vector3(x, y, 0f), 0.3f, false));
			expr_8D.Append(this.m_moveMoneyImg[i].DOMove(this.m_staticMoneyImg.position, duration, false));
			expr_8D.AppendCallback(delegate
			{
				this.m_moveMoneyImg[tmpInd].gameObject.SetActive(false);
				if (tmpInd == 0)
				{
					Singleton<GameManager>.Instance.addCoins(val);
				}
			});
		}
	}

	private void reflushDiamond(object data)
	{
		this.m_coins.DOText(Singleton<GameManager>.Instance.changeValToStr(Singleton<GameManager>.Instance.m_UserInfo.m_coins), 0.5f, true, ScrambleMode.None, null);
		Sequence expr_32 = DOTween.Sequence();
		expr_32.Append(this.m_coins.transform.DOScale(1.2f, 0.2f));
		expr_32.Append(this.m_coins.transform.DOScale(1f, 0.2f));
	}

	private void reflushTips(object data)
	{
		this.m_coinTips.gameObject.SetActive(true);
		this.m_coinTips.transform.localPosition = new Vector3(0f, 100f, 0f);
		this.m_coinTips.color = new Color(255f, 255f, 255f, 0f);
		this.m_coinBg.DOPause();
		Sequence expr_6A = DOTween.Sequence();
		expr_6A.Insert(0f, this.m_coinTips.DOFade(1f, 0.5f));
		expr_6A.Insert(0f, this.m_coinTips.transform.DOLocalMoveY(200f, 0.5f, false));
		expr_6A.Insert(1.5f, this.m_coinTips.DOFade(0f, 0.5f));
		expr_6A.Insert(1.5f, this.m_coinTips.transform.DOLocalMoveY(300f, 0.5f, false));
		expr_6A.InsertCallback(2f, delegate
		{
			this.m_coinTips.gameObject.SetActive(false);
		});
	}

	public void InitEventListener()
	{
		EventsMgr.Instance.AttachEvent("Diamond_Reflush", new EventsMgr.CommonEvent(this.reflushDiamond));
		EventsMgr.Instance.AttachEvent("Tips_Reflush", new EventsMgr.CommonEvent(this.reflushTips));
	}

	private void RemoveEventListener()
	{
		EventsMgr.Instance.DetachEvent("Diamond_Reflush", new EventsMgr.CommonEvent(this.reflushDiamond));
		EventsMgr.Instance.DetachEvent("Tips_Reflush", new EventsMgr.CommonEvent(this.reflushTips));
	}

	private void OnDestroy()
	{
		this.RemoveEventListener();
	}

	public void setShowTp(int tp)
	{
		if (tp == 0)
		{
			this.m_sysbtnBg.gameObject.SetActive(true);
			this.m_coinBg.gameObject.SetActive(true);
			return;
		}
		if (tp == 1)
		{
			this.m_sysbtnBg.gameObject.SetActive(false);
			this.m_coinBg.gameObject.SetActive(true);
		}
	}

	public void clickSysBtn()
	{
		if (this.m_btnBg.gameObject.activeSelf)
		{
			this.m_btnBg.gameObject.SetActive(false);
		}
		else
		{
			this.m_btnBg.gameObject.SetActive(true);
		}
		if (Singleton<GameManager>.Instance.m_UserInfo.SoundState)
		{
			this.m_soundImg.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/btn_music_o");
		}
		else
		{
			this.m_soundImg.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/btn_music_c");
		}
		if (Singleton<GameManager>.Instance.m_UserInfo.YaoState)
		{
			this.m_shakeImg.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/btn_shock_o");
			return;
		}
		this.m_shakeImg.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/btn_shock_c");
	}

	public void clickSound()
	{
		if (Singleton<GameManager>.Instance.m_UserInfo.SoundState)
		{
			Singleton<GameManager>.Instance.m_UserInfo.SoundState = false;
			AudioManager.m_isStop = false;
			AudioManager.SetBGMVolume(0f);
			AudioManager.SetEffectVolum(0f);
			this.m_soundImg.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/btn_music_c");
			Singleton<GameManager>.Instance.OnPause();
			return;
		}
		Singleton<GameManager>.Instance.m_UserInfo.SoundState = true;
		AudioManager.m_isStop = true;
		AudioManager.SetBGMVolume(1f);
		AudioManager.SetEffectVolum(1f);
		this.m_soundImg.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/btn_music_o");
		Singleton<GameManager>.Instance.OnPause();
	}

	public void clickShake()
	{
		if (Singleton<GameManager>.Instance.m_UserInfo.YaoState)
		{
			Singleton<GameManager>.Instance.m_UserInfo.YaoState = false;
			this.m_shakeImg.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/btn_shock_c");
			Singleton<GameManager>.Instance.OnPause();
			return;
		}
		Singleton<GameManager>.Instance.m_UserInfo.YaoState = true;
		this.m_shakeImg.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/btn_shock_o");
		Singleton<GameManager>.Instance.OnPause();
	}
}
