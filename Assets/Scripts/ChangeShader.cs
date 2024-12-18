using System;
using UnityEngine;

public class ChangeShader : MonoBehaviour
{
	public Material[] material;

	private Renderer rend;

	private int MatValue;

	private void Start()
	{
		this.rend = base.GetComponent<Renderer>();
		this.rend.enabled = true;
		this.rend.sharedMaterial = this.material[0];
	}

	public void ChangeShaderButtonClicked()
	{
		MonoBehaviour.print("Change mat");
		if (this.MatValue < this.material.Length - 1)
		{
			this.MatValue++;
		}
		else
		{
			this.MatValue = 0;
		}
		this.rend.sharedMaterial = this.material[this.MatValue];
	}
}
