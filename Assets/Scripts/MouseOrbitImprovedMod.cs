using System;
using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImprovedMod : MonoBehaviour
{
	public Transform target;

	public float distance = 5f;

	public float xSpeed = 120f;

	public float ySpeed = 120f;

	public float Yoffset;

	public float yMinLimit = -20f;

	public float yMaxLimit = 80f;

	public float distanceMin = 0.5f;

	public float distanceMax = 15f;

	private Rigidbody rigidbody;

	private float x;

	private float y;

	private bool Rotate;

	private Vector3 ModPosition;

	private void Start()
	{
		Vector3 eulerAngles = base.transform.eulerAngles;
		this.x = eulerAngles.y;
		this.y = eulerAngles.x;
		this.rigidbody = base.GetComponent<Rigidbody>();
		if (this.rigidbody != null)
		{
			this.rigidbody.freezeRotation = true;
		}
	}

	private void LateUpdate()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.Rotate = true;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			this.Rotate = false;
		}
		if (this.target)
		{
			if (this.Rotate)
			{
				this.x += UnityEngine.Input.GetAxis("Mouse X") * this.xSpeed * this.distance * 0.02f;
				this.y -= UnityEngine.Input.GetAxis("Mouse Y") * this.ySpeed * 0.02f;
				this.y = MouseOrbitImprovedMod.ClampAngle(this.y, this.yMinLimit, this.yMaxLimit);
			}
			Quaternion rotation = Quaternion.Euler(this.y, this.x, 0f);
			this.distance = Mathf.Clamp(this.distance - UnityEngine.Input.GetAxis("Mouse ScrollWheel") * 5f, this.distanceMin, this.distanceMax);
			RaycastHit raycastHit;
			if (Physics.Linecast(this.target.position, base.transform.position, out raycastHit))
			{
				this.distance -= raycastHit.distance;
			}
			Vector3 point = new Vector3(0f, 0f, -this.distance);
			this.ModPosition = this.target.position;
			this.ModPosition.y = this.ModPosition.y + this.Yoffset;
			Vector3 position = rotation * point + this.ModPosition;
			base.transform.rotation = rotation;
			base.transform.position = position;
		}
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}
}
