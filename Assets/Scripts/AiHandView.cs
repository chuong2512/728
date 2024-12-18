using System;
using UnityEngine;

public class AiHandView : MonoBehaviour
{
	public Transform m_obj;

	public bool m_canHand = true;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider collision)
	{
		if (!this.m_canHand)
		{
			return;
		}
		if (collision.tag.Equals("ball"))
		{
			this.m_canHand = false;
			Collider[] array = Physics.OverlapSphere(base.transform.position, 1.5f);
			for (int i = 0; i < array.Length; i++)
			{
				Collider collider = array[i];
				Rigidbody component = collider.GetComponent<Rigidbody>();
				if (component != null && collider.tag.Equals("ball"))
				{
					component.velocity = Vector3.zero;
					collision.transform.position = base.transform.position;
					component.AddExplosionForce(6000f, new Vector3(collision.transform.position.x - 1.5f, collision.transform.position.y + 1.5f, collision.transform.position.z), 10f);
					if (component.GetComponent<BallView>())
					{
						component.GetComponent<BallView>().onFire();
						MainMenuView.m_this.showExpress(2);
						MainMenuView.m_this.m_MainGameView.m_aiView.speedHand();
						AudioManager.PlayEffectAudio("impact", false, false);
						AudioManager.PlayEffectAudio("huanhu", false, false);
					}
					ControlsBase<AndroidControl>.Instance.PlayShock(100);
				}
			}
		}
	}
}
