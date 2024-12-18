using DG.Tweening;
using System;
using UnityEngine;

public class MoveAnimation_3 : MonoBehaviour
{
	public bool m_isAuto = true;

	public float m_duration = 1f;

	public int m_desiner = 1;

	public Ease m_Ease = Ease.Linear;

	private float m_startPos;

	public float m_endPos;

	private Sequence mySequence;

	private void Start()
	{
		this.m_startPos = base.transform.localPosition.y;
		if (this.m_isAuto)
		{
			this.doEf();
		}
	}

	public void doStop()
	{
		if (this.mySequence != null)
		{
			this.mySequence.Pause<Sequence>();
			base.transform.DOPause();
		}
	}

	public void doEf()
	{
		this.doStop();
		this.mySequence = DOTween.Sequence();
		this.mySequence.Append(base.transform.DOLocalMoveY(this.m_endPos, this.m_duration, false).SetEase(this.m_Ease));
		this.mySequence.Append(base.transform.DOLocalMoveY(this.m_startPos, this.m_duration, false).SetEase(this.m_Ease));
		this.mySequence.SetLoops(-1);
		this.mySequence.SetEase(this.m_Ease);
	}
}
