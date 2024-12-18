using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
	public class AudioController
	{
		private Dictionary<string, EAudioClip> m_dicEffectCache;

		private Dictionary<string, EAudioClip> m_dicStaticData;

		private Dictionary<string, EAudioClip> m_dicSceneData;

		private List<AudioObject> m_lstPlayerlist;

		private AudioListener m_audiolistener;

		private GameObject m_goAudioPrefab;

		private int m_iExpireTime = 120;

		private int m_iCacheCount = 10;

		private int m_iComCacheCount = 20;

		private static AudioController m_instance;

		private bool m_isInit;

		public static AudioController Instance
		{
			get
			{
				if (AudioController.m_instance == null)
				{
					AudioController.m_instance = new AudioController();
				}
				return AudioController.m_instance;
			}
		}

		public int mComCacheCount
		{
			get
			{
				return this.m_iComCacheCount;
			}
			set
			{
				this.m_iComCacheCount = value;
			}
		}

		public int mExpireTime
		{
			get
			{
				return this.m_iExpireTime;
			}
			set
			{
				this.m_iExpireTime = value;
			}
		}

		public int mCacheCount
		{
			get
			{
				return this.m_iCacheCount;
			}
			set
			{
				this.m_iCacheCount = value;
			}
		}

		private AudioController()
		{
			this.m_dicEffectCache = new Dictionary<string, EAudioClip>();
			this.m_dicSceneData = new Dictionary<string, EAudioClip>();
			this.m_dicStaticData = new Dictionary<string, EAudioClip>();
			this.m_lstPlayerlist = new List<AudioObject>();
			this.InitPrefab();
		}

		public void InitPrefab()
		{
			this.m_goAudioPrefab = (ResourcesLoad.Load("Audio/GameAudio") as GameObject);
		}

		private EAudioClip GetAudioClip(string fileName, string loadPath, int type)
		{
			Dictionary<string, EAudioClip> dictionary;
			switch (type)
			{
			case 1:
				dictionary = this.m_dicSceneData;
				break;
			case 2:
			case 4:
				dictionary = this.m_dicEffectCache;
				break;
			case 3:
				dictionary = this.m_dicStaticData;
				break;
			default:
				return null;
			}
			if (dictionary.ContainsKey(fileName))
			{
				return dictionary[fileName];
			}
			AudioClip audioData = (AudioClip)ResourcesLoad.Load(loadPath + fileName);
			EAudioClip eAudioClip = new EAudioClip();
			eAudioClip.m_strID = fileName;
			eAudioClip.m_audioData = audioData;
			eAudioClip.m_iLastPlayTime = SystemManager.GetLocSystemTime();
			if ((type == 2 || type == 4) && dictionary.Count > this.m_iCacheCount)
			{
				this.RemoveCache(dictionary);
			}
			dictionary.Add(fileName, eAudioClip);
			return eAudioClip;
		}

		private void RemoveCache(Dictionary<string, EAudioClip> dicData)
		{
			List<string> list = new List<string>();
			string text = string.Empty;
			int num = 2147483647;
			int locSystemTime = SystemManager.GetLocSystemTime();
			foreach (KeyValuePair<string, EAudioClip> current in dicData)
			{
				if (locSystemTime - current.Value.m_iLastPlayTime >= this.m_iExpireTime)
				{
					list.Add(current.Key);
				}
				if (current.Value.m_iLastPlayTime < num)
				{
					num = current.Value.m_iLastPlayTime;
					text = current.Key;
				}
			}
			if (list.Count > 0)
			{
				foreach (string current2 in list)
				{
					dicData.Remove(current2);
				}
			}
			if (text != string.Empty && dicData.ContainsKey(text))
			{
				dicData.Remove(text);
			}
		}

		public void RemoveSceneAudioData()
		{
			foreach (AudioObject expr_15 in this.m_lstPlayerlist)
			{
				expr_15.Stop();
				expr_15.DestroyData();
			}
			this.m_lstPlayerlist.Clear();
			this.m_dicSceneData.Clear();
		}

		public AudioObject Play(string fileName, string loadPath, int type, float volume, bool loop = false, bool isSingle = false)
		{
			int num = 0;
			int i = 0;
			int count = this.m_lstPlayerlist.Count;
			while (i < count)
			{
				if (this.m_lstPlayerlist[i].gameObject.name == fileName)
				{
					if (isSingle)
					{
						this.m_lstPlayerlist[i].Stop();
						break;
					}
					num++;
					if (num > this.m_iComCacheCount)
					{
						return null;
					}
				}
				i++;
			}
			EAudioClip audioClip = this.GetAudioClip(fileName, loadPath, type);
			return this.Play(audioClip, loop, volume, 0f, fileName, type);
		}

		public void Play(AudioResourceData audio, float volume)
		{
			audio.m_fVolume = volume;
			AudioClipLoader.LoadAudioClip(audio.m_strResourcePath, new LoadOverCall(this.LoadAudioClipOverCall), audio);
		}

		private void LoadAudioClipOverCall(object audioClip, object data)
		{
			AudioResourceData audioResourceData = (AudioResourceData)data;
			bool isLoop = false;
			Dictionary<string, EAudioClip> dictionary;
			switch (audioResourceData.m_iType)
			{
			case 1:
				isLoop = true;
				dictionary = this.m_dicSceneData;
				break;
			case 2:
				dictionary = this.m_dicEffectCache;
				break;
			case 3:
				dictionary = this.m_dicStaticData;
				break;
			case 4:
				isLoop = true;
				dictionary = this.m_dicEffectCache;
				break;
			default:
				UnityEngine.Debug.Log(string.Concat(new object[]
				{
					"type:",
					audioResourceData.m_iType,
					"no find: id: ",
					audioResourceData.m_strID
				}));
				return;
			}
			if (!dictionary.ContainsKey(audioResourceData.m_strID))
			{
				AudioClip audioData = (AudioClip)audioClip;
				EAudioClip eAudioClip = new EAudioClip();
				eAudioClip.m_strID = audioResourceData.m_strID;
				eAudioClip.m_audioData = audioData;
				eAudioClip.m_iLastPlayTime = SystemManager.GetLocSystemTime();
				if ((audioResourceData.m_iType == 2 || audioResourceData.m_iType == 4) && dictionary.Count > this.m_iCacheCount)
				{
					this.RemoveCache(dictionary);
				}
				dictionary.Add(audioResourceData.m_strID, eAudioClip);
			}
			this.Play(dictionary[audioResourceData.m_strID], isLoop, audioResourceData.m_fVolume, audioResourceData.m_fDelay, audioResourceData.m_strID, audioResourceData.m_iType);
		}

		public void Stop(AudioObject audio)
		{
			if (audio != null)
			{
				audio.Stop();
				audio.DestroyData();
				this.m_lstPlayerlist.Remove(audio);
			}
		}

		public void Stop(string fileName)
		{
			int i = 0;
			int num = this.m_lstPlayerlist.Count;
			while (i < num)
			{
				if (this.m_lstPlayerlist[i].gameObject.name == fileName)
				{
					this.m_lstPlayerlist[i].Stop();
					this.m_lstPlayerlist[i].DestroyData();
					this.m_lstPlayerlist.Remove(this.m_lstPlayerlist[i]);
					i--;
					num--;
				}
				i++;
			}
		}

		public void Stop(AudioResourceData audio)
		{
			AudioObject audioObject = null;
			foreach (AudioObject current in this.m_lstPlayerlist)
			{
				if (!(current == null) && current.gameObject.name.Equals(audio.m_strID))
				{
					audioObject = current;
					break;
				}
			}
			if (audioObject != null && audioObject.IsPlaying())
			{
				audioObject.Stop();
				audioObject.DestroyData();
				this.m_lstPlayerlist.Remove(audioObject);
			}
		}

		public void StopAllEffect()
		{
			if (this.m_lstPlayerlist != null && this.m_lstPlayerlist.Count > 0)
			{
				foreach (AudioObject current in this.m_lstPlayerlist)
				{
					if (!(current == null) && current != null && current.IsPlaying() && current.audioType != 1)
					{
						current.Stop();
					}
				}
			}
		}

		public void SetAllEffectVolume(float value)
		{
			if (this.m_lstPlayerlist != null && this.m_lstPlayerlist.Count > 0)
			{
				foreach (AudioObject current in this.m_lstPlayerlist)
				{
					if (!(current == null) && current != null && current.IsPlaying() && current.audioType != 1)
					{
						current.SetVolume(value);
					}
				}
			}
		}

		public bool IsPlaying(AudioResourceData audio)
		{
			AudioObject audioObject = null;
			foreach (AudioObject current in this.m_lstPlayerlist)
			{
				if (!(current == null) && current.gameObject.name.Equals(audio.m_strID))
				{
					audioObject = current;
					break;
				}
			}
			return audioObject != null && audioObject.IsPlaying();
		}

		public bool IsPlaying(string audioclipname)
		{
			AudioObject audioObject = null;
			foreach (AudioObject current in this.m_lstPlayerlist)
			{
				if (!(current == null) && current.gameObject.name.Equals(audioclipname))
				{
					audioObject = current;
					break;
				}
			}
			return audioObject != null && audioObject.IsPlaying();
		}

		private AudioObject Play(EAudioClip audioClip, bool isLoop, float volume, float delay, string objNameKey, int type)
		{
			audioClip.m_iLastPlayTime = SystemManager.GetLocSystemTime();
			SpawnPool spawnPool = PoolManager.Pools.Create("AudioManager");
			if (!this.m_isInit)
			{
				this.m_isInit = true;
				spawnPool.gameObject.AddComponent<DontDestroyOnSwitch>();
			}
			AudioObject component = spawnPool.Spawn(this.m_goAudioPrefab.transform).GetComponent<AudioObject>();
			AudioListener currentAudioListener = this.GetCurrentAudioListener();
			Vector3 worldPos = Vector3.zero;
			if (currentAudioListener != null)
			{
				worldPos = currentAudioListener.transform.position;
			}
			component.audioType = type;
			component.Init(isLoop, worldPos);
			component.SetAudioClip(audioClip.m_audioData);
			component.Play(volume, delay);
			component.CompletelyPlayedDelegate = new AudioObject.AudioEventDelegate(this.onCompletePlayed);
			this.m_lstPlayerlist.Add(component);
			component.gameObject.name = objNameKey;
			return component;
		}

		private void onCompletePlayed(AudioObject audioObj)
		{
			int num = this.m_lstPlayerlist.IndexOf(audioObj);
			if (num != -1)
			{
				this.m_lstPlayerlist.RemoveAt(num);
			}
		}

		public AudioListener GetCurrentAudioListener()
		{
			if (this.m_audiolistener == null)
			{
				this.m_audiolistener = (AudioListener)UnityEngine.Object.FindObjectOfType(typeof(AudioListener));
			}
			return this.m_audiolistener;
		}
	}
}
