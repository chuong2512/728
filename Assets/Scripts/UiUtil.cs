using System;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class UiUtil : Singleton<UiUtil>
{
	public void ShowText(Transform tra, Vector3 pos, string txt, int size = 40)
	{
		Color white = Color.white;
		this.ShowText(tra, pos, txt, white, size);
	}

	public void ShowText(Transform tra, Vector3 pos, string txt, Color colorint, int size = 40)
	{
		GameObject expr_0F = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>("Prefab/Ui/ShowText"));
		expr_0F.transform.SetParent(tra);
		expr_0F.transform.position = pos;
		expr_0F.transform.localScale = Vector3.one;
		expr_0F.GetComponent<Text>().text = txt;
		expr_0F.GetComponent<Text>().fontSize = size;
		expr_0F.GetComponent<Text>().color = colorint;
	}
}
