using System;
using UnityEngine;

namespace Engine
{
	public class MonoManager : MonoBehaviour
	{
		private static MonoManager m_instance;

		public static MonoManager Instance
		{
			get
			{
				if (MonoManager.m_instance == null)
				{
					if (Application.platform == RuntimePlatform.WindowsEditor)
					{
						MonoManager.m_instance = UnityEngine.Object.FindObjectOfType<MonoManager>();
					}
					if (MonoManager.m_instance == null)
					{
						GameObject expr_31 = new GameObject();
						MonoManager.m_instance = expr_31.AddComponent<MonoManager>();
						expr_31.name = "MonoManager";
						UnityEngine.Object.DontDestroyOnLoad(expr_31);
					}
				}
				return MonoManager.m_instance;
			}
		}

		private void Awake()
		{
		}

		private void Start()
		{
		}
	}
}
