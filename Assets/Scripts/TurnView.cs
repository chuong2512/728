using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class TurnView : MonoBehaviour
{
	[Serializable]
	private sealed class __c
	{
		public static readonly TurnView.__c __9 = new TurnView.__c();

		public static AndroidControl.Callback __9__40_1;

		public static AndroidControl.Callback __9__40_2;

		internal void _clickAdSpin_b__40_1()
		{
		}

		internal void _clickAdSpin_b__40_2()
		{
		}
	}

	public Text m_item1Txt;

	public Image m_item1Img;

	public Image m_item1BallImg;

	public Text m_item2Txt;

	public Image m_item2Img;

	public Image m_item2BallImg;

	public Text m_item3Txt;

	public Image m_item3Img;

	public Image m_item3BallImg;

	public Text m_item4Txt;

	public Image m_item4Img;

	public Image m_item4BallImg;

	public Text m_item5Txt;

	public Image m_item5Img;

	public Image m_item5BallImg;

	public Text m_item6Txt;

	public Image m_item6Img;

	public Image m_item6BallImg;

	public Transform m_turn;

	private int[] m_itemtps = new int[6];

	private int[] m_itemvals = new int[6];

	private bool m_spinFree;

	public Transform m_spinBtn;

	public Transform m_spinAdBtn;

	public TipsView m_TipsView;

	public int m_rwTp;

	public int m_rwInd;

	public int m_rwNum;

	public Transform m_endEf;

	public Transform m_LeftBtn1;

	public Transform m_RightBtn1;

	public Transform m_RightBtn2;

	public Transform m_ScaleView1;

	public static string[] m_ballPics = new string[]
	{
		"Texture/Ui/shop/item/volley_s",
		"Texture/Ui/shop/item/socc_s",
		"Texture/Ui/shop/item/chick_s",
		"Texture/Ui/shop/item/base_s",
		"Texture/Ui/shop/item/bas_s",
		"Texture/Ui/shop/item/pig_s",
		"Texture/Ui/shop/item/ball/1",
		"Texture/Ui/shop/item/ball/2",
		"Texture/Ui/shop/item/ball/3",
		"Texture/Ui/shop/item/ball/4",
		"Texture/Ui/shop/item/ball/5",
		"Texture/Ui/shop/item/ball/6",
		"Texture/Ui/shop/item/ball/7",
		"Texture/Ui/shop/item/ball/8",
		"Texture/Ui/shop/item/ball/9",
		"Texture/Ui/shop/item/ball/10",
		"Texture/Ui/shop/item/ball/11",
		"Texture/Ui/shop/item/ball/12",
		"Texture/Ui/shop/item/ball/Beachball",
		"Texture/Ui/shop/item/ball/china",
		"Texture/Ui/shop/item/ball/earth",
		"Texture/Ui/shop/item/ball/qwl",
		"Texture/Ui/shop/item/ball/panda",
		"Texture/Ui/shop/item/ball/pokemon",
		"Texture/Ui/shop/item/ball/pumpkin",
		"Texture/Ui/shop/item/ball/ROBOT",
		"Texture/Ui/shop/item/ball/socc",
		"Texture/Ui/shop/item/ball/Sphere"
	};

	private void Start()
	{
	}

	public void showView()
	{
		base.transform.gameObject.SetActive(true);
		this.randRw();
		this.m_turn.localEulerAngles = Vector3.zero;
		if (this.m_spinFree)
		{
			this.m_spinBtn.gameObject.SetActive(true);
			this.m_spinAdBtn.gameObject.SetActive(false);
		}
		else
		{
			this.m_spinBtn.gameObject.SetActive(false);
			this.m_spinAdBtn.gameObject.SetActive(true);
		}
		this.playEf();
	}

	public void playEf()
	{
		MainMenuView.m_this.m_PublicView.playEf();
		this.m_ScaleView1.localScale = Vector3.zero;
		float x = this.m_LeftBtn1.localPosition.x;
		this.m_LeftBtn1.localPosition = new Vector3(-200f, this.m_LeftBtn1.localPosition.y, this.m_LeftBtn1.localPosition.z);
		this.m_LeftBtn1.DOLocalMoveX(x, 0.6f, false).SetEase(Ease.OutSine);
		float x2 = this.m_RightBtn1.localPosition.x;
		this.m_RightBtn1.localPosition = new Vector3(1000f, this.m_RightBtn1.localPosition.y, this.m_RightBtn1.localPosition.z);
		this.m_RightBtn1.DOLocalMoveX(x2, 0.6f, false).SetEase(Ease.OutSine);
		Vector3 arg_E8_0 = this.m_RightBtn2.localPosition;
		this.m_RightBtn2.localPosition = new Vector3(1000f, this.m_RightBtn2.localPosition.y, this.m_RightBtn2.localPosition.z);
		this.m_RightBtn2.DOLocalMoveX(x2, 0.6f, false).SetEase(Ease.OutSine);
		DOTween.Sequence().Insert(0f, this.m_ScaleView1.DOScale(1f, 0.2f));
	}

	private void randRw()
	{
		this.m_itemtps[0] = 0;
		this.m_itemvals[0] = 200;
		this.m_itemtps[1] = 0;
		this.m_itemvals[1] = 400;
		this.m_itemtps[2] = 0;
		this.m_itemvals[2] = 800;
		this.m_itemtps[3] = 1;
		this.m_itemvals[3] = Singleton<GameManager>.Instance.getRandSkin();
		if (this.m_itemvals[3] == 0)
		{
			this.m_itemtps[3] = 0;
			this.m_itemvals[3] = 1000;
		}
		this.m_itemtps[4] = 1;
		this.m_itemvals[4] = Singleton<GameManager>.Instance.getRandSkin();
		if (this.m_itemvals[4] == 0)
		{
			this.m_itemtps[4] = 0;
			this.m_itemvals[4] = 1200;
		}
		this.m_itemtps[5] = 1;
		this.m_itemvals[5] = Singleton<GameManager>.Instance.getRandSkin();
		if (this.m_itemvals[5] == 0)
		{
			this.m_itemtps[5] = 0;
			this.m_itemvals[5] = 1500;
		}
		if (this.m_itemtps[0] == 0)
		{
			this.m_item1Img.gameObject.SetActive(true);
			this.m_item1BallImg.gameObject.SetActive(false);
			this.m_item1Img.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/qqtmicon_01");
			this.m_item1Txt.text = string.Concat(this.m_itemvals[0]);
		}
		else
		{
			this.m_item1Img.gameObject.SetActive(false);
			this.m_item1BallImg.gameObject.SetActive(true);
			this.m_item1BallImg.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/shop/item/volley_s");
			this.m_item1Txt.text = "1";
		}
		if (this.m_itemtps[1] == 0)
		{
			this.m_item2Img.gameObject.SetActive(true);
			this.m_item2BallImg.gameObject.SetActive(false);
			this.m_item2Img.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/qqtmicon_02");
			this.m_item2Txt.text = string.Concat(this.m_itemvals[1]);
		}
		else
		{
			this.m_item2Img.gameObject.SetActive(false);
			this.m_item2BallImg.gameObject.SetActive(true);
			this.m_item2BallImg.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/shop/item/volley_s");
			this.m_item2Txt.text = "1";
		}
		if (this.m_itemtps[2] == 0)
		{
			this.m_item3Img.gameObject.SetActive(true);
			this.m_item3BallImg.gameObject.SetActive(false);
			this.m_item3Img.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/qqtmicon_03");
			this.m_item3Txt.text = string.Concat(this.m_itemvals[2]);
		}
		else
		{
			this.m_item3Img.gameObject.SetActive(false);
			this.m_item3BallImg.gameObject.SetActive(true);
			this.m_item3BallImg.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/shop/item/volley_s");
			this.m_item3Txt.text = "1";
		}
		if (this.m_itemtps[3] == 0)
		{
			this.m_item4Img.gameObject.SetActive(true);
			this.m_item4BallImg.gameObject.SetActive(false);
			this.m_item4Img.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/qqtmicon_04");
			this.m_item4Txt.text = string.Concat(this.m_itemvals[3]);
		}
		else
		{
			this.m_item4Img.gameObject.SetActive(false);
			this.m_item4BallImg.gameObject.SetActive(true);
			this.m_item4BallImg.sprite = ResourcesLoad.Load<Sprite>(TurnView.m_ballPics[this.m_itemvals[3]]);
			this.m_item4Txt.text = "1";
		}
		if (this.m_itemtps[4] == 0)
		{
			this.m_item5Img.gameObject.SetActive(true);
			this.m_item5BallImg.gameObject.SetActive(false);
			this.m_item5Img.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/qqtmicon_05");
			this.m_item5Txt.text = string.Concat(this.m_itemvals[4]);
		}
		else
		{
			this.m_item5Img.gameObject.SetActive(false);
			this.m_item5BallImg.gameObject.SetActive(true);
			this.m_item5BallImg.sprite = ResourcesLoad.Load<Sprite>(TurnView.m_ballPics[this.m_itemvals[4]]);
			this.m_item5Txt.text = "1";
		}
		if (this.m_itemtps[5] == 0)
		{
			this.m_item6Img.gameObject.SetActive(true);
			this.m_item6BallImg.gameObject.SetActive(false);
			this.m_item6Img.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/manu/qqtmicon_06");
			this.m_item6Txt.text = string.Concat(this.m_itemvals[5]);
			return;
		}
		this.m_item6Img.gameObject.SetActive(false);
		this.m_item6BallImg.gameObject.SetActive(true);
		this.m_item6BallImg.sprite = ResourcesLoad.Load<Sprite>(TurnView.m_ballPics[this.m_itemvals[5]]);
		this.m_item6Txt.text = "1";
	}

	public void clickBack()
	{
		MainMenuView.m_this.gameObject.SetActive(true);
		base.transform.gameObject.SetActive(false);
		MainMenuView.m_this.showView();
	}

	public void clickSpin()
	{
		this.m_spinFree = false;
		this.startSpin();
		ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "转盘免费");
	}

	public void clickAdSpin()
	{
		this.m_spinFree = false;
        /*
		AdControl arg_5B_0 = ControlsBase<AdControl>.Instance;
		string arg_5B_1 = "3";
		AndroidControl.Callback arg_5B_2 = delegate
		{
			ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "转盘奖励");
			Singleton<MissionManager>.Instance.addMissionVal(2, 1);
			this.startSpin();
		};
		AndroidControl.Callback arg_5B_3;
		if ((arg_5B_3 = TurnView.__c.__9__40_1) == null)
		{
			arg_5B_3 = (TurnView.__c.__9__40_1 = new AndroidControl.Callback(TurnView.__c.__9._clickAdSpin_b__40_1));
		}
		AndroidControl.Callback arg_5B_4;
		if ((arg_5B_4 = TurnView.__c.__9__40_2) == null)
		{
			arg_5B_4 = (TurnView.__c.__9__40_2 = new AndroidControl.Callback(TurnView.__c.__9._clickAdSpin_b__40_2));
		}
		arg_5B_0.ShowRwAd(arg_5B_1, arg_5B_2, arg_5B_3, arg_5B_4);
		*/
        if (AdsControl.Instance.GetRewardAvailable())
        {
            //AdsControl.Instance.PlayDelegateRewardVideo(delegate
            {
                ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "转盘奖励");
                Singleton<MissionManager>.Instance.addMissionVal(2, 1);
                this.startSpin();
            };

        }
    }

	private void startSpin()
	{
		int num = (int)(UnityEngine.Random.value * 100f);
		int num2 = 0;
		if (num >= 30 && num < 55)
		{
			num2 = 1;
		}
		else if (num >= 55 && num < 70)
		{
			num2 = 2;
		}
		else if (num >= 70 && num < 80)
		{
			num2 = 3;
		}
		else if (num >= 80 && num < 90)
		{
			num2 = 4;
		}
		else if (num >= 90 && num < 100)
		{
			num2 = 5;
		}
		this.m_rwInd = this.m_itemtps[num2];
		if (this.m_itemtps[num2] == 0)
		{
			this.m_rwTp = 1;
			this.m_rwNum = this.m_itemvals[num2];
		}
		else
		{
			this.m_rwTp = 0;
			this.m_rwInd = this.m_itemvals[num2];
		}
		float num3 = (float)(3600 - 60 * num2);
		Sequence expr_A8 = DOTween.Sequence();
		expr_A8.Append(this.m_turn.DOLocalRotate(new Vector3(0f, 0f, 1800f), 2.5f, RotateMode.LocalAxisAdd).SetEase(Ease.InBack));
		expr_A8.Append(this.m_turn.DOLocalRotate(new Vector3(0f, 0f, num3 - 1800f), 2f, RotateMode.LocalAxisAdd).SetEase(Ease.OutSine));
		expr_A8.AppendCallback(delegate
		{
			this.endEf();
		});
		expr_A8.AppendInterval(0.5f);
		expr_A8.AppendCallback(delegate
		{
			this.endSpin();
		});
		expr_A8.SetEase(Ease.Linear);
	}

	private void endEf()
	{
		Sequence expr_05 = DOTween.Sequence();
		expr_05.AppendCallback(delegate
		{
			this.m_endEf.gameObject.SetActive(true);
		});
		expr_05.AppendInterval(0.1f);
		expr_05.AppendCallback(delegate
		{
			this.m_endEf.gameObject.SetActive(false);
		});
		expr_05.AppendInterval(0.1f);
		expr_05.AppendCallback(delegate
		{
			this.m_endEf.gameObject.SetActive(true);
		});
		expr_05.AppendInterval(0.1f);
		expr_05.AppendCallback(delegate
		{
			this.m_endEf.gameObject.SetActive(false);
		});
		expr_05.AppendInterval(0.1f);
		expr_05.AppendCallback(delegate
		{
			this.m_endEf.gameObject.SetActive(true);
		});
		expr_05.AppendInterval(0.1f);
		expr_05.AppendCallback(delegate
		{
			this.m_endEf.gameObject.SetActive(false);
		});
		expr_05.AppendInterval(0.1f);
	}

	private void endSpin()
	{
		this.m_TipsView.m_tipTp = this.m_rwTp;
		this.m_TipsView.m_rwInd = this.m_rwInd;
		this.m_TipsView.m_rwNum = this.m_rwNum;
		this.m_TipsView.showView();
		this.showView();
	}

	private void Update()
	{
		Input.GetKeyDown(KeyCode.Escape);
	}
}
