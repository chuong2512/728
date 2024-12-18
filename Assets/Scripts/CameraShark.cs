using DG.Tweening;
using System;
using UnityEngine;

public class CameraShark : MonoBehaviour
{
	private void Start()
	{
	}

	public void doShark()
	{
		base.transform.DOShakePosition(1.2f, 0.4f, 10, 90f, false, true);
	}

	private void Update()
	{
	}
}
