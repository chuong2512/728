using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonResponderV2 : MonoBehaviour
{
	public GameObject[] GameObjects;

	private int CurrentModel;

	private AnimateV2 CurrentModelSelected;

	private GameObject Camera;

	private void Start()
	{
		this.Camera = GameObject.Find("Camera");
		this.CurrentModelSelected = this.GameObjects[this.CurrentModel].GetComponent<AnimateV2>();
	}

	private void Update()
	{
	}

	public void ButtonResponderClicked()
	{
		if (this.CurrentModel < this.GameObjects.Length - 1)
		{
			this.CurrentModel++;
		}
		else
		{
			this.CurrentModel = 0;
		}
		this.CurrentModelSelected = this.GameObjects[this.CurrentModel].GetComponent<AnimateV2>();
		this.Camera.GetComponentInChildren<MouseOrbitImprovedMod>().target = this.GameObjects[this.CurrentModel].transform;
		base.GetComponentInChildren<Text>().text = this.CurrentModelSelected.name;
	}

	public void StandButtonClicked()
	{
		this.CurrentModelSelected.StandButtonClicked();
		MonoBehaviour.print("Stand Button CLicked");
	}

	public void SitButtonClicked()
	{
		this.CurrentModelSelected.SitButtonClicked();
	}

	public void LayButtonClicked()
	{
		this.CurrentModelSelected.LayButtonClicked();
	}

	public void ConsumeButtonClicked()
	{
		this.CurrentModelSelected.ConsumeButtonClicked();
	}

	public void AggressiveButtonClicked()
	{
		this.CurrentModelSelected.AggressiveButtonClicked();
	}

	public void WalkButtonClicked()
	{
		this.CurrentModelSelected.WalkButtonClicked();
	}

	public void ChangeMatButtonClicked()
	{
		this.CurrentModelSelected.GetComponentInChildren<ChangeShader>().ChangeShaderButtonClicked();
	}

	public void ChangeBlendButtonClicked()
	{
		this.CurrentModelSelected.GetComponentInChildren<ChangeBlendShape>().ChangeBlend();
	}
}
