using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class bl_OrbitTouchPad : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IDragHandler
{
	[Header("Movement"), SerializeField]
	private bool OverrideEditor = true;

	[SerializeField]
	private bl_CameraOrbit m_CameraOrbit;

	[SerializeField]
	private Vector2 MovementMultiplier = new Vector2(1f, 1f);

	[Header("Pinch Zoom")]
	public bool CancelRotateOnPinch = true;

	[Range(0.01f, 2f), SerializeField]
	private float m_PinchZoomSpeed = 0.5f;

	private Vector2 origin;

	private Vector2 direction;

	private Vector2 smoothDirection;

	private bool touched;

	private int pointerID;

	private bool Pinched;

	private void Awake()
	{
		this.direction = Vector2.zero;
		this.touched = false;
	}

	public void OnPointerDown(PointerEventData data)
	{
		if (this.m_CameraOrbit == null)
		{
			UnityEngine.Debug.LogWarning("Please assign a camera orbit target");
			return;
		}
		if (!this.touched)
		{
			this.touched = true;
			this.pointerID = data.pointerId;
			this.origin = data.position;
		}
	}

	public void OnDrag(PointerEventData data)
	{
		if (this.m_CameraOrbit == null)
		{
			UnityEngine.Debug.LogWarning("Please assign a camera orbit target");
			return;
		}
		this.PinchZoom(data);
		if (this.Pinched)
		{
			return;
		}
		if (!this.OverrideEditor && data.pointerId == this.pointerID)
		{
			this.direction = (data.position - this.origin).normalized;
			this.m_CameraOrbit.Horizontal = this.direction.x * this.MovementMultiplier.x;
			this.m_CameraOrbit.Vertical = -this.direction.y * this.MovementMultiplier.y;
		}
	}

	private void ReanudeControl()
	{
		this.m_CameraOrbit.Interact = true;
		this.m_CameraOrbit.CanRotate = true;
		this.Pinched = false;
	}

	private void PinchZoom(PointerEventData data)
	{
		if (UnityEngine.Input.touchCount == 2 && UnityEngine.Input.GetTouch(0).phase == TouchPhase.Moved && UnityEngine.Input.GetTouch(1).phase == TouchPhase.Moved)
		{
			Touch touch = UnityEngine.Input.GetTouch(0);
			Touch touch2 = UnityEngine.Input.GetTouch(1);
			Vector2 arg_69_0 = touch.position - touch.deltaPosition;
			Vector2 b = touch2.position - touch2.deltaPosition;
			float arg_97_0 = (arg_69_0 - b).magnitude;
			float magnitude = (touch.position - touch2.position).magnitude;
			float num = arg_97_0 - magnitude;
			this.m_CameraOrbit.SetStaticZoom(num * this.m_PinchZoomSpeed);
			if (this.CancelRotateOnPinch)
			{
				base.CancelInvoke("ReanudeControl");
				this.m_CameraOrbit.Interact = false;
				this.m_CameraOrbit.CanRotate = false;
				base.Invoke("ReanudeControl", 0.2f);
				this.Pinched = true;
			}
		}
	}

	public void OnPointerUp(PointerEventData data)
	{
		if (this.m_CameraOrbit == null)
		{
			UnityEngine.Debug.LogWarning("Please assign a camera orbit target");
			return;
		}
		if (data.pointerId == this.pointerID)
		{
			this.direction = Vector2.zero;
			this.touched = false;
		}
	}
}
