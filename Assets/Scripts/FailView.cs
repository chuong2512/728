using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class FailView : MonoBehaviour
{
    [Serializable]
    private sealed class __c
    {
        public static readonly FailView.__c __9 = new FailView.__c();

        public static AndroidControl.Callback __9__15_1;

        public static AndroidControl.Callback __9__15_2;

        public static AndroidControl.Callback __9__16_0;

        public static AndroidControl.Callback __9__16_1;

        public static AndroidControl.Callback __9__17_1;

        public static AndroidControl.Callback __9__17_2;

        public static AndroidControl.Callback __9__19_1;

        public static AndroidControl.Callback __9__19_2;

        internal void _adBtn_b__15_1()
        {
        }

        internal void _adBtn_b__15_2()
        {
        }

        internal void _getBtn_b__16_0()
        {
        }

        internal void _getBtn_b__16_1()
        {
        }

        internal void _clickSave_b__17_1()
        {
        }

        internal void _clickSave_b__17_2()
        {
        }

        internal void _clickSkin_b__19_1()
        {
        }

        internal void _clickSkin_b__19_2()
        {
        }
    }

    public Transform m_imgBg;

    public Image m_efBg;

    public Transform m_bg;

    public Transform m_bg2;

    public Transform m_view;

    public int m_coins;

    public Text m_coinTxt;

    public Image m_SkinBtn;

    public TipsView m_TipsView;

    public int m_skinInd;

    public int m_gameMode;

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void showView()
    {
        this.m_bg.gameObject.SetActive(true);
        this.m_bg2.gameObject.SetActive(false);
        base.transform.gameObject.SetActive(true);
        this.m_imgBg.gameObject.SetActive(true);
        AudioManager.PlayEffectAudio("Mocking_Tonal_Fail_1", false, false);
        this.m_view.localScale = Vector3.zero;
        this.m_bg.localScale = new Vector3(1f, 0f, 1f);
        Sequence expr_85 = DOTween.Sequence();
        expr_85.Insert(0f, this.m_bg.DOScale(1f, 0.2f).SetEase(Ease.Linear));
        expr_85.Insert(0.2f, this.m_view.DOScale(1f, 0.2f).SetEase(Ease.OutBack));
        this.m_efBg.fillAmount = 1f;
        this.m_efBg.DOFillAmount(0f, 10f);
        base.Invoke("saveEnd", 10f);
        this.m_coinTxt.text = "+" + this.m_coins;
        this.m_skinInd = Singleton<GameManager>.Instance.getRandSkin();
        this.m_SkinBtn.gameObject.SetActive(false);
        if (this.m_skinInd > 0)
        {
            this.m_SkinBtn.gameObject.SetActive(true);
        }
    }

    private void saveEnd()
    {
        if (base.gameObject.activeSelf)
        {
            this.m_imgBg.gameObject.SetActive(false);
            this.clickNo();
        }
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
                ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "联赛失败奖励");
            }
            else
            {
                ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "杯赛失败奖励");
            }
            Singleton<MissionManager>.Instance.addMissionVal(2, 1);
            Singleton<GameManager>.Instance.addCoins(this.m_coins * 3);
            this.clickClose();
        };
        AndroidControl.Callback arg_54_3;
        if ((arg_54_3 = FailView.__c.__9__15_1) == null)
        {
            arg_54_3 = (FailView.__c.__9__15_1 = new AndroidControl.Callback(FailView.__c.__9._adBtn_b__15_1));
        }
        AndroidControl.Callback arg_54_4;
        if ((arg_54_4 = FailView.__c.__9__15_2) == null)
        {
            arg_54_4 = (FailView.__c.__9__15_2 = new AndroidControl.Callback(FailView.__c.__9._adBtn_b__15_2));
        }
        arg_54_0.ShowRwAd(arg_54_1, arg_54_2, arg_54_3, arg_54_4);
        */
        if (AdsControl.Instance.GetRewardAvailable())
        {
        }
    }

    public void getBtn()
    {
        this.clickClose();
        Singleton<GameManager>.Instance.addCoins(this.m_coins);
        if (this.m_gameMode == 0)
        {
            ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "联赛失败插屏");
        }
        else
        {
            ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "杯赛失败插屏");
        }

        Singleton<GameManager>.Instance.OnPause();
        /*
        AdControl arg_9B_0 = ControlsBase<AdControl>.Instance;
        string arg_9B_1 = "3";
        AndroidControl.Callback arg_9B_2;
        if ((arg_9B_2 = FailView.__c.__9__16_0) == null)
        {
            arg_9B_2 = (FailView.__c.__9__16_0 = new AndroidControl.Callback(FailView.__c.__9._getBtn_b__16_0));
        }
        AndroidControl.Callback arg_9B_3;
        if ((arg_9B_3 = FailView.__c.__9__16_1) == null)
        {
            arg_9B_3 = (FailView.__c.__9__16_1 = new AndroidControl.Callback(FailView.__c.__9._getBtn_b__16_1));
        }
        arg_9B_0.ShowIntAd(arg_9B_1, arg_9B_2, arg_9B_3);
        */
        AdsControl.Instance.showAds();
    }

    public void clickSave()
    {
        /*
        AdControl arg_54_0 = ControlsBase<AdControl>.Instance;
        string arg_54_1 = "3";
        AndroidControl.Callback arg_54_2 = delegate
        {
            if (this.m_gameMode == 0)
            {
                ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "联赛复活");
            }
            else
            {
                ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "杯赛复活");
            }
            Singleton<MissionManager>.Instance.addMissionVal(2, 1);
            MainMenuView.m_this.m_MainGameView.saveGame();
            base.transform.gameObject.SetActive(false);
        };
        AndroidControl.Callback arg_54_3;
        if ((arg_54_3 = FailView.__c.__9__17_1) == null)
        {
            arg_54_3 = (FailView.__c.__9__17_1 = new AndroidControl.Callback(FailView.__c.__9._clickSave_b__17_1));
        }
        AndroidControl.Callback arg_54_4;
        if ((arg_54_4 = FailView.__c.__9__17_2) == null)
        {
            arg_54_4 = (FailView.__c.__9__17_2 = new AndroidControl.Callback(FailView.__c.__9._clickSave_b__17_2));
        }
        arg_54_0.ShowRwAd(arg_54_1, arg_54_2, arg_54_3, arg_54_4);
        */
        if (AdsControl.Instance.GetRewardAvailable())
        {
        }
    }

    public void clickClose()
    {
        if (this.m_gameMode == 0)
        {
            Singleton<GameManager>.Instance.m_UserInfo.m_leagueLvStep = 0;
            base.CancelInvoke("saveEnd");
            MainMenuView.m_this.initGame();
            base.transform.gameObject.SetActive(false);
            return;
        }

        base.CancelInvoke("saveEnd");
        MainMenuView.m_this.initGame();
        MainMenuView.m_this.m_MatchView.m_MatchCupView.matchResult(false);
        base.transform.gameObject.SetActive(false);
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
                ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "联赛失败皮肤-奖励");
            }
            else
            {
                ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "杯赛失败皮肤-奖励");
            }
            this.m_SkinBtn.gameObject.SetActive(false);
            this.m_TipsView.m_tipTp = 0;
            this.m_TipsView.m_rwInd = this.m_skinInd;
            this.m_TipsView.showView();
        };
        AndroidControl.Callback arg_54_3;
        if ((arg_54_3 = FailView.__c.__9__19_1) == null)
        {
            arg_54_3 = (FailView.__c.__9__19_1 = new AndroidControl.Callback(FailView.__c.__9._clickSkin_b__19_1));
        }
        AndroidControl.Callback arg_54_4;
        if ((arg_54_4 = FailView.__c.__9__19_2) == null)
        {
            arg_54_4 = (FailView.__c.__9__19_2 = new AndroidControl.Callback(FailView.__c.__9._clickSkin_b__19_2));
        }
        arg_54_0.ShowRwAd(arg_54_1, arg_54_2, arg_54_3, arg_54_4);
        */
        if (AdsControl.Instance.GetRewardAvailable())
        {
        }
    }

    public void clickNo()
    {
        this.m_bg.gameObject.SetActive(false);
        this.m_bg2.gameObject.SetActive(true);
    }
}