using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Util;

public class AiView : MonoBehaviour
{
	private sealed class __c__DisplayClass35_0
	{
		public GameObject trap;

		internal void _DoJump_b__0()
		{
			this.trap.transform.SetParent(null);
			UnityEngine.Object.Destroy(this.trap);
		}
	}

	public Animator m_ani;

	public Rigidbody m_body;

	public Transform m_bodyBg;

	public bool m_isJump;

	public bool m_isCanMove = true;

	public bool m_isCanMoveBack = true;

	public AiHandView m_handObj;

	private Vector3 m_handInitPos;

	private float CrossfadeVal = 0.05f;

	private static string[] m_readyAniStrs = new string[]
	{
		"Idle"
	};

	private static string[] m_sipJumpAniStrs = new string[]
	{
		"SJump"
	};

	private static string[] m_atkJumpAniStrs = new string[]
	{
		"AJump"
	};

	private static string[] m_walkAniStrs = new string[]
	{
		"Idle"
	};

	private static string[] m_walkBackAniStrs = new string[]
	{
		"Idle"
	};

	private static string[] m_runAniStrs = new string[]
	{
		"Idle"
	};

	private static string[] m_runBackAniStrs = new string[]
	{
		"Idle"
	};

	private static string[] m_winAniStrs = new string[]
	{
		"Win"
	};

	private static string[] m_faileAniStrs = new string[]
	{
		"Faile"
	};

	public int m_smashNums;

	private PhysicMaterial m_skin;

	private static float m_minVal = 0.02f;

	private static float m_maxVal = 0.2f;

	private void Start()
	{
		this.m_handInitPos = this.m_handObj.transform.localPosition;
		this.m_skin = base.GetComponent<CapsuleCollider>().material;
	}

	public void startFire()
	{
		this.m_skin.bounciness = 0f;
	}

	public void endFire()
	{
		this.m_skin.bounciness = 0f;
	}

	private string getReadyStr()
	{
		return AiView.m_readyAniStrs[0];
	}

	private string getSipJumpStr()
	{
		return AiView.m_sipJumpAniStrs[0];
	}

	private string getAtkJumpStr()
	{
		return AiView.m_atkJumpAniStrs[0];
	}

	private string getWalkStr()
	{
		return AiView.m_walkAniStrs[0];
	}

	private string getWalkBackStr()
	{
		return AiView.m_walkBackAniStrs[0];
	}

	private string getRunStr()
	{
		return AiView.m_runAniStrs[0];
	}

	private string getRunBackStr()
	{
		return AiView.m_runBackAniStrs[0];
	}

	private string getWinStr()
	{
		return AiView.m_winAniStrs[0];
	}

	private string getFaileStr()
	{
		return AiView.m_faileAniStrs[0];
	}

	private void Update()
	{
	}

	public void DoInit()
	{
		this.m_isCanMove = true;
		this.m_isCanMoveBack = true;
		this.m_smashNums = 0;
		this.m_ani.CrossFade(this.getReadyStr(), this.CrossfadeVal);
		float repeatRate = Singleton<GameManager>.Instance.getLoopTimes() * AppConst.m_loopMax;
		base.InvokeRepeating("loopAi", 1f, repeatRate);
		this.m_bodyBg.localRotation = Quaternion.Euler(0f, 90f, 0f);
	}

	private void loopAi()
	{
		float num = MainMenuView.m_this.m_MainGameView.m_ballView.m_body.transform.position.x - this.m_body.transform.position.x;
		float y = MainMenuView.m_this.m_MainGameView.m_ballView.transform.position.y;
		if (Mathf.Abs(num) <= 2f && y <= 6.5f)
		{
			if (num > 0f)
			{
				int arg_117_0 = (int)(UnityEngine.Random.value * 10f);
				int num2 = 5;
				if (Singleton<GameManager>.Instance.m_UserInfo.m_missInd <= 5)
				{
					num2 = 5;
				}
				else if (Singleton<GameManager>.Instance.m_UserInfo.m_missInd <= 10)
				{
					num2 = 6;
				}
				else if (Singleton<GameManager>.Instance.m_UserInfo.m_missInd <= 15)
				{
					num2 = 7;
				}
				else if (Singleton<GameManager>.Instance.m_UserInfo.m_missInd <= 20)
				{
					num2 = 8;
				}
				else if (Singleton<GameManager>.Instance.m_UserInfo.m_missInd <= 25)
				{
					num2 = 9;
				}
				else if (Singleton<GameManager>.Instance.m_UserInfo.m_missInd >= 30)
				{
					num2 = 11;
				}
				if (arg_117_0 < num2)
				{
					this.DoJump();
					return;
				}
			}
			else
			{
				this.DoJump();
			}
		}
	}

	public void DoJump()
	{
		if (!MainMenuView.m_this.m_MainGameView.canPlay())
		{
			return;
		}
		if (this.m_isJump)
		{
			return;
		}
		this.m_ani.CrossFade(this.getSipJumpStr(), this.CrossfadeVal);
		this.m_isJump = true;
		this.m_body.AddRelativeForce(new Vector3(0f, 35f, 0f), ForceMode.VelocityChange);
		GameObject trap = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>("Prefab/effect/effect_jump_ground"));
		trap.transform.SetParent(MainMenuView.m_this.m_MainGameView.m_effBg);
		trap.transform.position = base.transform.position - new Vector3(0f, 0.2f, 0.8f);
		trap.transform.localScale = new Vector3(1f, 1f, 1f);
		Sequence expr_F0 = DOTween.Sequence();
		expr_F0.AppendInterval(1.5f);
		expr_F0.AppendCallback(delegate
		{
			trap.transform.SetParent(null);
			UnityEngine.Object.Destroy(trap);
		});
	}

	private void DoHand()
	{
		if (!MainMenuView.m_this.m_MainGameView.canPlay())
		{
			return;
		}
		if (MainMenuView.m_this.m_MainGameView.m_ballView.transform.position.x < -0.5f)
		{
			return;
		}
		int num = (int)Singleton<GameManager>.Instance.getSkillTimes();
		if ((int)(UnityEngine.Random.value * 100f) > num)
		{
			return;
		}
		Sequence expr_59 = DOTween.Sequence();
		expr_59.AppendCallback(delegate
		{
			this.m_handObj.gameObject.SetActive(true);
			this.m_handObj.m_canHand = true;
			this.m_body.velocity = Vector3.zero;
			this.m_body.AddRelativeForce(new Vector3(0f, 22f, 0f), ForceMode.VelocityChange);
		});
		expr_59.AppendInterval(0.45f);
		expr_59.AppendCallback(delegate
		{
			this.m_ani.speed = 1f;
			this.m_handObj.gameObject.SetActive(false);
			this.m_handObj.transform.localPosition = this.m_handInitPos;
			this.m_handObj.transform.localEulerAngles = Vector3.zero;
		});
	}

	public void speedHand()
	{
		this.m_ani.speed = 2f;
	}

	public void doWin()
	{
		this.m_ani.CrossFade(this.getWinStr(), this.CrossfadeVal);
		this.m_bodyBg.localRotation = Quaternion.Euler(0f, 180f, 0f);
	}

	public void doFail()
	{
		this.m_ani.CrossFade(this.getFaileStr(), this.CrossfadeVal);
		this.m_bodyBg.localRotation = Quaternion.Euler(0f, 180f, 0f);
	}

	private void FixedUpdate()
	{
		if (!MainMenuView.m_this.m_MainGameView.canPlay())
		{
			return;
		}
		float x = MainMenuView.m_this.m_MainGameView.m_ballView.m_body.transform.position.x;
		float num = base.transform.position.x - x + 0.5f;
		if (Mathf.Abs(num) < AiView.m_minVal)
		{
			return;
		}
		if (!this.m_isCanMove && num < 0f)
		{
			return;
		}
		if (!this.m_isCanMoveBack && num > 0f)
		{
			return;
		}
		num = num * num * num * num * num;
		if (Mathf.Abs(num) > AiView.m_maxVal)
		{
			if (this.m_isJump)
			{
				if (num < 0f)
				{
					num = -AiView.m_maxVal * 0.3f;
				}
				else
				{
					num = AiView.m_maxVal * 0.3f;
				}
			}
			else if (num < 0f)
			{
				num = -AiView.m_maxVal;
			}
			else
			{
				num = AiView.m_maxVal;
			}
		}
		float speedTimes = Singleton<GameManager>.Instance.getSpeedTimes();
		this.m_body.AddRelativeForce(new Vector3(-num * speedTimes, 0f, 0f), ForceMode.VelocityChange);
		float x2 = this.m_body.velocity.x;
		if (!this.m_isJump)
		{
			if (Mathf.Abs(x2) < 3f)
			{
				if (x2 > 0f)
				{
					this.m_ani.CrossFade(this.getWalkBackStr(), this.CrossfadeVal);
					return;
				}
				this.m_ani.CrossFade(this.getWalkStr(), this.CrossfadeVal);
				return;
			}
			else if (Mathf.Abs(x2) < 10f)
			{
				if (x2 > 0f)
				{
					this.m_ani.CrossFade(this.getRunBackStr(), this.CrossfadeVal);
					return;
				}
				this.m_ani.CrossFade(this.getRunStr(), this.CrossfadeVal);
				return;
			}
			else
			{
				this.m_ani.CrossFade("Idle", this.CrossfadeVal);
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag.Equals("ball"))
		{
			BallView component = collision.gameObject.GetComponent<BallView>();
			if (component != null && component.m_norBall.gameObject.activeSelf)
			{
				collision.gameObject.GetComponent<Rigidbody>().velocity = collision.gameObject.GetComponent<Rigidbody>().velocity * 0.5f;
				return;
			}
			collision.gameObject.GetComponent<Rigidbody>().velocity = collision.gameObject.GetComponent<Rigidbody>().velocity * 1.5f;
		}
	}
}
