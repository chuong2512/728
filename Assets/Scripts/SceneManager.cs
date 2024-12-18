using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;

public class SceneManager : Singleton<global::SceneManager>
{
	private int m_SceneTag;

	public int SceneTag
	{
		get
		{
			return this.m_SceneTag;
		}
	}

	protected override void Init()
	{
		this.m_SceneTag = 0;
	}

	public AsyncOperation LoadSceneAsync(string sceneName)
	{
		return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
	}

	public void LoadScene(string sceneName)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
	}
}
