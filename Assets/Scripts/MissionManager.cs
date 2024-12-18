using System;
using Util;

public class MissionManager : Singleton<MissionManager>
{
	public MissionInfo m_MissionInfo;

	public static string[] m_dayNames = new string[]
	{
		"win 3 battle",
		"watch 1 video"
	};

	public static string[] m_achNames = new string[]
	{
		"win 10 battles",
		"win 1 tournament",
		"win 3 tournaments",
		"win 5 tournaments",
		"win 20 battles",
		"win 40 battles",
		"win 60 battles",
		"win 100 battles"
	};

	public static int[] m_dayMoneys = new int[]
	{
		200,
		300
	};

	public static int[] m_achMoneys = new int[]
	{
		500,
		500,
		1000,
		3000,
		5000,
		7000,
		10000,
		30000
	};

	public static int[] m_dayValMaxs = new int[]
	{
		3,
		1
	};

	public static int[] m_achValMaxs = new int[]
	{
		10,
		1,
		3,
		5,
		20,
		40,
		60,
		100
	};

	protected override void Init()
	{
		this.m_MissionInfo = new MissionInfo();
		this.LoadData();
	}

	private void LoadData()
	{
		Singleton<SaveDataManager>.Instance.ReadData();
		this.m_MissionInfo = Singleton<SaveDataManager>.Instance.getMissionInfo();
	}

	public void initDay()
	{
		int arg_0D_0 = DateTime.Now.Year;
		int arg_1B_0 = DateTime.Now.Month;
		int day = DateTime.Now.Day;
		if (this.m_MissionInfo.LastSigninDay != day)
		{
			this.m_MissionInfo.DayBallteNum = 0;
			this.m_MissionInfo.DayTourNums = 0;
			this.m_MissionInfo.DayVedioNums = 0;
			this.m_MissionInfo.m_DayTp[0] = 0;
			this.m_MissionInfo.m_DayTp[1] = 0;
		}
	}

	public string getDayMissionName(int ind)
	{
		return MissionManager.m_dayNames[ind];
	}

	public int getDayMissionDeVal(int ind)
	{
		int num = this.m_MissionInfo.m_DayVal[ind];
		if (num >= this.getDayMissionDeMaxVal(ind))
		{
			num = this.getDayMissionDeMaxVal(ind);
		}
		return num;
	}

	public int getDayMissionDeMaxVal(int ind)
	{
		return MissionManager.m_dayValMaxs[ind];
	}

	public int getDayMissionMoney(int ind)
	{
		return MissionManager.m_dayMoneys[ind];
	}

	public int getDayMissionTp(int ind)
	{
		return this.m_MissionInfo.m_DayTp[ind];
	}

	public string getAchMissionName(int ind)
	{
		return MissionManager.m_achNames[ind];
	}

	public int getAchMissionDeVal(int ind)
	{
		int num = this.m_MissionInfo.m_AchVal[ind];
		if (num >= this.getAchMissionDeMaxVal(ind))
		{
			num = this.getAchMissionDeMaxVal(ind);
		}
		return num;
	}

	public int getAchMissionDeMaxVal(int ind)
	{
		return MissionManager.m_achValMaxs[ind];
	}

	public int getAchMissionMoney(int ind)
	{
		return MissionManager.m_achMoneys[ind];
	}

	public int getAchMissionTp(int ind)
	{
		return this.m_MissionInfo.m_AchTp[ind];
	}

	public void addMissionVal(int tp, int val)
	{
		if (tp == 0)
		{
			this.m_MissionInfo.DayBallteNum++;
			this.m_MissionInfo.m_DayVal[0]++;
			this.m_MissionInfo.m_AchVal[0]++;
			this.m_MissionInfo.m_AchVal[4]++;
			this.m_MissionInfo.m_AchVal[5]++;
			this.m_MissionInfo.m_AchVal[6]++;
			this.m_MissionInfo.m_AchVal[7]++;
			return;
		}
		if (tp == 1)
		{
			this.m_MissionInfo.DayTourNums++;
			this.m_MissionInfo.m_AchVal[1]++;
			this.m_MissionInfo.m_AchVal[2]++;
			this.m_MissionInfo.m_AchVal[3]++;
			return;
		}
		if (tp == 2)
		{
			this.m_MissionInfo.DayVedioNums++;
			this.m_MissionInfo.m_DayVal[1]++;
		}
	}
}
