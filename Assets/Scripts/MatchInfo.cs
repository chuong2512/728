using System;

[Serializable]
public class MatchInfo
{
	public int[] m_matchTp = new int[6];

	public MatchListInfo[] m_matchListInfo = new MatchListInfo[]
	{
		new MatchListInfo(),
		new MatchListInfo(),
		new MatchListInfo(),
		new MatchListInfo(),
		new MatchListInfo(),
		new MatchListInfo()
	};
}
