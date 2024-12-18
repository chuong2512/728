using System;
using UnityEngine;

public class AdModels : MonoBehaviour
{
	private bool m_IsLoad;

	private bool m_IsShow;

	public bool IsLoad
	{
		get
		{
			return this.m_IsLoad;
		}
		set
		{
			this.m_IsLoad = value;
		}
	}

	public bool IsShow
	{
		get
		{
			return this.m_IsShow;
		}
		set
		{
			this.m_IsShow = value;
		}
	}
}
