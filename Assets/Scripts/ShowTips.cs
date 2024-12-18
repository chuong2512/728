using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTips : MonoBehaviour
{
	public Text m_showText;

	private List<string> m_showInfos;

	private void Start()
	{
		this.m_showInfos = new List<string>();
		base.InvokeRepeating("ChangeTips", 0f, 3f);
	}

	private void Update()
	{
	}

	private void ChangeTips()
	{
		if (this.m_showInfos.Count > 0)
		{
			this.m_showText.text = "tips";
		}
	}
}
