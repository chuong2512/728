using System;
using UnityEngine;

namespace Engine
{
	public static class InstanceHandler
	{
		public delegate GameObject InstantiateDelegate(GameObject prefab, Vector3 pos, Quaternion rot);

		public delegate void DestroyDelegate(GameObject instance);

		public static InstanceHandler.InstantiateDelegate InstantiateDelegates;

		public static InstanceHandler.DestroyDelegate DestroyDelegates;

		internal static GameObject InstantiatePrefab(GameObject prefab, Vector3 pos, Quaternion rot)
		{
			if (InstanceHandler.InstantiateDelegates != null)
			{
				return InstanceHandler.InstantiateDelegates(prefab, pos, rot);
			}
			return UnityEngine.Object.Instantiate<GameObject>(prefab, pos, rot);
		}

		internal static void DestroyInstance(GameObject instance)
		{
			if (InstanceHandler.DestroyDelegates != null)
			{
				InstanceHandler.DestroyDelegates(instance);
				return;
			}
			UnityEngine.Object.Destroy(instance);
		}
	}
}
