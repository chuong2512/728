using System;
using UnityEngine;

public class UpdatePartical : MonoBehaviour
{
	public bool m_isRelush;

	private void Start()
	{
	}

	private void Update()
	{
		Transform[] componentsInChildren = base.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			if (transform.GetComponent<ParticleSystem>() != null)
			{
				transform.localPosition = new Vector3(0f, 0f, -200f);
				transform.localScale = new Vector3(5f, 5f, 5f);
			}
		}
	}
}
