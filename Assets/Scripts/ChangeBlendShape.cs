using System;
using UnityEngine;

public class ChangeBlendShape : MonoBehaviour
{
	private int blendShapeCount;

	private SkinnedMeshRenderer skinnedMeshRenderer;

	private Mesh skinnedMesh;

	private float blendCounter;

	public float blendSpeed = 10f;

	private bool blendFinished = true;

	private int CurrentBlend;

	private float PreviosBlendCounter;

	private void Awake()
	{
		this.skinnedMeshRenderer = base.GetComponent<SkinnedMeshRenderer>();
		this.skinnedMesh = base.GetComponent<SkinnedMeshRenderer>().sharedMesh;
	}

	private void Start()
	{
		this.blendShapeCount = this.skinnedMesh.blendShapeCount;
	}

	public void ChangeBlend()
	{
		this.blendCounter = 0f;
		this.blendFinished = false;
		this.PreviosBlendCounter = this.skinnedMeshRenderer.GetBlendShapeWeight(this.CurrentBlend);
		this.skinnedMeshRenderer.SetBlendShapeWeight(this.CurrentBlend, 0f);
		if (this.CurrentBlend < this.blendShapeCount)
		{
			this.CurrentBlend++;
			return;
		}
		this.CurrentBlend = 0;
	}

	private void Update()
	{
		if (!this.blendFinished)
		{
			if (this.PreviosBlendCounter > 0f)
			{
				this.PreviosBlendCounter -= this.blendSpeed;
				this.skinnedMeshRenderer.SetBlendShapeWeight(this.CurrentBlend - 1, this.PreviosBlendCounter);
			}
			if (this.blendCounter < 100f)
			{
				this.skinnedMeshRenderer.SetBlendShapeWeight(this.CurrentBlend, this.blendCounter);
				this.blendCounter += this.blendSpeed;
				return;
			}
			this.blendFinished = true;
		}
	}
}
