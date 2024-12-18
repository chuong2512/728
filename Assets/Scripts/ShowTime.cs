using System;
using UnityEngine;
using UnityEngine.UI;

public class ShowTime : MonoBehaviour
{
	public Text m_UILabel;

	private void Start()
	{
		if (this.m_UILabel == null)
		{
			this.m_UILabel = base.GetComponent<Text>();
		}
	}

	private void Update()
	{
		if (this.m_UILabel != null)
		{
			DateTime now = DateTime.Now;
			this.m_UILabel.text = now.ToShortTimeString();
		}
	}
}
