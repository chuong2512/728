using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AiTopHandView : MonoBehaviour
{
	private sealed class __c__DisplayClass5_0
	{
		public GameObject trap;

		internal void _OnTriggerEnter_b__0()
		{
			this.trap.transform.SetParent(null);
			UnityEngine.Object.Destroy(this.trap);
		}
	}

	public AiView m_AiView;

	private bool m_canPlay = true;

	private void Start()
	{
		this.m_canPlay = true;
	}

	private void Update()
	{
	}

	private void reSetPlay()
	{
		this.m_canPlay = true;
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.tag.Equals("ball"))
		{
			if (!this.m_AiView.m_isJump)
			{
				return;
			}
			if (!this.m_canPlay)
			{
				return;
			}
			this.m_canPlay = false;
			base.Invoke("reSetPlay", 0.5f);
			GameObject trap = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>("Prefab/effect/effect_ball"));
			trap.transform.SetParent(MainMenuView.m_this.m_MainGameView.m_effBg);
			trap.transform.position = collider.gameObject.transform.position;
			trap.transform.localScale = new Vector3(1f, 1f, 1f);
			trap.transform.localEulerAngles = new Vector3(-43.959f, 90f, -90f);
			Sequence expr_EA = DOTween.Sequence();
			expr_EA.AppendInterval(1.5f);
			expr_EA.AppendCallback(delegate
			{
				trap.transform.SetParent(null);
				UnityEngine.Object.Destroy(trap);
			});
			Rigidbody component = collider.gameObject.GetComponent<Rigidbody>();
			if (component != null && collider.gameObject.tag.Equals("ball"))
			{
				float num = 5f - this.m_AiView.transform.localPosition.y;
				if (num <= 0f)
				{
					num = 0f;
				}
				float num2 = num / 1.5f;
				if (num2 > 1f)
				{
					num2 = 1f;
				}
				float num3 = UnityEngine.Random.value * 15f + 5f;
				float num4 = UnityEngine.Random.value * 8f + 5f;
				float num5 = UnityEngine.Random.value * 10f + 20f;
				component.AddForce(new Vector3(num3 * num2, num4 * num2, 0f), ForceMode.VelocityChange);
				component.AddTorque(new Vector3(num3 * 2f, num4 * 2f, num5 * 2f), ForceMode.VelocityChange);
				ControlsBase<AndroidControl>.Instance.PlayShock(30);
				AudioManager.PlayEffectAudio("ball_touch", false, false);
			}
		}
	}
}
