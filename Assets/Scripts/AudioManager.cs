using Engine;
using System;

public class AudioManager
{
	private static AudioObject m_bgmAudio;

	public static bool m_isStop;

	public static AudioObject PlayBGM(string name)
	{
		if (AudioManager.m_isStop)
		{
			return null;
		}
		float gameMusicVolume = GameSet.m_gameMusicVolume;
		if (AudioManager.m_bgmAudio && AudioManager.m_bgmAudio.name == name)
		{
			if (!AudioManager.m_bgmAudio.IsPlaying())
			{
				AudioManager.m_bgmAudio.Play(gameMusicVolume, 0f);
			}
			return AudioManager.m_bgmAudio;
		}
		if (AudioManager.m_bgmAudio)
		{
			AudioManager.m_bgmAudio.Stop();
			AudioManager.m_bgmAudio.DestroyData();
			AudioManager.m_bgmAudio = null;
		}
		AudioManager.m_bgmAudio = AudioController.Instance.Play(name, "Audio/", 1, gameMusicVolume, true, false);
		return AudioManager.m_bgmAudio;
	}

	public static void StopBGM()
	{
		if (AudioManager.m_bgmAudio)
		{
			AudioManager.m_bgmAudio.Stop();
			AudioManager.m_bgmAudio.DestroyData();
			AudioManager.m_bgmAudio = null;
		}
	}

	public static AudioObject PlayEffectAudio(string name, bool loop, float Volume, bool isSignle = false)
	{
		if (GameSet.m_toggleMusicEffect <= 0f)
		{
			return null;
		}
		if (AudioManager.m_isStop)
		{
			return null;
		}
		return AudioController.Instance.Play(name, "Audio/", 2, GameSet.m_toggleMusicEffect, loop, isSignle);
	}

	public static AudioObject PlayEffectAudio(string name, bool loop = false, bool isSignle = false)
	{
		if (GameSet.m_toggleMusicEffect <= 0f)
		{
			return null;
		}
		if (AudioManager.m_isStop)
		{
			return null;
		}
		return AudioController.Instance.Play(name, "Audio/", 2, GameSet.m_toggleMusicEffect, loop, isSignle);
	}

	public static AudioObject PlayLoopAudio(string name)
	{
		if (GameSet.m_toggleMusicEffect <= 0f)
		{
			return null;
		}
		if (AudioManager.m_isStop)
		{
			return null;
		}
		return AudioController.Instance.Play(name, "Audio/", 4, GameSet.m_toggleMusicEffect, false, false);
	}

	public static void PlayAudio(AudioResourceData audio)
	{
		if (AudioManager.m_isStop)
		{
			return;
		}
		AudioController.Instance.Play(audio, GameSet.m_gameMusicVolume);
	}

	public static void StopAudio(string name)
	{
		AudioController.Instance.Stop(name);
	}

	public static void StopAudio(AudioResourceData audio)
	{
		AudioController.Instance.Stop(audio);
	}

	public static bool IsPlaying(AudioResourceData audio)
	{
		return AudioController.Instance.IsPlaying(audio);
	}

	public static void SetBGMVolume(float volume)
	{
		if (AudioManager.m_bgmAudio)
		{
			if (!AudioManager.m_bgmAudio.IsPlaying())
			{
				AudioManager.m_bgmAudio.Play(volume, 0f);
				return;
			}
			AudioManager.m_bgmAudio.SetVolume(volume);
		}
	}

	public static void SetEffectVolum(float volum)
	{
		AudioController.Instance.SetAllEffectVolume(volum);
	}

	public static void FadeOutBGM(float volume, float time)
	{
		if (AudioManager.m_bgmAudio)
		{
			AudioManager.m_bgmAudio.IsPlaying();
		}
	}
}
