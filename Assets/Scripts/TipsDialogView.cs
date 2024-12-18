using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TipsDialogView : MonoBehaviour
{
	public Text m_Content;

	public int m_ind;

	public Image m_SureImg;

	private void Start()
	{
		if (this.m_ind == 0)
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Insert(0f, base.transform.DOLocalMoveY(20f, 0.8f, false));
			sequence.Insert(0f, this.m_Content.DOFade(0f, 0.8f));
			Sequence expr_56 = DOTween.Sequence();
			expr_56.AppendInterval(1.5f);
			expr_56.Append(sequence);
			expr_56.AppendCallback(new TweenCallback(this.showEnd));
			return;
		}
		Sequence sequence2 = DOTween.Sequence();
		sequence2.Insert(0f, base.transform.DOScale(0.5f, 0.8f));
		sequence2.Insert(0f, this.m_Content.DOFade(0f, 0.8f));
		Sequence expr_CA = DOTween.Sequence();
		expr_CA.AppendInterval(1.5f);
		expr_CA.Append(sequence2);
		expr_CA.AppendCallback(new TweenCallback(this.showEnd));
	}

	private void showEnd()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public static void showTips(string txt, Transform tran)
	{
		GameObject expr_0F = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>("Prefab/MainGame/Tips"));
		expr_0F.transform.SetParent(tran);
		expr_0F.transform.localPosition = new Vector3(200f, -400f, 0f);
		expr_0F.transform.localScale = Vector3.one;
		expr_0F.GetComponent<TipsDialogView>().m_Content.text = txt;
		expr_0F.GetComponent<TipsDialogView>().m_ind = 1;
	}

	public static void showTipsEf(string txt, Transform tran)
	{
	}

	public static void showTips1Ef(string txt, Transform tran)
	{
		GameObject expr_0F = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>("Prefab/MainGame/Tip1s"));
		expr_0F.transform.SetParent(tran);
		expr_0F.transform.localPosition = new Vector3(0f, -400f, 0f);
		expr_0F.transform.localScale = Vector3.one;
		expr_0F.GetComponent<TipsDialogView>().m_Content.text = txt;
	}

	private void Update()
	{
	}
}
