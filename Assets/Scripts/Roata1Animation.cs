using DG.Tweening;
using System;
using UnityEngine;

public class Roata1Animation : MonoBehaviour
{
	public float m_duration = 1f;

	public float m_pause;

	public Ease m_Ease = Ease.Linear;

	public int m_desiner = -1;

	public bool m_isAuto;

	private Sequence mySequence;

	private void Start()
	{
		if (this.m_isAuto)
		{
			base.Invoke("doEf", 0.01f);
		}
	}

	public void doStop()
	{
		this.mySequence.Pause<Sequence>();
		base.transform.DOPause();
	}

	public void doEf()
	{
		Vector3 endValue = new Vector3(0f, 0f, (float)(360 * this.m_desiner));
		this.mySequence = DOTween.Sequence();
		this.mySequence.Append(base.transform.DOLocalRotate(endValue, this.m_duration, RotateMode.LocalAxisAdd).SetEase(this.m_Ease));
		this.mySequence.SetLoops(-1);
	}

	private void Update()
	{
	}
}
