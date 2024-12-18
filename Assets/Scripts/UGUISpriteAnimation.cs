using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UGUISpriteAnimation : MonoBehaviour
{
	public delegate void OnOneShowAnimationFinish();

	private Image ImageSource;

	private int mCurFrame;

	private float mDelta;

	public float fPS = 30f;

	private float FPSTimes = 0.0333333351f;

	public List<Sprite> SpriteFrames;

	public bool IsPlaying;

	public bool Foward = true;

	public bool AutoPlay;

	public bool Loop;

	public bool IsSetNitiveSize;

	private int m_showInd;

	public UGUISpriteAnimation.OnOneShowAnimationFinish OnoneShowAnimationFinish;

	public int m_isFlush;

	public int FrameCount
	{
		get
		{
			return this.SpriteFrames.Count;
		}
	}

	public float FPS
	{
		get
		{
			return this.fPS;
		}
		set
		{
			this.fPS = value;
			this.FPSTimes = 1f / this.FPS;
		}
	}

	private void Awake()
	{
		this.ImageSource = base.GetComponent<Image>();
		if (this.AutoPlay)
		{
			this.Play();
		}
		else
		{
			this.IsPlaying = false;
		}
		this.FPSTimes = 1f / this.FPS;
	}

	private void Start()
	{
	}

	private void SetSprite(int idx)
	{
		if (this.m_showInd == idx)
		{
			return;
		}
		this.m_showInd = idx;
		if (this.m_isFlush != 0)
		{
			this.m_isFlush++;
			if (this.m_isFlush == 2)
			{
				this.m_isFlush = 0;
			}
			return;
		}
		this.m_isFlush = 1;
		this.ImageSource.overrideSprite = this.SpriteFrames[idx];
		if (this.IsSetNitiveSize)
		{
			this.ImageSource.SetNativeSize();
		}
	}

	public void Play()
	{
		this.IsPlaying = true;
		this.Foward = true;
		this.mCurFrame = 0;
	}

	public void Play(UGUISpriteAnimation.OnOneShowAnimationFinish callBack)
	{
		this.OnoneShowAnimationFinish = callBack;
		this.Play();
	}

	public void PlayReverse()
	{
		this.IsPlaying = true;
		this.Foward = false;
		this.mCurFrame = 0;
	}

	public void PlayReverse(UGUISpriteAnimation.OnOneShowAnimationFinish callBack)
	{
		this.OnoneShowAnimationFinish = callBack;
		this.PlayReverse();
	}

	private void Update()
	{
		if (!this.IsPlaying || this.FrameCount == 0)
		{
			return;
		}
		this.mDelta += Time.deltaTime;
		if (this.mDelta > this.FPSTimes)
		{
			this.mDelta = 0f;
			if (this.Foward)
			{
				this.mCurFrame++;
			}
			else
			{
				this.mCurFrame--;
			}
			if (this.mCurFrame >= this.FrameCount)
			{
				if (!this.Loop)
				{
					if (this.OnoneShowAnimationFinish != null)
					{
						this.OnoneShowAnimationFinish();
					}
					this.IsPlaying = false;
					return;
				}
				this.mCurFrame = 0;
			}
			else if (this.mCurFrame < 0)
			{
				if (!this.Loop)
				{
					if (this.OnoneShowAnimationFinish != null)
					{
						this.OnoneShowAnimationFinish();
					}
					this.IsPlaying = false;
					return;
				}
				this.mCurFrame = this.FrameCount - 1;
			}
			this.SetSprite(this.mCurFrame);
		}
	}

	public void Pause()
	{
		this.IsPlaying = false;
	}

	public void Resume()
	{
		if (!this.IsPlaying)
		{
			this.IsPlaying = true;
		}
	}

	public void Stop()
	{
		this.mCurFrame = 0;
		this.SetSprite(this.mCurFrame);
		this.IsPlaying = false;
	}

	public void Rewind()
	{
		this.mCurFrame = 0;
		this.SetSprite(this.mCurFrame);
		this.Play();
	}
}
