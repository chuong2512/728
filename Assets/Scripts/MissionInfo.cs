using System;

[Serializable]
public class MissionInfo
{
	public int LastSigninDay;

	public int DayBallteNum;

	public int DayTourNums;

	public int DayVedioNums;

	public int AllBallteNum;

	public int AllTourNums;

	public int[] m_DayTp = new int[2];

	public int[] m_AchTp = new int[8];

	public int[] m_DayVal = new int[2];

	public int[] m_AchVal = new int[8];
}
