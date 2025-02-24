using System;
using UnityEngine;

namespace Engine
{
	[AddComponentMenu("Path-o-logical/PoolManager/Pre-Runtime Pool Item")]
	public class PreRuntimePoolItem : MonoBehaviour
	{
		public string poolName = "";

		public string prefabName = "";

		public bool despawnOnStart = true;

		public bool doNotReparent;

		private void Start()
		{
			SpawnPool spawnPool;
			if (!PoolManager.Pools.TryGetValue(this.poolName, out spawnPool))
			{
				UnityEngine.Debug.LogError(string.Format("PreRuntimePoolItem Error ('{0}'): No pool with the name '{1}' exists! Create one using the PoolManager Inspector interface or PoolManager.CreatePool().See the online docs for more information at http://docs.poolmanager.path-o-logical.com", base.name, this.poolName));
				return;
			}
			spawnPool.Add(base.transform, this.prefabName, this.despawnOnStart, !this.doNotReparent);
		}
	}
}
