using DG.Tweening;
using System;
using UnityEngine;

public class ShakeAnimation : MonoBehaviour
{
	private Sequence mySequence;

	private void Start()
	{
		this.startEf();
	}

	public void startEf()
	{
		this.stopEf();
		this.mySequence = DOTween.Sequence();
		this.mySequence.Append(base.transform.DOLocalRotate(new Vector3(0f, 0f, -9f), 0.1f, RotateMode.Fast).SetEase(Ease.InSine));
		this.mySequence.Append(base.transform.DOLocalRotate(new Vector3(0f, 0f, 8f), 0.1f, RotateMode.Fast).SetEase(Ease.InSine));
		this.mySequence.Append(base.transform.DOLocalRotate(new Vector3(0f, 0f, -7f), 0.1f, RotateMode.Fast).SetEase(Ease.InSine));
		this.mySequence.Append(base.transform.DOLocalRotate(new Vector3(0f, 0f, 6f), 0.1f, RotateMode.Fast).SetEase(Ease.InSine));
		this.mySequence.Append(base.transform.DOLocalRotate(new Vector3(0f, 0f, -5f), 0.1f, RotateMode.Fast).SetEase(Ease.InSine));
		this.mySequence.Append(base.transform.DOLocalRotate(new Vector3(0f, 0f, 4f), 0.1f, RotateMode.Fast).SetEase(Ease.InSine));
		this.mySequence.Append(base.transform.DOLocalRotate(new Vector3(0f, 0f, -3f), 0.1f, RotateMode.Fast).SetEase(Ease.InSine));
		this.mySequence.Append(base.transform.DOLocalRotate(new Vector3(0f, 0f, 2f), 0.1f, RotateMode.Fast).SetEase(Ease.InSine));
		this.mySequence.Append(base.transform.DOLocalRotate(new Vector3(0f, 0f, -1f), 0.1f, RotateMode.Fast).SetEase(Ease.InSine));
		this.mySequence.Append(base.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.1f, RotateMode.Fast).SetEase(Ease.InSine));
		this.mySequence.AppendInterval(1f);
		this.mySequence.SetLoops(-1);
	}

	public void stopEf()
	{
		this.mySequence.Pause<Sequence>();
		base.transform.DOPause();
	}

	private void Update()
	{
	}
}
