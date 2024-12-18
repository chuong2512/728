using DG.Tweening;
using System;
using UnityEngine;

public class PlayerHandView : MonoBehaviour
{
	public Transform m_obj;

	public bool m_canHand = true;

	public bool m_doHand;

	public BallView m_Ball;

	private void Start()
	{
		this.m_Ball = null;
	}

	private void Update()
	{
	}

	public void handBall()
	{
		this.m_doHand = true;
		if (this.m_Ball != null)
		{
			this.m_Ball.transform.DOPause();
			this.m_Ball.m_body.velocity = Vector3.zero;
			this.m_Ball.transform.position = base.transform.position;
			this.m_Ball.m_body.AddExplosionForce(6000f, new Vector3(this.m_Ball.transform.position.x + 0.6f, this.m_Ball.transform.position.y + 0.4f + UnityEngine.Random.value * 0.2f, this.m_Ball.transform.position.z), 10f);
			this.m_Ball.onFire();
			MainMenuView.m_this.showExpress(1);
			MainMenuView.m_this.m_MainGameView.m_PlayerView.speedHand();
			AudioManager.PlayEffectAudio("impact", false, false);
			AudioManager.PlayEffectAudio("huanhu", false, false);
			MainMenuView.m_this.m_MainGameView.m_PlayerView.m_handNum++;
			MainMenuView.m_this.m_MainGameView.m_PlayerView.m_isCanHand = false;
			if (MainMenuView.m_this.m_MainGameView.m_PlayerView.m_handNum >= 2)
			{
				base.Invoke("HandWin", 0.1f);
			}
			ControlsBase<AndroidControl>.Instance.PlayShock(100);
			this.m_Ball = null;
			MainMenuView.m_this.m_CameraShark.doShark();
		}
	}

	private void OnTriggerEnter(Collider collision)
	{
		if (!this.m_canHand && this.m_doHand)
		{
			if (collision.tag.Equals("ball") && collision.GetComponentInChildren<BallView>())
			{
				collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
				this.m_Ball = collision.GetComponentInChildren<BallView>();
				this.handBall();
				this.m_doHand = false;
			}
			return;
		}
		if (this.m_canHand && this.m_Ball == null && collision.tag.Equals("ball"))
		{
			collision.transform.DOMove(base.transform.position + new Vector3(0f, 0f, 0f), 0.15f, false);
			collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
			if (collision.GetComponentInChildren<BallView>())
			{
				this.m_Ball = collision.GetComponentInChildren<BallView>();
			}
		}
	}

	private void OnTriggerExit(Collider collision)
	{
		if (collision.tag.Equals("ball"))
		{
			this.m_canHand = false;
			if (collision.GetComponentInChildren<BallView>())
			{
				this.m_Ball = null;
			}
		}
	}

	private void HandWin()
	{
		GameObject expr_0F = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>("Prefab/effect/effect_slowmotionblast"));
		expr_0F.transform.SetParent(MainMenuView.m_this.m_MainGameView.m_effBg);
		expr_0F.transform.localScale = new Vector3(1f, 1f, 1f);
		expr_0F.transform.position = MainMenuView.m_this.m_MainGameView.m_PlayerView.m_body.transform.position;
		MainMenuView.m_this.m_MainGameView.m_bl_CameraOrbit.DoRotateCenter();
		MainMenuView.m_this.m_MainGameView.smashWin();
	}
}
