using System;
using UnityEngine;
using UnityEngine.UI;

public class AnimateV2 : MonoBehaviour
{
	private Animator anim;

	private string AnimatorName;

	private int Move;

	private int Pose;

	private int CurrentPose;

	private bool ChangePose;

	public bool StateChangeComplete = true;

	private string CurrentButtonPressed = "Stand";

	private GameObject AggressiveButton;

	private GameObject LayButton;

	private GameObject StandButton;

	private GameObject SitButton;

	private GameObject ConsumeButton;

	private float CrossfadeVal = 0.25f;

	private bool BackWards;

	private void Start()
	{
		this.AggressiveButton = GameObject.Find("Aggressive");
		this.LayButton = GameObject.Find("Lay");
		this.StandButton = GameObject.Find("Stand");
		this.SitButton = GameObject.Find("Sit");
		this.ConsumeButton = GameObject.Find("Consume");
		this.anim = base.GetComponent<Animator>();
		this.AnimatorName = this.anim.name;
		MonoBehaviour.print("name " + this.AnimatorName);
	}

	private void Update()
	{
		if (this.ChangePose)
		{
			MonoBehaviour.print("Change Pose");
			this.ChangePose = false;
			if (this.CurrentPose == 0)
			{
				if (this.Pose == 1)
				{
					this.anim.CrossFade("IdleToAggressive", this.CrossfadeVal);
				}
				else if (this.Pose == 2)
				{
					this.anim.CrossFade("IdleToSit", this.CrossfadeVal);
				}
				else if (this.Pose == 3)
				{
					this.anim.CrossFade("IdleToLay", this.CrossfadeVal);
				}
				else if (this.Pose == 5)
				{
					this.anim.CrossFade("IdleToConsume", this.CrossfadeVal);
				}
				this.CurrentPose = this.Pose;
				return;
			}
			if (this.CurrentPose == 1)
			{
				if (this.Pose == 0)
				{
					this.anim.CrossFade("AggressiveToIdle", this.CrossfadeVal);
				}
				else if (this.Pose == 2)
				{
					this.anim.CrossFade("AggressiveToSitTrans", this.CrossfadeVal);
				}
				else if (this.Pose == 3)
				{
					this.anim.CrossFade("AggressiveToLayTrans", this.CrossfadeVal);
				}
				else if (this.Pose == 4)
				{
					this.anim.CrossFade("AggressiveToIdle", this.CrossfadeVal);
				}
				else if (this.Pose == 5)
				{
					this.anim.CrossFade("AggressiveToEat", this.CrossfadeVal);
				}
				this.CurrentPose = this.Pose;
				return;
			}
			if (this.CurrentPose == 2)
			{
				if (this.Pose == 0)
				{
					this.anim.CrossFade("SitToIdle", this.CrossfadeVal);
				}
				else if (this.Pose == 1)
				{
					this.anim.CrossFade("SitToAggressiveTrans", this.CrossfadeVal);
				}
				else if (this.Pose == 3)
				{
					this.anim.CrossFade("SitToLay", this.CrossfadeVal);
				}
				else if (this.Pose == 4)
				{
					this.anim.CrossFade("SitToIdle", this.CrossfadeVal);
				}
				else if (this.Pose == 5)
				{
					this.anim.CrossFade("SitToEat", this.CrossfadeVal);
				}
				this.CurrentPose = this.Pose;
				return;
			}
			if (this.CurrentPose == 3)
			{
				if (this.Pose == 0)
				{
					this.anim.CrossFade("LayToIdle", this.CrossfadeVal);
				}
				else if (this.Pose == 1)
				{
					this.anim.CrossFade("LayToAggressiveTrans", this.CrossfadeVal);
				}
				else if (this.Pose == 2)
				{
					this.anim.CrossFade("LayToSit", this.CrossfadeVal);
				}
				else if (this.Pose == 4)
				{
					this.anim.CrossFade("LayToIdle", this.CrossfadeVal);
				}
				else if (this.Pose == 5)
				{
					this.anim.CrossFade("LayToEat", this.CrossfadeVal);
				}
				this.CurrentPose = this.Pose;
				return;
			}
			if (this.CurrentPose == 4 || this.CurrentPose == 5)
			{
				if (this.Pose == 0)
				{
					this.anim.CrossFade("Idle", this.CrossfadeVal);
				}
				else if (this.Pose == 1)
				{
					this.anim.CrossFade("IdleToAggressive", this.CrossfadeVal);
				}
				else if (this.Pose == 2)
				{
					this.anim.CrossFade("IdleToSit", this.CrossfadeVal);
				}
				else if (this.Pose == 3)
				{
					this.anim.CrossFade("IdleToLay", this.CrossfadeVal);
				}
				else if (this.Pose == 5)
				{
					this.anim.CrossFade("IdleToConsume", this.CrossfadeVal);
				}
				this.CurrentPose = this.Pose;
			}
		}
	}

	public void StandButtonClicked()
	{
		if (this.CurrentButtonPressed != "Stand")
		{
			this.Pose = 0;
			this.ChangePose = true;
			this.ResetButtonNames();
		}
		else
		{
			this.anim.CrossFade(this.StandButton.GetComponentInChildren<Text>().text, 0.5f);
		}
		this.Move = 0;
		this.anim.SetFloat("Move", (float)this.Move);
		this.CurrentButtonPressed = "Stand";
	}

	public void SitButtonClicked()
	{
		if (this.CurrentButtonPressed != "Sit")
		{
			this.Pose = 2;
			this.ChangePose = true;
			this.ResetButtonNames();
		}
		else
		{
			this.anim.CrossFade(this.SitButton.GetComponentInChildren<Text>().text, 0.5f);
		}
		this.Move = 0;
		this.CurrentButtonPressed = "Sit";
		this.anim.SetFloat("Move", (float)this.Move);
	}

	public void LayButtonClicked()
	{
		if (this.CurrentButtonPressed != "Lay")
		{
			this.Pose = 3;
			this.ChangePose = true;
			this.ResetButtonNames();
		}
		else
		{
			this.anim.CrossFade(this.LayButton.GetComponentInChildren<Text>().text, 0.5f);
		}
		this.Move = 0;
		this.anim.SetFloat("Move", (float)this.Move);
		this.CurrentButtonPressed = "Lay";
	}

	public void ConsumeButtonClicked()
	{
		if (this.CurrentButtonPressed != "Consume")
		{
			this.Pose = 5;
			this.ChangePose = true;
			this.ResetButtonNames();
		}
		else
		{
			this.anim.CrossFade(this.ConsumeButton.GetComponentInChildren<Text>().text, 0.5f);
		}
		this.Move = 0;
		this.anim.SetFloat("Move", (float)this.Move);
		this.CurrentButtonPressed = "Consume";
	}

	public void AggressiveButtonClicked()
	{
		if (this.CurrentButtonPressed != "Aggressive")
		{
			this.Pose = 1;
			this.ChangePose = true;
			this.ResetButtonNames();
		}
		else
		{
			this.anim.CrossFade(this.AggressiveButton.GetComponentInChildren<Text>().text, 0.5f);
		}
		this.Move = 0;
		this.anim.SetFloat("Move", (float)this.Move);
		this.CurrentButtonPressed = "Aggressive";
	}

	public void WalkButtonClicked()
	{
		if (this.Move < 3 && !this.BackWards)
		{
			this.Move++;
		}
		else
		{
			this.BackWards = true;
			this.Move--;
			if (this.Move == 1)
			{
				this.BackWards = false;
			}
		}
		this.anim.SetFloat("Move", (float)this.Move);
		if (this.Pose != 4)
		{
			this.ChangePose = true;
			this.ResetButtonNames();
		}
		this.Pose = 4;
		this.CurrentButtonPressed = "Walk";
	}

	private void ResetButtonNames()
	{
		GameObject gameObject = GameObject.Find(this.CurrentButtonPressed);
		gameObject.GetComponentInChildren<Text>().text = this.CurrentButtonPressed;
		MonoBehaviour.print("change button name and it is now " + gameObject.GetComponentInChildren<Text>().text);
		gameObject.GetComponentInChildren<ChangeButtonText>().ValuetoGet = 0;
	}
}
