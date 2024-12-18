using System;

public class AiInfos
{
	public string m_nickName;

	public float m_loopTimes;

	public float m_speed;

	public float m_skill;

	public AiInfos(string name, float skill, float loop, float speed)
	{
		this.m_nickName = name;
		this.m_loopTimes = loop;
		this.m_speed = speed;
		this.m_skill = skill;
	}
}
