using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class MatchCupView : MonoBehaviour
{
	public Text m_team1Name1;

	public Text m_team1Name2;

	public Text m_team1Name3;

	public Text m_team1Name4;

	public Text m_team1Src1;

	public Text m_team1Src2;

	public Text m_team1Src3;

	public Text m_team1Src4;

	public Transform m_team1Bg1;

	public Transform m_team1Bg2;

	public Transform m_team1Bg3;

	public Transform m_team1Bg4;

	public Text m_team2Name1;

	public Text m_team2Name2;

	public Text m_team2Name3;

	public Text m_team2Name4;

	public Text m_team2Src1;

	public Text m_team2Src2;

	public Text m_team2Src3;

	public Text m_team2Src4;

	public Transform m_team2Bg1;

	public Transform m_team2Bg2;

	public Transform m_team2Bg3;

	public Transform m_team2Bg4;

	public Text m_team3Name1;

	public Text m_team3Name2;

	public Text m_team3Name3;

	public Text m_team3Name4;

	public Text m_team3Src1;

	public Text m_team3Src2;

	public Text m_team3Src3;

	public Text m_team3Src4;

	public Transform m_team3Bg1;

	public Transform m_team3Bg2;

	public Transform m_team3Bg3;

	public Transform m_team3Bg4;

	public Text m_team4Name1;

	public Text m_team4Name2;

	public Text m_team4Name3;

	public Text m_team4Name4;

	public Text m_team4Src1;

	public Text m_team4Src2;

	public Text m_team4Src3;

	public Text m_team4Src4;

	public Transform m_team4Bg1;

	public Transform m_team4Bg2;

	public Transform m_team4Bg3;

	public Transform m_team4Bg4;

	public Text m_round1Name1;

	public Text m_round1Name2;

	public Text m_round1Name3;

	public Text m_round1Name4;

	public Text m_round1Name5;

	public Text m_round1Name6;

	public Text m_round1Name7;

	public Text m_round1Name8;

	public Transform m_round1Bg1;

	public Transform m_round1Bg2;

	public Transform m_round1Bg3;

	public Transform m_round1Bg4;

	public Transform m_round1Bg5;

	public Transform m_round1Bg6;

	public Transform m_round1Bg7;

	public Transform m_round1Bg8;

	public Text m_round1Src1;

	public Text m_round1Src2;

	public Text m_round1Src3;

	public Text m_round1Src4;

	public Text m_round2Name1;

	public Text m_round2Name2;

	public Text m_round2Name3;

	public Text m_round2Name4;

	public Transform m_round2Bg1;

	public Transform m_round2Bg2;

	public Transform m_round2Bg3;

	public Transform m_round2Bg4;

	public Text m_round2Src1;

	public Text m_round2Src2;

	public Text m_round3Name1;

	public Text m_round3Name2;

	public Transform m_round3Bg1;

	public Transform m_round3Bg2;

	public Text m_round3Src1;

	public MatchView m_MatchView;

	public int m_matchInd;

	public Transform m_bg1;

	public Transform m_bg2;

	public Transform m_resultBg;

	public Transform m_faileBg;

	public Transform m_nextBg;

	public Transform m_LeftBtn1;

	public Transform m_ScaleView1;

	public int m_moneyVal;

	private void Start()
	{
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

	public void clickBack()
	{
		base.transform.gameObject.SetActive(false);
		this.m_MatchView.showView();
	}

	public void showView()
	{
		base.transform.gameObject.SetActive(true);
		this.m_round1Name1.text = "1/8";
		this.m_round1Name2.text = "1/8";
		this.m_round1Name3.text = "1/8";
		this.m_round1Name4.text = "1/8";
		this.m_round1Name5.text = "1/8";
		this.m_round1Name6.text = "1/8";
		this.m_round1Name7.text = "1/8";
		this.m_round1Name8.text = "1/8";
		this.m_round2Name1.text = "1/4";
		this.m_round2Name2.text = "1/4";
		this.m_round2Name3.text = "1/4";
		this.m_round2Name4.text = "1/4";
		this.m_round3Name1.text = "1/2";
		this.m_round3Name2.text = "1/2";
		this.initData();
		this.m_resultBg.gameObject.SetActive(false);
		this.m_nextBg.gameObject.SetActive(false);
		this.m_faileBg.gameObject.SetActive(false);
		this.playEf();
		Singleton<GameManager>.Instance.OnPause();
	}

	public void playEf()
	{
		float x = this.m_LeftBtn1.localPosition.x;
		this.m_LeftBtn1.localPosition = new Vector3(-200f, this.m_LeftBtn1.localPosition.y, this.m_LeftBtn1.localPosition.z);
		this.m_LeftBtn1.DOLocalMoveX(x, 0.6f, false).SetEase(Ease.OutSine);
		this.m_ScaleView1.localScale = Vector3.zero;
		DOTween.Sequence().Insert(0f, this.m_ScaleView1.DOScale(1f, 0.2f));
	}

	private void initData()
	{
		Singleton<MatchManager>.Instance.randMatchInfo(this.m_matchInd);
		this.reflush();
	}

	public void reflush()
	{
		this.m_bg1.gameObject.SetActive(false);
		this.m_bg2.gameObject.SetActive(false);
		this.m_team1Bg1.gameObject.SetActive(false);
		this.m_team1Bg2.gameObject.SetActive(false);
		this.m_team1Bg3.gameObject.SetActive(false);
		this.m_team1Bg4.gameObject.SetActive(false);
		this.m_team2Bg1.gameObject.SetActive(false);
		this.m_team2Bg2.gameObject.SetActive(false);
		this.m_team2Bg3.gameObject.SetActive(false);
		this.m_team2Bg4.gameObject.SetActive(false);
		this.m_team3Bg1.gameObject.SetActive(false);
		this.m_team3Bg2.gameObject.SetActive(false);
		this.m_team3Bg3.gameObject.SetActive(false);
		this.m_team3Bg4.gameObject.SetActive(false);
		this.m_team4Bg1.gameObject.SetActive(false);
		this.m_team4Bg2.gameObject.SetActive(false);
		this.m_team4Bg3.gameObject.SetActive(false);
		this.m_team4Bg4.gameObject.SetActive(false);
		this.m_round1Bg1.gameObject.SetActive(false);
		this.m_round1Bg2.gameObject.SetActive(false);
		this.m_round1Bg3.gameObject.SetActive(false);
		this.m_round1Bg4.gameObject.SetActive(false);
		this.m_round1Bg5.gameObject.SetActive(false);
		this.m_round1Bg6.gameObject.SetActive(false);
		this.m_round1Bg7.gameObject.SetActive(false);
		this.m_round1Bg8.gameObject.SetActive(false);
		this.m_round2Bg1.gameObject.SetActive(false);
		this.m_round2Bg2.gameObject.SetActive(false);
		this.m_round2Bg3.gameObject.SetActive(false);
		this.m_round2Bg4.gameObject.SetActive(false);
		this.m_round3Bg1.gameObject.SetActive(false);
		this.m_round3Bg2.gameObject.SetActive(false);
		int loopInd = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_loopInd;
		if (loopInd > 2)
		{
			int ind = Singleton<MatchManager>.Instance.getOpponSelfInd(this.m_matchInd);
			this.showToonBg(ind);
		}
		if (loopInd == 0)
		{
			this.m_bg1.gameObject.SetActive(true);
			this.m_team1Name1.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_1);
			this.m_team1Name2.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_2);
			this.m_team1Name3.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_3);
			this.m_team1Name4.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_4);
			this.m_team2Name1.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_1);
			this.m_team2Name2.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_2);
			this.m_team2Name3.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_3);
			this.m_team2Name4.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_4);
			this.m_team3Name1.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_1);
			this.m_team3Name2.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_2);
			this.m_team3Name3.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_3);
			this.m_team3Name4.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_4);
			this.m_team4Name1.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_1);
			this.m_team4Name2.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_2);
			this.m_team4Name3.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_3);
			this.m_team4Name4.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_4);
			this.m_team1Src1.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_1);
			this.m_team1Src2.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_2);
			this.m_team1Src3.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_3);
			this.m_team1Src4.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_4);
			this.m_team2Src1.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_1);
			this.m_team2Src2.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_2);
			this.m_team2Src3.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_3);
			this.m_team2Src4.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_4);
			this.m_team3Src1.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_1);
			this.m_team3Src2.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_2);
			this.m_team3Src3.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_3);
			this.m_team3Src4.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_4);
			this.m_team4Src1.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_1);
			this.m_team4Src2.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_2);
			this.m_team4Src3.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_3);
			this.m_team4Src4.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_4);
			int ind = Singleton<MatchManager>.Instance.getGroupSelfInd(this.m_matchInd);
			this.showGroupBg(ind);
			return;
		}
		if (loopInd == 1)
		{
			this.m_bg1.gameObject.SetActive(true);
			this.m_team1Name1.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_1);
			this.m_team1Name2.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_2);
			this.m_team1Name3.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_3);
			this.m_team1Name4.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_4);
			this.m_team2Name1.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_1);
			this.m_team2Name2.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_2);
			this.m_team2Name3.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_3);
			this.m_team2Name4.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_4);
			this.m_team3Name1.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_1);
			this.m_team3Name2.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_2);
			this.m_team3Name3.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_3);
			this.m_team3Name4.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_4);
			this.m_team4Name1.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_1);
			this.m_team4Name2.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_2);
			this.m_team4Name3.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_3);
			this.m_team4Name4.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_4);
			this.m_team1Src1.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_1);
			this.m_team1Src2.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_2);
			this.m_team1Src3.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_3);
			this.m_team1Src4.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_4);
			this.m_team2Src1.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_1);
			this.m_team2Src2.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_2);
			this.m_team2Src3.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_3);
			this.m_team2Src4.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_4);
			this.m_team3Src1.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_1);
			this.m_team3Src2.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_2);
			this.m_team3Src3.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_3);
			this.m_team3Src4.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_4);
			this.m_team4Src1.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_1);
			this.m_team4Src2.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_2);
			this.m_team4Src3.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_3);
			this.m_team4Src4.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_4);
			base.Invoke("sortTeam", 0.5f);
			return;
		}
		if (loopInd == 2)
		{
			this.m_bg1.gameObject.SetActive(true);
			this.m_team1Name1.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_1);
			this.m_team1Name2.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_2);
			this.m_team1Name3.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_3);
			this.m_team1Name4.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_4);
			this.m_team2Name1.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_1);
			this.m_team2Name2.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_2);
			this.m_team2Name3.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_3);
			this.m_team2Name4.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_4);
			this.m_team3Name1.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_1);
			this.m_team3Name2.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_2);
			this.m_team3Name3.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_3);
			this.m_team3Name4.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_4);
			this.m_team4Name1.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_1);
			this.m_team4Name2.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_2);
			this.m_team4Name3.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_3);
			this.m_team4Name4.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_4);
			this.m_team1Src1.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_1);
			this.m_team1Src2.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_2);
			this.m_team1Src3.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_3);
			this.m_team1Src4.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_4);
			this.m_team2Src1.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_1);
			this.m_team2Src2.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_2);
			this.m_team2Src3.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_3);
			this.m_team2Src4.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_4);
			this.m_team3Src1.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_1);
			this.m_team3Src2.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_2);
			this.m_team3Src3.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_3);
			this.m_team3Src4.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_4);
			this.m_team4Src1.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_1);
			this.m_team4Src2.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_2);
			this.m_team4Src3.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_3);
			this.m_team4Src4.text = Singleton<MatchManager>.Instance.getMatchUserSrc(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_4);
			base.Invoke("sortTeam", 0.5f);
			return;
		}
		if (loopInd == 3)
		{
			this.m_bg2.gameObject.SetActive(true);
			this.m_round1Name1.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_elimTourInd_1_1);
			this.m_round1Name2.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_elimTourInd_1_2);
			this.m_round1Name3.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_elimTourInd_1_3);
			this.m_round1Name4.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_elimTourInd_1_4);
			this.m_round1Name5.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_elimTourInd_1_5);
			this.m_round1Name6.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_elimTourInd_1_6);
			this.m_round1Name7.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_elimTourInd_1_7);
			this.m_round1Name8.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_elimTourInd_1_8);
			return;
		}
		if (loopInd == 4)
		{
			this.m_bg2.gameObject.SetActive(true);
			this.m_round2Name1.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_elimTourInd_2_1);
			this.m_round2Name2.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_elimTourInd_2_2);
			this.m_round2Name3.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_elimTourInd_2_3);
			this.m_round2Name4.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_elimTourInd_2_4);
			return;
		}
		if (loopInd == 5)
		{
			this.m_bg2.gameObject.SetActive(true);
			this.m_round3Name1.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_elimTourInd_3_1);
			this.m_round3Name2.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_elimTourInd_3_2);
		}
	}

	private void sortTeam()
	{
		this.m_team1Bg1.gameObject.SetActive(false);
		this.m_team1Bg2.gameObject.SetActive(false);
		this.m_team1Bg3.gameObject.SetActive(false);
		this.m_team1Bg4.gameObject.SetActive(false);
		this.m_team2Bg1.gameObject.SetActive(false);
		this.m_team2Bg2.gameObject.SetActive(false);
		this.m_team2Bg3.gameObject.SetActive(false);
		this.m_team2Bg4.gameObject.SetActive(false);
		this.m_team3Bg1.gameObject.SetActive(false);
		this.m_team3Bg2.gameObject.SetActive(false);
		this.m_team3Bg3.gameObject.SetActive(false);
		this.m_team3Bg4.gameObject.SetActive(false);
		this.m_team4Bg1.gameObject.SetActive(false);
		this.m_team4Bg2.gameObject.SetActive(false);
		this.m_team4Bg3.gameObject.SetActive(false);
		this.m_team4Bg4.gameObject.SetActive(false);
		int[] array = new int[]
		{
			0,
			1,
			2,
			3
		};
		int[] array2 = new int[4];
		array[0] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_1;
		array[1] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_2;
		array[2] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_3;
		array[3] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_1_4;
		array2[0] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_teamIndSrcs[array[0]];
		array2[1] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_teamIndSrcs[array[1]];
		array2[2] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_teamIndSrcs[array[2]];
		array2[3] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_teamIndSrcs[array[3]];
		for (int i = 0; i < 3; i++)
		{
			for (int j = i + 1; j < 4; j++)
			{
				if (array2[j] > array2[i] || (array2[j] == array2[i] && array2[j] == Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd))
				{
					int num = array2[j];
					array2[j] = array2[i];
					array2[i] = num;
					num = array[j];
					array[j] = array[i];
					array[i] = num;
				}
			}
		}
		this.m_team1Name1.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, array[0]);
		this.m_team1Name2.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, array[1]);
		this.m_team1Name3.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, array[2]);
		this.m_team1Name4.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, array[3]);
		this.m_team1Src1.text = string.Concat(array2[0]);
		this.m_team1Src2.text = string.Concat(array2[1]);
		this.m_team1Src3.text = string.Concat(array2[2]);
		this.m_team1Src4.text = string.Concat(array2[3]);
		if (array[0] == Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd)
		{
			this.m_team1Bg1.gameObject.SetActive(true);
		}
		else if (array[1] == Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd)
		{
			this.m_team1Bg2.gameObject.SetActive(true);
		}
		else if (array[2] == Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd)
		{
			this.m_team1Bg3.gameObject.SetActive(true);
		}
		else if (array[3] == Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd)
		{
			this.m_team1Bg4.gameObject.SetActive(true);
		}
		array[0] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_1;
		array[1] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_2;
		array[2] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_3;
		array[3] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_2_4;
		array2[0] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_teamIndSrcs[array[0]];
		array2[1] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_teamIndSrcs[array[1]];
		array2[2] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_teamIndSrcs[array[2]];
		array2[3] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_teamIndSrcs[array[3]];
		for (int k = 0; k < 3; k++)
		{
			for (int l = k + 1; l < 4; l++)
			{
				if (array2[l] > array2[k] || (array2[l] == array2[k] && array2[l] == Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd))
				{
					int num2 = array2[l];
					array2[l] = array2[k];
					array2[k] = num2;
					num2 = array[l];
					array[l] = array[k];
					array[k] = num2;
				}
			}
		}
		this.m_team2Name1.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, array[0]);
		this.m_team2Name2.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, array[1]);
		this.m_team2Name3.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, array[2]);
		this.m_team2Name4.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, array[3]);
		this.m_team2Src1.text = string.Concat(array2[0]);
		this.m_team2Src2.text = string.Concat(array2[1]);
		this.m_team2Src3.text = string.Concat(array2[2]);
		this.m_team2Src4.text = string.Concat(array2[3]);
		if (array[0] == Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd)
		{
			this.m_team2Bg1.gameObject.SetActive(true);
		}
		else if (array[1] == Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd)
		{
			this.m_team2Bg2.gameObject.SetActive(true);
		}
		else if (array[2] == Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd)
		{
			this.m_team2Bg3.gameObject.SetActive(true);
		}
		else if (array[3] == Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd)
		{
			this.m_team2Bg4.gameObject.SetActive(true);
		}
		array[0] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_1;
		array[1] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_2;
		array[2] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_3;
		array[3] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_3_4;
		array2[0] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_teamIndSrcs[array[0]];
		array2[1] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_teamIndSrcs[array[1]];
		array2[2] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_teamIndSrcs[array[2]];
		array2[3] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_teamIndSrcs[array[3]];
		for (int m = 0; m < 3; m++)
		{
			for (int n = m + 1; n < 4; n++)
			{
				if (array2[n] > array2[m] || (array2[n] == array2[m] && array2[n] == Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd))
				{
					int num3 = array2[n];
					array2[n] = array2[m];
					array2[m] = num3;
					num3 = array[n];
					array[n] = array[m];
					array[m] = num3;
				}
			}
		}
		this.m_team3Name1.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, array[0]);
		this.m_team3Name2.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, array[1]);
		this.m_team3Name3.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, array[2]);
		this.m_team3Name4.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, array[3]);
		this.m_team3Src1.text = string.Concat(array2[0]);
		this.m_team3Src2.text = string.Concat(array2[1]);
		this.m_team3Src3.text = string.Concat(array2[2]);
		this.m_team3Src4.text = string.Concat(array2[3]);
		if (array[0] == Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd)
		{
			this.m_team3Bg1.gameObject.SetActive(true);
		}
		else if (array[1] == Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd)
		{
			this.m_team3Bg2.gameObject.SetActive(true);
		}
		else if (array[2] == Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd)
		{
			this.m_team3Bg3.gameObject.SetActive(true);
		}
		else if (array[3] == Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd)
		{
			this.m_team3Bg4.gameObject.SetActive(true);
		}
		array[0] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_1;
		array[1] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_2;
		array[2] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_3;
		array[3] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_groupTourInd_4_4;
		array2[0] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_teamIndSrcs[array[0]];
		array2[1] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_teamIndSrcs[array[1]];
		array2[2] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_teamIndSrcs[array[2]];
		array2[3] = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_teamIndSrcs[array[3]];
		for (int num4 = 0; num4 < 3; num4++)
		{
			for (int num5 = num4 + 1; num5 < 4; num5++)
			{
				if (array2[num5] > array2[num4] || (array2[num5] == array2[num4] && array2[num5] == Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd))
				{
					int num6 = array2[num5];
					array2[num5] = array2[num4];
					array2[num4] = num6;
					num6 = array[num5];
					array[num5] = array[num4];
					array[num4] = num6;
				}
			}
		}
		this.m_team4Name1.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, array[0]);
		this.m_team4Name2.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, array[1]);
		this.m_team4Name3.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, array[2]);
		this.m_team4Name4.text = Singleton<MatchManager>.Instance.getMatchUserName(this.m_matchInd, array[3]);
		this.m_team4Src1.text = string.Concat(array2[0]);
		this.m_team4Src2.text = string.Concat(array2[1]);
		this.m_team4Src3.text = string.Concat(array2[2]);
		this.m_team4Src4.text = string.Concat(array2[3]);
		if (array[0] == Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd)
		{
			this.m_team4Bg1.gameObject.SetActive(true);
			return;
		}
		if (array[1] == Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd)
		{
			this.m_team4Bg2.gameObject.SetActive(true);
			return;
		}
		if (array[2] == Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd)
		{
			this.m_team4Bg3.gameObject.SetActive(true);
			return;
		}
		if (array[3] == Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd)
		{
			this.m_team4Bg4.gameObject.SetActive(true);
		}
	}

	private void showGroupBg(int ind)
	{
		if (ind == 0)
		{
			this.m_team1Bg1.gameObject.SetActive(true);
			return;
		}
		if (ind == 1)
		{
			this.m_team1Bg2.gameObject.SetActive(true);
			return;
		}
		if (ind == 2)
		{
			this.m_team1Bg3.gameObject.SetActive(true);
			return;
		}
		if (ind == 3)
		{
			this.m_team1Bg4.gameObject.SetActive(true);
			return;
		}
		if (ind == 4)
		{
			this.m_team2Bg1.gameObject.SetActive(true);
			return;
		}
		if (ind == 5)
		{
			this.m_team2Bg2.gameObject.SetActive(true);
			return;
		}
		if (ind == 6)
		{
			this.m_team2Bg3.gameObject.SetActive(true);
			return;
		}
		if (ind == 7)
		{
			this.m_team2Bg4.gameObject.SetActive(true);
			return;
		}
		if (ind == 8)
		{
			this.m_team3Bg1.gameObject.SetActive(true);
			return;
		}
		if (ind == 9)
		{
			this.m_team3Bg2.gameObject.SetActive(true);
			return;
		}
		if (ind == 10)
		{
			this.m_team3Bg3.gameObject.SetActive(true);
			return;
		}
		if (ind == 11)
		{
			this.m_team3Bg4.gameObject.SetActive(true);
			return;
		}
		if (ind == 12)
		{
			this.m_team4Bg1.gameObject.SetActive(true);
			return;
		}
		if (ind == 13)
		{
			this.m_team4Bg2.gameObject.SetActive(true);
			return;
		}
		if (ind == 14)
		{
			this.m_team4Bg3.gameObject.SetActive(true);
			return;
		}
		if (ind == 15)
		{
			this.m_team4Bg4.gameObject.SetActive(true);
		}
	}

	private void showToonBg(int ind)
	{
		int loopInd = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_loopInd;
		if (loopInd == 3)
		{
			if (ind == 0)
			{
				this.m_round1Bg1.gameObject.SetActive(true);
				return;
			}
			if (ind == 1)
			{
				this.m_round1Bg2.gameObject.SetActive(true);
				return;
			}
			if (ind == 2)
			{
				this.m_round1Bg3.gameObject.SetActive(true);
				return;
			}
			if (ind == 3)
			{
				this.m_round1Bg4.gameObject.SetActive(true);
				return;
			}
			if (ind == 4)
			{
				this.m_round1Bg5.gameObject.SetActive(true);
				return;
			}
			if (ind == 5)
			{
				this.m_round1Bg6.gameObject.SetActive(true);
				return;
			}
			if (ind == 6)
			{
				this.m_round1Bg7.gameObject.SetActive(true);
				return;
			}
			if (ind == 7)
			{
				this.m_round1Bg8.gameObject.SetActive(true);
				return;
			}
		}
		else if (loopInd == 4)
		{
			if (ind == 0)
			{
				this.m_round2Bg1.gameObject.SetActive(true);
				return;
			}
			if (ind == 1)
			{
				this.m_round2Bg2.gameObject.SetActive(true);
				return;
			}
			if (ind == 2)
			{
				this.m_round2Bg3.gameObject.SetActive(true);
				return;
			}
			if (ind == 3)
			{
				this.m_round2Bg4.gameObject.SetActive(true);
				return;
			}
		}
		else if (loopInd == 5)
		{
			if (ind == 0)
			{
				this.m_round3Bg1.gameObject.SetActive(true);
				return;
			}
			if (ind == 1)
			{
				this.m_round3Bg2.gameObject.SetActive(true);
			}
		}
	}

	public void matchResult(bool isWin)
	{
		base.transform.gameObject.SetActive(true);
		if (Singleton<MatchManager>.Instance.nextLoop(this.m_matchInd, isWin))
		{
			this.reflush();
			if (Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_loopInd == 3)
			{
				int selfInd = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_selfInd;
				if (Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_teamIndSrcs[selfInd] <= 3)
				{
					this.m_faileBg.gameObject.SetActive(true);
					base.Invoke("clickFaile", 3f);
				}
				else
				{
					this.m_nextBg.gameObject.SetActive(true);
					Sequence expr_C6 = DOTween.Sequence();
					expr_C6.AppendInterval(1f);
					expr_C6.AppendCallback(delegate
					{
						this.m_nextBg.gameObject.SetActive(false);
					});
				}
			}
		}
		else if (Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_loopInd == 5 & isWin)
		{
			this.m_resultBg.gameObject.SetActive(true);
			base.Invoke("clickWin", 2f);
		}
		else
		{
			this.m_faileBg.gameObject.SetActive(true);
			base.Invoke("clickFaile", 2f);
		}
		Singleton<GameManager>.Instance.OnPause();
	}

	public void clickPlay()
	{
		base.transform.gameObject.SetActive(false);
		MainMenuView.m_this.m_scaneId = this.m_matchInd;
		MainMenuView.m_this.clickStartCupGame(Singleton<MatchManager>.Instance.getMatchOpponInd(this.m_matchInd));
		MainMenuView.m_this.m_MainGameView.m_moneyVal = (int)((float)this.m_moneyVal * 0.5f);
		ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "杯赛-开始-" + Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_loopInd);
	}

	public void clickRevi()
	{
	}

	public void clickWin()
	{
		Singleton<MatchManager>.Instance.m_MatchInfo.m_matchTp[this.m_matchInd] = 0;
		Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_loopInd = 0;
		ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "杯赛-胜利");
		Singleton<GameManager>.Instance.OnPause();
		base.transform.gameObject.SetActive(false);
		this.m_MatchView.showViewEff(this.m_moneyVal * 3);
	}

	public void clickFaile()
	{
		int loopInd = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_loopInd;
		Singleton<MatchManager>.Instance.m_MatchInfo.m_matchTp[this.m_matchInd] = 0;
		Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[this.m_matchInd].m_loopInd = 0;
		base.transform.gameObject.SetActive(false);
		if (loopInd == 3)
		{
			this.m_MatchView.showViewEff((int)((float)this.m_moneyVal * 0.2f));
		}
		else if (loopInd == 4)
		{
			this.m_MatchView.showViewEff((int)((float)this.m_moneyVal * 0.5f));
		}
		else if (loopInd == 5)
		{
			this.m_MatchView.showViewEff(this.m_moneyVal);
		}
		else
		{
			this.m_MatchView.showViewEff(0);
		}
		ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "杯赛-失败");
		Singleton<GameManager>.Instance.OnPause();
	}
}
