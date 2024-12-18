using System;
using UnityEngine;
using UnityEngine.UI;

public class bl_COExample : MonoBehaviour
{
	public bl_CameraOrbit Orbit;

	[SerializeField]
	private Transform[] Targets;

	[SerializeField]
	private Text CurrenTragetText;

	private int CurrentTarget;

	private void Start()
	{
		this.CurrenTragetText.text = this.Targets[0].name;
	}

	public void ChangeType(int _type)
	{
		switch (_type)
		{
		case 0:
			this.Orbit.MovementType = CameraMovementType.Normal;
			this.Orbit.LerpSpeed = 10f;
			return;
		case 1:
			this.Orbit.MovementType = CameraMovementType.Dynamic;
			this.Orbit.LerpSpeed = 7f;
			return;
		case 2:
			this.Orbit.MovementType = CameraMovementType.Towars;
			this.Orbit.LerpSpeed = 6f;
			return;
		default:
			return;
		}
	}

	public void OnAxisSpeed(float value)
	{
		this.Orbit.SpeedAxis.x = value;
		this.Orbit.SpeedAxis.y = value;
	}

	public void OnAxisSmooth(float value)
	{
		this.Orbit.LerpSpeed = value;
	}

	public void LockCursor(bool value)
	{
		this.Orbit.LockCursorOnRotate = value;
	}

	public void ReuieredInput(bool value)
	{
		this.Orbit.RequiredInput = value;
	}

	public void OnOutInput(float value)
	{
		this.Orbit.OutInputSpeed = value;
	}

	public void OnPuw(float value)
	{
		this.Orbit.TouchZoomAmount = value;
	}

	public void Teleport(bool value)
	{
		this.Orbit.TeleporOnHit = value;
	}

	public void AutoRot(bool value)
	{
		this.Orbit.AutoRotate = value;
	}

	public void AutoRotSpeed(float value)
	{
		this.Orbit.AutoRotationSpeed = value;
	}

	public void ZoomSpeed(float value)
	{
		this.Orbit.ZoomSpeed = value;
	}

	public void Radius(float value)
	{
		this.Orbit.CollisionRadius = value;
	}

	public void DetectCollision(bool value)
	{
		this.Orbit.DetectCollision = value;
	}

	public void ChangeTarget(bool b)
	{
		if (b)
		{
			this.CurrentTarget = (this.CurrentTarget + 1) % this.Targets.Length;
		}
		else if (this.CurrentTarget > 0)
		{
			this.CurrentTarget = (this.CurrentTarget - 1) % this.Targets.Length;
		}
		else
		{
			this.CurrentTarget = this.Targets.Length - 1;
		}
		this.Orbit.SetTarget(this.Targets[this.CurrentTarget]);
		this.CurrenTragetText.text = this.Targets[this.CurrentTarget].name;
	}
}
