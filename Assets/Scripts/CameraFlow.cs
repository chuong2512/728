using System;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
	public Transform target;

	private Vector3 offset;

	public bool m_isRun = true;

	private void Start()
	{
		this.offset = this.target.position - base.transform.position;
	}

	private void Update()
	{
		base.transform.position = this.target.position - this.offset;
	}
}
