using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Util;

public class ScrollViewListener : MonoBehaviour, IDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler
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

	private ScrollViewListener.MoveDirection direction;

	private int CurIndex;

	private int MaxIndex = 27;

	public bool canMove = true;

	private Vector3 originalPos;

	private float maxDeltaX;

	public static Action<int> OnPageChange;

	public List<int> m_PageVector;

	private float m_Beginx;

	private float m_endx;

	private void Start()
	{
		this.m_PageVector.Add(2430);
		this.m_PageVector.Add(2250);
		this.m_PageVector.Add(2070);
		this.m_PageVector.Add(1890);
		this.m_PageVector.Add(1710);
		this.m_PageVector.Add(1530);
		this.m_PageVector.Add(1350);
		this.m_PageVector.Add(1170);
		this.m_PageVector.Add(990);
		this.m_PageVector.Add(810);
		this.m_PageVector.Add(630);
		this.m_PageVector.Add(450);
		this.m_PageVector.Add(270);
		this.m_PageVector.Add(90);
		this.m_PageVector.Add(-90);
		this.m_PageVector.Add(-270);
		this.m_PageVector.Add(-450);
		this.m_PageVector.Add(-630);
		this.m_PageVector.Add(-810);
		this.m_PageVector.Add(-990);
		this.m_PageVector.Add(-1170);
		this.m_PageVector.Add(-1350);
		this.m_PageVector.Add(-1530);
		this.m_PageVector.Add(-1710);
		this.m_PageVector.Add(-1890);
		this.m_PageVector.Add(-2070);
		this.m_PageVector.Add(-2250);
		this.m_PageVector.Add(-2430);
		this.content.DOLocalMoveX((float)this.m_PageVector[Singleton<GameManager>.Instance.m_UserInfo.m_ballInd], 0f, false);
		this.CurIndex = Singleton<GameManager>.Instance.m_UserInfo.m_ballInd;
		if (ScrollViewListener.OnPageChange != null)
		{
			ScrollViewListener.OnPageChange(this.CurIndex);
		}
	}

	private void MoveToNext()
	{
		if (this.direction == ScrollViewListener.MoveDirection.Left)
		{
			if (this.CurIndex < this.MaxIndex)
			{
				this.CurIndex++;
				this.CheckCurIndex();
				this.canMove = false;
				this.content.DOLocalMoveX((float)this.m_PageVector[this.CurIndex], 0.3f, false).OnComplete(delegate
				{
					if (ScrollViewListener.OnPageChange != null)
					{
						ScrollViewListener.OnPageChange(this.CurIndex);
					}
					this.Toggle(this.CurIndex);
					this.canMove = true;
				});
				return;
			}
		}
		else if (this.direction == ScrollViewListener.MoveDirection.Right && this.CurIndex > 0)
		{
			this.CurIndex--;
			this.CheckCurIndex();
			this.canMove = false;
			this.content.DOLocalMoveX((float)this.m_PageVector[this.CurIndex], 0.3f, false).OnComplete(delegate
			{
				if (ScrollViewListener.OnPageChange != null)
				{
					ScrollViewListener.OnPageChange(this.CurIndex);
				}
				this.canMove = true;
				this.Toggle(this.CurIndex);
			});
		}
	}

	public void CheckCurIndex()
	{
		if (this.CurIndex >= 28)
		{
			this.CurIndex = 27;
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
		if (ScrollViewListener.OnPageChange != null)
		{
			ScrollViewListener.OnPageChange(this.CurIndex);
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
			this.direction = ScrollViewListener.MoveDirection.Right;
			return;
		}
		if (eventData.delta.x < 0f)
		{
			this.direction = ScrollViewListener.MoveDirection.Left;
			return;
		}
		this.direction = ScrollViewListener.MoveDirection.None;
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
		for (int i = 0; i < 28; i++)
		{
		}
	}
}
