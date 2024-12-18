using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
	public class SpawnPoolsDict : IDictionary<string, SpawnPool>, ICollection<KeyValuePair<string, SpawnPool>>, IEnumerable<KeyValuePair<string, SpawnPool>>, IEnumerable
	{
		public delegate void OnCreatedDelegate(SpawnPool pool);

		internal Dictionary<string, SpawnPoolsDict.OnCreatedDelegate> onCreatedDelegates = new Dictionary<string, SpawnPoolsDict.OnCreatedDelegate>();

		private Dictionary<string, SpawnPool> _pools = new Dictionary<string, SpawnPool>();

		public int Count
		{
			get
			{
				return this._pools.Count;
			}
		}

		public SpawnPool this[string key]
		{
			get
			{
				SpawnPool result;
				try
				{
					result = this._pools[key];
				}
				catch (KeyNotFoundException)
				{
					throw new KeyNotFoundException(string.Format("A Pool with the name '{0}' not found. \nPools={1}", key, this.ToString()));
				}
				return result;
			}
			set
			{
				throw new NotImplementedException("Cannot set PoolManager.Pools[key] directly. SpawnPools add themselves to PoolManager.Pools when created, so there is no need to set them explicitly. Create pools using PoolManager.Pools.Create() or add a SpawnPool component to a GameObject.");
			}
		}

		public ICollection<string> Keys
		{
			get
			{
				throw new NotImplementedException("If you need this, please request it.");
			}
		}

		public ICollection<SpawnPool> Values
		{
			get
			{
				throw new NotImplementedException("If you need this, please request it.");
			}
		}

		private bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		bool ICollection<KeyValuePair<string, SpawnPool>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		public void AddOnCreatedDelegate(string poolName, SpawnPoolsDict.OnCreatedDelegate createdDelegate)
		{
			if (!this.onCreatedDelegates.ContainsKey(poolName))
			{
				this.onCreatedDelegates.Add(poolName, createdDelegate);
				UnityEngine.Debug.Log(string.Format("Added onCreatedDelegates for pool '{0}': {1}", poolName, createdDelegate.Target));
				return;
			}
			Dictionary<string, SpawnPoolsDict.OnCreatedDelegate> dictionary = this.onCreatedDelegates;
			dictionary[poolName] = (SpawnPoolsDict.OnCreatedDelegate)Delegate.Combine(dictionary[poolName], createdDelegate);
		}

		public void RemoveOnCreatedDelegate(string poolName, SpawnPoolsDict.OnCreatedDelegate createdDelegate)
		{
			if (!this.onCreatedDelegates.ContainsKey(poolName))
			{
				throw new KeyNotFoundException("No OnCreatedDelegates found for pool name '" + poolName + "'.");
			}
			Dictionary<string, SpawnPoolsDict.OnCreatedDelegate> dictionary = this.onCreatedDelegates;
			dictionary[poolName] = (SpawnPoolsDict.OnCreatedDelegate)Delegate.Remove(dictionary[poolName], createdDelegate);
			UnityEngine.Debug.Log(string.Format("Removed onCreatedDelegates for pool '{0}': {1}", poolName, createdDelegate.Target));
		}

		public SpawnPool Create(string poolName)
		{
			string text = poolName.Replace("Pool", "");
			if (text != poolName)
			{
				UnityEngine.Debug.LogWarning(string.Format("'{0}' has the word 'Pool' in it. This word is reserved for GameObject defaul naming. The pool name has been changed to '{1}'", poolName, text));
				poolName = text;
			}
			if (this.ContainsKey(poolName))
			{
				return this._pools[poolName];
			}
			return new GameObject(poolName + "Pool").AddComponent<SpawnPool>();
		}

		public SpawnPool Create(string poolName, GameObject owner)
		{
			string text = poolName.Replace("Pool", "");
			if (text != poolName)
			{
				UnityEngine.Debug.LogWarning(string.Format("'{0}' has the word 'Pool' in it. This word is reserved for GameObject defaul naming. The pool name has been changed to '{1}'", poolName, text));
				poolName = text;
			}
			if (this.ContainsKey(poolName))
			{
				return this._pools[poolName];
			}
			string name = owner.gameObject.name;
			SpawnPool result;
			try
			{
				owner.gameObject.name = poolName;
				result = owner.AddComponent<SpawnPool>();
			}
			finally
			{
				owner.gameObject.name = name;
			}
			return result;
		}

		private bool assertValidPoolName(string poolName)
		{
			string text = poolName.Replace("Pool", "");
			if (text != poolName)
			{
				UnityEngine.Debug.LogWarning(string.Format("'{0}' has the word 'Pool' in it. This word is reserved for GameObject defaul naming. The pool name has been changed to '{1}'", poolName, text));
				poolName = text;
			}
			if (this.ContainsKey(poolName))
			{
				UnityEngine.Debug.Log(string.Format("A pool with the name '{0}' already exists", poolName));
				return false;
			}
			return true;
		}

		public override string ToString()
		{
			string[] array = new string[this._pools.Count];
			this._pools.Keys.CopyTo(array, 0);
			return string.Format("[{0}]", string.Join(", ", array));
		}

		public bool Destroy(string poolName)
		{
			SpawnPool spawnPool;
			if (!this._pools.TryGetValue(poolName, out spawnPool))
			{
				UnityEngine.Debug.LogError(string.Format("PoolManager: Unable to destroy '{0}'. Not in PoolManager", poolName));
				return false;
			}
			UnityEngine.Object.Destroy(spawnPool.gameObject);
			this._pools.Remove(spawnPool.poolName);
			return true;
		}

		public void DestroyAll()
		{
			foreach (KeyValuePair<string, SpawnPool> current in this._pools)
			{
				UnityEngine.Debug.Log("DESTROYING: " + current.Value.gameObject.name);
				UnityEngine.Object.Destroy(current.Value.gameObject);
			}
			this._pools.Clear();
		}

		internal void Add(SpawnPool spawnPool)
		{
			if (this.ContainsKey(spawnPool.poolName))
			{
				UnityEngine.Debug.LogError(string.Format("A pool with the name '{0}' already exists. This should only happen if a SpawnPool with this name is added to a scene twice.", spawnPool.poolName));
				return;
			}
			this._pools.Add(spawnPool.poolName, spawnPool);
			if (this.onCreatedDelegates.ContainsKey(spawnPool.poolName))
			{
				this.onCreatedDelegates[spawnPool.poolName](spawnPool);
			}
		}

		public void Add(string key, SpawnPool value)
		{
			throw new NotImplementedException("SpawnPools add themselves to PoolManager.Pools when created, so there is no need to Add() them explicitly. Create pools using PoolManager.Pools.Create() or add a SpawnPool component to a GameObject.");
		}

		internal bool Remove(SpawnPool spawnPool)
		{
			if (!this.ContainsValue(spawnPool) & Application.isPlaying)
			{
				UnityEngine.Debug.LogError(string.Format("PoolManager: Unable to remove '{0}'. Pool not in PoolManager", spawnPool.poolName));
				return false;
			}
			this._pools.Remove(spawnPool.poolName);
			return true;
		}

		public bool Remove(string poolName)
		{
			throw new NotImplementedException("SpawnPools can only be destroyed, not removed and kept alive outside of PoolManager. There are only 2 legal ways to destroy a SpawnPool: Destroy the GameObject directly, if you have a reference, or use PoolManager.Destroy(string poolName).");
		}

		public bool ContainsKey(string poolName)
		{
			return this._pools.ContainsKey(poolName);
		}

		public bool ContainsValue(SpawnPool pool)
		{
			return this._pools.ContainsValue(pool);
		}

		public bool TryGetValue(string poolName, out SpawnPool spawnPool)
		{
			return this._pools.TryGetValue(poolName, out spawnPool);
		}

		public bool Contains(KeyValuePair<string, SpawnPool> item)
		{
			throw new NotImplementedException("Use PoolManager.Pools.ContainsKey(string poolName) or PoolManager.Pools.ContainsValue(SpawnPool pool) instead.");
		}

		public void Add(KeyValuePair<string, SpawnPool> item)
		{
			throw new NotImplementedException("SpawnPools add themselves to PoolManager.Pools when created, so there is no need to Add() them explicitly. Create pools using PoolManager.Pools.Create() or add a SpawnPool component to a GameObject.");
		}

		public void Clear()
		{
			throw new NotImplementedException("Use PoolManager.Pools.DestroyAll() instead.");
		}

		private void CopyTo(KeyValuePair<string, SpawnPool>[] array, int arrayIndex)
		{
			throw new NotImplementedException("PoolManager.Pools cannot be copied");
		}

		void ICollection<KeyValuePair<string, SpawnPool>>.CopyTo(KeyValuePair<string, SpawnPool>[] array, int arrayIndex)
		{
			throw new NotImplementedException("PoolManager.Pools cannot be copied");
		}

		public bool Remove(KeyValuePair<string, SpawnPool> item)
		{
			throw new NotImplementedException("SpawnPools can only be destroyed, not removed and kept alive outside of PoolManager. There are only 2 legal ways to destroy a SpawnPool: Destroy the GameObject directly, if you have a reference, or use PoolManager.Destroy(string poolName).");
		}

		public IEnumerator<KeyValuePair<string, SpawnPool>> GetEnumerator()
		{
			return this._pools.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._pools.GetEnumerator();
		}
	}
}
