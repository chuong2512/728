using System;
using UnityEngine;

public class ShowFPS : MonoBehaviour
{
	public float Update_Interval = 0.5f;

	private float m_lastInterval;

	private int m_frames;

	private float m_fps;

	private void Start()
	{
		this.m_lastInterval = Time.realtimeSinceStartup;
		this.m_frames = 0;
	}

	private void OnGUI()
	{
		GUILayout.Label("FPS:" + this.m_fps.ToString("f2"), Array.Empty<GUILayoutOption>());
	}

	private void Update()
	{
		this.m_frames++;
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (realtimeSinceStartup > this.m_lastInterval + this.Update_Interval)
		{
			this.m_fps = (float)this.m_frames / (realtimeSinceStartup - this.m_lastInterval);
			this.m_frames = 0;
			this.m_lastInterval = realtimeSinceStartup;
		}
	}
}
