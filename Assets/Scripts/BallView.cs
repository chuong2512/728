using DG.Tweening;
using System;
using UnityEngine;

public class BallView : MonoBehaviour
{
	public Rigidbody m_body;

	public Transform m_norBall;

	public Transform m_fireBall;

	private bool m_isStart;

	private PhysicMaterial m_skin;

	private bool m_isEf;

	private int m_minSpeedTimes;

	private void Start()
	{
		this.m_skin = base.GetComponent<SphereCollider>().material;
		this.m_body.angularVelocity = new Vector3(0f, -0.5f, 0.8f);
	}

	public void starPlay()
	{
		this.m_isStart = true;
		this.m_isEf = false;
		this.m_norBall.gameObject.SetActive(true);
		this.m_fireBall.gameObject.SetActive(false);
		base.InvokeRepeating("loopTimes", 0.1f, 0.2f);
	}

	public void onFire()
	{
		this.m_norBall.gameObject.SetActive(false);
		this.m_fireBall.gameObject.SetActive(true);
		this.m_isEf = true;
		base.Invoke("setEf", 0.3f);
		base.Invoke("endFire", 2f);
		AudioManager.PlayEffectAudio("fireball", false, false);
		MainMenuView.m_this.changeView();
		MainMenuView.m_this.m_MainGameView.m_aiView.startFire();
		MainMenuView.m_this.m_MainGameView.m_PlayerView.startFire();
		this.m_skin.bounciness = 1f;
	}

	private void loopTimes()
	{
		if (this.m_isStart)
		{
			if (this.m_body.velocity.sqrMagnitude < 20f)
			{
				this.m_minSpeedTimes++;
			}
			else
			{
				this.m_minSpeedTimes = 0;
			}
			if (this.m_minSpeedTimes >= 10)
			{
				MainMenuView.m_this.m_MainGameView.downBall(base.transform);
				base.CancelInvoke("loopTimes");
			}
		}
	}

	private void setEf()
	{
		this.m_isEf = false;
	}

	private void endFire()
	{
		AudioManager.StopAudio("fireball");
		this.m_isEf = false;
		this.m_norBall.gameObject.SetActive(true);
		this.m_fireBall.gameObject.SetActive(false);
		MainMenuView.m_this.m_MainGameView.m_aiView.endFire();
		MainMenuView.m_this.m_MainGameView.m_PlayerView.endFire();
		this.m_skin.bounciness = 0f;
	}

	private void FixedUpdate()
	{
		if (!this.m_isStart)
		{
			return;
		}
		this.m_body.AddForce(new Vector3(0f, -0.17f, 0f), ForceMode.VelocityChange);
		if (this.m_body.velocity.sqrMagnitude > 1000f)
		{
			float d = 1000f / this.m_body.velocity.sqrMagnitude;
			this.m_body.velocity = this.m_body.velocity * d;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		string tag = collision.collider.tag;
		if (tag.Equals("plattop"))
		{
			GameObject expr_2B = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>("Prefab/effect/HitEff"));
			expr_2B.transform.SetParent(MainMenuView.m_this.m_MainGameView.m_effBg);
			expr_2B.transform.position = base.transform.position + new Vector3(0f, 0.6f, 0f);
			expr_2B.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
			expr_2B.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
			expr_2B.transform.DOScale(0.9f, 0.5f);
			expr_2B.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
			return;
		}
		if (tag.Equals("platleft"))
		{
			GameObject expr_FD = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>("Prefab/effect/HitEff"));
			expr_FD.transform.SetParent(MainMenuView.m_this.m_MainGameView.m_effBg);
			expr_FD.transform.position = base.transform.position + new Vector3(-0.65f, 0f, 0f);
			expr_FD.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			expr_FD.transform.localEulerAngles = new Vector3(0f, 92f, 0f);
			expr_FD.transform.DOScale(0.7f, 0.5f);
			expr_FD.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
			return;
		}
		if (tag.Equals("platright"))
		{
			GameObject expr_1CF = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>("Prefab/effect/HitEff"));
			expr_1CF.transform.SetParent(MainMenuView.m_this.m_MainGameView.m_effBg);
			expr_1CF.transform.position = base.transform.position + new Vector3(0.65f, 0f, 0f);
			expr_1CF.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			expr_1CF.transform.localEulerAngles = new Vector3(0f, 88f, 0f);
			expr_1CF.transform.DOScale(0.7f, 0.5f);
			expr_1CF.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
			return;
		}
		if (tag.Equals("Ai") && this.m_fireBall.gameObject.activeSelf && this.m_isEf)
		{
			this.m_isEf = false;
			if (this.m_body.velocity.sqrMagnitude > 100f)
			{
				MainMenuView.m_this.m_MainGameView.showSuperView();
				GameObject expr_2F4 = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>("Prefab/effect/effect_groundblast"));
				expr_2F4.transform.SetParent(MainMenuView.m_this.m_MainGameView.m_effBg);
				expr_2F4.transform.localScale = new Vector3(1f, 1f, 1f);
				expr_2F4.transform.position = this.m_body.transform.position;
				expr_2F4.GetComponent<ParticleSystem>().collision.SetPlane(0, MainMenuView.m_this.m_MainGameView.m_planeWall);
			}
		}
	}

	private void Update()
	{
	}
}
