using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollViewListener2 : MonoBehaviour, IDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler
{
	public enum MoveDirection
	{
		None,
		Left,
		Right
	}

	public float SingleItemWidth;

	public RectTransform content;

	public float DragMinValue = 5f;

	private ScrollViewListener2.MoveDirection direction;

	private int CurIndex;

	private int MaxIndex = 22;

	public bool canMove = true;

	private Vector3 originalPos;

	private float maxDeltaX;

	public static Action<int> OnPageChange;

	public List<int> m_PageVector;

	private float m_Beginx;

	private float m_endx;

	private void Start()
	{
		this.m_PageVector.Add(2200);
		this.m_PageVector.Add(2000);
		this.m_PageVector.Add(1800);
		this.m_PageVector.Add(1600);
		this.m_PageVector.Add(1400);
		this.m_PageVector.Add(1200);
		this.m_PageVector.Add(1000);
		this.m_PageVector.Add(800);
		this.m_PageVector.Add(600);
		this.m_PageVector.Add(400);
		this.m_PageVector.Add(200);
		this.m_PageVector.Add(0);
		this.m_PageVector.Add(-200);
		this.m_PageVector.Add(-400);
		this.m_PageVector.Add(-600);
		this.m_PageVector.Add(-800);
		this.m_PageVector.Add(-1000);
		this.m_PageVector.Add(-1200);
		this.m_PageVector.Add(-1400);
		this.m_PageVector.Add(-1600);
		this.m_PageVector.Add(-1800);
		this.m_PageVector.Add(-2000);
		this.m_PageVector.Add(-2200);
		this.content.DOLocalMoveX((float)this.m_PageVector[0], 0f, false);
	}

	private void MoveToNext()
	{
		if (this.direction == ScrollViewListener2.MoveDirection.Left)
		{
			if (this.CurIndex < this.MaxIndex)
			{
				this.CurIndex++;
				this.CheckCurIndex();
				this.canMove = false;
				this.content.DOLocalMoveX((float)this.m_PageVector[this.CurIndex], 0.3f, false).OnComplete(delegate
				{
					if (ScrollViewListener2.OnPageChange != null)
					{
						ScrollViewListener2.OnPageChange(this.CurIndex);
					}
					this.Toggle(this.CurIndex);
					this.canMove = true;
				});
				return;
			}
		}
		else if (this.direction == ScrollViewListener2.MoveDirection.Right && this.CurIndex > 0)
		{
			this.CurIndex--;
			this.CheckCurIndex();
			this.canMove = false;
			this.content.DOLocalMoveX((float)this.m_PageVector[this.CurIndex], 0.3f, false).OnComplete(delegate
			{
				if (ScrollViewListener2.OnPageChange != null)
				{
					ScrollViewListener2.OnPageChange(this.CurIndex);
				}
				this.canMove = true;
				this.Toggle(this.CurIndex);
			});
		}
	}

	public void CheckCurIndex()
	{
		if (this.CurIndex >= 23)
		{
			this.CurIndex = 22;
		}
		if (this.CurIndex <= 0)
		{
			this.CurIndex = 0;
		}
	}

	public void SetMaxIndex(int max)
	{
		this.MaxIndex = max - 1;
	}

	public void SetCurIndex(int index)
	{
		this.CurIndex = index;
		this.CheckCurIndex();
		int arg_1E_0 = this.m_PageVector[this.CurIndex];
		this.content.DOLocalMoveX((float)this.m_PageVector[this.CurIndex], 0.5f, false);
		this.Toggle(index);
		if (ScrollViewListener2.OnPageChange != null)
		{
			ScrollViewListener2.OnPageChange(this.CurIndex);
		}
	}

	public int GetCurIndex()
	{
		return this.CurIndex;
	}

	public void ResetPosition()
	{
		this.content.localPosition = this.originalPos;
	}

	private void Awake()
	{
		this.CurIndex = 0;
		this.originalPos = this.content.localPosition;
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (Mathf.Abs(eventData.delta.x) > this.maxDeltaX)
		{
			this.maxDeltaX = Mathf.Abs(eventData.delta.x);
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		UnityEngine.Debug.Log(eventData.position);
		this.m_Beginx = eventData.position.x;
		if (eventData.delta.x > 0f)
		{
			this.direction = ScrollViewListener2.MoveDirection.Right;
			return;
		}
		if (eventData.delta.x < 0f)
		{
			this.direction = ScrollViewListener2.MoveDirection.Left;
			return;
		}
		this.direction = ScrollViewListener2.MoveDirection.None;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		UnityEngine.Debug.Log(eventData.position);
		this.m_endx = eventData.position.x;
		if (Mathf.Abs(this.m_Beginx - this.m_endx) > this.DragMinValue)
		{
			if (this.canMove)
			{
				this.MoveToNext();
			}
		}
		else
		{
			this.content.DOLocalMoveX((float)this.m_PageVector[this.CurIndex], 0.2f, false);
		}
		this.maxDeltaX = 0f;
	}

	public void Toggle(int type)
	{
		for (int i = 0; i < 23; i++)
		{
		}
	}
}
