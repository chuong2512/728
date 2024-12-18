using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Engine
{
	public static class AssetBundleManager
	{
		private class AssetBundleRef
		{
			public WWW www;

			public AssetBundle assetBundle;

			public string hashCode;

			public string url;

			public AssetBundleRef(string strUrlIn, string strHashCode)
			{
				this.url = strUrlIn;
				this.hashCode = strHashCode;
			}
		}

		private sealed class _DownloadAssetBundle_d__8 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int __1__state;

			private object __2__current;

			public string bundleUrl;

			public int version;

			private string _url_5__2;

			private string _keyName_5__3;

			private WWW _www_5__4;

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

			public _DownloadAssetBundle_d__8(int __1__state)
			{
				this.__1__state = __1__state;
			}

			void IDisposable.Dispose()
			{
			}

			bool IEnumerator.MoveNext()
			{
				switch (this.__1__state)
				{
				case 0:
					this.__1__state = -1;
					this._url_5__2 = AssetBundleManager.WEB_SERVER_URL + "/AssetBundle/" + this.bundleUrl;
					this._keyName_5__3 = this.bundleUrl;
					if (!AssetBundleManager.m_dicAssetBundleDownloaded.ContainsKey(this._keyName_5__3) || AssetBundleManager.m_dicAssetBundleDownloaded[this._keyName_5__3])
					{
						UnityEngine.Debug.LogError("url: " + this._url_5__2 + " keyName:" + this._keyName_5__3);
						if (!AssetBundleManager.m_dicAssetBundleDownloaded.ContainsKey(this._keyName_5__3))
						{
							AssetBundleManager.m_dicAssetBundleDownloaded.Add(this._keyName_5__3, false);
						}
						if (!AssetBundleManager.m_dicAssetBundle.ContainsKey(this._keyName_5__3))
						{
							this._www_5__4 = WWW.LoadFromCacheOrDownload(this._url_5__2, this.version);
							this.__2__current = this._www_5__4;
							this.__1__state = 2;
							return true;
						}
						return false;
					}
					break;
				case 1:
					this.__1__state = -1;
					break;
				case 2:
					this.__1__state = -1;
					if (this._www_5__4.error != null)
					{
						UnityEngine.Debug.LogError("下载错误 url:" + this._url_5__2 + " error:" + this._www_5__4.error);
					}
					AssetBundleManager.m_dicAssetBundle.Add(this._keyName_5__3, this._www_5__4.assetBundle);
					AssetBundleManager.m_dicAssetBundleDownloaded[this._keyName_5__3] = true;
					this._www_5__4 = null;
					return false;
				default:
					return false;
				}
				if (!AssetBundleManager.m_dicAssetBundleDownloaded[this._keyName_5__3])
				{
					this.__2__current = new WaitForSeconds(0.05f);
					this.__1__state = 1;
					return true;
				}
				return false;
			}

			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		private sealed class _DownloadAssetBundle_d__10 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int __1__state;

			private object __2__current;

			public string url;

			public string hashCode;

			public bool bEncrypt;

			private string _keyName_5__2;

			private AssetBundleManager.AssetBundleRef _abRef_5__3;

			private string _savePath_5__4;

			private AssetBundleCreateRequest _assetRequest_5__5;

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

			public _DownloadAssetBundle_d__10(int __1__state)
			{
				this.__1__state = __1__state;
			}

			void IDisposable.Dispose()
			{
			}

			bool IEnumerator.MoveNext()
			{
				switch (this.__1__state)
				{
				case 0:
				{
					this.__1__state = -1;
					this._keyName_5__2 = this.url + this.hashCode;
					if (AssetBundleManager.m_dicAssetBundleRefs.ContainsKey(this._keyName_5__2))
					{
						this.__2__current = null;
						this.__1__state = 1;
						return true;
					}
					this._abRef_5__3 = new AssetBundleManager.AssetBundleRef(this.url, this.hashCode);
					this._savePath_5__4 = AssetDownFileRecord.Instance.GetCacheDataSavePath(this.url);
					if (!AssetDownFileRecord.Instance.HasCache(this.url, this.hashCode))
					{
						string str = string.Empty;
						if (this.url.StartsWith("http"))
						{
							str = "?random=" + UnityEngine.Random.value;
						}
						this._abRef_5__3.www = new WWW(this.url + str);
						AssetBundleManager.m_dicAssetBundleRefs.Add(this._keyName_5__2, this._abRef_5__3);
						goto IL_28E;
					}
					string text = "file://" + this._savePath_5__4;
					this._abRef_5__3.www = new WWW(text);
					break;
				}
				case 1:
					this.__1__state = -1;
					return false;
				case 2:
					this.__1__state = -1;
					break;
				case 3:
					this.__1__state = -1;
					if (this.bEncrypt)
					{
						byte[] binary = SystemManager.DecryptBundleFile(this._abRef_5__3.www.bytes);
						this._assetRequest_5__5 = AssetBundle.LoadFromMemoryAsync(binary);
						this.__2__current = this._assetRequest_5__5;
						this.__1__state = 4;
						return true;
					}
					this._abRef_5__3.assetBundle = this._abRef_5__3.www.assetBundle;
					goto IL_3C6;
				case 4:
					this.__1__state = -1;
					if (this._assetRequest_5__5.isDone)
					{
						this._abRef_5__3.assetBundle = this._assetRequest_5__5.assetBundle;
					}
					this._assetRequest_5__5 = null;
					goto IL_3C6;
				case 5:
					this.__1__state = -1;
					goto IL_28E;
				case 6:
					this.__1__state = -1;
					if (this.bEncrypt)
					{
						byte[] binary2 = SystemManager.DecryptBundleFile(this._abRef_5__3.www.bytes);
						this._assetRequest_5__5 = AssetBundle.LoadFromMemoryAsync(binary2);
						this.__2__current = this._assetRequest_5__5;
						this.__1__state = 7;
						return true;
					}
					this._abRef_5__3.assetBundle = this._abRef_5__3.www.assetBundle;
					goto IL_3C6;
				case 7:
					this.__1__state = -1;
					if (this._assetRequest_5__5.isDone)
					{
						this._abRef_5__3.assetBundle = this._assetRequest_5__5.assetBundle;
					}
					this._assetRequest_5__5 = null;
					goto IL_3C6;
				default:
					return false;
				}
				if (this._abRef_5__3.www.isDone)
				{
					if (this._abRef_5__3.www.error != null)
					{
						UnityEngine.Debug.LogError("error: " + this._abRef_5__3.www.error);
					}
					AssetBundleManager.m_dicAssetBundleRefs.Add(this._keyName_5__2, this._abRef_5__3);
					this.__2__current = this._abRef_5__3.assetBundle;
					this.__1__state = 3;
					return true;
				}
				this.__2__current = new WaitForSeconds(0.5f);
				this.__1__state = 2;
				return true;
				IL_28E:
				if (this._abRef_5__3.www.isDone)
				{
					if (this._abRef_5__3.www.error == null)
					{
						if (this.url.StartsWith("http"))
						{
							File.WriteAllBytes(this._savePath_5__4, this._abRef_5__3.www.bytes);
							AssetDownFileRecord.Instance.SaveCacheData(this.url, this.hashCode);
						}
					}
					else
					{
						UnityEngine.Debug.LogError("error: " + this._abRef_5__3.www.error);
					}
					this.__2__current = this._abRef_5__3.www;
					this.__1__state = 6;
					return true;
				}
				this.__2__current = null;
				this.__1__state = 5;
				return true;
				IL_3C6:
				this._abRef_5__3 = null;
				this._savePath_5__4 = null;
				return false;
			}

			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		private static Dictionary<string, AssetBundle> m_dicAssetBundle;

		private static Dictionary<string, bool> m_dicAssetBundleDownloaded;

		private static Dictionary<string, AssetBundleManager.AssetBundleRef> m_dicAssetBundleRefs;

		public static string WEB_SERVER_URL;

		static AssetBundleManager()
		{
			AssetBundleManager.m_dicAssetBundle = new Dictionary<string, AssetBundle>();
			AssetBundleManager.m_dicAssetBundleDownloaded = new Dictionary<string, bool>();
			AssetBundleManager.WEB_SERVER_URL = string.Empty;
			AssetBundleManager.m_dicAssetBundleRefs = new Dictionary<string, AssetBundleManager.AssetBundleRef>();
		}

		public static AssetBundle GetAssetBundle(string bundleUrl)
		{
			if (AssetBundleManager.m_dicAssetBundle.ContainsKey(bundleUrl))
			{
				return AssetBundleManager.m_dicAssetBundle[bundleUrl];
			}
			return null;
		}

		public static AssetBundle GetAssetBundle(string url, string hashCode)
		{
			string key = url + hashCode;
			AssetBundleManager.AssetBundleRef assetBundleRef;
			if (AssetBundleManager.m_dicAssetBundleRefs.TryGetValue(key, out assetBundleRef))
			{
				return assetBundleRef.assetBundle;
			}
			return null;
		}

//		[IteratorStateMachine(typeof(AssetBundleManager._003CDownloadAssetBundle_003Ed__8))]
		public static IEnumerator DownloadAssetBundle(string bundleUrl, int version)
		{
			string text = AssetBundleManager.WEB_SERVER_URL + "/AssetBundle/" + bundleUrl;
			if (AssetBundleManager.m_dicAssetBundleDownloaded.ContainsKey(bundleUrl) && !AssetBundleManager.m_dicAssetBundleDownloaded[bundleUrl])
			{
				while (!AssetBundleManager.m_dicAssetBundleDownloaded[bundleUrl])
				{
					yield return new WaitForSeconds(0.05f);
				}
			}
			else
			{
				UnityEngine.Debug.LogError("url: " + text + " keyName:" + bundleUrl);
				if (!AssetBundleManager.m_dicAssetBundleDownloaded.ContainsKey(bundleUrl))
				{
					AssetBundleManager.m_dicAssetBundleDownloaded.Add(bundleUrl, false);
				}
				if (!AssetBundleManager.m_dicAssetBundle.ContainsKey(bundleUrl))
				{
					WWW wWW = WWW.LoadFromCacheOrDownload(text, version);
					yield return wWW;
					if (wWW.error != null)
					{
						UnityEngine.Debug.LogError("下载错误 url:" + text + " error:" + wWW.error);
					}
					AssetBundleManager.m_dicAssetBundle.Add(bundleUrl, wWW.assetBundle);
					AssetBundleManager.m_dicAssetBundleDownloaded[bundleUrl] = true;
					wWW = null;
				}
			}
			yield break;
		}

		public static void UnloadAll(bool unloadAllOjects = true)
		{
			foreach (AssetBundle current in AssetBundleManager.m_dicAssetBundle.Values)
			{
				if (current != null)
				{
					current.Unload(unloadAllOjects);
				}
			}
			AssetBundleManager.m_dicAssetBundle.Clear();
			AssetBundleManager.m_dicAssetBundleDownloaded.Clear();
		}

	//	[IteratorStateMachine(typeof(AssetBundleManager._003CDownloadAssetBundle_003Ed__10))]
		public static IEnumerator DownloadAssetBundle(string url, string hashCode, bool bEncrypt)
		{
			string key = url + hashCode;
			if (AssetBundleManager.m_dicAssetBundleRefs.ContainsKey(key))
			{
				yield return null;
			}
			else
			{
				AssetBundleManager.AssetBundleRef assetBundleRef = new AssetBundleManager.AssetBundleRef(url, hashCode);
				string text = AssetDownFileRecord.Instance.GetCacheDataSavePath(url);
				if (AssetDownFileRecord.Instance.HasCache(url, hashCode))
				{
					string url2 = "file://" + text;
					assetBundleRef.www = new WWW(url2);
					while (!assetBundleRef.www.isDone)
					{
						yield return new WaitForSeconds(0.5f);
					}
					if (assetBundleRef.www.error != null)
					{
						UnityEngine.Debug.LogError("error: " + assetBundleRef.www.error);
					}
					AssetBundleManager.m_dicAssetBundleRefs.Add(key, assetBundleRef);
					yield return assetBundleRef.assetBundle;
					if (bEncrypt)
					{
						byte[] binary = SystemManager.DecryptBundleFile(assetBundleRef.www.bytes);
						AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.LoadFromMemoryAsync(binary);
						yield return assetBundleCreateRequest;
						if (assetBundleCreateRequest.isDone)
						{
							assetBundleRef.assetBundle = assetBundleCreateRequest.assetBundle;
						}
						assetBundleCreateRequest = null;
					}
					else
					{
						assetBundleRef.assetBundle = assetBundleRef.www.assetBundle;
					}
				}
				else
				{
					string str = string.Empty;
					if (url.StartsWith("http"))
					{
						str = "?random=" + UnityEngine.Random.value;
					}
					assetBundleRef.www = new WWW(url + str);
					AssetBundleManager.m_dicAssetBundleRefs.Add(key, assetBundleRef);
					while (!assetBundleRef.www.isDone)
					{
						yield return null;
					}
					if (assetBundleRef.www.error == null)
					{
						if (url.StartsWith("http"))
						{
							File.WriteAllBytes(text, assetBundleRef.www.bytes);
							AssetDownFileRecord.Instance.SaveCacheData(url, hashCode);
						}
					}
					else
					{
						UnityEngine.Debug.LogError("error: " + assetBundleRef.www.error);
					}
					yield return assetBundleRef.www;
					if (bEncrypt)
					{
						byte[] binary2 = SystemManager.DecryptBundleFile(assetBundleRef.www.bytes);
						AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.LoadFromMemoryAsync(binary2);
						yield return assetBundleCreateRequest;
						if (assetBundleCreateRequest.isDone)
						{
							assetBundleRef.assetBundle = assetBundleCreateRequest.assetBundle;
						}
						assetBundleCreateRequest = null;
					}
					else
					{
						assetBundleRef.assetBundle = assetBundleRef.www.assetBundle;
					}
				}
				assetBundleRef = null;
				text = null;
			}
			yield break;
		}

		public static void Unload(string url, string hashCode, bool allObjects)
		{
			string key = url + hashCode;
			AssetBundleManager.AssetBundleRef assetBundleRef;
			if (AssetBundleManager.m_dicAssetBundleRefs.TryGetValue(key, out assetBundleRef))
			{
				if (assetBundleRef != null && null != assetBundleRef.assetBundle)
				{
					assetBundleRef.assetBundle.Unload(allObjects);
					assetBundleRef.assetBundle = null;
				}
				AssetBundleManager.m_dicAssetBundleRefs.Remove(key);
			}
		}

		public static void UnloadAllMirroring(string hashCode)
		{
			AssetBundleManager.AssetBundleRef[] array = new AssetBundleManager.AssetBundleRef[AssetBundleManager.m_dicAssetBundleRefs.Count];
			int num = 0;
			foreach (string current in AssetBundleManager.m_dicAssetBundleRefs.Keys)
			{
				AssetBundleManager.m_dicAssetBundleRefs.TryGetValue(current, out array[num]);
				num++;
			}
			num--;
			for (int i = 0; i < array.Length; i++)
			{
				AssetBundleManager.m_dicAssetBundleRefs.Remove(array[i].url + hashCode);
				if (array[i].assetBundle)
				{
					array[i].assetBundle.Unload(true);
				}
				array[i] = null;
			}
		}

		public static float GetDownloadProgress(string url, string hashCode)
		{
			string key = url + hashCode;
			AssetBundleManager.AssetBundleRef assetBundleRef;
			if (AssetBundleManager.m_dicAssetBundleRefs.TryGetValue(key, out assetBundleRef))
			{
				return assetBundleRef.www.progress;
			}
			return 0f;
		}

		public static void StopDownload(string url, string hashCode)
		{
			string key = url + hashCode;
			AssetBundleManager.AssetBundleRef assetBundleRef;
			if (AssetBundleManager.m_dicAssetBundleRefs.TryGetValue(key, out assetBundleRef))
			{
				assetBundleRef.www.Dispose();
			}
		}
	}
}
