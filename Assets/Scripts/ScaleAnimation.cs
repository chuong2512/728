using DG.Tweening;
using System;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
	private void Start()
	{
		Sequence expr_05 = DOTween.Sequence();
		expr_05.Append(base.transform.DOScale(1.1f, 1f).SetEase(Ease.InSine));
		expr_05.Append(base.transform.DOScale(1f, 1f).SetEase(Ease.InSine));
		expr_05.SetLoops(-1);
	}

	private void Update()
	{
	}
}
