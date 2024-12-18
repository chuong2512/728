using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonText : MonoBehaviour
{
	public int ValuetoGet;

	public string[] AnimationNames;

	public string AnimationSelected;

	private void Start()
	{
		this.AnimationSelected = this.AnimationNames[0];
	}

	public void ChangeText()
	{
		base.GetComponentInChildren<Text>().text = this.AnimationNames[this.ValuetoGet];
		if (this.ValuetoGet < this.AnimationNames.Length - 1)
		{
			this.ValuetoGet++;
			return;
		}
		this.ValuetoGet = 0;
	}
}
