using System;

[Serializable]
public class MatchListInfo
{
	public int m_selfInd;

	public int[] m_teamInds = new int[]
	{
		0,
		1,
		2,
		3,
		4,
		5,
		6,
		7,
		8,
		9,
		10,
		11,
		12,
		13,
		14,
		15
	};

	public int[] m_teamIndSrcs = new int[16];

	public int m_loopInd;

	public int m_groupTourInd_1_1 = 1;

	public int m_groupTourInd_1_2 = 2;

	public int m_groupTourInd_1_3 = 3;

	public int m_groupTourInd_1_4 = 4;

	public int m_groupTourInd_2_1 = 5;

	public int m_groupTourInd_2_2 = 6;

	public int m_groupTourInd_2_3 = 7;

	public int m_groupTourInd_2_4 = 8;

	public int m_groupTourInd_3_1 = 9;

	public int m_groupTourInd_3_2 = 10;

	public int m_groupTourInd_3_3 = 11;

	public int m_groupTourInd_3_4 = 12;

	public int m_groupTourInd_4_1 = 13;

	public int m_groupTourInd_4_2 = 14;

	public int m_groupTourInd_4_3 = 15;

	public int m_groupTourInd_4_4 = 16;

	public int m_elimTourInd_1_1 = 1;

	public int m_elimTourInd_1_2 = 2;

	public int m_elimTourInd_1_3 = 3;

	public int m_elimTourInd_1_4 = 4;

	public int m_elimTourInd_1_5 = 5;

	public int m_elimTourInd_1_6 = 6;

	public int m_elimTourInd_1_7 = 7;

	public int m_elimTourInd_1_8 = 8;

	public int m_elimTourInd_2_1 = 1;

	public int m_elimTourInd_2_2 = 2;

	public int m_elimTourInd_2_3 = 3;

	public int m_elimTourInd_2_4 = 4;

	public int m_elimTourInd_3_1 = 1;

	public int m_elimTourInd_3_2 = 2;

	public int m_elimTourInd_4_1 = 1;
}
