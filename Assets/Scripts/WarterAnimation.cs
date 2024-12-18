using System;
using UnityEngine;

public class WarterAnimation : MonoBehaviour
{
	private float radian;

	private float perRadian = 0.05f;

	private float radius = 0.2f;

	private Vector3 oldPos;

	private void Start()
	{
		this.oldPos = base.transform.position;
	}

	private void Update()
	{
		this.radian += this.perRadian;
		float num = Mathf.Sin(this.radian) * this.radius;
		base.transform.position = new Vector3(base.transform.position.x, this.oldPos.y + num, base.transform.position.z);
	}
}
