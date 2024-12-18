using System;
using UnityEngine;

public class DontDestroyOnSwitch : MonoBehaviour
{
	private void Awake()
	{
        Application.targetFrameRate = 60;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}
}
