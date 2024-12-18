using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TextEffAnimation : MonoBehaviour
{
	private void Start()
	{
	}

	public void startMove()
	{
		base.transform.localScale = Vector3.zero;
		base.gameObject.GetComponent<Text>().color = new Color(base.gameObject.GetComponent<Text>().color.r, base.gameObject.GetComponent<Text>().color.g, base.gameObject.GetComponent<Text>().color.b, 0f);
		Sequence expr_6E = DOTween.Sequence();
		expr_6E.Insert(0f, base.gameObject.GetComponent<Text>().DOFade(255f, 0.2f).SetEase(Ease.Linear));
		expr_6E.Insert(0f, base.transform.DOScale(1f, 0.2f).SetEase(Ease.Linear));
		expr_6E.Insert(0f, base.transform.DOLocalMoveY(base.transform.localPosition.y + 20f, 0.2f, false).SetEase(Ease.Linear));
		expr_6E.Insert(0.2f, base.transform.DOLocalMoveY(base.transform.localPosition.y + 100f, 0.8f, false).SetEase(Ease.Linear));
		expr_6E.Insert(0.8f, base.gameObject.GetComponent<Text>().DOFade(0f, 0.2f).SetEase(Ease.Linear));
		expr_6E.InsertCallback(1.1f, delegate
		{
			base.transform.SetParent(null);
			UnityEngine.Object.Destroy(base.gameObject);
		});
	}

	private void Update()
	{
	}
}
