using System;

namespace Util
{
	public class Singleton<T> where T : new()
	{
		protected static T _instance = default(T);

		private static object _objLock = new object();

		private bool m_IsInit;

		public static T Instance
		{
			get
			{
				if (Singleton<T>._instance == null)
				{
					object objLock = Singleton<T>._objLock;
					lock (objLock)
					{
						if (Singleton<T>._instance == null)
						{
							Singleton<T>._instance = Activator.CreateInstance<T>();
						}
					}
				}
				return Singleton<T>._instance;
			}
		}

		protected Singleton()
		{
			this.Init();
		}

		protected virtual void Init()
		{
		}

		public static void Depose()
		{
			Singleton<T>._instance = default(T);
		}
	}
}
