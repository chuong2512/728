using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Util
{
	public class UIEventListener : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, ISelectHandler, IUpdateSelectedHandler, IDeselectHandler, IDragHandler, IEndDragHandler, IDropHandler, IScrollHandler, IMoveHandler
	{
		public delegate void VoidDelegate(GameObject go);

		public UIEventListener.VoidDelegate onClick;

		public UIEventListener.VoidDelegate onDown;

		public UIEventListener.VoidDelegate onEnter;

		public UIEventListener.VoidDelegate onExit;

		public UIEventListener.VoidDelegate onUp;

		public UIEventListener.VoidDelegate onSelect;

		public UIEventListener.VoidDelegate onUpdateSelect;

		public UIEventListener.VoidDelegate onDeSelect;

		public UIEventListener.VoidDelegate onDrag;

		public UIEventListener.VoidDelegate onDragEnd;

		public UIEventListener.VoidDelegate onDrop;

		public UIEventListener.VoidDelegate onScroll;

		public UIEventListener.VoidDelegate onMove;

		public void OnPointerClick(PointerEventData eventData)
		{
			if (this.onClick != null)
			{
				this.onClick(base.gameObject);
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.onDown != null)
			{
				this.onDown(base.gameObject);
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.onEnter != null)
			{
				this.onEnter(base.gameObject);
			}
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.onExit != null)
			{
				this.onExit(base.gameObject);
			}
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (this.onUp != null)
			{
				this.onUp(base.gameObject);
			}
		}

		public void OnSelect(BaseEventData eventData)
		{
			if (this.onSelect != null)
			{
				this.onSelect(base.gameObject);
			}
		}

		public void OnUpdateSelected(BaseEventData eventData)
		{
			if (this.onUpdateSelect != null)
			{
				this.onUpdateSelect(base.gameObject);
			}
		}

		public void OnDeselect(BaseEventData eventData)
		{
			if (this.onDeSelect != null)
			{
				this.onDeSelect(base.gameObject);
			}
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (this.onDrag != null)
			{
				this.onDrag(base.gameObject);
			}
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (this.onDragEnd != null)
			{
				this.onDragEnd(base.gameObject);
			}
		}

		public void OnDrop(PointerEventData eventData)
		{
			if (this.onDrop != null)
			{
				this.onDrop(base.gameObject);
			}
		}

		public void OnScroll(PointerEventData eventData)
		{
			if (this.onScroll != null)
			{
				this.onScroll(base.gameObject);
			}
		}

		public void OnMove(AxisEventData eventData)
		{
			if (this.onMove != null)
			{
				this.onMove(base.gameObject);
			}
		}

		public static UIEventListener Get(GameObject go)
		{
			UIEventListener uIEventListener = go.GetComponent<UIEventListener>();
			if (uIEventListener == null)
			{
				uIEventListener = go.AddComponent<UIEventListener>();
			}
			return uIEventListener;
		}
	}
}
