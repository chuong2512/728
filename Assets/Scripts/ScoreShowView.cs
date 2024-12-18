using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreShowView : MonoBehaviour
{
	public Text m_Txt;

	private void Start()
	{
		Sequence expr_05 = DOTween.Sequence();
		expr_05.Insert(0.2f, base.transform.DOLocalMoveY(base.transform.localPosition.y + 50f, 1f, false));
		expr_05.Insert(0.2f, this.m_Txt.DOFade(0f, 1f));
		expr_05.AppendCallback(new TweenCallback(this.showEnd));
	}

	private void Update()
	{
	}

	private void showEnd()
	{
		base.transform.SetParent(null);
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
