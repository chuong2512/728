using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class MatchManager : Singleton<MatchManager>
{
	public MatchInfo m_MatchInfo;

	protected override void Init()
	{
		this.m_MatchInfo = new MatchInfo();
		this.LoadData();
	}

	private void LoadData()
	{
		Singleton<SaveDataManager>.Instance.ReadData();
		this.m_MatchInfo = Singleton<SaveDataManager>.Instance.getMatchInfo();
	}

	public void initMatch()
	{
	}

	public void randMatchInfo(int ind)
	{
		if (this.m_MatchInfo.m_matchTp[ind] == 0)
		{
			this.m_MatchInfo.m_matchTp[ind] = 1;
			this.m_MatchInfo.m_matchListInfo[ind].m_loopInd = 0;
			int num = (int)(UnityEngine.Random.value * 16f);
			this.m_MatchInfo.m_matchListInfo[ind].m_selfInd = num;
			this.m_MatchInfo.m_matchListInfo[ind].m_teamInds[num] = -1;
			for (int i = 0; i < this.m_MatchInfo.m_matchListInfo[ind].m_teamIndSrcs.Length; i++)
			{
				this.m_MatchInfo.m_matchListInfo[ind].m_teamIndSrcs[i] = 0;
			}
			List<int> list = new List<int>();
			for (int j = 0; j < AppConst.m_AiInfos.Length; j++)
			{
				list.Add(j);
			}
			for (int k = 0; k < list.Count; k++)
			{
				int index = (int)(UnityEngine.Random.value * (float)list.Count);
				this.m_MatchInfo.m_matchListInfo[ind].m_teamInds[k] = list[index];
				list.RemoveAt(index);
			}
			List<int> list2 = new List<int>();
			for (int l = 0; l < 16; l++)
			{
				list2.Add(l);
			}
			int index2 = (int)(UnityEngine.Random.value * (float)list2.Count);
			this.m_MatchInfo.m_matchListInfo[ind].m_groupTourInd_1_1 = list2[index2];
			list2.RemoveAt(index2);
			index2 = (int)(UnityEngine.Random.value * (float)list2.Count);
			this.m_MatchInfo.m_matchListInfo[ind].m_groupTourInd_1_2 = list2[index2];
			list2.RemoveAt(index2);
			index2 = (int)(UnityEngine.Random.value * (float)list2.Count);
			this.m_MatchInfo.m_matchListInfo[ind].m_groupTourInd_1_3 = list2[index2];
			list2.RemoveAt(index2);
			index2 = (int)(UnityEngine.Random.value * (float)list2.Count);
			this.m_MatchInfo.m_matchListInfo[ind].m_groupTourInd_1_4 = list2[index2];
			list2.RemoveAt(index2);
			index2 = (int)(UnityEngine.Random.value * (float)list2.Count);
			this.m_MatchInfo.m_matchListInfo[ind].m_groupTourInd_2_1 = list2[index2];
			list2.RemoveAt(index2);
			index2 = (int)(UnityEngine.Random.value * (float)list2.Count);
			this.m_MatchInfo.m_matchListInfo[ind].m_groupTourInd_2_2 = list2[index2];
			list2.RemoveAt(index2);
			index2 = (int)(UnityEngine.Random.value * (float)list2.Count);
			this.m_MatchInfo.m_matchListInfo[ind].m_groupTourInd_2_3 = list2[index2];
			list2.RemoveAt(index2);
			index2 = (int)(UnityEngine.Random.value * (float)list2.Count);
			this.m_MatchInfo.m_matchListInfo[ind].m_groupTourInd_2_4 = list2[index2];
			list2.RemoveAt(index2);
			index2 = (int)(UnityEngine.Random.value * (float)list2.Count);
			this.m_MatchInfo.m_matchListInfo[ind].m_groupTourInd_3_1 = list2[index2];
			list2.RemoveAt(index2);
			index2 = (int)(UnityEngine.Random.value * (float)list2.Count);
			this.m_MatchInfo.m_matchListInfo[ind].m_groupTourInd_3_2 = list2[index2];
			list2.RemoveAt(index2);
			index2 = (int)(UnityEngine.Random.value * (float)list2.Count);
			this.m_MatchInfo.m_matchListInfo[ind].m_groupTourInd_3_3 = list2[index2];
			list2.RemoveAt(index2);
			index2 = (int)(UnityEngine.Random.value * (float)list2.Count);
			this.m_MatchInfo.m_matchListInfo[ind].m_groupTourInd_3_4 = list2[index2];
			list2.RemoveAt(index2);
			index2 = (int)(UnityEngine.Random.value * (float)list2.Count);
			this.m_MatchInfo.m_matchListInfo[ind].m_groupTourInd_4_1 = list2[index2];
			list2.RemoveAt(index2);
			index2 = (int)(UnityEngine.Random.value * (float)list2.Count);
			this.m_MatchInfo.m_matchListInfo[ind].m_groupTourInd_4_2 = list2[index2];
			list2.RemoveAt(index2);
			index2 = (int)(UnityEngine.Random.value * (float)list2.Count);
			this.m_MatchInfo.m_matchListInfo[ind].m_groupTourInd_4_3 = list2[index2];
			list2.RemoveAt(index2);
			index2 = (int)(UnityEngine.Random.value * (float)list2.Count);
			this.m_MatchInfo.m_matchListInfo[ind].m_groupTourInd_4_4 = list2[index2];
			list2.RemoveAt(index2);
		}
	}

	public void addScore(int matchInd, int tmpInd, bool isWin)
	{
		int loopInd = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_loopInd;
		if (loopInd == 0)
		{
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_1, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_2, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_3, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_4, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_1, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_2, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_3, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_4, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_1, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_2, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_3, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_4, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_1, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_2, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_3, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_4, tmpInd, isWin);
			return;
		}
		if (loopInd == 1)
		{
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_1, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_3, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_2, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_4, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_1, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_3, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_2, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_4, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_1, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_3, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_2, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_4, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_1, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_3, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_2, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_4, tmpInd, isWin);
			return;
		}
		if (loopInd == 2)
		{
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_1, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_4, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_2, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_3, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_1, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_4, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_2, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_3, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_1, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_4, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_2, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_3, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_1, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_4, tmpInd, isWin);
			this.doResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_2, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_3, tmpInd, isWin);
			return;
		}
		if (loopInd == 3)
		{
			this.doTResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_1_1, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_1_2, 1, tmpInd, isWin);
			this.doTResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_1_3, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_1_4, 2, tmpInd, isWin);
			this.doTResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_1_5, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_1_6, 3, tmpInd, isWin);
			this.doTResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_1_7, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_1_8, 4, tmpInd, isWin);
			return;
		}
		if (loopInd == 4)
		{
			this.doTResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_1, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_2, 1, tmpInd, isWin);
			this.doTResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_3, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_4, 2, tmpInd, isWin);
			return;
		}
		if (loopInd == 5)
		{
			this.doTResult(matchInd, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_3_1, Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_3_2, 1, tmpInd, isWin);
		}
	}

	private void doResult(int matchInd, int ind1, int ind2, int selfInd, bool isWin)
	{
		if (ind1 == selfInd)
		{
			if (isWin)
			{
				this.m_MatchInfo.m_matchListInfo[matchInd].m_teamIndSrcs[ind1] = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamIndSrcs[ind1] + 3;
				return;
			}
			this.m_MatchInfo.m_matchListInfo[matchInd].m_teamIndSrcs[ind2] = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamIndSrcs[ind2] + 3;
			return;
		}
		else if (ind2 == selfInd)
		{
			if (isWin)
			{
				this.m_MatchInfo.m_matchListInfo[matchInd].m_teamIndSrcs[ind2] = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamIndSrcs[ind2] + 3;
				return;
			}
			this.m_MatchInfo.m_matchListInfo[matchInd].m_teamIndSrcs[ind1] = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamIndSrcs[ind1] + 3;
			return;
		}
		else
		{
			if ((int)(UnityEngine.Random.value * 100f) <= 50)
			{
				this.m_MatchInfo.m_matchListInfo[matchInd].m_teamIndSrcs[ind1] = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamIndSrcs[ind1] + 3;
				return;
			}
			this.m_MatchInfo.m_matchListInfo[matchInd].m_teamIndSrcs[ind2] = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamIndSrcs[ind2] + 3;
			return;
		}
	}

	private void doTResult(int matchInd, int ind1, int ind2, int teamInd, int selfInd, bool isWin)
	{
		int loopInd = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_loopInd;
		if (loopInd == 3)
		{
			if (ind1 == selfInd)
			{
				if (isWin)
				{
					if (teamInd == 1)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_1 = ind1;
						return;
					}
					if (teamInd == 2)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_2 = ind1;
						return;
					}
					if (teamInd == 3)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_3 = ind1;
						return;
					}
					if (teamInd == 4)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_4 = ind1;
						return;
					}
				}
				else
				{
					if (teamInd == 1)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_1 = ind2;
						return;
					}
					if (teamInd == 2)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_2 = ind2;
						return;
					}
					if (teamInd == 3)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_3 = ind2;
						return;
					}
					if (teamInd == 4)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_4 = ind2;
						return;
					}
				}
			}
			else if (ind2 == selfInd)
			{
				if (isWin)
				{
					if (teamInd == 1)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_1 = ind2;
						return;
					}
					if (teamInd == 2)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_2 = ind2;
						return;
					}
					if (teamInd == 3)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_3 = ind2;
						return;
					}
					if (teamInd == 4)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_4 = ind2;
						return;
					}
				}
				else
				{
					if (teamInd == 1)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_1 = ind1;
						return;
					}
					if (teamInd == 2)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_2 = ind1;
						return;
					}
					if (teamInd == 3)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_3 = ind1;
						return;
					}
					if (teamInd == 4)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_4 = ind1;
						return;
					}
				}
			}
			else if ((int)(UnityEngine.Random.value * 100f) <= 50)
			{
				if (isWin)
				{
					if (teamInd == 1)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_1 = ind1;
						return;
					}
					if (teamInd == 2)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_2 = ind1;
						return;
					}
					if (teamInd == 3)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_3 = ind1;
						return;
					}
					if (teamInd == 4)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_4 = ind1;
						return;
					}
				}
				else
				{
					if (teamInd == 1)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_1 = ind2;
						return;
					}
					if (teamInd == 2)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_2 = ind2;
						return;
					}
					if (teamInd == 3)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_3 = ind2;
						return;
					}
					if (teamInd == 4)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_4 = ind2;
						return;
					}
				}
			}
			else if (isWin)
			{
				if (teamInd == 1)
				{
					Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_1 = ind2;
					return;
				}
				if (teamInd == 2)
				{
					Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_2 = ind2;
					return;
				}
				if (teamInd == 3)
				{
					Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_3 = ind2;
					return;
				}
				if (teamInd == 4)
				{
					Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_4 = ind2;
					return;
				}
			}
			else
			{
				if (teamInd == 1)
				{
					Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_1 = ind1;
					return;
				}
				if (teamInd == 2)
				{
					Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_2 = ind1;
					return;
				}
				if (teamInd == 3)
				{
					Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_3 = ind1;
					return;
				}
				if (teamInd == 4)
				{
					Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_4 = ind1;
					return;
				}
			}
		}
		else if (loopInd == 4)
		{
			if (ind1 == selfInd)
			{
				if (isWin)
				{
					if (teamInd == 1)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_3_1 = ind1;
						return;
					}
					if (teamInd == 2)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_3_2 = ind1;
						return;
					}
				}
				else
				{
					if (teamInd == 1)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_3_1 = ind2;
						return;
					}
					if (teamInd == 2)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_3_2 = ind2;
						return;
					}
				}
			}
			else if (ind2 == selfInd)
			{
				if (isWin)
				{
					if (teamInd == 1)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_3_1 = ind2;
						return;
					}
					if (teamInd == 2)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_3_2 = ind2;
						return;
					}
				}
				else
				{
					if (teamInd == 1)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_3_1 = ind1;
						return;
					}
					if (teamInd == 2)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_3_2 = ind1;
						return;
					}
				}
			}
			else if ((int)(UnityEngine.Random.value * 100f) <= 50)
			{
				if (isWin)
				{
					if (teamInd == 1)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_3_1 = ind1;
						return;
					}
					if (teamInd == 2)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_3_2 = ind1;
						return;
					}
				}
				else
				{
					if (teamInd == 1)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_3_1 = ind2;
						return;
					}
					if (teamInd == 2)
					{
						Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_3_2 = ind2;
						return;
					}
				}
			}
			else if (isWin)
			{
				if (teamInd == 1)
				{
					Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_3_1 = ind2;
					return;
				}
				if (teamInd == 2)
				{
					Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_3_2 = ind2;
					return;
				}
			}
			else
			{
				if (teamInd == 1)
				{
					Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_3_1 = ind1;
					return;
				}
				if (teamInd == 2)
				{
					Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_3_2 = ind1;
				}
			}
		}
	}

	public bool nextLoop(int matchInd, bool isWin)
	{
		bool result = true;
		int selfInd = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd;
		int loopInd = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_loopInd;
		if (loopInd == 0)
		{
			Singleton<MatchManager>.Instance.addScore(matchInd, selfInd, isWin);
			Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_loopInd++;
		}
		else if (loopInd == 1)
		{
			Singleton<MatchManager>.Instance.addScore(matchInd, selfInd, isWin);
			Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_loopInd++;
		}
		else if (loopInd == 2)
		{
			Singleton<MatchManager>.Instance.addScore(matchInd, selfInd, isWin);
			Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_loopInd++;
		}
		else if (loopInd == 3)
		{
			if (!isWin)
			{
				result = false;
			}
			Singleton<MatchManager>.Instance.addScore(matchInd, selfInd, isWin);
			Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_loopInd++;
		}
		else if (loopInd == 4)
		{
			if (!isWin)
			{
				result = false;
			}
			Singleton<MatchManager>.Instance.addScore(matchInd, selfInd, isWin);
			Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_loopInd++;
		}
		else if (loopInd == 5)
		{
			result = false;
		}
		return result;
	}

	public int getMatchOpponInd(int matchInd)
	{
		int num = (int)(UnityEngine.Random.value * 16f);
		num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[num];
		if (this.m_MatchInfo.m_matchListInfo[matchInd].m_loopInd == 0)
		{
			if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 0)
			{
				int groupTourInd_1_ = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_2;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_1_];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 1)
			{
				int groupTourInd_1_2 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_1;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_1_2];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 2)
			{
				int groupTourInd_1_3 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_4;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_1_3];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 3)
			{
				int groupTourInd_1_4 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_3;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_1_4];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 4)
			{
				int groupTourInd_2_ = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_2;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_2_];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 5)
			{
				int groupTourInd_2_2 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_1;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_2_2];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 6)
			{
				int groupTourInd_2_3 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_4;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_2_3];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 7)
			{
				int groupTourInd_2_4 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_3;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_2_4];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 8)
			{
				int groupTourInd_3_ = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_2;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_3_];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 9)
			{
				int groupTourInd_3_2 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_1;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_3_2];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 10)
			{
				int groupTourInd_3_3 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_4;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_3_3];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 11)
			{
				int groupTourInd_3_4 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_3;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_3_4];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 12)
			{
				int groupTourInd_4_ = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_2;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_4_];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 13)
			{
				int groupTourInd_4_2 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_1;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_4_2];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 14)
			{
				int groupTourInd_4_3 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_4;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_4_3];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 15)
			{
				int groupTourInd_4_4 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_3;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_4_4];
			}
		}
		else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_loopInd == 1)
		{
			if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 0)
			{
				int groupTourInd_1_5 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_3;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_1_5];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 1)
			{
				int groupTourInd_1_6 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_4;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_1_6];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 2)
			{
				int groupTourInd_1_7 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_1;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_1_7];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 3)
			{
				int groupTourInd_1_8 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_2;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_1_8];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 4)
			{
				int groupTourInd_2_5 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_3;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_2_5];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 5)
			{
				int groupTourInd_2_6 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_4;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_2_6];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 6)
			{
				int groupTourInd_2_7 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_1;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_2_7];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 7)
			{
				int groupTourInd_2_8 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_2;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_2_8];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 8)
			{
				int groupTourInd_3_5 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_3;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_3_5];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 9)
			{
				int groupTourInd_3_6 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_4;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_3_6];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 10)
			{
				int groupTourInd_3_7 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_1;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_3_7];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 11)
			{
				int groupTourInd_3_8 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_2;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_3_8];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 12)
			{
				int groupTourInd_4_5 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_3;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_4_5];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 13)
			{
				int groupTourInd_4_6 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_4;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_4_6];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 14)
			{
				int groupTourInd_4_7 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_1;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_4_7];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 15)
			{
				int groupTourInd_4_8 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_2;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_4_8];
			}
		}
		else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_loopInd == 2)
		{
			if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 0)
			{
				int groupTourInd_1_9 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_4;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_1_9];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 1)
			{
				int groupTourInd_1_10 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_3;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_1_10];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 2)
			{
				int groupTourInd_1_11 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_2;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_1_11];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 3)
			{
				int groupTourInd_1_12 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_1;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_1_12];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 4)
			{
				int groupTourInd_2_9 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_4;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_2_9];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 5)
			{
				int groupTourInd_2_10 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_3;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_2_10];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 6)
			{
				int groupTourInd_2_11 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_2;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_2_11];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 7)
			{
				int groupTourInd_2_12 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_1;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_2_12];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 8)
			{
				int groupTourInd_3_9 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_4;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_3_9];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 9)
			{
				int groupTourInd_3_10 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_3;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_3_10];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 10)
			{
				int groupTourInd_3_11 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_2;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_3_11];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 11)
			{
				int groupTourInd_3_12 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_1;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_3_12];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 12)
			{
				int groupTourInd_4_9 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_4;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_4_9];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 13)
			{
				int groupTourInd_4_10 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_3;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_4_10];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 14)
			{
				int groupTourInd_4_11 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_2;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_4_11];
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd == 15)
			{
				int groupTourInd_4_12 = this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_1;
				num = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[groupTourInd_4_12];
			}
		}
		return num;
	}

	public int getGroupSelfInd(int matchInd)
	{
		int result = 0;
		if (this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_1 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
		{
			result = 0;
		}
		else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_2 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
		{
			result = 1;
		}
		else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_3 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
		{
			result = 2;
		}
		else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_1_4 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
		{
			result = 3;
		}
		else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_1 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
		{
			result = 4;
		}
		else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_2 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
		{
			result = 5;
		}
		else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_3 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
		{
			result = 6;
		}
		else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_2_4 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
		{
			result = 7;
		}
		else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_1 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
		{
			result = 8;
		}
		else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_2 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
		{
			result = 9;
		}
		else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_3 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
		{
			result = 10;
		}
		else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_3_4 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
		{
			result = 11;
		}
		else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_1 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
		{
			result = 12;
		}
		else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_2 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
		{
			result = 13;
		}
		else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_3 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
		{
			result = 14;
		}
		else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_groupTourInd_4_4 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
		{
			result = 15;
		}
		return result;
	}

	public int getOpponSelfInd(int matchInd)
	{
		int result = 0;
		int loopInd = Singleton<MatchManager>.Instance.m_MatchInfo.m_matchListInfo[matchInd].m_loopInd;
		if (loopInd == 3)
		{
			if (this.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_1_1 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
			{
				result = 0;
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_1_2 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
			{
				result = 1;
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_1_3 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
			{
				result = 2;
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_1_4 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
			{
				result = 3;
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_1_5 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
			{
				result = 4;
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_1_6 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
			{
				result = 5;
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_1_7 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
			{
				result = 6;
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_1_8 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
			{
				result = 7;
			}
		}
		else if (loopInd == 4)
		{
			if (this.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_1 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
			{
				result = 0;
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_2 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
			{
				result = 1;
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_3 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
			{
				result = 2;
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_2_4 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
			{
				result = 3;
			}
		}
		else if (loopInd == 5)
		{
			if (this.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_3_1 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
			{
				result = 0;
			}
			else if (this.m_MatchInfo.m_matchListInfo[matchInd].m_elimTourInd_3_2 == this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
			{
				result = 1;
			}
		}
		return result;
	}

	public string getMatchUserName(int matchInd, int teamInd)
	{
		string result = "user";
		int ind = this.m_MatchInfo.m_matchListInfo[matchInd].m_teamInds[teamInd];
		if (teamInd != this.m_MatchInfo.m_matchListInfo[matchInd].m_selfInd)
		{
			result = Singleton<GameManager>.Instance.getAiName(ind);
		}
		return result;
	}

	public string getMatchUserSrc(int matchInd, int teamInd)
	{
		return string.Concat(this.m_MatchInfo.m_matchListInfo[matchInd].m_teamIndSrcs[teamInd]);
	}
}
