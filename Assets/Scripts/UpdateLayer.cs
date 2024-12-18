using System;
using UnityEngine;

public class UpdateLayer : MonoBehaviour
{
	public Camera m_mainCamera;

	public string m_layer = "Default";

	private void Start()
	{
	}

	private void Update()
	{
		Transform[] componentsInChildren = base.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].gameObject.layer = LayerMask.NameToLayer(this.m_layer);
		}
	}
}
