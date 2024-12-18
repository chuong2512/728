using System;
using UnityEngine;
using Util;

public class LoadView : MonoBehaviour
{
	private void Start()
	{
		base.Invoke("loadSc", 3f);
		Singleton<MissionManager>.Instance.initDay();
	}

	private void loadSc()
	{
		Singleton<SceneManager>.Instance.LoadSceneAsync("MainGame");
	}

	private void Update()
	{
	}
}
