using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
	private sealed class __c__DisplayClass38_0
	{
		public GameObject trap;

		internal void _DoJump_b__0()
		{
			this.trap.transform.SetParent(null);
			UnityEngine.Object.Destroy(this.trap);
		}
	}

	public Animator m_Ani;

	public Rigidbody m_body;

	public bool m_isJump;

	public bool m_isCanMove = true;

	public bool m_isCanMoveBack = true;

	public PlayerHandView m_handObj;

	private Vector3 m_handInitPos;

	private bool m_isDoPlay;

	private float CrossfadeVal = 0.05f;

	public Transform m_fg;

	public Transform m_bodyBg;

	public int m_handNum;

	public bool m_isCanHand;

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

	private PhysicMaterial m_skin;

	private static float m_minVal = 0.05f;

	private static float m_maxVal = 0.2f;

	private void Start()
	{
		this.m_handInitPos = this.m_handObj.transform.localPosition;
		this.m_skin = base.GetComponent<CapsuleCollider>().material;
		this.m_fg.gameObject.SetActive(false);
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
		return PlayerView.m_readyAniStrs[0];
	}

	private string getSipJumpStr()
	{
		return PlayerView.m_sipJumpAniStrs[0];
	}

	private string getAtkJumpStr()
	{
		return PlayerView.m_atkJumpAniStrs[0];
	}

	private string getWalkStr()
	{
		return PlayerView.m_walkAniStrs[0];
	}

	private string getWalkBackStr()
	{
		return PlayerView.m_walkBackAniStrs[0];
	}

	private string getRunStr()
	{
		return PlayerView.m_runAniStrs[0];
	}

	private string getRunBackStr()
	{
		return PlayerView.m_runBackAniStrs[0];
	}

	private string getWinStr()
	{
		return PlayerView.m_winAniStrs[0];
	}

	private string getFaileStr()
	{
		return PlayerView.m_faileAniStrs[0];
	}

	public void DoInit()
	{
		this.m_isCanMove = true;
		this.m_isCanMoveBack = true;
		this.m_isDoPlay = false;
		this.m_handNum = 0;
		this.m_isCanHand = false;
		this.m_Ani.CrossFade(this.getReadyStr(), this.CrossfadeVal);
		this.m_fg.gameObject.SetActive(true);
		base.Invoke("EndInit", 3f);
		this.m_bodyBg.localRotation = Quaternion.Euler(0f, -90f, 0f);
	}

	private void EndInit()
	{
		this.m_fg.gameObject.SetActive(false);
	}

	public void DoDown()
	{
		if (!this.m_isCanMove)
		{
			return;
		}
		if (this.m_body.velocity.y <= 0f)
		{
			this.m_Ani.CrossFade(this.getWalkStr(), this.CrossfadeVal);
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
		this.m_isDoPlay = true;
		this.m_isJump = true;
		if (MainMenuView.m_this.m_MainGameView.m_ballView.transform.position.x < 0.5f && MainMenuView.m_this.m_MainGameView.m_ballView.transform.position.x < base.transform.position.x - 1.5f)
		{
			base.Invoke("DoHand", 0f);
			this.m_body.AddRelativeForce(new Vector3(0f, 40f, 0f), ForceMode.VelocityChange);
			this.m_Ani.CrossFade(this.getAtkJumpStr(), this.CrossfadeVal);
		}
		else
		{
			this.m_body.AddRelativeForce(new Vector3(0f, 40f, 0f), ForceMode.VelocityChange);
			this.m_Ani.CrossFade(this.getSipJumpStr(), this.CrossfadeVal);
		}
		GameObject trap = UnityEngine.Object.Instantiate<GameObject>(ResourcesLoad.Load<GameObject>("Prefab/effect/effect_jump_ground"));
		trap.transform.SetParent(MainMenuView.m_this.m_MainGameView.m_effBg);
		trap.transform.position = base.transform.position - new Vector3(0f, 0.2f, 0.8f);
		trap.transform.localScale = new Vector3(1f, 1f, 1f);
		Sequence expr_19B = DOTween.Sequence();
		expr_19B.AppendInterval(1.5f);
		expr_19B.AppendCallback(delegate
		{
			trap.transform.SetParent(null);
			UnityEngine.Object.Destroy(trap);
		});
		MainMenuView.m_this.m_MainGameView.m_bl_CameraOrbit.DoShake();
	}

	public void doWin()
	{
		this.m_isCanMove = false;
		this.m_Ani.CrossFade(this.getWinStr(), this.CrossfadeVal);
		this.m_bodyBg.localRotation = Quaternion.Euler(0f, -180f, 0f);
	}

	public void doFail()
	{
		this.m_isCanMove = false;
		this.m_Ani.CrossFade(this.getFaileStr(), this.CrossfadeVal);
		this.m_bodyBg.localRotation = Quaternion.Euler(0f, -180f, 0f);
	}

	private void DoHand()
	{
		if (!MainMenuView.m_this.m_MainGameView.canPlay())
		{
			return;
		}
		Sequence expr_17 = DOTween.Sequence();
		expr_17.AppendCallback(delegate
		{
			this.m_isCanHand = true;
			this.m_handObj.gameObject.SetActive(true);
			this.m_handObj.m_canHand = true;
			this.m_handObj.m_doHand = false;
			if (this.m_body.velocity.y <= 0f)
			{
				this.m_body.velocity = Vector3.zero;
			}
		});
		expr_17.AppendInterval(0.35f);
		expr_17.AppendCallback(delegate
		{
			this.m_body.velocity = Vector3.zero;
			this.m_handObj.handBall();
			this.m_isCanHand = false;
			this.m_handObj.m_canHand = false;
		});
		expr_17.AppendInterval(0.05f);
		expr_17.AppendCallback(delegate
		{
			this.m_Ani.speed = 1f;
			this.m_handObj.gameObject.SetActive(false);
			this.m_handObj.transform.localPosition = this.m_handInitPos;
			this.m_handObj.transform.localEulerAngles = Vector3.zero;
			this.m_handObj.m_doHand = false;
		});
	}

	public void speedHand()
	{
		this.m_Ani.speed = 1f;
		this.m_body.velocity = Vector3.zero;
		this.m_body.AddRelativeForce(new Vector3(0f, 15f, 0f), ForceMode.VelocityChange);
	}

	private void FixedUpdate()
	{
		if (!MainMenuView.m_this.m_MainGameView.canPlay())
		{
			return;
		}
		float x = MainMenuView.m_this.m_MainGameView.m_ballView.m_body.transform.position.x;
		float num = base.transform.position.x - x;
		if (this.m_isDoPlay)
		{
			num -= 0.5f;
		}
		if (Mathf.Abs(num) < PlayerView.m_minVal)
		{
			return;
		}
		if (!this.m_isCanMove && num > 0f)
		{
			return;
		}
		if (!this.m_isCanMoveBack && num < 0f)
		{
			return;
		}
		num = num * num * num * num * num;
		if (Mathf.Abs(num) > PlayerView.m_maxVal)
		{
			if (this.m_isJump)
			{
				if (num < 0f)
				{
					num = -PlayerView.m_maxVal * 0.5f;
				}
				else
				{
					num = PlayerView.m_maxVal * 0.5f;
				}
			}
			else if (num < 0f)
			{
				num = -PlayerView.m_maxVal;
			}
			else
			{
				num = PlayerView.m_maxVal;
			}
		}
		this.m_body.AddRelativeForce(new Vector3(-num * 7f, 0f, 0f), ForceMode.VelocityChange);
		float x2 = this.m_body.velocity.x;
		if (!this.m_isJump)
		{
			if (Mathf.Abs(x2) < 3f)
			{
				if (x2 > 0f)
				{
					this.m_Ani.CrossFade(this.getWalkBackStr(), this.CrossfadeVal);
					return;
				}
				this.m_Ani.CrossFade(this.getWalkStr(), this.CrossfadeVal);
				return;
			}
			else if (Mathf.Abs(x2) < 10f)
			{
				if (x2 > 0f)
				{
					this.m_Ani.CrossFade(this.getRunBackStr(), this.CrossfadeVal);
					return;
				}
				this.m_Ani.CrossFade(this.getRunStr(), this.CrossfadeVal);
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
