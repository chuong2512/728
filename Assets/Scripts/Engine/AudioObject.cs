using System;
using UnityEngine;

namespace Engine
{
	[RequireComponent(typeof(AudioSource))]
	public class AudioObject : MonoBehaviour
	{
		public delegate void AudioEventDelegate(AudioObject audioObject);

		private AudioObject.AudioEventDelegate m_completelyPlayedDelegate;

		public AudioSource m_asAudioSoure;

		public int audioType;

		public AudioObject.AudioEventDelegate CompletelyPlayedDelegate
		{
			get
			{
				return this.m_completelyPlayedDelegate;
			}
			set
			{
				this.m_completelyPlayedDelegate = value;
			}
		}

		private void Update()
		{
			this.CheckPlayedComplete();
		}

		public bool IsPlaying()
		{
			return !(null == this.m_asAudioSoure) && this.m_asAudioSoure.isPlaying;
		}

		public void Play(float volume = 1f, float delay = 0f)
		{
			this.m_asAudioSoure.volume = volume;
			if (delay != 0f)
			{
				this.m_asAudioSoure.PlayDelayed(delay);
				return;
			}
			this.m_asAudioSoure.Play();
		}

		public void SetVolume(float volume = 1f)
		{
			this.m_asAudioSoure.volume = volume;
		}

		public void Stop()
		{
			if (this.IsPlaying())
			{
				this.m_asAudioSoure.Stop();
			}
		}

		private void CheckPlayedComplete()
		{
			if (!this.IsPlaying())
			{
				if (this.m_completelyPlayedDelegate != null)
				{
					this.m_completelyPlayedDelegate(this);
				}
				this.DestroyData();
			}
		}

		public void Init(bool isLoop, Vector3 worldPos)
		{
			if (this.m_asAudioSoure == null)
			{
				this.m_asAudioSoure = base.GetComponent<AudioSource>();
			}
			this.m_asAudioSoure.loop = isLoop;
			base.transform.position = worldPos;
		}

		public void SetAudioClip(AudioClip data)
		{
			if (this.m_asAudioSoure == null)
			{
				UnityEngine.Debug.LogError("m_asAudioSoure is null!");
				return;
			}
			this.m_asAudioSoure.clip = data;
		}

		public void DestroyData()
		{
			if (null == this.m_asAudioSoure)
			{
				return;
			}
			this.m_asAudioSoure.clip = null;
			PoolManager.Pools.Create("AudioManager").Despawn(base.transform);
		}

		private void OnAudioToFinish(object args)
		{
			if (args != null)
			{
				base.CancelInvoke("FadeInBGM");
				float time = (float)args;
				base.Invoke("FadeInBGM", time);
				UnityEngine.Object.DestroyObject(this.m_asAudioSoure.gameObject);
			}
		}

		private void FadeInBGM()
		{
		}
	}
}
