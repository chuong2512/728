using Engine;
using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class GameManager : Singleton<GameManager>
{
	public UserInfo m_UserInfo;

	public int m_userPoint;

	public int m_aiPoint;

	public int m_gameMode;

	public int m_sceneInd;

	public static int[] m_roleCostInfos = new int[]
	{
		0,
		200,
		500,
		500,
		1000,
		1000,
		1000,
		2000,
		2000,
		5000
	};

	private float[] m_lvCoins = new float[]
	{
		200f,
		300f,
		400f,
		600f,
		800f,
		1000f,
		1000f
	};

	private int[] m_lvSteps = new int[]
	{
		2,
		4,
		6,
		6,
		8,
		10,
		20
	};

	private string[] m_lvStrs = new string[]
	{
		"F",
		"E",
		"D",
		"C",
		"B",
		"A",
		"S"
	};

	protected override void Init()
	{
		this.m_UserInfo = new UserInfo();
		this.LoadData();
	}

	public void LoadData()
	{
		Singleton<SaveDataManager>.Instance.ReadData();
		this.m_UserInfo = Singleton<SaveDataManager>.Instance.getUserInfo();
		if (this.m_UserInfo.check())
		{
			Singleton<SaveDataManager>.Instance.setUserInfo(this.m_UserInfo);
		}
	}

	public void initGame()
	{
		this.m_userPoint = 0;
		this.m_aiPoint = 0;
	}

	public void startGame()
	{
	}

	public void saveGame()
	{
		this.m_aiPoint--;
	}

	public void OnPause()
	{
		Singleton<SaveDataManager>.Instance.setUserInfo(this.m_UserInfo);
		Singleton<SaveDataManager>.Instance.SaveDate();
	}

	public void OnResume()
	{
	}

	public int addPoint(int urP, int aiP)
	{
		int result = 0;
		this.m_userPoint += urP;
		this.m_aiPoint += aiP;
		if (this.m_userPoint >= 3)
		{
			result = 1;
		}
		else if (this.m_aiPoint >= 3)
		{
			result = 2;
		}
		return result;
	}

	public string getNickName()
	{
		string nickName;
		if (this.m_UserInfo.m_missInd < AppConst.m_AiInfos.Length)
		{
			nickName = AppConst.m_AiInfos[this.m_UserInfo.m_missInd].m_nickName;
		}
		else
		{
			int num = this.m_UserInfo.m_missInd % (AppConst.m_AiInfos.Length - 1);
			nickName = AppConst.m_AiInfos[num].m_nickName;
		}
		return nickName;
	}

	public float getLoopTimes()
	{
		float num = 1f;
		if (this.m_gameMode == 0)
		{
			if (this.m_UserInfo.m_missInd < AppConst.m_AiInfos.Length)
			{
				num = AppConst.m_AiInfos[this.m_UserInfo.m_missInd].m_loopTimes;
			}
			else
			{
				int num2 = this.m_UserInfo.m_missInd % (AppConst.m_AiInfos.Length - 1);
				num = AppConst.m_AiInfos[num2].m_loopTimes;
			}
		}
		else if (this.m_sceneInd == 0)
		{
			int num3 = (int)(UnityEngine.Random.value * 5f);
			if (num3 < AppConst.m_AiInfos.Length)
			{
				num = AppConst.m_AiInfos[num3].m_loopTimes;
			}
			else
			{
				int num4 = num3 % (AppConst.m_AiInfos.Length - 1);
				num = AppConst.m_AiInfos[num4].m_loopTimes;
			}
		}
		else if (this.m_sceneInd == 1)
		{
			int num5 = (int)(UnityEngine.Random.value * 5f) + 5;
			if (num5 < AppConst.m_AiInfos.Length)
			{
				num = AppConst.m_AiInfos[num5].m_loopTimes;
			}
			else
			{
				int num6 = num5 % (AppConst.m_AiInfos.Length - 1);
				num = AppConst.m_AiInfos[num6].m_loopTimes;
			}
		}
		else if (this.m_sceneInd == 2)
		{
			int num7 = (int)(UnityEngine.Random.value * 5f) + 10;
			if (num7 < AppConst.m_AiInfos.Length)
			{
				num = AppConst.m_AiInfos[num7].m_loopTimes;
			}
			else
			{
				int num8 = num7 % (AppConst.m_AiInfos.Length - 1);
				num = AppConst.m_AiInfos[num8].m_loopTimes;
			}
		}
		else if (this.m_sceneInd == 3)
		{
			int num9 = (int)(UnityEngine.Random.value * 5f) + 15;
			if (num9 < AppConst.m_AiInfos.Length)
			{
				num = AppConst.m_AiInfos[num9].m_loopTimes;
			}
			else
			{
				int num10 = num9 % (AppConst.m_AiInfos.Length - 1);
				num = AppConst.m_AiInfos[num10].m_loopTimes;
			}
		}
		else if (this.m_sceneInd == 4)
		{
			int num11 = (int)(UnityEngine.Random.value * 5f) + 20;
			if (num11 < AppConst.m_AiInfos.Length)
			{
				num = AppConst.m_AiInfos[num11].m_loopTimes;
			}
			else
			{
				int num12 = num11 % (AppConst.m_AiInfos.Length - 1);
				num = AppConst.m_AiInfos[num12].m_loopTimes;
			}
		}
		else if (this.m_sceneInd == 5)
		{
			int num13 = (int)(UnityEngine.Random.value * 5f) + 25;
			if (num13 < AppConst.m_AiInfos.Length)
			{
				num = AppConst.m_AiInfos[num13].m_loopTimes;
			}
			else
			{
				int num14 = num13 % (AppConst.m_AiInfos.Length - 1);
				num = AppConst.m_AiInfos[num14].m_loopTimes;
			}
		}
		return AppConst.m_loopMax * num;
	}

	public float getSpeedTimes()
	{
		float num = 1f;
		if (this.m_gameMode == 0)
		{
			if (this.m_UserInfo.m_missInd < AppConst.m_AiInfos.Length)
			{
				num = AppConst.m_AiInfos[this.m_UserInfo.m_missInd].m_speed;
			}
			else
			{
				int num2 = this.m_UserInfo.m_missInd % (AppConst.m_AiInfos.Length - 1);
				num = AppConst.m_AiInfos[num2].m_speed;
			}
		}
		return AppConst.m_speedMax * num;
	}

	public float getSkillTimes()
	{
		float num = 1f;
		if (this.m_gameMode == 0)
		{
			if (this.m_UserInfo.m_missInd < AppConst.m_AiInfos.Length)
			{
				num = AppConst.m_AiInfos[this.m_UserInfo.m_missInd].m_skill;
			}
			else
			{
				int num2 = this.m_UserInfo.m_missInd % (AppConst.m_AiInfos.Length - 1);
				num = AppConst.m_AiInfos[num2].m_skill;
			}
		}
		return AppConst.m_skillMax * num;
	}

	public int getRoleCost(int ind)
	{
		int result;
		if (ind < GameManager.m_roleCostInfos.Length)
		{
			result = GameManager.m_roleCostInfos[ind];
		}
		else
		{
			result = 5000;
		}
		return result;
	}

	public string changeValToStr(long val)
	{
		string result = "";
		if (val < 1000L)
		{
			result = string.Concat(val);
		}
		else if (val < 1000000L)
		{
			result = string.Format("{0:f2}", (float)val / 1000f) + "k";
		}
		else if (val < 1000000000L)
		{
			result = string.Format("{0:f2}", (float)val / 1000000f) + "m";
		}
		return result;
	}

	public bool addCoins(int vals)
	{
		bool result = false;
		if (this.m_UserInfo.m_coins + (long)vals >= 0L)
		{
			this.m_UserInfo.m_coins = this.m_UserInfo.m_coins + (long)vals;
			result = true;
			EventsMgr.Instance.TriggerEvent("Diamond_Reflush", null);
			AudioManager.PlayEffectAudio("claim", false, false);
		}
		else
		{
			EventsMgr.Instance.TriggerEvent("Tips_Reflush", null);
		}
		return result;
	}

	public bool enableCoins(int vals)
	{
		bool result = false;
		if (this.m_UserInfo.m_coins + (long)vals >= 0L)
		{
			result = true;
		}
		return result;
	}

	public void addBallSkin(int ind)
	{
		this.m_UserInfo.m_ballInfos[ind] = 1;
		this.m_UserInfo.m_ballInd = ind;
		AudioManager.PlayEffectAudio("claim", false, false);
	}

	public int randBallSkin()
	{
		int result = -1;
		List<int> list = new List<int>();
		for (int i = 0; i < this.m_UserInfo.m_ballInfos.Length; i++)
		{
			if (this.m_UserInfo.m_ballInfos[i] == 0)
			{
				list.Add(i);
			}
		}
		if (list.Count > 0)
		{
			int index = (int)(UnityEngine.Random.value * (float)list.Count);
			result = list[index];
		}
		return result;
	}

	public bool canBuy()
	{
		bool result = false;
		for (int i = 0; i < this.m_UserInfo.m_roleInfos.Length; i++)
		{
			if (this.m_UserInfo.m_roleInfos[i] == 0)
			{
				int roleCost = this.getRoleCost(i);
				if (this.m_UserInfo.m_coins >= (long)roleCost)
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	public bool canMission()
	{
		bool result = false;
		for (int i = 0; i < Singleton<MissionManager>.Instance.m_MissionInfo.m_DayVal.Length; i++)
		{
			if (Singleton<MissionManager>.Instance.getDayMissionDeVal(i) >= Singleton<MissionManager>.Instance.getDayMissionDeMaxVal(i))
			{
				return true;
			}
		}
		for (int j = 0; j < Singleton<MissionManager>.Instance.m_MissionInfo.m_AchVal.Length; j++)
		{
			if (Singleton<MissionManager>.Instance.getAchMissionDeVal(j) >= Singleton<MissionManager>.Instance.getAchMissionDeMaxVal(j))
			{
				return true;
			}
		}
		return result;
	}

	public string getAiName(int ind)
	{
		string result = "player";
		if (ind < AppConst.m_AiInfos.Length)
		{
			result = AppConst.m_AiInfos[ind].m_nickName;
		}
		return result;
	}

	public string getLeagueStr()
	{
		string text = this.m_lvStrs[this.m_UserInfo.m_leagueLv % 7];
		int num = this.m_UserInfo.m_leagueLv / 7;
		for (int i = 0; i < num; i++)
		{
			text = "S" + text;
		}
		return text;
	}

	public int getLeagueMax()
	{
		return this.m_lvSteps[this.m_UserInfo.m_leagueLv % 7];
	}

	public int getLeagueNow()
	{
		return this.m_UserInfo.m_leagueLvStep;
	}

	public bool addLeagueLv(int nums)
	{
		bool result = false;
		this.m_UserInfo.m_leagueLvStep = this.m_UserInfo.m_leagueLvStep + nums;
		int leagueMax = this.getLeagueMax();
		if (this.m_UserInfo.m_leagueLvStep >= leagueMax)
		{
			this.m_UserInfo.m_leagueLvStep = 0;
			this.m_UserInfo.m_leagueLv = this.m_UserInfo.m_leagueLv + 1;
			result = true;
		}
		return result;
	}

	public float getLvCoins()
	{
		return this.m_lvCoins[this.m_UserInfo.m_leagueLv % 7];
	}

	public int getRandSkin()
	{
		int result = 0;
		List<int> list = new List<int>();
		for (int i = 0; i < this.m_UserInfo.m_ballInfos.Length; i++)
		{
			if (this.m_UserInfo.m_ballInfos[i] == 0)
			{
				list.Add(i);
			}
		}
		if (list.Count > 0)
		{
			result = (int)(UnityEngine.Random.value * (float)list.Count);
		}
		return result;
	}
}
