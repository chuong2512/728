using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class bl_CameraOrbit : bl_CameraBase
{
	[Serializable]
	private sealed class __c
	{
		public static readonly bl_CameraOrbit.__c __9 = new bl_CameraOrbit.__c();

		public static TweenCallback __9__71_0;

		public static TweenCallback __9__71_1;

		internal void _DoRotateCenter_b__71_0()
		{
			Time.timeScale = 0.4f;
		}

		internal void _DoRotateCenter_b__71_1()
		{
			Time.timeScale = 1f;
		}
	}

	private sealed class _TranslateTarget_d__96 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int __1__state;

		private object __2__current;

		public bl_CameraOrbit __4__this;

		public Transform newTarget;

		object IEnumerator<object>.Current
		{
			get
			{
				return this.__2__current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this.__2__current;
			}
		}

		public _TranslateTarget_d__96(int __1__state)
		{
			this.__1__state = __1__state;
		}

		void IDisposable.Dispose()
		{
		}

		bool IEnumerator.MoveNext()
		{
			int num = this.__1__state;
			bl_CameraOrbit bl_CameraOrbit = this.__4__this;
			switch (num)
			{
			case 0:
				this.__1__state = -1;
				bl_CameraOrbit.isSwitchingTarget = true;
				break;
			case 1:
				this.__1__state = -1;
				break;
			case 2:
				this.__1__state = -1;
				goto IL_FB;
			default:
				return false;
			}
			if (bl_CameraOrbit.FadeAlpha < 1f)
			{
				bl_CameraOrbit.transform.position = Vector3.Lerp(bl_CameraOrbit.transform.position, bl_CameraOrbit.transform.position + new Vector3(0f, 2f, -2f), Time.deltaTime);
				bl_CameraOrbit.FadeAlpha += Time.smoothDeltaTime * bl_CameraOrbit.SwichtSpeed;
				this.__2__current = null;
				this.__1__state = 1;
				return true;
			}
			bl_CameraOrbit.Target = this.newTarget;
			bl_CameraOrbit.isSwitchingTarget = false;
			IL_FB:
			if (bl_CameraOrbit.FadeAlpha <= 0f)
			{
				return false;
			}
			bl_CameraOrbit.FadeAlpha -= Time.smoothDeltaTime * bl_CameraOrbit.SwichtSpeed;
			this.__2__current = null;
			this.__1__state = 2;
			return true;
		}

		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	private sealed class _DetectHit_d__97 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int __1__state;

		private object __2__current;

		public bl_CameraOrbit __4__this;

		object IEnumerator<object>.Current
		{
			get
			{
				return this.__2__current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this.__2__current;
			}
		}

		public _DetectHit_d__97(int __1__state)
		{
			this.__1__state = __1__state;
		}

		void IDisposable.Dispose()
		{
		}

		bool IEnumerator.MoveNext()
		{
			int num = this.__1__state;
			bl_CameraOrbit bl_CameraOrbit = this.__4__this;
			if (num == 0)
			{
				this.__1__state = -1;
				bl_CameraOrbit.isDetectingHit = true;
				this.__2__current = new WaitForSeconds(0.4f);
				this.__1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.__1__state = -1;
			if (bl_CameraOrbit.haveHit)
			{
				bl_CameraOrbit.distance = bl_CameraOrbit.LastDistance;
				bl_CameraOrbit.haveHit = false;
			}
			bl_CameraOrbit.isDetectingHit = false;
			return false;
		}

		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	private sealed class _IEDelayFog_d__98 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int __1__state;

		private object __2__current;

		public bl_CameraOrbit __4__this;

		object IEnumerator<object>.Current
		{
			get
			{
				return this.__2__current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this.__2__current;
			}
		}

		public _IEDelayFog_d__98(int __1__state)
		{
			this.__1__state = __1__state;
		}

		void IDisposable.Dispose()
		{
		}

		bool IEnumerator.MoveNext()
		{
			int num = this.__1__state;
			bl_CameraOrbit bl_CameraOrbit = this.__4__this;
			if (num == 0)
			{
				this.__1__state = -1;
				this.__2__current = new WaitForSeconds(bl_CameraOrbit.DelayStartFoV);
				this.__1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.__1__state = -1;
			bl_CameraOrbit.canFogControl = true;
			return false;
		}

		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[HideInInspector]
	public bool m_Interact = true;

	[Header("Target")]
	public Transform Target;

	[Header("Settings")]
	public bool isForMobile;

	public bool AutoTakeInfo = true;

	public float Distance = 5f;

	[Range(0.01f, 5f)]
	public float SwichtSpeed = 2f;

	public Vector2 DistanceClamp = new Vector2(1.5f, 5f);

	public Vector2 XLimitMove = new Vector2(-3f, 3f);

	public Vector2 YLimitMove = new Vector2(-2f, 2f);

	public Vector3 m_InitPos = new Vector3(0f, 0f, 0f);

	public Vector2 YLimitClamp = new Vector2(-20f, 80f);

	public Vector2 XLimitClamp = new Vector2(360f, 360f);

	public Vector2 SpeedAxis = new Vector2(100f, 100f);

	public bool LockCursorOnRotate = true;

	[Header("Input")]
	public bool RequiredInput = true;

	public CameraMouseInputType RotateInputKey = CameraMouseInputType.LeftAndRight;

	[Range(0.001f, 0.07f)]
	public float InputMultiplier = 0.02f;

	[Range(0.1f, 15f)]
	public float InputLerp = 7f;

	public bool useKeys;

	[Header("Movement")]
	public CameraMovementType MovementType;

	[Range(-90f, 90f)]
	public float TouchZoomAmount = -5f;

	[Range(0.1f, 20f)]
	public float LerpSpeed = 7f;

	[Range(1f, 100f)]
	public float OutInputSpeed = 20f;

	[Header("Fog"), Range(5f, 179f)]
	public float StartFov = 179f;

	[Range(0.1f, 15f)]
	public float FovLerp = 7f;

	[Range(0f, 7f)]
	public float DelayStartFoV = 1.2f;

	[Range(1f, 10f)]
	public float ScrollSensitivity = 5f;

	[Range(1f, 25f)]
	public float ZoomSpeed = 7f;

	[Header("Auto Rotation")]
	public bool AutoRotate = true;

	public CameraAutoRotationType AutoRotationType;

	[Range(0f, 20f)]
	public float AutoRotSpeed = 5f;

	[Header("Collision")]
	public bool DetectCollision = true;

	public bool TeleporOnHit = true;

	[Range(0.01f, 4f)]
	public float CollisionRadius = 2f;

	public LayerMask DetectCollisionLayers;

	[Header("Fade")]
	public bool FadeOnStart = true;

	[Range(0.01f, 5f)]
	public float FadeSpeed = 2f;

	[SerializeField]
	private Texture2D FadeTexture;

	private float y;

	private float x;

	private Ray Ray;

	private bool LastHaveInput;

	private float distance;

	private float currentFog = 60f;

	private float defaultFog;

	private float horizontal;

	private float vertical;

	private float defaultAutoSpeed;

	private float lastHorizontal;

	private bool canFogControl;

	private bool haveHit;

	private float LastDistance;

	private bool m_CanRotate = true;

	private Vector3 ZoomVector;

	private Quaternion CurrentRotation;

	private Vector3 CurrentPosition;

	private float FadeAlpha = 1f;

	private bool isSwitchingTarget;

	private bool isDetectingHit;

	private float initXRotation;

	public Transform m_moveOffset;

	public Transform m_rotaOffset;

	private Vector3 m_initPos = new Vector3(-0f, 2.3f, 0f);

	private Vector3 m_initRota = new Vector3(9.08f, 0.07f, 0f);

	private Vector3 m_missionPos = new Vector3(0f, -15f, 0f);

	private Vector3 m_playerPos = new Vector3(1f, -4.4f, 11f);

	private Vector3 m_playerRota = new Vector3(0f, 15f, 0f);

	private Vector3 m_ballPos = new Vector3(1f, 1.5f, 11f);

	private Vector3 m_ballRota = new Vector3(0f, 15f, 0f);

	private float m_moveDefaultSpeed = 0.3f;

	private float m_moveSpeed = 5f;

	private bool isInputKeyRotate
	{
		get
		{
			if (!this.RequiredInput)
			{
				return false;
			}
			switch (this.RotateInputKey)
			{
			case CameraMouseInputType.LeftMouse:
				return UnityEngine.Input.GetKey(KeyCode.Mouse0);
			case CameraMouseInputType.RightMouse:
				return UnityEngine.Input.GetKey(KeyCode.Mouse1);
			case CameraMouseInputType.LeftAndRight:
				return UnityEngine.Input.GetKey(KeyCode.Mouse0) || UnityEngine.Input.GetKey(KeyCode.Mouse1);
			case CameraMouseInputType.MouseScroll:
				return UnityEngine.Input.GetKey(KeyCode.Mouse2);
			case CameraMouseInputType.MobileTouch:
				return Input.GetMouseButton(0) || Input.GetMouseButton(1);
			case CameraMouseInputType.All:
				return UnityEngine.Input.GetKey(KeyCode.Mouse0) || UnityEngine.Input.GetKey(KeyCode.Mouse1) || UnityEngine.Input.GetKey(KeyCode.Mouse2) || Input.GetMouseButton(0);
			default:
				return UnityEngine.Input.GetKey(KeyCode.Mouse0);
			}
		}
	}

	private bool isInputUpKeyRotate
	{
		get
		{
			switch (this.RotateInputKey)
			{
			case CameraMouseInputType.LeftMouse:
				return UnityEngine.Input.GetKeyUp(KeyCode.Mouse0) || Input.GetMouseButtonUp(0);
			case CameraMouseInputType.RightMouse:
				return UnityEngine.Input.GetKeyUp(KeyCode.Mouse1);
			case CameraMouseInputType.LeftAndRight:
				return UnityEngine.Input.GetKeyUp(KeyCode.Mouse0) || UnityEngine.Input.GetKeyUp(KeyCode.Mouse1);
			case CameraMouseInputType.MouseScroll:
				return UnityEngine.Input.GetKeyUp(KeyCode.Mouse2);
			case CameraMouseInputType.MobileTouch:
				return Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1);
			case CameraMouseInputType.All:
				return UnityEngine.Input.GetKeyUp(KeyCode.Mouse0) || UnityEngine.Input.GetKeyUp(KeyCode.Mouse1) || UnityEngine.Input.GetKeyUp(KeyCode.Mouse2) || Input.GetMouseButtonUp(0);
			default:
				return UnityEngine.Input.GetKey(KeyCode.Mouse0) || Input.GetMouseButton(0);
			}
		}
	}

	public float Horizontal
	{
		get
		{
			return this.horizontal;
		}
		set
		{
			this.horizontal += value;
			this.lastHorizontal = this.horizontal;
		}
	}

	public float Vertical
	{
		get
		{
			return this.vertical;
		}
		set
		{
			this.vertical += value;
		}
	}

	public bool Interact
	{
		get
		{
			return this.m_Interact;
		}
		set
		{
			this.m_Interact = value;
		}
	}

	public bool CanRotate
	{
		get
		{
			return this.m_CanRotate;
		}
		set
		{
			this.m_CanRotate = value;
		}
	}

	public float AutoRotationSpeed
	{
		get
		{
			return this.defaultAutoSpeed;
		}
		set
		{
			this.defaultAutoSpeed = value;
		}
	}

	private void Start()
	{
		this.m_moveDefaultSpeed = this.LerpSpeed;
		this.SetUp();
	}

	public void setInitPos(Transform tran)
	{
		this.m_InitPos = tran.position;
		this.SetTarget(tran);
	}

	private void SetUp()
	{
		if (this.Target == null)
		{
			UnityEngine.Debug.LogWarning("Target is not assigned to orbit camera!", this);
			return;
		}
		if (this.AutoTakeInfo)
		{
			this.distance = Vector3.Distance(base.transform.position, this.Target.position);
			this.Distance = this.distance;
			Vector3 eulerAngles = base.Transform.eulerAngles;
			this.x = eulerAngles.y;
			this.y = eulerAngles.x;
			this.initXRotation = eulerAngles.y;
			this.horizontal = this.x;
			this.vertical = this.y;
		}
		else
		{
			this.distance = this.Distance;
		}
		this.currentFog = base.GetCamera.fieldOfView;
		this.defaultFog = this.currentFog;
		base.GetCamera.fieldOfView = this.StartFov;
		this.defaultAutoSpeed = this.AutoRotSpeed;
		base.StartCoroutine(this.IEDelayFog());
		if (this.RotateInputKey == CameraMouseInputType.MobileTouch && UnityEngine.Object.FindObjectOfType<bl_OrbitTouchPad>() == null)
		{
			UnityEngine.Debug.LogWarning("For use  mobile touched be sure to put the 'OrbitTouchArea in the canvas of scene");
		}
	}

	private void LateUpdate()
	{
		if (this.Target == null)
		{
			return;
		}
		if (this.isSwitchingTarget)
		{
			return;
		}
		if (this.CanRotate)
		{
			this.ZoomControll(false);
			this.OrbitControll();
			if (this.AutoRotate && !this.isInputKeyRotate)
			{
				this.AutoRotation();
			}
		}
		else
		{
			this.ZoomControll(true);
		}
		if (!this.m_Interact)
		{
			return;
		}
		this.FogControl();
		this.InputControl();
	}

	private void InputControl()
	{
		if (this.LockCursorOnRotate && !this.useKeys && !this.isForMobile)
		{
			if (!this.isInputKeyRotate && this.LastHaveInput)
			{
				if (this.LockCursorOnRotate && this.Interact)
				{
					bl_CameraUtils.LockCursor(false);
				}
				this.LastHaveInput = false;
				if (this.lastHorizontal >= 0f)
				{
					this.AutoRotSpeed = this.OutInputSpeed;
				}
				else
				{
					this.AutoRotSpeed = -this.OutInputSpeed;
				}
			}
			if (this.isInputKeyRotate && !this.LastHaveInput)
			{
				if (this.LockCursorOnRotate && this.Interact)
				{
					bl_CameraUtils.LockCursor(true);
				}
				this.LastHaveInput = true;
			}
		}
		if (this.isInputUpKeyRotate)
		{
			this.currentFog -= this.TouchZoomAmount;
		}
	}

	private void AutoRotation()
	{
		switch (this.AutoRotationType)
		{
		case CameraAutoRotationType.Dinamicaly:
			this.AutoRotSpeed = ((this.lastHorizontal > 0f) ? Mathf.Lerp(this.AutoRotSpeed, this.defaultAutoSpeed, Time.deltaTime / 2f) : Mathf.Lerp(this.AutoRotSpeed, -this.defaultAutoSpeed, Time.deltaTime / 2f));
			break;
		case CameraAutoRotationType.Left:
			this.AutoRotSpeed = Mathf.Lerp(this.AutoRotSpeed, this.defaultAutoSpeed, Time.deltaTime / 2f);
			break;
		case CameraAutoRotationType.Right:
			this.AutoRotSpeed = Mathf.Lerp(this.AutoRotSpeed, -this.defaultAutoSpeed, Time.deltaTime / 2f);
			break;
		}
		this.horizontal += Time.deltaTime * this.AutoRotSpeed;
	}

	private void FogControl()
	{
		if (!this.canFogControl)
		{
			return;
		}
		this.currentFog = Mathf.SmoothStep(this.currentFog, this.defaultFog, Time.deltaTime * this.FovLerp);
		base.GetCamera.fieldOfView = Mathf.Lerp(base.GetCamera.fieldOfView, this.currentFog, Time.deltaTime * this.FovLerp);
	}

	private void OrbitControll()
	{
		if (this.m_Interact && !this.isForMobile)
		{
			if ((this.RequiredInput && !this.useKeys && this.isInputKeyRotate) || !this.RequiredInput)
			{
				this.horizontal += this.SpeedAxis.x * this.InputMultiplier * base.AxisX;
				this.vertical -= this.SpeedAxis.y * this.InputMultiplier * base.AxisY;
				this.lastHorizontal = base.AxisX;
			}
			else if (this.useKeys)
			{
				this.horizontal -= base.KeyAxisX * this.SpeedAxis.x * this.InputMultiplier;
				this.vertical += base.KeyAxisY * this.SpeedAxis.y * this.InputMultiplier;
				this.lastHorizontal = base.KeyAxisX;
			}
		}
		this.vertical = bl_CameraUtils.ClampAngle(this.vertical, this.YLimitClamp.x, this.YLimitClamp.y);
		if (this.XLimitClamp.x < 360f && this.XLimitClamp.y < 360f)
		{
			this.horizontal = bl_CameraUtils.ClampAngle(this.horizontal, this.initXRotation - this.XLimitClamp.y, this.XLimitClamp.x + this.initXRotation);
		}
		this.x = Mathf.Lerp(this.x, this.horizontal, Time.deltaTime * this.InputLerp);
		this.y = Mathf.Lerp(this.y, this.vertical, Time.deltaTime * this.InputLerp);
		this.y = bl_CameraUtils.ClampAngle(this.y, this.YLimitClamp.x, this.YLimitClamp.y);
		this.CurrentRotation = Quaternion.Euler(this.y, this.x, 0f);
		this.CurrentPosition = this.CurrentRotation * this.ZoomVector + this.Target.position;
		if (this.CurrentPosition.x < this.m_InitPos.x + this.XLimitMove.x)
		{
			this.CurrentPosition.x = this.m_InitPos.x + this.XLimitMove.x;
		}
		if (this.CurrentPosition.x > this.m_InitPos.x + this.XLimitMove.y)
		{
			this.CurrentPosition.x = this.m_InitPos.x + this.XLimitMove.y;
		}
		if (this.CurrentPosition.y < this.m_InitPos.y + this.YLimitMove.x)
		{
			this.CurrentPosition.y = this.m_InitPos.y + this.YLimitMove.x;
		}
		if (this.CurrentPosition.y > this.m_InitPos.y + this.YLimitMove.y)
		{
			this.CurrentPosition.y = this.m_InitPos.y + this.YLimitMove.y;
		}
		switch (this.MovementType)
		{
		case CameraMovementType.Normal:
			base.Transform.rotation = Quaternion.Euler(this.CurrentRotation.x + this.m_rotaOffset.localEulerAngles.x, this.CurrentRotation.y + this.m_rotaOffset.localEulerAngles.y, this.CurrentRotation.z + this.m_rotaOffset.localEulerAngles.z);
			base.Transform.position = this.CurrentPosition + this.m_moveOffset.localPosition + this.m_rotaOffset.localPosition;
			return;
		case CameraMovementType.Dynamic:
			base.Transform.position = Vector3.Lerp(base.Transform.position, this.CurrentPosition + this.m_moveOffset.localPosition + this.m_rotaOffset.localPosition, this.LerpSpeed * Time.deltaTime);
			base.Transform.rotation = Quaternion.Lerp(base.Transform.rotation, Quaternion.Euler(this.CurrentRotation.x + this.m_rotaOffset.localEulerAngles.x, this.CurrentRotation.y + this.m_rotaOffset.localEulerAngles.y, this.CurrentRotation.z + this.m_rotaOffset.localEulerAngles.z), this.LerpSpeed * 2f * Time.deltaTime);
			return;
		case CameraMovementType.Towars:
			base.Transform.rotation = Quaternion.RotateTowards(base.Transform.rotation, Quaternion.Euler(this.CurrentRotation.x + this.m_rotaOffset.localEulerAngles.x, this.CurrentRotation.y + this.m_rotaOffset.localEulerAngles.y, this.CurrentRotation.z + this.m_rotaOffset.localEulerAngles.z), this.LerpSpeed);
			base.Transform.position = Vector3.MoveTowards(base.Transform.position, this.CurrentPosition + this.m_moveOffset.localPosition + this.m_rotaOffset.localPosition, this.LerpSpeed);
			return;
		default:
			return;
		}
	}

	public void DoShake()
	{
		Sequence expr_05 = DOTween.Sequence();
		expr_05.Append(this.m_moveOffset.DOLocalMoveZ(1f, 0.8f, false));
		expr_05.Append(this.m_moveOffset.DOLocalMoveZ(0f, 0.8f, false));
	}

	public void DoRotateCenter()
	{
		Sequence expr_05 = DOTween.Sequence();
		expr_05.Insert(0f, this.m_rotaOffset.DOLocalRotateQuaternion(Quaternion.Euler(this.m_initRota + new Vector3(0f, -25f, 0f)), 0.05f)).SetEase(Ease.InSine);
		expr_05.Insert(0f, this.m_moveOffset.DOLocalMove(this.m_initPos + new Vector3(8.5f, 2f, 3f), 0.05f, false)).SetEase(Ease.InSine);
		float arg_B2_1 = 0.2f;
		TweenCallback arg_B2_2;
		if ((arg_B2_2 = bl_CameraOrbit.__c.__9__71_0) == null)
		{
			arg_B2_2 = (bl_CameraOrbit.__c.__9__71_0 = new TweenCallback(bl_CameraOrbit.__c.__9._DoRotateCenter_b__71_0));
		}
		expr_05.InsertCallback(arg_B2_1, arg_B2_2);
		float arg_DD_1 = 0.8f;
		TweenCallback arg_DD_2;
		if ((arg_DD_2 = bl_CameraOrbit.__c.__9__71_1) == null)
		{
			arg_DD_2 = (bl_CameraOrbit.__c.__9__71_1 = new TweenCallback(bl_CameraOrbit.__c.__9._DoRotateCenter_b__71_1));
		}
		expr_05.InsertCallback(arg_DD_1, arg_DD_2);
		expr_05.Insert(1.3f, this.m_rotaOffset.DOLocalRotateQuaternion(Quaternion.Euler(this.m_initRota + new Vector3(0f, 0f, 0f)), 0.05f)).SetEase(Ease.OutSine);
		expr_05.Insert(1.3f, this.m_moveOffset.DOLocalMove(this.m_initPos + new Vector3(0f, 0f, 0f), 0.05f, false)).SetEase(Ease.OutSine);
	}

	public void DoRotateWin()
	{
		Sequence expr_05 = DOTween.Sequence();
		expr_05.Insert(0f, this.m_rotaOffset.DOLocalRotateQuaternion(Quaternion.Euler(this.m_initRota + new Vector3(0f, -15f, 0f)), 0.05f)).SetEase(Ease.InSine);
		expr_05.Insert(0f, this.m_moveOffset.DOLocalMove(this.m_initPos + new Vector3(4f, 0f, 1f), 0.05f, false)).SetEase(Ease.InSine);
	}

	public void DoSet()
	{
		Sequence expr_05 = DOTween.Sequence();
		expr_05.Insert(0f, this.m_rotaOffset.DOLocalRotateQuaternion(Quaternion.Euler(this.m_initRota), 0.05f)).SetEase(Ease.InSine);
		expr_05.Insert(0f, this.m_moveOffset.DOLocalMove(this.m_initPos, 0.05f, false)).SetEase(Ease.InSine);
	}

	public void moveDefault()
	{
		this.LerpSpeed = this.m_moveDefaultSpeed;
	}

	public void ShowInit()
	{
		this.m_moveOffset.localPosition = this.m_initPos;
		this.m_rotaOffset.localRotation = Quaternion.Euler(this.m_initRota.x, this.m_initRota.y, this.m_initRota.z);
		this.LerpSpeed = this.m_moveSpeed;
		base.Invoke("moveDefault", 1f);
	}

	public void ShowMission()
	{
		this.m_moveOffset.localPosition = this.m_missionPos;
		this.LerpSpeed = 5f;
		base.Invoke("moveDefault", 1f);
	}

	public void ShowShopPlayer()
	{
		this.LerpSpeed = 5f;
		this.m_moveOffset.localPosition = this.m_playerPos;
		this.m_rotaOffset.localRotation = Quaternion.Euler(this.m_playerRota.x, this.m_playerRota.y, this.m_playerRota.z);
	}

	public void ShowShopBall()
	{
		this.LerpSpeed = 5f;
		this.m_moveOffset.localPosition = this.m_ballPos;
		this.m_rotaOffset.localRotation = Quaternion.Euler(this.m_ballRota.x, this.m_ballRota.y, this.m_ballRota.z);
	}

	private void ZoomControll(bool autoApply)
	{
		bool flag = false;
		float deltaTime = Time.deltaTime;
		this.distance = Mathf.Clamp(this.distance - base.MouseScrollWheel * this.ScrollSensitivity, this.DistanceClamp.x, this.DistanceClamp.y);
		if (this.DetectCollision)
		{
			Vector3 vector = base.Transform.position - this.Target.position;
			this.Ray = new Ray(this.Target.position, vector.normalized);
			RaycastHit raycastHit;
			if (Physics.SphereCast(this.Ray.origin, this.CollisionRadius, this.Ray.direction, out raycastHit, this.distance, this.DetectCollisionLayers))
			{
				if (!this.haveHit)
				{
					this.LastDistance = this.distance;
					this.haveHit = true;
				}
				this.distance = Mathf.Clamp(raycastHit.distance, this.DistanceClamp.x, this.DistanceClamp.y);
				if (this.TeleporOnHit)
				{
					this.Distance = this.distance;
				}
				flag = true;
			}
			else if (!this.isDetectingHit)
			{
				base.StartCoroutine(this.DetectHit());
			}
			this.distance = ((this.distance < 1f) ? 1f : this.distance);
			if (!this.haveHit || !this.TeleporOnHit)
			{
				float num = flag ? 3.14159274f : 1f;
				this.Distance = Mathf.SmoothStep(this.Distance, this.distance, deltaTime * (this.ZoomSpeed * num));
			}
		}
		else
		{
			this.distance = ((this.distance < 1f) ? 1f : this.distance);
			this.Distance = Mathf.SmoothStep(this.Distance, this.distance, deltaTime * this.ZoomSpeed);
		}
		this.ZoomVector = new Vector3(0f, 0f, -this.Distance);
		if (autoApply)
		{
			this.CurrentPosition = this.CurrentRotation * this.ZoomVector + this.Target.position;
			switch (this.MovementType)
			{
			case CameraMovementType.Normal:
				base.Transform.rotation = this.CurrentRotation;
				base.Transform.position = this.CurrentPosition;
				return;
			case CameraMovementType.Dynamic:
				base.Transform.position = Vector3.Lerp(base.Transform.position, this.CurrentPosition, this.LerpSpeed * deltaTime);
				base.Transform.rotation = Quaternion.Lerp(base.Transform.rotation, this.CurrentRotation, this.LerpSpeed * 2f * deltaTime);
				return;
			case CameraMovementType.Towars:
				base.Transform.rotation = Quaternion.RotateTowards(base.Transform.rotation, this.CurrentRotation, this.LerpSpeed);
				base.Transform.position = Vector3.MoveTowards(base.Transform.position, this.CurrentPosition, this.LerpSpeed);
				break;
			default:
				return;
			}
		}
	}

	private void OnGUI()
	{
		this.Target = null;
	}

	public void SetTarget(Transform newTarget)
	{
		base.StopCoroutine("TranslateTarget");
		base.StartCoroutine("TranslateTarget", newTarget);
	}

	public void SetViewPoint(int side)
	{
		this.AutoRotate = false;
		if (side == 0)
		{
			this.vertical = 90f;
			this.horizontal = 0f;
			return;
		}
		if (side == 1)
		{
			this.vertical = 0f;
			this.horizontal = 0f;
			return;
		}
		if (side == 2)
		{
			this.vertical = 0f;
			this.horizontal = -90f;
			return;
		}
		if (side == 3)
		{
			this.vertical = 0f;
			this.horizontal = 90f;
			return;
		}
		if (side == 4)
		{
			this.vertical = 0f;
			this.horizontal = 180f;
		}
	}

//	[IteratorStateMachine(typeof(bl_CameraOrbit._003CTranslateTarget_003Ed__96))]
	private IEnumerator TranslateTarget(Transform newTarget)
	{
		while (true)
		{
			int num = 0;
			switch (num)
			{
			case 0:
				this.isSwitchingTarget = true;
				goto IL_A6;
			case 1:
				goto IL_A6;
			case 2:
				goto IL_FB;
			}
			break;
			IL_A6:
			if (this.FadeAlpha < 1f)
			{
				base.transform.position = Vector3.Lerp(base.transform.position, base.transform.position + new Vector3(0f, 2f, -2f), Time.deltaTime);
				this.FadeAlpha += Time.smoothDeltaTime * this.SwichtSpeed;
				yield return null;
				continue;
			}
			this.Target = newTarget;
			this.isSwitchingTarget = false;
			IL_FB:
			if (this.FadeAlpha <= 0f)
			{
				goto Block_3;
			}
			this.FadeAlpha -= Time.smoothDeltaTime * this.SwichtSpeed;
			yield return null;
		}
		yield break;
		Block_3:
		yield break;
	}

//	[IteratorStateMachine(typeof(bl_CameraOrbit._003CDetectHit_003Ed__97))]
	private IEnumerator DetectHit()
	{
		int num = 0;
		while (num == 0)
		{
			this.isDetectingHit = true;
			yield return new WaitForSeconds(0.4f);
		}
		if (num != 1)
		{
			yield break;
		}
		if (this.haveHit)
		{
			this.distance = this.LastDistance;
			this.haveHit = false;
		}
		this.isDetectingHit = false;
		yield break;
	}

//[IteratorStateMachine(typeof(bl_CameraOrbit._003CIEDelayFog_003Ed__98))]
	private IEnumerator IEDelayFog()
	{
		int num = 0;
		while (num == 0)
		{
			yield return new WaitForSeconds(this.DelayStartFoV);
		}
		if (num != 1)
		{
			yield break;
		}
		this.canFogControl = true;
		yield break;
	}

	public void SetZoom(float value)
	{
		this.distance += -(value * 0.5f) * this.ScrollSensitivity;
	}

	public void SetStaticZoom(float value)
	{
		this.distance += value;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color32(0, 221, 221, 255);
		if (this.Target != null)
		{
			Gizmos.DrawLine(base.transform.position, this.Target.position);
			Gizmos.matrix = Matrix4x4.TRS(this.Target.position, base.transform.rotation, new Vector3(1f, 0f, 1f));
			Gizmos.DrawWireSphere(this.Target.position, this.Distance);
			Gizmos.matrix = Matrix4x4.identity;
		}
		Gizmos.DrawCube(base.transform.position, new Vector3(1f, 0.2f, 0.2f));
		Gizmos.DrawCube(base.transform.position, Vector3.one / 2f);
	}
}
