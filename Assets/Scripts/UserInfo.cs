using System;

[Serializable]
public class UserInfo
{
	public bool YaoState = true;

	public bool SoundState = true;

	public long m_coins;

	public int m_missInd = 1;

	public int m_skinInd;

	public int m_hightSrc;

	public long m_cointimes;

	public bool m_isClickSkip;

	public int m_roleInd;

	public int m_ballInd;

	public int m_leagueLv;

	public int m_leagueLvStep;

	public int[] m_guidSteps = new int[5];

	public int[] m_ballInfos;

	public int[] m_roleInfos;

	public bool check()
	{
		bool result = false;
		int[] expr_09 = new int[28];
		expr_09[0] = 1;
		int[] array = expr_09;
		int[] expr_15 = new int[23];
		expr_15[0] = 1;
		int[] array2 = expr_15;
		if (this.m_ballInfos.Length < array.Length)
		{
			for (int i = 0; i < this.m_ballInfos.Length; i++)
			{
				array[i] = this.m_ballInfos[i];
			}
			this.m_ballInfos = array;
			result = true;
		}
		if (this.m_roleInfos.Length < array2.Length)
		{
			for (int j = 0; j < this.m_roleInfos.Length; j++)
			{
				array2[j] = this.m_roleInfos[j];
			}
			this.m_roleInfos = array2;
			result = true;
		}
		return result;
	}

	public UserInfo()
	{
		int[] expr_29 = new int[28];
		expr_29[0] = 1;
		this.m_ballInfos = expr_29;
		int[] expr_3A = new int[23];
		expr_3A[0] = 1;
		this.m_roleInfos = expr_3A;
		
	}
}
