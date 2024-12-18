using Spine.Unity;
using System;
using UnityEngine;

public class ParticalDepth : MonoBehaviour
{
	public int order;

	private void OnEnable()
	{
	}

	private void Update()
	{
		Transform[] componentsInChildren = base.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			if (transform.GetComponent<ParticleSystem>() != null || transform.GetComponent<SkeletonAnimation>() != null)
			{
				Renderer[] componentsInChildren2 = transform.GetComponentsInChildren<Renderer>();
				int num = componentsInChildren2.Length;
				for (int j = 0; j < num; j++)
				{
					Renderer renderer = componentsInChildren2[j];
					if (this.order >= 0)
					{
						if (renderer.sortingOrder < this.order)
						{
							renderer.sortingOrder += this.order;
						}
					}
					else
					{
						renderer.sortingOrder = this.order;
					}
				}
			}
		}
	}
}
