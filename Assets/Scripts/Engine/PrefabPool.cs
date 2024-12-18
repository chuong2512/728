using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Engine
{
	[Serializable]
	public class PrefabPool
	{
		private sealed class _CullDespawned_d__37 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int __1__state;

			private object __2__current;

			public PrefabPool __4__this;

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

			public _CullDespawned_d__37(int __1__state)
			{
				this.__1__state = __1__state;
			}

			void IDisposable.Dispose()
			{
			}

			bool IEnumerator.MoveNext()
			{
				int num = this.__1__state;
				PrefabPool prefabPool = this.__4__this;
				switch (num)
				{
				case 0:
					this.__1__state = -1;
					if (prefabPool.logMessages)
					{
						UnityEngine.Debug.Log(string.Format("SpawnPool {0} ({1}): CULLING TRIGGERED! Waiting {2}sec to begin checking for despawns...", prefabPool.spawnPool.poolName, prefabPool.prefab.name, prefabPool.cullDelay));
					}
					this.__2__current = new WaitForSeconds((float)prefabPool.cullDelay);
					this.__1__state = 1;
					return true;
				case 1:
					this.__1__state = -1;
					break;
				case 2:
					this.__1__state = -1;
					break;
				case 3:
					this.__1__state = -1;
					return false;
				default:
					return false;
				}
				if (prefabPool.totalCount <= prefabPool.cullAbove)
				{
					if (prefabPool.logMessages)
					{
						UnityEngine.Debug.Log(string.Format("SpawnPool {0} ({1}): CULLING FINISHED! Stopping", prefabPool.spawnPool.poolName, prefabPool.prefab.name));
					}
					prefabPool.cullingActive = false;
					this.__2__current = null;
					this.__1__state = 3;
					return true;
				}
				int num2 = 0;
				while (num2 < prefabPool.cullMaxPerPass && prefabPool.totalCount > prefabPool.cullAbove)
				{
					if (prefabPool._despawned.Count > 0)
					{
						Transform transform = prefabPool._despawned[0];
						prefabPool._despawned.RemoveAt(0);
						prefabPool.spawnPool.DestroyInstance(transform.gameObject);
						if (prefabPool.logMessages)
						{
							UnityEngine.Debug.Log(string.Format("SpawnPool {0} ({1}): CULLING to {2} instances. Now at {3}.", new object[]
							{
								prefabPool.spawnPool.poolName,
								prefabPool.prefab.name,
								prefabPool.cullAbove,
								prefabPool.totalCount
							}));
						}
					}
					else if (prefabPool.logMessages)
					{
						UnityEngine.Debug.Log(string.Format("SpawnPool {0} ({1}): CULLING waiting for despawn. Checking again in {2}sec", prefabPool.spawnPool.poolName, prefabPool.prefab.name, prefabPool.cullDelay));
						break;
					}
					num2++;
				}
				this.__2__current = new WaitForSeconds((float)prefabPool.cullDelay);
				this.__1__state = 2;
				return true;
			}

			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		private sealed class _PreloadOverTime_d__44 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int __1__state;

			private object __2__current;

			public PrefabPool __4__this;

			private int _remainder_5__2;

			private int _numPerFrame_5__3;

			private int _numThisFrame_5__4;

			private int _i_5__5;

			private int _n_5__6;

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

			public _PreloadOverTime_d__44(int __1__state)
			{
				this.__1__state = __1__state;
			}

			void IDisposable.Dispose()
			{
			}

			bool IEnumerator.MoveNext()
			{
				int num = this.__1__state;
				PrefabPool prefabPool = this.__4__this;
				int num3;
				switch (num)
				{
				case 0:
					this.__1__state = -1;
					this.__2__current = new WaitForSeconds(prefabPool.preloadDelay);
					this.__1__state = 1;
					return true;
				case 1:
				{
					this.__1__state = -1;
					int num2 = prefabPool.preloadAmount - prefabPool.totalCount;
					if (num2 <= 0)
					{
						return false;
					}
					this._remainder_5__2 = num2 % prefabPool.preloadFrames;
					this._numPerFrame_5__3 = num2 / prefabPool.preloadFrames;
					prefabPool.forceLoggingSilent = true;
					this._i_5__5 = 0;
					goto IL_135;
				}
				case 2:
					this.__1__state = -1;
					num3 = this._n_5__6;
					this._n_5__6 = num3 + 1;
					break;
				default:
					return false;
				}
				IL_107:
				if (this._n_5__6 < this._numThisFrame_5__4)
				{
					Transform transform = prefabPool.SpawnNew();
					if (transform != null)
					{
						prefabPool.DespawnInstance(transform, false);
					}
					this.__2__current = null;
					this.__1__state = 2;
					return true;
				}
				if (prefabPool.totalCount > prefabPool.preloadAmount)
				{
					goto IL_146;
				}
				num3 = this._i_5__5;
				this._i_5__5 = num3 + 1;
				IL_135:
				if (this._i_5__5 < prefabPool.preloadFrames)
				{
					this._numThisFrame_5__4 = this._numPerFrame_5__3;
					if (this._i_5__5 == prefabPool.preloadFrames - 1)
					{
						this._numThisFrame_5__4 += this._remainder_5__2;
					}
					this._n_5__6 = 0;
					goto IL_107;
				}
				IL_146:
				prefabPool.forceLoggingSilent = false;
				return false;
			}

			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		public Transform prefab;

		internal GameObject prefabGO;

		public int preloadAmount = 1;

		public bool preloadTime;

		public int preloadFrames = 2;

		public float preloadDelay;

		public bool limitInstances;

		public int limitAmount = 100;

		public bool limitFIFO;

		public bool cullDespawned;

		public int cullAbove = 50;

		public int cullDelay = 60;

		public int cullMaxPerPass = 5;

		public bool _logMessages;

		private bool forceLoggingSilent;

		public SpawnPool spawnPool;

		private bool cullingActive;

		internal List<Transform> _spawned = new List<Transform>();

		internal List<Transform> _despawned = new List<Transform>();

		private bool _preloaded;

		public bool logMessages
		{
			get
			{
				if (this.forceLoggingSilent)
				{
					return false;
				}
				if (this.spawnPool.logMessages)
				{
					return this.spawnPool.logMessages;
				}
				return this._logMessages;
			}
		}

		public List<Transform> spawned
		{
			get
			{
				return new List<Transform>(this._spawned);
			}
		}

		public List<Transform> despawned
		{
			get
			{
				return new List<Transform>(this._despawned);
			}
		}

		public int totalCount
		{
			get
			{
				return 0 + this._spawned.Count + this._despawned.Count;
			}
		}

		internal bool preloaded
		{
			get
			{
				return this._preloaded;
			}
			private set
			{
				this._preloaded = value;
			}
		}

		public PrefabPool(Transform prefab)
		{
			this.prefab = prefab;
			this.prefabGO = prefab.gameObject;
		}

		public PrefabPool()
		{
		}

		internal void inspectorInstanceConstructor()
		{
			this.prefabGO = this.prefab.gameObject;
			this._spawned = new List<Transform>();
			this._despawned = new List<Transform>();
		}

		internal void SelfDestruct()
		{
			if (this.logMessages)
			{
				UnityEngine.Debug.Log(string.Format("SpawnPool {0}: Cleaning up PrefabPool for {1}...", this.spawnPool.poolName, this.prefabGO.name));
			}
			foreach (Transform current in this._despawned)
			{
				if (current != null && this.spawnPool != null)
				{
					this.spawnPool.DestroyInstance(current.gameObject);
				}
			}
			foreach (Transform current2 in this._spawned)
			{
				if (current2 != null && this.spawnPool != null)
				{
					this.spawnPool.DestroyInstance(current2.gameObject);
				}
			}
			this._spawned.Clear();
			this._despawned.Clear();
			this.prefab = null;
			this.prefabGO = null;
			this.spawnPool = null;
		}

		internal bool DespawnInstance(Transform xform)
		{
			return this.DespawnInstance(xform, true);
		}

		internal bool DespawnInstance(Transform xform, bool sendEventMessage)
		{
			if (this.logMessages)
			{
				UnityEngine.Debug.Log(string.Format("SpawnPool {0} ({1}): Despawning '{2}'", this.spawnPool.poolName, this.prefab.name, xform.name));
			}
			this._spawned.Remove(xform);
			this._despawned.Add(xform);
			if (sendEventMessage)
			{
				xform.gameObject.BroadcastMessage("OnDespawned", this.spawnPool, SendMessageOptions.DontRequireReceiver);
			}
			xform.gameObject.SetActive(false);
			if (!this.cullingActive && this.cullDespawned && this.totalCount > this.cullAbove)
			{
				this.cullingActive = true;
				this.spawnPool.StartCoroutine(this.CullDespawned());
			}
			return true;
		}

//		[IteratorStateMachine(typeof(PrefabPool._003CCullDespawned_003Ed__37))]
		internal IEnumerator CullDespawned()
		{
			while (true)
			{
				int num = 0;
				switch (num)
				{
				case 0:
					if (this.logMessages)
					{
						UnityEngine.Debug.Log(string.Format("SpawnPool {0} ({1}): CULLING TRIGGERED! Waiting {2}sec to begin checking for despawns...", this.spawnPool.poolName, this.prefab.name, this.cullDelay));
					}
					yield return new WaitForSeconds((float)this.cullDelay);
					continue;
				case 1:
					goto IL_1A5;
				case 2:
					goto IL_1A5;
				case 3:
					goto IL_1FA;
				}
				break;
				IL_1A5:
				if (this.totalCount <= this.cullAbove)
				{
					if (this.logMessages)
					{
						UnityEngine.Debug.Log(string.Format("SpawnPool {0} ({1}): CULLING FINISHED! Stopping", this.spawnPool.poolName, this.prefab.name));
					}
					this.cullingActive = false;
					yield return null;
				}
				else
				{
					int num2 = 0;
					while (num2 < this.cullMaxPerPass && this.totalCount > this.cullAbove)
					{
						if (this._despawned.Count > 0)
						{
							Transform transform = this._despawned[0];
							this._despawned.RemoveAt(0);
							this.spawnPool.DestroyInstance(transform.gameObject);
							if (this.logMessages)
							{
								UnityEngine.Debug.Log(string.Format("SpawnPool {0} ({1}): CULLING to {2} instances. Now at {3}.", new object[]
								{
									this.spawnPool.poolName,
									this.prefab.name,
									this.cullAbove,
									this.totalCount
								}));
							}
						}
						else if (this.logMessages)
						{
							UnityEngine.Debug.Log(string.Format("SpawnPool {0} ({1}): CULLING waiting for despawn. Checking again in {2}sec", this.spawnPool.poolName, this.prefab.name, this.cullDelay));
							break;
						}
						num2++;
					}
					yield return new WaitForSeconds((float)this.cullDelay);
				}
			}
			yield break;
			IL_1FA:
			yield break;
		}

		internal Transform SpawnInstance(Vector3 pos, Quaternion rot)
		{
			if (this.limitInstances && this.limitFIFO && this._spawned.Count >= this.limitAmount)
			{
				Transform transform = this._spawned[0];
				if (this.logMessages)
				{
					UnityEngine.Debug.Log(string.Format("SpawnPool {0} ({1}): LIMIT REACHED! FIFO=True. Calling despawning for {2}...", this.spawnPool.poolName, this.prefab.name, transform));
				}
				this.DespawnInstance(transform);
				this.spawnPool._spawned.Remove(transform);
			}
			Transform transform2;
			if (this._despawned.Count == 0)
			{
				transform2 = this.SpawnNew(pos, rot);
			}
			else
			{
				transform2 = this._despawned[0];
				this._despawned.RemoveAt(0);
				this._spawned.Add(transform2);
				if (transform2 == null)
				{
					throw new MissingReferenceException("Make sure you didn't delete a despawned instance directly.");
				}
				if (this.logMessages)
				{
					UnityEngine.Debug.Log(string.Format("SpawnPool {0} ({1}): respawning '{2}'.", this.spawnPool.poolName, this.prefab.name, transform2.name));
				}
				transform2.position = pos;
				transform2.rotation = rot;
				transform2.gameObject.SetActive(true);
			}
			return transform2;
		}

		public Transform SpawnNew()
		{
			return this.SpawnNew(Vector3.zero, Quaternion.identity);
		}

		public Transform SpawnNew(Vector3 pos, Quaternion rot)
		{
			if (this.limitInstances && this.totalCount >= this.limitAmount)
			{
				if (this.logMessages)
				{
					UnityEngine.Debug.Log(string.Format("SpawnPool {0} ({1}): LIMIT REACHED! Not creating new instances! (Returning null)", this.spawnPool.poolName, this.prefab.name));
				}
				return null;
			}
			if (pos == Vector3.zero)
			{
				pos = this.spawnPool.group.position;
			}
			if (rot == Quaternion.identity)
			{
				rot = this.spawnPool.group.rotation;
			}
			Transform transform = this.spawnPool.InstantiatePrefab(this.prefabGO, pos, rot).transform;
			this.nameInstance(transform);
			if (!this.spawnPool.dontReparent)
			{
				bool worldPositionStays = !(transform is RectTransform);
				transform.SetParent(this.spawnPool.group, worldPositionStays);
			}
			if (this.spawnPool.matchPoolScale)
			{
				transform.localScale = Vector3.one;
			}
			if (this.spawnPool.matchPoolLayer)
			{
				this.SetRecursively(transform, this.spawnPool.gameObject.layer);
			}
			this._spawned.Add(transform);
			if (this.logMessages)
			{
				UnityEngine.Debug.Log(string.Format("SpawnPool {0} ({1}): Spawned new instance '{2}'.", this.spawnPool.poolName, this.prefab.name, transform.name));
			}
			return transform;
		}

		private void SetRecursively(Transform xform, int layer)
		{
			xform.gameObject.layer = layer;
			foreach (Transform xform2 in xform)
			{
				this.SetRecursively(xform2, layer);
			}
		}

		internal void AddUnpooled(Transform inst, bool despawn)
		{
			this.nameInstance(inst);
			if (despawn)
			{
				inst.gameObject.SetActive(false);
				this._despawned.Add(inst);
				return;
			}
			this._spawned.Add(inst);
		}

		internal void PreloadInstances()
		{
			if (this.preloaded)
			{
				UnityEngine.Debug.Log(string.Format("SpawnPool {0} ({1}): Already preloaded! You cannot preload twice. If you are running this through code, make sure it isn't also defined in the Inspector.", this.spawnPool.poolName, this.prefab.name));
				return;
			}
			this.preloaded = true;
			if (this.prefab == null)
			{
				UnityEngine.Debug.LogError(string.Format("SpawnPool {0} ({1}): Prefab cannot be null.", this.spawnPool.poolName, this.prefab.name));
				return;
			}
			if (this.limitInstances && this.preloadAmount > this.limitAmount)
			{
				UnityEngine.Debug.LogWarning(string.Format("SpawnPool {0} ({1}): You turned ON 'Limit Instances' and entered a 'Limit Amount' greater than the 'Preload Amount'! Setting preload amount to limit amount.", this.spawnPool.poolName, this.prefab.name));
				this.preloadAmount = this.limitAmount;
			}
			if (this.cullDespawned && this.preloadAmount > this.cullAbove)
			{
				UnityEngine.Debug.LogWarning(string.Format("SpawnPool {0} ({1}): You turned ON Culling and entered a 'Cull Above' threshold greater than the 'Preload Amount'! This will cause the culling feature to trigger immediatly, which is wrong conceptually. Only use culling for extreme situations. See the docs.", this.spawnPool.poolName, this.prefab.name));
			}
			if (this.preloadTime)
			{
				if (this.preloadFrames > this.preloadAmount)
				{
					UnityEngine.Debug.LogWarning(string.Format("SpawnPool {0} ({1}): Preloading over-time is on but the frame duration is greater than the number of instances to preload. The minimum spawned per frame is 1, so the maximum time is the same as the number of instances. Changing the preloadFrames value...", this.spawnPool.poolName, this.prefab.name));
					this.preloadFrames = this.preloadAmount;
				}
				this.spawnPool.StartCoroutine(this.PreloadOverTime());
				return;
			}
			this.forceLoggingSilent = true;
			while (this.totalCount < this.preloadAmount)
			{
				Transform xform = this.SpawnNew();
				this.DespawnInstance(xform, false);
			}
			this.forceLoggingSilent = false;
		}

	//[IteratorStateMachine(typeof(PrefabPool._003CPreloadOverTime_003Ed__44))]
		private IEnumerator PreloadOverTime()
		{
			while (true)
			{
				int num = 0;
				int num4 = 0;
				int num5 = 0;
				int num7 = 0;
				int num6 = 0;
				switch (num)
				{
				case 0:
					yield return new WaitForSeconds(this.preloadDelay);
					continue;
				case 1:
				{
					int num2 = this.preloadAmount - this.totalCount;
					if (num2 <= 0)
					{
						goto Block_2;
					}
					int num3 = num2 % this.preloadFrames;
					num4 = num2 / this.preloadFrames;
					this.forceLoggingSilent = true;
					num5 = 0;
					goto IL_135;
				}
				case 2:
					num6 = num7;
					num7 = num6 + 1;
					goto IL_107;
				}
				break;
				IL_107:
				int num8 = 0;
				if (num7 < num8)
				{
					Transform transform = this.SpawnNew();
					if (transform != null)
					{
						this.DespawnInstance(transform, false);
					}
					yield return null;
					continue;
				}
				if (this.totalCount > this.preloadAmount)
				{
					goto IL_146;
				}
				num6 = num5;
				num5 = num6 + 1;
				IL_135:
				if (num5 >= this.preloadFrames)
				{
					goto IL_146;
				}
				num8 = num4;
				if (num5 == this.preloadFrames - 1)
				{
					int num3 = 0;
					num8 += num3;
				}
				num7 = 0;
				goto IL_107;
			}
			yield break;
			Block_2:
			yield break;
			IL_146:
			this.forceLoggingSilent = false;
			yield break;
		}

		public bool Contains(Transform transform)
		{
			if (this.prefabGO == null)
			{
				UnityEngine.Debug.LogError(string.Format("SpawnPool {0}: PrefabPool.prefabGO is null", this.spawnPool.poolName));
			}
			return this.spawned.Contains(transform) || this.despawned.Contains(transform);
		}

		private void nameInstance(Transform instance)
		{
			instance.name += (this.totalCount + 1).ToString("#000");
		}
	}
}
