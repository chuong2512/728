using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
	public class PrefabsDict : IDictionary<string, Transform>, ICollection<KeyValuePair<string, Transform>>, IEnumerable<KeyValuePair<string, Transform>>, IEnumerable
	{
		private Dictionary<string, Transform> _prefabs = new Dictionary<string, Transform>();

		public int Count
		{
			get
			{
				return this._prefabs.Count;
			}
		}

		public Transform this[string key]
		{
			get
			{
				Transform result;
				try
				{
					result = this._prefabs[key];
				}
				catch (KeyNotFoundException)
				{
					throw new KeyNotFoundException(string.Format("A Prefab with the name '{0}' not found. \nPrefabs={1}", key, this.ToString()));
				}
				return result;
			}
			set
			{
				throw new NotImplementedException("Read-only.");
			}
		}

		public ICollection<string> Keys
		{
			get
			{
				return this._prefabs.Keys;
			}
		}

		public ICollection<Transform> Values
		{
			get
			{
				return this._prefabs.Values;
			}
		}

		private bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		bool ICollection<KeyValuePair<string, Transform>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		public override string ToString()
		{
			string[] array = new string[this._prefabs.Count];
			this._prefabs.Keys.CopyTo(array, 0);
			return string.Format("[{0}]", string.Join(", ", array));
		}

		internal void _Add(string prefabName, Transform prefab)
		{
			this._prefabs.Add(prefabName, prefab);
		}

		internal bool _Remove(string prefabName)
		{
			return this._prefabs.Remove(prefabName);
		}

		internal void _Clear()
		{
			this._prefabs.Clear();
		}

		public bool ContainsKey(string prefabName)
		{
			return this._prefabs.ContainsKey(prefabName);
		}

		public bool TryGetValue(string prefabName, out Transform prefab)
		{
			return this._prefabs.TryGetValue(prefabName, out prefab);
		}

		public void Add(string key, Transform value)
		{
			throw new NotImplementedException("Read-Only");
		}

		public bool Remove(string prefabName)
		{
			throw new NotImplementedException("Read-Only");
		}

		public bool Contains(KeyValuePair<string, Transform> item)
		{
			throw new NotImplementedException("Use Contains(string prefabName) instead.");
		}

		public void Add(KeyValuePair<string, Transform> item)
		{
			throw new NotImplementedException("Read-only");
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		private void CopyTo(KeyValuePair<string, Transform>[] array, int arrayIndex)
		{
			throw new NotImplementedException("Cannot be copied");
		}

		void ICollection<KeyValuePair<string, Transform>>.CopyTo(KeyValuePair<string, Transform>[] array, int arrayIndex)
		{
			throw new NotImplementedException("Cannot be copied");
		}

		public bool Remove(KeyValuePair<string, Transform> item)
		{
			throw new NotImplementedException("Read-only");
		}

		public IEnumerator<KeyValuePair<string, Transform>> GetEnumerator()
		{
			return this._prefabs.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._prefabs.GetEnumerator();
		}
	}
}
