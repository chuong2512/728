using System;
using UnityEngine;
using UnityEngine.UI;

public class HelpView : ViewBase<HelpView>
{
	public Image[] m_Point;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void onChangePage(int index)
	{
		for (int i = 0; i < this.m_Point.Length; i++)
		{
			if (index == i)
			{
				this.m_Point[i].sprite = ResourcesLoad.Load<Sprite>("Texture/UI/Help/point_2");
			}
			else
			{
				this.m_Point[i].sprite = ResourcesLoad.Load<Sprite>("Texture/UI/Help/point_1");
			}
		}
	}

	public void ClickBack()
	{
		UnityEngine.Object.Destroy(base.transform.parent.gameObject);
	}
}
