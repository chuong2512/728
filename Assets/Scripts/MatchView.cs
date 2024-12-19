using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class MatchView : MonoBehaviour
{
    [Serializable]
    private sealed class __c
    {
        public static readonly MatchView.__c __9 = new MatchView.__c();

        public static AndroidControl.Callback __9__25_1;

        public static AndroidControl.Callback __9__25_2;

        internal void _clickAdMatch_b__25_1()
        {
        }

        internal void _clickAdMatch_b__25_2()
        {
        }
    }

    public Transform m_matchBg1;

    public Transform m_matchBg2;

    public Transform m_matchBg3;

    public Transform m_matchBg4;

    public Transform m_matchBg5;

    public Transform m_matchBg6;

    public ScrollViewListener3 m_scrol;

    public Image m_buyBtn;

    public Transform m_playBtn;

    public Transform m_adBtn;

    public Text m_costTxt;

    public Transform m_moneyImg;

    private static int[] m_costs = new int[]
    {
        300,
        700,
        1500,
        4000,
        10000,
        20000
    };

    public MatchCupView m_MatchCupView;

    private int m_ind;

    private void Start()
    {
        ScrollViewListener3.OnPageChange = new Action<int>(this.showInd);
    }

    private void Update()
    {
    }

    private void OnDisable()
    {
        MainMenuView.m_this.m_PublicView.setShowTp(0);
    }

    private void OnEnable()
    {
        MainMenuView.m_this.m_PublicView.setShowTp(1);
    }

    public void showView()
    {
        MainMenuView.m_this.gameObject.SetActive(false);
        base.transform.gameObject.SetActive(true);
        this.showInd(this.m_ind);
    }

    public void showViewEff(int val)
    {
        MainMenuView.m_this.gameObject.SetActive(false);
        base.transform.gameObject.SetActive(true);
        this.showInd(this.m_ind);
        if (val > 0)
        {
            this.moveMoney(val);
        }
    }

    private void moveMoney(int val)
    {
        MainMenuView.m_this.m_PublicView.moveMoney(val, this.m_moneyImg.position);
    }

    public void clickBack()
    {
        base.transform.gameObject.SetActive(false);
        MainMenuView.m_this.gameObject.SetActive(true);
        MainMenuView.m_this.showView();
    }

    public void clickMatch()
    {
        if (Singleton<GameManager>.Instance.addCoins(-MatchView.m_costs[this.m_ind]))
        {
            ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "金币-买门票");
            base.transform.gameObject.SetActive(false);
            this.m_MatchCupView.m_matchInd = this.m_ind;
            this.m_MatchCupView.m_moneyVal = MatchView.m_costs[this.m_ind];
            this.m_MatchCupView.showView();
        }
    }

    public void clickPlayMatch()
    {
        if (Singleton<MatchManager>.Instance.m_MatchInfo.m_matchTp[this.m_ind] == 1)
        {
            ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "继续杯赛");
            base.transform.gameObject.SetActive(false);
            this.m_MatchCupView.m_matchInd = this.m_ind;
            this.m_MatchCupView.m_moneyVal = MatchView.m_costs[this.m_ind];
            this.m_MatchCupView.showView();
        }
    }

    public void clickAdMatch()
    {
        /*
        AdControl arg_54_0 = ControlsBase<AdControl>.Instance;
        string arg_54_1 = "3";
        AndroidControl.Callback arg_54_2 = delegate
        {
            ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "激励-买门票");
            Singleton<MissionManager>.Instance.addMissionVal(2, 1);
            this.m_MatchCupView.m_matchInd = 0;
            this.m_MatchCupView.m_moneyVal = MatchView.m_costs[this.m_ind];
            base.transform.gameObject.SetActive(false);
            this.m_MatchCupView.showView();
        };
        AndroidControl.Callback arg_54_3;
        if ((arg_54_3 = MatchView.__c.__9__25_1) == null)
        {
            arg_54_3 = (MatchView.__c.__9__25_1 = new AndroidControl.Callback(MatchView.__c.__9._clickAdMatch_b__25_1));
        }
        AndroidControl.Callback arg_54_4;
        if ((arg_54_4 = MatchView.__c.__9__25_2) == null)
        {
            arg_54_4 = (MatchView.__c.__9__25_2 = new AndroidControl.Callback(MatchView.__c.__9._clickAdMatch_b__25_2));
        }
        arg_54_0.ShowRwAd(arg_54_1, arg_54_2, arg_54_3, arg_54_4);
        */
        if (AdsControl.Instance.GetRewardAvailable())
        {
            ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "激励-买门票");
            Singleton<MissionManager>.Instance.addMissionVal(2, 1);
            this.m_MatchCupView.m_matchInd = 0;
            this.m_MatchCupView.m_moneyVal = MatchView.m_costs[this.m_ind];
            base.transform.gameObject.SetActive(false);
            this.m_MatchCupView.showView();
        }
    }

    public void showInd(int ind)
    {
        this.m_buyBtn.gameObject.SetActive(false);
        this.m_playBtn.gameObject.SetActive(false);
        this.m_adBtn.gameObject.SetActive(false);
        this.m_ind = ind;
        this.m_costTxt.text = string.Concat(MatchView.m_costs[this.m_ind]);
        if (ind == 0)
        {
            if (Singleton<MatchManager>.Instance.m_MatchInfo.m_matchTp[0] != 0)
            {
                this.m_playBtn.gameObject.SetActive(true);
                return;
            }

            this.m_buyBtn.gameObject.SetActive(true);
            this.m_buyBtn.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/match/rpqn_04");
            return;
        }
        else if (ind == 1)
        {
            if (Singleton<MatchManager>.Instance.m_MatchInfo.m_matchTp[1] != 0)
            {
                this.m_playBtn.gameObject.SetActive(true);
                return;
            }

            this.m_buyBtn.gameObject.SetActive(true);
            if (Singleton<GameManager>.Instance.enableCoins(-MatchView.m_costs[this.m_ind]))
            {
                this.m_buyBtn.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/match/rpqn_04");
                return;
            }

            this.m_buyBtn.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/match/rpqn_01");
            return;
        }
        else if (ind == 2)
        {
            if (Singleton<MatchManager>.Instance.m_MatchInfo.m_matchTp[2] != 0)
            {
                this.m_playBtn.gameObject.SetActive(true);
                return;
            }

            this.m_buyBtn.gameObject.SetActive(true);
            if (Singleton<GameManager>.Instance.enableCoins(-MatchView.m_costs[this.m_ind]))
            {
                this.m_buyBtn.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/match/rpqn_04");
                return;
            }

            this.m_buyBtn.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/match/rpqn_01");
            return;
        }
        else if (ind == 3)
        {
            if (Singleton<MatchManager>.Instance.m_MatchInfo.m_matchTp[3] != 0)
            {
                this.m_playBtn.gameObject.SetActive(true);
                return;
            }

            this.m_buyBtn.gameObject.SetActive(true);
            if (Singleton<GameManager>.Instance.enableCoins(-MatchView.m_costs[this.m_ind]))
            {
                this.m_buyBtn.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/match/rpqn_04");
                return;
            }

            this.m_buyBtn.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/match/rpqn_01");
            return;
        }
        else
        {
            if (ind != 4)
            {
                if (ind == 5)
                {
                    if (Singleton<MatchManager>.Instance.m_MatchInfo.m_matchTp[5] == 0)
                    {
                        this.m_buyBtn.gameObject.SetActive(true);
                        if (Singleton<GameManager>.Instance.enableCoins(-MatchView.m_costs[this.m_ind]))
                        {
                            this.m_buyBtn.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/match/rpqn_04");
                            return;
                        }

                        this.m_buyBtn.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/match/rpqn_01");
                        return;
                    }
                    else
                    {
                        this.m_playBtn.gameObject.SetActive(true);
                    }
                }

                return;
            }

            if (Singleton<MatchManager>.Instance.m_MatchInfo.m_matchTp[4] != 0)
            {
                this.m_playBtn.gameObject.SetActive(true);
                return;
            }

            this.m_buyBtn.gameObject.SetActive(true);
            if (Singleton<GameManager>.Instance.enableCoins(-MatchView.m_costs[this.m_ind]))
            {
                this.m_buyBtn.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/match/rpqn_04");
                return;
            }

            this.m_buyBtn.sprite = ResourcesLoad.Load<Sprite>("Texture/Ui/match/rpqn_01");
            return;
        }
    }
}