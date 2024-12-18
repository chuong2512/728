using System;
using UnityEngine;

public class WallFwView : MonoBehaviour
{
	public bool m_isBack;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider collider)
	{
		string tag = collider.tag;
		if (tag.Equals("Player"))
		{
			PlayerView component = collider.GetComponent<PlayerView>();
			if (component != null)
			{
				if (this.m_isBack)
				{
					component.m_isCanMoveBack = false;
					component.m_body.velocity = new Vector3(0f, component.m_body.velocity.y, component.m_body.velocity.z);
					return;
				}
				component.m_isCanMove = false;
				component.m_body.velocity = new Vector3(0f, component.m_body.velocity.y, component.m_body.velocity.z);
				return;
			}
		}
		else if (tag.Equals("Ai"))
		{
			AiView component2 = collider.GetComponent<AiView>();
			if (component2 != null)
			{
				if (this.m_isBack)
				{
					component2.m_isCanMoveBack = false;
					component2.m_body.velocity = new Vector3(0f, component2.m_body.velocity.y, component2.m_body.velocity.z);
					return;
				}
				component2.m_isCanMove = false;
				component2.m_body.velocity = new Vector3(0f, component2.m_body.velocity.y, component2.m_body.velocity.z);
			}
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		string tag = collider.tag;
		if (tag.Equals("Player"))
		{
			PlayerView component = collider.GetComponent<PlayerView>();
			if (component != null)
			{
				if (this.m_isBack)
				{
					component.m_isCanMoveBack = true;
					return;
				}
				component.m_isCanMove = true;
				return;
			}
		}
		else if (tag.Equals("Ai"))
		{
			AiView component2 = collider.GetComponent<AiView>();
			if (component2 != null)
			{
				if (this.m_isBack)
				{
					component2.m_isCanMoveBack = true;
					return;
				}
				component2.m_isCanMove = true;
			}
		}
	}
}
