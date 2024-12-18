using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Engine
{
	[AddComponentMenu("Path-o-logical/PoolManager/SpawnPool")]
	public sealed class SpawnPool : MonoBehaviour, IList<Transform>, ICollection<Transform>, IEnumerable<Transform>, IEnumerable
	{
		public delegate GameObject InstantiateDelegate(GameObject prefab, Vector3 pos, Quaternion rot);

		public delegate void DestroyDelegate(GameObject instance);

		private sealed class _DoDespawnAfterSeconds_d__56 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int __1__state;

			private object __2__current;

			public Transform instance;

			public float seconds;

			public bool useParent;

			public SpawnPool __4__this;

			public Transform parent;

			private GameObject _go_5__2;

			object IEnumerator<object>.Current
			{
				get
				{
					return this.__2__current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this.__2__current;
				}
			}

			public _DoDespawnAfterSeconds_d__56(int __1__state)
			{
				this.__1__state = __1__state;
			}

			void IDisposable.Dispose()
			{
			}

			bool IEnumerator.MoveNext()
			{
				int num = this.__1__state;
				SpawnPool spawnPool = this.__4__this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.__1__state = -1;
					if (!this._go_5__2.activeInHierarchy)
					{
						return false;
					}
					this.seconds -= Time.deltaTime;
				}
				else
				{
					this.__1__state = -1;
					this._go_5__2 = this.instance.gameObject;
				}
				if (this.seconds <= 0f)
				{
					if (this.useParent)
					{
						spawnPool.Despawn(this.instance, this.parent);
					}
					else
					{
						spawnPool.Despawn(this.instance);
					}
					return false;
				}
				this.__2__current = null;
				this.__1__state = 1;
				return true;
			}

			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		private sealed class _ListForAudioStop_d__63 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int __1__state;

			private object __2__current;

			public AudioSource src;

			public SpawnPool __4__this;

			private GameObject _srcGameObject_5__2;

			object IEnumerator<object>.Current
			{
				get
				{
					return this.__2__current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this.__2__current;
				}
			}

			public _ListForAudioStop_d__63(int __1__state)
			{
				this.__1__state = __1__state;
			}

			void IDisposable.Dispose()
			{
			}

			bool IEnumerator.MoveNext()
			{
				int num = this.__1__state;
				SpawnPool spawnPool = this.__4__this;
				switch (num)
				{
				case 0:
					this.__1__state = -1;
					this.__2__current = null;
					this.__1__state = 1;
					return true;
				case 1:
					this.__1__state = -1;
					this._srcGameObject_5__2 = this.src.gameObject;
					break;
				case 2:
					this.__1__state = -1;
					break;
				default:
					return false;
				}
				if (this.src.isPlaying)
				{
					this.__2__current = null;
					this.__1__state = 2;
					return true;
				}
				if (!this._srcGameObject_5__2.activeInHierarchy)
				{
					this.src.Stop();
					return false;
				}
				spawnPool.Despawn(this.src.transform);
				return false;
			}

			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		private sealed class _ListenForEmitDespawn_d__64 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int __1__state;

			private object __2__current;

			public ParticleSystem emitter;

			public SpawnPool __4__this;

			private float _safetimer_5__2;

			private GameObject _emitterGO_5__3;

			object IEnumerator<object>.Current
			{
				get
				{
					return this.__2__current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this.__2__current;
				}
			}

			public _ListenForEmitDespawn_d__64(int __1__state)
			{
				this.__1__state = __1__state;
			}

			void IDisposable.Dispose()
			{
			}

			bool IEnumerator.MoveNext()
			{
				int num = this.__1__state;
				SpawnPool spawnPool = this.__4__this;
				switch (num)
				{
				case 0:
					this.__1__state = -1;
					this.__2__current = new WaitForSeconds(this.emitter.startDelay + 0.25f);
					this.__1__state = 1;
					return true;
				case 1:
					this.__1__state = -1;
					this._safetimer_5__2 = 0f;
					this._emitterGO_5__3 = this.emitter.gameObject;
					break;
				case 2:
					this.__1__state = -1;
					break;
				default:
					return false;
				}
				if (!this.emitter.IsAlive(true) || !this._emitterGO_5__3.activeInHierarchy)
				{
					if (this._emitterGO_5__3.activeInHierarchy)
					{
						spawnPool.Despawn(this.emitter.transform);
						this.emitter.Clear(true);
					}
					return false;
				}
				this._safetimer_5__2 += Time.deltaTime;
				if (this._safetimer_5__2 > spawnPool.maxParticleDespawnTime)
				{
					UnityEngine.Debug.LogWarning(string.Format("SpawnPool {0}: Timed out while listening for all particles to die. Waited for {1}sec.", spawnPool.poolName, spawnPool.maxParticleDespawnTime));
				}
				this.__2__current = null;
				this.__1__state = 2;
				return true;
			}

			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		private sealed class _GetEnumerator_d__73 : IEnumerator<Transform>, IEnumerator, IDisposable
		{
			private int __1__state;

			private Transform __2__current;

			public SpawnPool __4__this;

			private int _i_5__2;

			Transform IEnumerator<Transform>.Current
			{
				get
				{
					return this.__2__current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this.__2__current;
				}
			}

			public _GetEnumerator_d__73(int __1__state)
			{
				this.__1__state = __1__state;
			}

			void IDisposable.Dispose()
			{
			}

			bool IEnumerator.MoveNext()
			{
				int num = this.__1__state;
				SpawnPool spawnPool = this.__4__this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.__1__state = -1;
					int num2 = this._i_5__2;
					this._i_5__2 = num2 + 1;
				}
				else
				{
					this.__1__state = -1;
					this._i_5__2 = 0;
				}
				if (this._i_5__2 >= spawnPool._spawned.Count)
				{
					return false;
				}
				this.__2__current = spawnPool._spawned[this._i_5__2];
				this.__1__state = 1;
				return true;
			}

			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		private sealed class _System_Collections_IEnumerable_GetEnumerator_d__74 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int __1__state;

			private object __2__current;

			public SpawnPool __4__this;

			private int _i_5__2;

			object IEnumerator<object>.Current
			{
				get
				{
					return this.__2__current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this.__2__current;
				}
			}

			public _System_Collections_IEnumerable_GetEnumerator_d__74(int __1__state)
			{
				this.__1__state = __1__state;
			}

			void IDisposable.Dispose()
			{
			}

			bool IEnumerator.MoveNext()
			{
				int num = this.__1__state;
				SpawnPool spawnPool = this.__4__this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.__1__state = -1;
					int num2 = this._i_5__2;
					this._i_5__2 = num2 + 1;
				}
				else
				{
					this.__1__state = -1;
					this._i_5__2 = 0;
				}
				if (this._i_5__2 >= spawnPool._spawned.Count)
				{
					return false;
				}
				this.__2__current = spawnPool._spawned[this._i_5__2];
				this.__1__state = 1;
				return true;
			}

			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		public string poolName = "";

		public bool matchPoolScale;

		public bool matchPoolLayer;

		public bool dontReparent;

		public bool _dontDestroyOnLoad;

		public bool logMessages;

		public List<PrefabPool> _perPrefabPoolOptions = new List<PrefabPool>();

		public Dictionary<object, bool> prefabsFoldOutStates = new Dictionary<object, bool>();

		public float maxParticleDespawnTime = 300f;

		private Transform _group_k__BackingField;

		public PrefabsDict prefabs = new PrefabsDict();

		public Dictionary<object, bool> _editorListItemStates = new Dictionary<object, bool>();

		private List<PrefabPool> _prefabPools = new List<PrefabPool>();

		internal List<Transform> _spawned = new List<Transform>();

		public SpawnPool.InstantiateDelegate instantiateDelegates;

		public SpawnPool.DestroyDelegate destroyDelegates;

		public bool dontDestroyOnLoad
		{
			get
			{
				return this._dontDestroyOnLoad;
			}
			set
			{
				this._dontDestroyOnLoad = value;
				if (this.group != null)
				{
					UnityEngine.Object.DontDestroyOnLoad(this.group.gameObject);
				}
			}
		}

		public Transform group
		{
			get;
			private set;
		}

		public Dictionary<string, PrefabPool> prefabPools
		{
			get
			{
				Dictionary<string, PrefabPool> dictionary = new Dictionary<string, PrefabPool>();
				for (int i = 0; i < this._prefabPools.Count; i++)
				{
					dictionary[this._prefabPools[i].prefabGO.name] = this._prefabPools[i];
				}
				return dictionary;
			}
		}

		public Transform this[int index]
		{
			get
			{
				return this._spawned[index];
			}
			set
			{
				throw new NotImplementedException("Read-only.");
			}
		}

		public int Count
		{
			get
			{
				return this._spawned.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		private void Awake()
		{
			if (this._dontDestroyOnLoad)
			{
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			}
			this.group = base.transform;
			if (this.poolName == "")
			{
				this.poolName = this.group.name.Replace("Pool", "");
				this.poolName = this.poolName.Replace("(Clone)", "");
			}
			if (this.logMessages)
			{
				UnityEngine.Debug.Log(string.Format("SpawnPool {0}: Initializing..", this.poolName));
			}
			for (int i = 0; i < this._perPrefabPoolOptions.Count; i++)
			{
				if (this._perPrefabPoolOptions[i].prefab == null)
				{
					UnityEngine.Debug.LogWarning(string.Format("Initialization Warning: Pool '{0}' contains a PrefabPool with no prefab reference. Skipping.", this.poolName));
				}
				else
				{
					this._perPrefabPoolOptions[i].inspectorInstanceConstructor();
					this.CreatePrefabPool(this._perPrefabPoolOptions[i]);
				}
			}
			PoolManager.Pools.Add(this);
		}

		internal GameObject InstantiatePrefab(GameObject prefab, Vector3 pos, Quaternion rot)
		{
			if (this.instantiateDelegates != null)
			{
				return this.instantiateDelegates(prefab, pos, rot);
			}
			return InstanceHandler.InstantiatePrefab(prefab, pos, rot);
		}

		internal void DestroyInstance(GameObject instance)
		{
			if (this.destroyDelegates != null)
			{
				this.destroyDelegates(instance);
				return;
			}
			InstanceHandler.DestroyInstance(instance);
		}

		private void OnDestroy()
		{
			if (this.logMessages)
			{
				UnityEngine.Debug.Log(string.Format("SpawnPool {0}: Destroying...", this.poolName));
			}
			if (PoolManager.Pools.ContainsValue(this))
			{
				PoolManager.Pools.Remove(this);
			}
			base.StopAllCoroutines();
			this._spawned.Clear();
			using (List<PrefabPool>.Enumerator enumerator = this._prefabPools.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					enumerator.Current.SelfDestruct();
				}
			}
			this._prefabPools.Clear();
			this.prefabs._Clear();
		}

		public void CreatePrefabPool(PrefabPool prefabPool)
		{
			if (this.GetPrefabPool(prefabPool.prefab) != null)
			{
				throw new Exception(string.Format("Prefab '{0}' is already in  SpawnPool '{1}'. Prefabs can be in more than 1 SpawnPool but cannot be in the same SpawnPool twice.", prefabPool.prefab, this.poolName));
			}
			prefabPool.spawnPool = this;
			this._prefabPools.Add(prefabPool);
			this.prefabs._Add(prefabPool.prefab.name, prefabPool.prefab);
			if (!prefabPool.preloaded)
			{
				if (this.logMessages)
				{
					UnityEngine.Debug.Log(string.Format("SpawnPool {0}: Preloading {1} {2}", this.poolName, prefabPool.preloadAmount, prefabPool.prefab.name));
				}
				prefabPool.PreloadInstances();
			}
		}

		public void Add(Transform instance, string prefabName, bool despawn, bool parent)
		{
			for (int i = 0; i < this._prefabPools.Count; i++)
			{
				if (this._prefabPools[i].prefabGO == null)
				{
					UnityEngine.Debug.LogError("Unexpected Error: PrefabPool.prefabGO is null");
					return;
				}
				if (this._prefabPools[i].prefabGO.name == prefabName)
				{
					this._prefabPools[i].AddUnpooled(instance, despawn);
					if (this.logMessages)
					{
						UnityEngine.Debug.Log(string.Format("SpawnPool {0}: Adding previously unpooled instance {1}", this.poolName, instance.name));
					}
					if (parent)
					{
						bool worldPositionStays = !(instance is RectTransform);
						instance.SetParent(this.group, worldPositionStays);
					}
					if (!despawn)
					{
						this._spawned.Add(instance);
					}
					return;
				}
			}
			UnityEngine.Debug.LogError(string.Format("SpawnPool {0}: PrefabPool {1} not found.", this.poolName, prefabName));
		}

		public void Add(Transform item)
		{
			throw new NotImplementedException("Use SpawnPool.Spawn() to properly add items to the pool.");
		}

		public void Remove(Transform item)
		{
			throw new NotImplementedException("Use Despawn() to properly manage items that should remain in the pool but be deactivated.");
		}

		public Transform Spawn(Transform prefab, Vector3 pos, Quaternion rot, Transform parent)
		{
			int i = 0;
			Transform transform;
			bool worldPositionStays;
			while (i < this._prefabPools.Count)
			{
				if (this._prefabPools[i].prefabGO == prefab.gameObject)
				{
					transform = this._prefabPools[i].SpawnInstance(pos, rot);
					if (transform == null)
					{
						return null;
					}
					worldPositionStays = !(transform is RectTransform);
					if (parent != null)
					{
						transform.SetParent(parent, worldPositionStays);
					}
					else if (!this.dontReparent && transform.parent != this.group)
					{
						transform.SetParent(this.group, worldPositionStays);
					}
					this._spawned.Add(transform);
					transform.gameObject.BroadcastMessage("OnSpawned", this, SendMessageOptions.DontRequireReceiver);
					return transform;
				}
				else
				{
					i++;
				}
			}
			PrefabPool prefabPool = new PrefabPool(prefab);
			this.CreatePrefabPool(prefabPool);
			transform = prefabPool.SpawnInstance(pos, rot);
			worldPositionStays = !(transform is RectTransform);
			if (parent != null)
			{
				transform.SetParent(parent, worldPositionStays);
			}
			else if (!this.dontReparent && transform.parent != this.group)
			{
				transform.SetParent(this.group, worldPositionStays);
			}
			this._spawned.Add(transform);
			transform.gameObject.BroadcastMessage("OnSpawned", this, SendMessageOptions.DontRequireReceiver);
			return transform;
		}

		public Transform Spawn(Transform prefab, Vector3 pos, Quaternion rot)
		{
			Transform transform = this.Spawn(prefab, pos, rot, null);
			if (transform == null)
			{
				return null;
			}
			return transform;
		}

		public Transform Spawn(Transform prefab)
		{
			return this.Spawn(prefab, Vector3.zero, Quaternion.identity);
		}

		public Transform Spawn(Transform prefab, Transform parent)
		{
			return this.Spawn(prefab, Vector3.zero, Quaternion.identity, parent);
		}

		public Transform Spawn(GameObject prefab, Vector3 pos, Quaternion rot, Transform parent)
		{
			return this.Spawn(prefab.transform, pos, rot, parent);
		}

		public Transform Spawn(GameObject prefab, Vector3 pos, Quaternion rot)
		{
			return this.Spawn(prefab.transform, pos, rot);
		}

		public Transform Spawn(GameObject prefab)
		{
			return this.Spawn(prefab.transform);
		}

		public Transform Spawn(GameObject prefab, Transform parent)
		{
			return this.Spawn(prefab.transform, parent);
		}

		public Transform Spawn(string prefabName)
		{
			Transform prefab = this.prefabs[prefabName];
			return this.Spawn(prefab);
		}

		public Transform Spawn(string prefabName, Transform parent)
		{
			Transform prefab = this.prefabs[prefabName];
			return this.Spawn(prefab, parent);
		}

		public Transform Spawn(string prefabName, Vector3 pos, Quaternion rot)
		{
			Transform prefab = this.prefabs[prefabName];
			return this.Spawn(prefab, pos, rot);
		}

		public Transform Spawn(string prefabName, Vector3 pos, Quaternion rot, Transform parent)
		{
			Transform prefab = this.prefabs[prefabName];
			return this.Spawn(prefab, pos, rot, parent);
		}

		public AudioSource Spawn(AudioSource prefab, Vector3 pos, Quaternion rot)
		{
			return this.Spawn(prefab, pos, rot, null);
		}

		public AudioSource Spawn(AudioSource prefab)
		{
			return this.Spawn(prefab, Vector3.zero, Quaternion.identity, null);
		}

		public AudioSource Spawn(AudioSource prefab, Transform parent)
		{
			return this.Spawn(prefab, Vector3.zero, Quaternion.identity, parent);
		}

		public AudioSource Spawn(AudioSource prefab, Vector3 pos, Quaternion rot, Transform parent)
		{
			Transform transform = this.Spawn(prefab.transform, pos, rot, parent);
			if (transform == null)
			{
				return null;
			}
			AudioSource component = transform.GetComponent<AudioSource>();
			component.Play();
			base.StartCoroutine(this.ListForAudioStop(component));
			return component;
		}

		public ParticleSystem Spawn(ParticleSystem prefab, Vector3 pos, Quaternion rot)
		{
			return this.Spawn(prefab, pos, rot, null);
		}

		public ParticleSystem Spawn(ParticleSystem prefab, Vector3 pos, Quaternion rot, Transform parent)
		{
			Transform transform = this.Spawn(prefab.transform, pos, rot, parent);
			if (transform == null)
			{
				return null;
			}
			ParticleSystem component = transform.GetComponent<ParticleSystem>();
			base.StartCoroutine(this.ListenForEmitDespawn(component));
			return component;
		}

		public void Despawn(Transform instance)
		{
			bool flag = false;
			for (int i = 0; i < this._prefabPools.Count; i++)
			{
				if (this._prefabPools[i]._spawned.Contains(instance))
				{
					flag = this._prefabPools[i].DespawnInstance(instance);
					break;
				}
				if (this._prefabPools[i]._despawned.Contains(instance))
				{
					UnityEngine.Debug.LogError(string.Format("SpawnPool {0}: {1} has already been despawned. You cannot despawn something more than once!", this.poolName, instance.name));
					return;
				}
			}
			if (!flag)
			{
				UnityEngine.Debug.LogError(string.Format("SpawnPool {0}: {1} not found in SpawnPool", this.poolName, instance.name));
				return;
			}
			this._spawned.Remove(instance);
		}

		public void Despawn(Transform instance, Transform parent)
		{
			bool worldPositionStays = !(instance is RectTransform);
			instance.SetParent(parent, worldPositionStays);
			this.Despawn(instance);
		}

		public void Despawn(Transform instance, float seconds)
		{
			base.StartCoroutine(this.DoDespawnAfterSeconds(instance, seconds, false, null));
		}

		public void Despawn(Transform instance, float seconds, Transform parent)
		{
			base.StartCoroutine(this.DoDespawnAfterSeconds(instance, seconds, true, parent));
		}

//		[IteratorStateMachine(typeof(SpawnPool._003CDoDespawnAfterSeconds_003Ed__56))]
		private IEnumerator DoDespawnAfterSeconds(Transform instance, float seconds, bool useParent, Transform parent)
		{
			while (true)
			{
				int num = 0;
				if (num != 0)
				{
					if (num != 1)
					{
						break;
					}
					GameObject gameObject = null;
					if (!gameObject.activeInHierarchy)
					{
						goto Block_3;
					}
					seconds -= Time.deltaTime;
				}
				else
				{
					GameObject gameObject = instance.gameObject;
				}
				if (seconds <= 0f)
				{
					goto Block_4;
				}
				yield return null;
			}
			yield break;
			Block_3:
			yield break;
			Block_4:
			if (useParent)
			{
				this.Despawn(instance, parent);
			}
			else
			{
				this.Despawn(instance);
			}
			yield break;
		}

		public void DespawnAll()
		{
			List<Transform> list = new List<Transform>(this._spawned);
			for (int i = 0; i < list.Count; i++)
			{
				this.Despawn(list[i]);
			}
		}

		public bool IsSpawned(Transform instance)
		{
			return this._spawned.Contains(instance);
		}

		public PrefabPool GetPrefabPool(Transform prefab)
		{
			for (int i = 0; i < this._prefabPools.Count; i++)
			{
				if (this._prefabPools[i].prefabGO == null)
				{
					UnityEngine.Debug.LogError(string.Format("SpawnPool {0}: PrefabPool.prefabGO is null", this.poolName));
				}
				if (this._prefabPools[i].prefabGO == prefab.gameObject)
				{
					return this._prefabPools[i];
				}
			}
			return null;
		}

		public PrefabPool GetPrefabPool(GameObject prefab)
		{
			for (int i = 0; i < this._prefabPools.Count; i++)
			{
				if (this._prefabPools[i].prefabGO == null)
				{
					UnityEngine.Debug.LogError(string.Format("SpawnPool {0}: PrefabPool.prefabGO is null", this.poolName));
				}
				if (this._prefabPools[i].prefabGO == prefab)
				{
					return this._prefabPools[i];
				}
			}
			return null;
		}

		public Transform GetPrefab(Transform instance)
		{
			for (int i = 0; i < this._prefabPools.Count; i++)
			{
				if (this._prefabPools[i].Contains(instance))
				{
					return this._prefabPools[i].prefab;
				}
			}
			return null;
		}

		public GameObject GetPrefab(GameObject instance)
		{
			for (int i = 0; i < this._prefabPools.Count; i++)
			{
				if (this._prefabPools[i].Contains(instance.transform))
				{
					return this._prefabPools[i].prefabGO;
				}
			}
			return null;
		}

//		[IteratorStateMachine(typeof(SpawnPool._003CListForAudioStop_003Ed__63))]
		private IEnumerator ListForAudioStop(AudioSource src)
		{
			GameObject gameObject = null;
			while (true)
			{
				int num = 0;
				switch (num)
				{
				case 0:
					yield return null;
					continue;
				case 1:
					gameObject = src.gameObject;
					goto IL_6A;
				case 2:
					goto IL_6A;
				}
				break;
				IL_6A:
				if (!src.isPlaying)
				{
					goto Block_2;
				}
				yield return null;
			}
			yield break;
			Block_2:
			if (!gameObject.activeInHierarchy)
			{
				src.Stop();
				yield break;
			}
			this.Despawn(src.transform);
			yield break;
		}

	//[IteratorStateMachine(typeof(SpawnPool._003CListenForEmitDespawn_003Ed__64))]
		private IEnumerator ListenForEmitDespawn(ParticleSystem emitter)
		{
			GameObject gameObject = null;
			while (true)
			{
				int num = 0;
				float num2 = 0f;
				switch (num)
				{
				case 0:
					yield return new WaitForSeconds(emitter.startDelay + 0.25f);
					continue;
				case 1:
					num2 = 0f;
					gameObject = emitter.gameObject;
					goto IL_CA;
				case 2:
					goto IL_CA;
				}
				break;
				IL_CA:
				if (!emitter.IsAlive(true) || !gameObject.activeInHierarchy)
				{
					goto IL_E5;
				}
				num2 += Time.deltaTime;
				if (num2 > this.maxParticleDespawnTime)
				{
					UnityEngine.Debug.LogWarning(string.Format("SpawnPool {0}: Timed out while listening for all particles to die. Waited for {1}sec.", this.poolName, this.maxParticleDespawnTime));
				}
				yield return null;
			}
			yield break;
			IL_E5:
			if (gameObject.activeInHierarchy)
			{
				this.Despawn(emitter.transform);
				emitter.Clear(true);
			}
			yield break;
		}

		public new string ToString()
		{
			List<string> list = new List<string>();
			foreach (Transform current in this._spawned)
			{
				list.Add(current.name);
			}
			return string.Join(", ", list.ToArray());
		}

		public bool Contains(Transform item)
		{
			throw new NotImplementedException("Use IsSpawned(Transform instance) instead.");
		}

		public void CopyTo(Transform[] array, int arrayIndex)
		{
			this._spawned.CopyTo(array, arrayIndex);
		}

		//[IteratorStateMachine(typeof(SpawnPool._003CGetEnumerator_003Ed__73))]
		public IEnumerator<Transform> GetEnumerator()
		{
			while (true)
			{
				int num = 0;
				int num3 = 0;
				if (num != 0)
				{
					if (num != 1)
					{
						break;
					}
					int num2 = num3;
					num3 = num2 + 1;
				}
				else
				{
					num3 = 0;
				}
				if (num3 >= this._spawned.Count)
				{
					goto Block_3;
				}
				yield return this._spawned[num3];
			}
			yield break;
			Block_3:
			yield break;
		}

//		[IteratorStateMachine(typeof(SpawnPool._003CSystem-Collections-IEnumerable-GetEnumerator_003Ed__74))]
		IEnumerator IEnumerable.GetEnumerator()
		{
			while (true)
			{
				int num = 0;
				int num3 = 0;
				if (num != 0)
				{
					if (num != 1)
					{
						break;
					}
					int num2 = num3;
					num3 = num2 + 1;
				}
				else
				{
					num3 = 0;
				}
				if (num3 >= this._spawned.Count)
				{
					goto Block_3;
				}
				yield return this._spawned[num3];
			}
			yield break;
			Block_3:
			yield break;
		}

		public int IndexOf(Transform item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, Transform item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		bool ICollection<Transform>.Remove(Transform item)
		{
			throw new NotImplementedException();
		}
	}
}
