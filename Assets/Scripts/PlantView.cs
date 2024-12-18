using System;
using UnityEngine;

public class PlantView : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnCollisionEnter(Collision collision)
	{
		string tag = collision.collider.tag;
		if (tag.Equals("Player"))
		{
			PlayerView component = collision.collider.GetComponent<PlayerView>();
			if (component != null)
			{
				component.m_isJump = false;
				component.DoDown();
				return;
			}
		}
		else if (tag.Equals("Ai"))
		{
			AiView component2 = collision.collider.GetComponent<AiView>();
			if (component2 != null)
			{
				component2.m_isJump = false;
				return;
			}
		}
		else if (tag.Equals("ball"))
		{
			collision.collider.tag = "trap";
			MainMenuView.m_this.m_MainGameView.downBall(collision.transform);
		}
	}
}
