using JsonFx.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Engine
{
	public class AssetLoadManager
	{
		public delegate void CsvLoadOverCall();

		public delegate void AssetLoadOverCall(AssetLoadData data);

		private sealed class _LoadManifestAssetByMemory_d__33 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int __1__state;

			private object __2__current;

			public AssetLoadManager __4__this;

			public string assetPath;

			public AssetLoadManager.AssetLoadOverCall callback;

			public string assetName;

			public bool bUnLoad;

			private AssetLoadData _assetLoadData_5__2;

			private string[] _depends_5__3;

			private int _i_5__4;

			private int _len_5__5;

			private AssetLoadData _assetData_5__6;

			private AssetBundleCreateRequest _asset_5__7;

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

			public _LoadManifestAssetByMemory_d__33(int __1__state)
			{
				this.__1__state = __1__state;
			}

			void IDisposable.Dispose()
			{
			}

			bool IEnumerator.MoveNext()
			{
				int num = this.__1__state;
				AssetLoadManager assetLoadManager = this.__4__this;
				switch (num)
				{
				case 0:
					this.__1__state = -1;
					if (assetLoadManager.m_dicLoadAssetData.ContainsKey(this.assetPath) && assetLoadManager.m_dicLoadAssetData[this.assetPath].m_loadedStatus == AssetStatus.CanUse)
					{
						this._assetLoadData_5__2 = assetLoadManager.m_dicLoadAssetData[this.assetPath];
						if (this.callback != null)
						{
							this.callback(this._assetLoadData_5__2);
						}
					}
					else if (assetLoadManager.m_dicLoadAssetData.ContainsKey(this.assetPath))
					{
						this._assetLoadData_5__2 = assetLoadManager.m_dicLoadAssetData[this.assetPath];
					}
					else
					{
						this._assetLoadData_5__2 = new AssetLoadData();
						assetLoadManager.m_dicLoadAssetData.Add(this.assetPath, this._assetLoadData_5__2);
					}
					this._assetLoadData_5__2.m_strAssetPath = this.assetPath;
					if (assetLoadManager.m_dicLoadAssetData[this.assetPath].m_loadedStatus != AssetStatus.CanUse)
					{
						this._depends_5__3 = assetLoadManager.AssetBundleManifest.GetAllDependencies(this.assetPath);
						this._i_5__4 = 0;
						this._len_5__5 = this._depends_5__3.Length;
						goto IL_306;
					}
					return false;
				case 1:
					this.__1__state = -1;
					if (this._asset_5__7.isDone)
					{
						this._assetData_5__6.m_assetBundle = this._asset_5__7.assetBundle;
						this._assetData_5__6.m_loadedStatus = AssetStatus.CanUse;
					}
					this._asset_5__7 = null;
					goto IL_2ED;
				case 2:
					this.__1__state = -1;
					break;
				default:
					return false;
				}
				IL_2DF:
				if (this._assetData_5__6.m_loadedStatus != AssetStatus.CanUse)
				{
					this.__2__current = 1;
					this.__1__state = 2;
					return true;
				}
				IL_2ED:
				this._assetData_5__6 = null;
				IL_2F4:
				int num2 = this._i_5__4;
				this._i_5__4 = num2 + 1;
				IL_306:
				if (this._i_5__4 >= this._len_5__5)
				{
					MonoManager.Instance.StartCoroutine(assetLoadManager.GetAssetBundleFromMemory(this._assetLoadData_5__2, this.assetName, this.callback, this.bUnLoad));
					this._depends_5__3 = null;
				}
				else
				{
					if (assetLoadManager.m_dicLoadAssetData.ContainsKey(this._depends_5__3[this._i_5__4]))
					{
						this._assetData_5__6 = assetLoadManager.m_dicLoadAssetData[this._depends_5__3[this._i_5__4]];
						if (this._assetData_5__6.m_loadedStatus == AssetStatus.CanUse)
						{
							goto IL_2F4;
						}
					}
					else
					{
						this._assetData_5__6 = new AssetLoadData();
						assetLoadManager.m_dicLoadAssetData.Add(this._depends_5__3[this._i_5__4], this._assetData_5__6);
					}
					this._assetData_5__6.m_strAssetPath = this._depends_5__3[this._i_5__4];
					if (this._assetData_5__6.m_loadedStatus == AssetStatus.NotReady)
					{
						this._assetData_5__6.m_loadedStatus = AssetStatus.Downloading;
						string text = assetLoadManager.m_localAssetFromFilePath + this._assetData_5__6.m_strAssetPath;
						if (!File.Exists(text))
						{
							UnityEngine.Debug.LogError("资源不存在, assetPath = " + text);
						}
						FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.ReadWrite);
						byte[] array = new byte[fileStream.Length];
						fileStream.Read(array, 0, (int)fileStream.Length);
						fileStream.Close();
						byte[] binary = array;
						if (assetLoadManager.m_assetImportData.m_bEncrypt)
						{
							binary = SystemManager.DecryptBundleFile(array);
						}
						this._asset_5__7 = AssetBundle.LoadFromMemoryAsync(binary);
						this.__2__current = this._asset_5__7;
						this.__1__state = 1;
						return true;
					}
					if (this._assetData_5__6.m_loadedStatus == AssetStatus.Downloading)
					{
						goto IL_2DF;
					}
					goto IL_2ED;
				}
				return false;
			}

			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		private sealed class _LoadManifestAssetByWWW_d__34 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int __1__state;

			private object __2__current;

			public AssetLoadManager __4__this;

			public string assetPath;

			public AssetLoadManager.AssetLoadOverCall callback;

			public string assetName;

			public bool bUnLoad;

			private AssetLoadData _assetLoadData_5__2;

			private string[] _depends_5__3;

			private int _i_5__4;

			private int _len_5__5;

			private AssetLoadData _assetData_5__6;

			private WWW _www_5__7;

			private AssetBundleCreateRequest _assetRequest_5__8;

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

			public _LoadManifestAssetByWWW_d__34(int __1__state)
			{
				this.__1__state = __1__state;
			}

			void IDisposable.Dispose()
			{
			}

			bool IEnumerator.MoveNext()
			{
				int num = this.__1__state;
				AssetLoadManager assetLoadManager = this.__4__this;
				switch (num)
				{
				case 0:
					this.__1__state = -1;
					if (assetLoadManager.m_dicLoadAssetData.ContainsKey(this.assetPath) && assetLoadManager.m_dicLoadAssetData[this.assetPath].m_loadedStatus == AssetStatus.CanUse)
					{
						this._assetLoadData_5__2 = assetLoadManager.m_dicLoadAssetData[this.assetPath];
						if (this.callback != null)
						{
							this.callback(this._assetLoadData_5__2);
						}
					}
					else if (assetLoadManager.m_dicLoadAssetData.ContainsKey(this.assetPath))
					{
						this._assetLoadData_5__2 = assetLoadManager.m_dicLoadAssetData[this.assetPath];
					}
					else
					{
						this._assetLoadData_5__2 = new AssetLoadData();
						assetLoadManager.m_dicLoadAssetData.Add(this.assetPath, this._assetLoadData_5__2);
					}
					this._assetLoadData_5__2.m_strAssetPath = this.assetPath;
					if (assetLoadManager.m_dicLoadAssetData[this.assetPath].m_loadedStatus != AssetStatus.CanUse)
					{
						this._depends_5__3 = assetLoadManager.AssetBundleManifest.GetAllDependencies(this.assetPath);
						this._i_5__4 = 0;
						this._len_5__5 = this._depends_5__3.Length;
						goto IL_373;
					}
					return false;
				case 1:
					this.__1__state = -1;
					if (this._www_5__7.error != null)
					{
						UnityEngine.Debug.LogError("error: " + this._www_5__7.error);
					}
					else if (this._www_5__7.isDone)
					{
						if (assetLoadManager.m_assetImportData.m_bEncrypt)
						{
							byte[] binary = SystemManager.DecryptBundleFile(this._www_5__7.bytes);
							this._assetRequest_5__8 = AssetBundle.LoadFromMemoryAsync(binary);
							this.__2__current = this._assetRequest_5__8;
							this.__1__state = 2;
							return true;
						}
						this._assetData_5__6.m_assetBundle = this._www_5__7.assetBundle;
						this._assetData_5__6.m_loadedStatus = AssetStatus.CanUse;
					}
					break;
				case 2:
					this.__1__state = -1;
					if (this._assetRequest_5__8.isDone)
					{
						this._assetData_5__6.m_assetBundle = this._assetRequest_5__8.assetBundle;
						this._assetData_5__6.m_loadedStatus = AssetStatus.CanUse;
					}
					this._assetRequest_5__8 = null;
					break;
				case 3:
					this.__1__state = -1;
					goto IL_34C;
				default:
					return false;
				}
				this._www_5__7 = null;
				goto IL_35A;
				IL_34C:
				if (this._assetData_5__6.m_loadedStatus != AssetStatus.CanUse)
				{
					this.__2__current = 1;
					this.__1__state = 3;
					return true;
				}
				IL_35A:
				this._assetData_5__6 = null;
				IL_361:
				int num2 = this._i_5__4;
				this._i_5__4 = num2 + 1;
				IL_373:
				if (this._i_5__4 >= this._len_5__5)
				{
					MonoManager.Instance.StartCoroutine(assetLoadManager.GetAssetBundleFromWWW(this._assetLoadData_5__2, this.assetName, this.callback, this.bUnLoad));
					this._depends_5__3 = null;
				}
				else
				{
					if (assetLoadManager.m_dicLoadAssetData.ContainsKey(this._depends_5__3[this._i_5__4]))
					{
						this._assetData_5__6 = assetLoadManager.m_dicLoadAssetData[this._depends_5__3[this._i_5__4]];
						if (this._assetData_5__6.m_loadedStatus == AssetStatus.CanUse)
						{
							goto IL_361;
						}
					}
					else
					{
						this._assetData_5__6 = new AssetLoadData();
						assetLoadManager.m_dicLoadAssetData.Add(this._depends_5__3[this._i_5__4], this._assetData_5__6);
					}
					this._assetData_5__6.m_strAssetPath = this._depends_5__3[this._i_5__4];
					string text = assetLoadManager.m_localAssetBundlePath + this._assetData_5__6.m_strAssetPath;
					if (this._assetData_5__6.m_loadedStatus == AssetStatus.NotReady)
					{
						this._assetData_5__6.m_loadedStatus = AssetStatus.Downloading;
						if (!text.Contains("://"))
						{
							UnityEngine.Debug.LogError("WWW加载路径错误，strAssetPath = : " + text);
						}
						this._www_5__7 = new WWW(text);
						this.__2__current = this._www_5__7;
						this.__1__state = 1;
						return true;
					}
					if (this._assetData_5__6.m_loadedStatus == AssetStatus.Downloading)
					{
						goto IL_34C;
					}
					goto IL_35A;
				}
				return false;
			}

			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		[Serializable]
		private sealed class __c
		{
			public static readonly AssetLoadManager.__c __9 = new AssetLoadManager.__c();

			public static AssetLoadManager.AssetLoadOverCall __9__35_0;

			public static AssetLoadManager.AssetLoadOverCall __9__36_0;

			internal void _GetAssetBundleFromMemory_b__35_0(AssetLoadData data)
			{
			}

			internal void _GetAssetBundleFromWWW_b__36_0(AssetLoadData data)
			{
			}
		}

		private sealed class _GetAssetBundleFromMemory_d__35 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int __1__state;

			private object __2__current;

			public AssetLoadData assetData;

			public AssetLoadManager __4__this;

			public string assetName;

			public bool bUnLoad;

			public AssetLoadManager.AssetLoadOverCall callback;

			private AssetBundleCreateRequest _asset_5__2;

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

			public _GetAssetBundleFromMemory_d__35(int __1__state)
			{
				this.__1__state = __1__state;
			}

			void IDisposable.Dispose()
			{
			}

			bool IEnumerator.MoveNext()
			{
				int num = this.__1__state;
				AssetLoadManager assetLoadManager = this.__4__this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.__1__state = -1;
					if (this._asset_5__2.isDone)
					{
						if (!string.IsNullOrEmpty(this.assetName))
						{
							this.assetData.m_assetObject = this._asset_5__2.assetBundle.LoadAsset(this.assetName);
						}
						if (this.bUnLoad)
						{
							this._asset_5__2.assetBundle.Unload(false);
						}
						else
						{
							this.assetData.m_assetBundle = this._asset_5__2.assetBundle;
						}
						this.assetData.m_loadedStatus = AssetStatus.CanUse;
						if (this.callback != null)
						{
							this.callback(this.assetData);
						}
					}
					this._asset_5__2 = null;
				}
				else
				{
					this.__1__state = -1;
					if (this.assetData.m_loadedStatus == AssetStatus.NotReady)
					{
						this.assetData.m_loadedStatus = AssetStatus.Downloading;
						string text = assetLoadManager.m_localAssetFromFilePath + this.assetData.m_strAssetPath;
						if (!File.Exists(text))
						{
							UnityEngine.Debug.LogError("资源不存在, assetPath = " + text);
						}
						FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.ReadWrite);
						byte[] array = new byte[fileStream.Length];
						fileStream.Read(array, 0, (int)fileStream.Length);
						fileStream.Close();
						byte[] binary = array;
						if (assetLoadManager.m_assetImportData.m_bEncrypt)
						{
							binary = SystemManager.DecryptBundleFile(array);
						}
						this._asset_5__2 = AssetBundle.LoadFromMemoryAsync(binary);
						this.__2__current = this._asset_5__2;
						this.__1__state = 1;
						return true;
					}
					if (this.assetData.m_loadedStatus == AssetStatus.Downloading && this.callback != null)
					{
						if (!assetLoadManager.m_dicCacheLoadingCallBackFunc.ContainsKey(this.assetData.m_strAssetPath))
						{
							Dictionary<string, AssetLoadManager.AssetLoadOverCall> arg_1E1_0 = assetLoadManager.m_dicCacheLoadingCallBackFunc;
							string arg_1E1_1 = this.assetData.m_strAssetPath;
							AssetLoadManager.AssetLoadOverCall arg_1E1_2;
							if ((arg_1E1_2 = AssetLoadManager.__c.__9__35_0) == null)
							{
								arg_1E1_2 = (AssetLoadManager.__c.__9__35_0 = new AssetLoadManager.AssetLoadOverCall(AssetLoadManager.__c.__9._GetAssetBundleFromMemory_b__35_0));
							}
							arg_1E1_0.Add(arg_1E1_1, arg_1E1_2);
						}
						Dictionary<string, AssetLoadManager.AssetLoadOverCall> dicCacheLoadingCallBackFunc = assetLoadManager.m_dicCacheLoadingCallBackFunc;
						string strAssetPath = this.assetData.m_strAssetPath;
						dicCacheLoadingCallBackFunc[strAssetPath] = (AssetLoadManager.AssetLoadOverCall)Delegate.Combine(dicCacheLoadingCallBackFunc[strAssetPath], this.callback);
					}
				}
				return false;
			}

			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		private sealed class _GetAssetBundleFromWWW_d__36 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int __1__state;

			private object __2__current;

			public AssetLoadManager __4__this;

			public AssetLoadData assetData;

			public AssetLoadManager.AssetLoadOverCall callback;

			public string assetName;

			public bool bUnLoad;

			private AssetLoadManager.AssetLoadOverCall _tmpCallback_5__2;

			private WWW _www_5__3;

			private AssetBundleCreateRequest _assetRequest_5__4;

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

			public _GetAssetBundleFromWWW_d__36(int __1__state)
			{
				this.__1__state = __1__state;
			}

			void IDisposable.Dispose()
			{
			}

			bool IEnumerator.MoveNext()
			{
				int num = this.__1__state;
				AssetLoadManager assetLoadManager = this.__4__this;
				switch (num)
				{
				case 0:
				{
					this.__1__state = -1;
					string text = assetLoadManager.m_localAssetBundlePath + this.assetData.m_strAssetPath;
					this._tmpCallback_5__2 = this.callback;
					if (this.assetData.m_loadedStatus == AssetStatus.NotReady)
					{
						this.assetData.m_loadedStatus = AssetStatus.Downloading;
						if (!text.Contains("://"))
						{
							UnityEngine.Debug.LogError("WWW加载路径错误，strAssetPath = : " + text);
						}
						this._www_5__3 = new WWW(text);
						this.__2__current = this._www_5__3;
						this.__1__state = 1;
						return true;
					}
					if (this.assetData.m_loadedStatus == AssetStatus.Downloading && this.callback != null)
					{
						if (!assetLoadManager.m_dicCacheLoadingCallBackFunc.ContainsKey(this.assetData.m_strAssetPath))
						{
							Dictionary<string, AssetLoadManager.AssetLoadOverCall> arg_2C6_0 = assetLoadManager.m_dicCacheLoadingCallBackFunc;
							string arg_2C6_1 = this.assetData.m_strAssetPath;
							AssetLoadManager.AssetLoadOverCall arg_2C6_2;
							if ((arg_2C6_2 = AssetLoadManager.__c.__9__36_0) == null)
							{
								arg_2C6_2 = (AssetLoadManager.__c.__9__36_0 = new AssetLoadManager.AssetLoadOverCall(AssetLoadManager.__c.__9._GetAssetBundleFromWWW_b__36_0));
							}
							arg_2C6_0.Add(arg_2C6_1, arg_2C6_2);
						}
						Dictionary<string, AssetLoadManager.AssetLoadOverCall> dicCacheLoadingCallBackFunc = assetLoadManager.m_dicCacheLoadingCallBackFunc;
						string strAssetPath = this.assetData.m_strAssetPath;
						dicCacheLoadingCallBackFunc[strAssetPath] = (AssetLoadManager.AssetLoadOverCall)Delegate.Combine(dicCacheLoadingCallBackFunc[strAssetPath], this.callback);
						return false;
					}
					return false;
				}
				case 1:
					this.__1__state = -1;
					if (this._www_5__3.error != null)
					{
						UnityEngine.Debug.LogError("error: " + this._www_5__3.error);
					}
					else if (this._www_5__3.isDone)
					{
						if (assetLoadManager.m_assetImportData.m_bEncrypt)
						{
							byte[] binary = SystemManager.DecryptBundleFile(this._www_5__3.bytes);
							this._assetRequest_5__4 = AssetBundle.LoadFromMemoryAsync(binary);
							this.__2__current = this._assetRequest_5__4;
							this.__1__state = 2;
							return true;
						}
						if (!string.IsNullOrEmpty(this.assetName))
						{
							this.assetData.m_assetObject = this._www_5__3.assetBundle.LoadAsset(this.assetName);
						}
						if (this.bUnLoad)
						{
							this._www_5__3.assetBundle.Unload(false);
						}
						else
						{
							this.assetData.m_assetBundle = this._www_5__3.assetBundle;
						}
						this.assetData.m_loadedStatus = AssetStatus.CanUse;
						if (this._tmpCallback_5__2 != null)
						{
							this._tmpCallback_5__2(this.assetData);
						}
					}
					break;
				case 2:
					this.__1__state = -1;
					if (this._assetRequest_5__4.isDone)
					{
						if (!string.IsNullOrEmpty(this.assetName))
						{
							this.assetData.m_assetObject = this._assetRequest_5__4.assetBundle.LoadAsset(this.assetName);
						}
						if (this.bUnLoad)
						{
							this._assetRequest_5__4.assetBundle.Unload(false);
						}
						else
						{
							this.assetData.m_assetBundle = this._assetRequest_5__4.assetBundle;
						}
						this.assetData.m_loadedStatus = AssetStatus.CanUse;
						if (this._tmpCallback_5__2 != null)
						{
							this._tmpCallback_5__2(this.assetData);
						}
					}
					this._assetRequest_5__4 = null;
					break;
				default:
					return false;
				}
				this._www_5__3 = null;
				return false;
			}

			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		private AssetLoadManager.CsvLoadOverCall m_csvLoadOverCall;

		private Dictionary<string, AssetLoadManager.AssetLoadOverCall> m_dicCacheLoadingCallBackFunc = new Dictionary<string, AssetLoadManager.AssetLoadOverCall>();

		private List<string> m_lstCallBackDeleteKey = new List<string>();

		private Dictionary<string, AssetBaseData> m_dicAssetBaseData = new Dictionary<string, AssetBaseData>();

		private AssetImportData m_assetImportData = new AssetImportData();

		private bool m_bDataInit;

		private string m_assetBundlePath;

		private string m_localAssetBundlePath;

		private string m_localAssetFromFilePath;

		private string m_basePath = "Assets/AssetBundle/";

		private Dictionary<string, AssetLoadData> m_dicLoadAssetData = new Dictionary<string, AssetLoadData>();

		private static AssetLoadManager m_instance;

		private AssetBundleManifest m_assetBundleManifest;

		public static AssetLoadManager Instance
		{
			get
			{
				if (AssetLoadManager.m_instance == null)
				{
					AssetLoadManager.m_instance = new AssetLoadManager();
				}
				return AssetLoadManager.m_instance;
			}
		}

		private AssetBundleManifest AssetBundleManifest
		{
			get
			{
				if (this.m_assetBundleManifest == null)
				{
					string str;
					if (Application.platform == RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.WindowsEditor)
					{
						str = "AssetbundlesIOS";
					}
					else if (Application.platform == RuntimePlatform.Android && Application.platform != RuntimePlatform.WindowsEditor)
					{
						str = "AssetbundlesAndroid";
					}
					else
					{
						str = "AssetbundlesWindow";
					}
					AssetBundle assetBundle = AssetBundle.LoadFromFile(this.m_localAssetFromFilePath + str);
					if (assetBundle != null)
					{
						this.m_assetBundleManifest = (AssetBundleManifest)assetBundle.LoadAsset("AssetBundleManifest");
						assetBundle.Unload(false);
					}
					else
					{
						UnityEngine.Debug.LogError("加载出错！AssetbundlesWindow 文件丢失" + this.m_localAssetFromFilePath + "Assetbundle");
					}
				}
				return this.m_assetBundleManifest;
			}
		}

		public void Run()
		{
			foreach (KeyValuePair<string, AssetLoadManager.AssetLoadOverCall> current in this.m_dicCacheLoadingCallBackFunc)
			{
				if (this.m_dicLoadAssetData[current.Key].m_loadedStatus == AssetStatus.CanUse)
				{
					this.m_lstCallBackDeleteKey.Add(current.Key);
					if (current.Value != null)
					{
						current.Value(this.m_dicLoadAssetData[current.Key]);
					}
				}
			}
			if (this.m_lstCallBackDeleteKey.Count > 0)
			{
				foreach (string current2 in this.m_lstCallBackDeleteKey)
				{
					this.m_dicCacheLoadingCallBackFunc.Remove(current2);
				}
				this.m_lstCallBackDeleteKey.Clear();
			}
		}

		public static string BundleExtensionName()
		{
			return ".u3d";
		}

		public void InitAssetBundlePath()
		{
			this.InitAssetBundlePath("");
		}

		public void InitAssetBundlePath(string httpResourcesIP)
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.WindowsEditor)
			{
				this.m_assetBundlePath = httpResourcesIP + "AssetbundlesIOS/";
				this.m_localAssetBundlePath = "file://" + Application.streamingAssetsPath + "/AssetbundlesIOS/";
			}
			else
			{
				if (Application.platform == RuntimePlatform.Android && Application.platform != RuntimePlatform.WindowsEditor)
				{
					this.m_assetBundlePath = httpResourcesIP + "AssetbundlesAndroid/";
					this.m_localAssetBundlePath = Application.streamingAssetsPath + "/AssetbundlesAndroid/";
					this.m_localAssetFromFilePath = Application.dataPath + "!assets/AssetbundlesAndroid/";
					return;
				}
				this.m_assetBundlePath = httpResourcesIP + "AssetbundlesWindow/";
				this.m_localAssetBundlePath = "file://" + Application.streamingAssetsPath + "/AssetbundlesWindow/";
			}
			this.m_localAssetFromFilePath = this.m_localAssetBundlePath.Substring(this.m_localAssetBundlePath.IndexOf("://") + 3);
		}

		private string GetAssetPath(string path)
		{
			if (path.IndexOf(AssetLoadManager.BundleExtensionName()) != -1)
			{
				return path.ToLower();
			}
			return (path + AssetLoadManager.BundleExtensionName()).ToLower();
		}

		public void LoadAssetData(AssetLoadManager.CsvLoadOverCall csvLoadOverCall, string fileName)
		{
			if (!this.m_bDataInit)
			{
				this.m_csvLoadOverCall = csvLoadOverCall;
				string url = this.m_localAssetBundlePath + fileName;
				TXTFileLoader.Instance.LoadTXTFileByName(new TXTFileLoader.OnFinishLoadTXTCall(this.LoadAssetDataByWWWResource), url, "utf-8");
			}
		}

		private void LoadAssetDataByWWWResource(string assetVersionData)
		{
			if (!this.m_bDataInit)
			{
				this.AnalysisAssetJsonData(assetVersionData);
				this.m_bDataInit = true;
				this.LoadAssetbundle("textdata/csv", new AssetLoadManager.AssetLoadOverCall(this.onLoadCsvDataOver));
			}
		}

		private void onLoadCsvDataOver(AssetLoadData data)
		{
			if (this.m_csvLoadOverCall != null)
			{
				this.m_csvLoadOverCall();
			}
		}

		private void AnalysisAssetJsonData(string data)
		{
			new Dictionary<string, AssetBaseData>();
			this.m_assetImportData = JsonReader.Deserialize<AssetImportData>(data);
			if (this.m_assetImportData.m_assetBaseData != null)
			{
				int i = 0;
				int num = this.m_assetImportData.m_assetBaseData.Length;
				while (i < num)
				{
					string strAssetName = this.m_assetImportData.m_assetBaseData[i].m_strAssetName;
					if (!this.m_dicAssetBaseData.ContainsKey(strAssetName))
					{
						this.m_dicAssetBaseData.Add(strAssetName, this.m_assetImportData.m_assetBaseData[i]);
					}
					i++;
				}
			}
			this.m_assetImportData.m_assetBaseData = null;
		}

		public bool HasDicAssetData(string assetName)
		{
			assetName = this.m_basePath + assetName;
			return this.m_dicAssetBaseData.ContainsKey(assetName);
		}

		public void LoadAssetObject(string assetName, AssetLoadManager.AssetLoadOverCall callback)
		{
			assetName = this.m_basePath + assetName;
			string text = string.Empty;
			if (!this.m_dicAssetBaseData.ContainsKey(assetName))
			{
				UnityEngine.Debug.LogError("AssetTotal里未找到此资源:" + assetName.ToString());
				return;
			}
			text = this.m_dicAssetBaseData[assetName].m_strAssetPath;
			string assetName2 = assetName + this.m_dicAssetBaseData[assetName].m_assetExtension;
			if (Application.platform == RuntimePlatform.WindowsPlayer)
			{
				MonoManager.Instance.StartCoroutine(this.LoadManifestAssetByMemory(text.ToLower(), assetName2, callback, true));
				return;
			}
			MonoManager.Instance.StartCoroutine(this.LoadManifestAssetByWWW(text.ToLower(), assetName2, callback, true));
		}

		public void LoadAssetbundle(string assetPath, AssetLoadManager.AssetLoadOverCall callback)
		{
			assetPath = this.GetAssetPath(assetPath);
			if (Application.platform == RuntimePlatform.WindowsPlayer)
			{
				MonoManager.Instance.StartCoroutine(this.LoadManifestAssetByMemory(assetPath, string.Empty, callback, false));
				return;
			}
			MonoManager.Instance.StartCoroutine(this.LoadManifestAssetByWWW(assetPath, string.Empty, callback, false));
		}

		public string GetCsvData(string name)
		{
			string text = "textdata/csv.u3d";
			if (!this.m_dicLoadAssetData.ContainsKey(text))
			{
				UnityEngine.Debug.LogError("加载CSV数据error!!! : " + text);
				return "";
			}
			TextAsset textAsset = this.m_dicLoadAssetData[text].m_assetBundle.LoadAsset(name) as TextAsset;
			if (textAsset == null)
			{
				UnityEngine.Debug.LogError("未找到CSV数据: " + name);
				return "";
			}
			return textAsset.text;
		}

		public string GetFileData(string name)
		{
			string text = "textdata/csv.u3d";
			if (!this.m_dicLoadAssetData.ContainsKey(text))
			{
				UnityEngine.Debug.LogError("加载CSV数据error!!! : " + text);
				return "";
			}
			TextAsset textAsset = this.m_dicLoadAssetData[text].m_assetBundle.LoadAsset(name) as TextAsset;
			if (textAsset == null)
			{
				UnityEngine.Debug.LogError("未找到CSV数据: " + name);
				return "";
			}
			return textAsset.text;
		}

//		[IteratorStateMachine(typeof(AssetLoadManager._003CLoadManifestAssetByMemory_003Ed__33))]
		private IEnumerator LoadManifestAssetByMemory(string assetPath, string assetName, AssetLoadManager.AssetLoadOverCall callback = null, bool bUnLoad = false)
		{
			AssetLoadData assetLoadData = new AssetLoadData();
			string[] array = new string[1000];
			while (true)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				AssetLoadData assetLoadData2 = null;
				switch (num)
				{
				case 0:
					if (this.m_dicLoadAssetData.ContainsKey(assetPath) && this.m_dicLoadAssetData[assetPath].m_loadedStatus == AssetStatus.CanUse)
					{
						assetLoadData = this.m_dicLoadAssetData[assetPath];
						if (callback != null)
						{
							callback(assetLoadData);
						}
					}
					else if (this.m_dicLoadAssetData.ContainsKey(assetPath))
					{
						assetLoadData = this.m_dicLoadAssetData[assetPath];
					}
					else
					{
						assetLoadData = new AssetLoadData();
						this.m_dicLoadAssetData.Add(assetPath, assetLoadData);
					}
					assetLoadData.m_strAssetPath = assetPath;
					if (this.m_dicLoadAssetData[assetPath].m_loadedStatus != AssetStatus.CanUse)
					{
						array = this.AssetBundleManifest.GetAllDependencies(assetPath);
						num2 = 0;
						num3 = array.Length;
						goto IL_306;
					}
					goto IL_347;
				case 1:
				{
					AssetBundleCreateRequest assetBundleCreateRequest = null;
					if (assetBundleCreateRequest.isDone)
					{
						assetLoadData2.m_assetBundle = assetBundleCreateRequest.assetBundle;
						assetLoadData2.m_loadedStatus = AssetStatus.CanUse;
					}
					assetBundleCreateRequest = null;
					goto IL_2ED;
				}
				case 2:
					goto IL_2DF;
				}
				break;
				IL_306:
				if (num2 >= num3)
				{
					goto Block_14;
				}
				if (this.m_dicLoadAssetData.ContainsKey(array[num2]))
				{
					assetLoadData2 = this.m_dicLoadAssetData[array[num2]];
					if (assetLoadData2.m_loadedStatus == AssetStatus.CanUse)
					{
						goto IL_2F4;
					}
				}
				else
				{
					assetLoadData2 = new AssetLoadData();
					this.m_dicLoadAssetData.Add(array[num2], assetLoadData2);
				}
				assetLoadData2.m_strAssetPath = array[num2];
				if (assetLoadData2.m_loadedStatus == AssetStatus.NotReady)
				{
					assetLoadData2.m_loadedStatus = AssetStatus.Downloading;
					string text = this.m_localAssetFromFilePath + assetLoadData2.m_strAssetPath;
					if (!File.Exists(text))
					{
						UnityEngine.Debug.LogError("资源不存在, assetPath = " + text);
					}
					FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.ReadWrite);
					byte[] array2 = new byte[fileStream.Length];
					fileStream.Read(array2, 0, (int)fileStream.Length);
					fileStream.Close();
					byte[] binary = array2;
					if (this.m_assetImportData.m_bEncrypt)
					{
						binary = SystemManager.DecryptBundleFile(array2);
					}
					AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.LoadFromMemoryAsync(binary);
					yield return assetBundleCreateRequest;
					continue;
				}
				if (assetLoadData2.m_loadedStatus != AssetStatus.Downloading)
				{
					goto IL_2ED;
				}
				IL_2DF:
				if (assetLoadData2.m_loadedStatus == AssetStatus.CanUse)
				{
					goto IL_2ED;
				}
				yield return 1;
				continue;
				IL_2F4:
				int num4 = num2;
				num2 = num4 + 1;
				goto IL_306;
				IL_2ED:
				assetLoadData2 = null;
				goto IL_2F4;
			}
			yield break;
			Block_14:
			MonoManager.Instance.StartCoroutine(this.GetAssetBundleFromMemory(assetLoadData, assetName, callback, bUnLoad));
			array = null;
			IL_347:
			yield break;
		}

//		[IteratorStateMachine(typeof(AssetLoadManager._003CLoadManifestAssetByWWW_003Ed__34))]
		private IEnumerator LoadManifestAssetByWWW(string assetPath, string assetName, AssetLoadManager.AssetLoadOverCall callback = null, bool bUnLoad = false)
		{
			AssetLoadData assetLoadData = null;
			string[] array = new string[1000];
			while (true)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				WWW wWW =  null;
				AssetLoadData assetLoadData2 = null;
				switch (num)
				{
				case 0:
					if (this.m_dicLoadAssetData.ContainsKey(assetPath) && this.m_dicLoadAssetData[assetPath].m_loadedStatus == AssetStatus.CanUse)
					{
						assetLoadData = this.m_dicLoadAssetData[assetPath];
						if (callback != null)
						{
							callback(assetLoadData);
						}
					}
					else if (this.m_dicLoadAssetData.ContainsKey(assetPath))
					{
						assetLoadData = this.m_dicLoadAssetData[assetPath];
					}
					else
					{
						assetLoadData = new AssetLoadData();
						this.m_dicLoadAssetData.Add(assetPath, assetLoadData);
					}
					assetLoadData.m_strAssetPath = assetPath;
					if (this.m_dicLoadAssetData[assetPath].m_loadedStatus != AssetStatus.CanUse)
					{
						array = this.AssetBundleManifest.GetAllDependencies(assetPath);
						num2 = 0;
						num3 = array.Length;
						goto IL_373;
					}
					goto IL_3B4;
				case 1:
					if (wWW.error != null)
					{
						UnityEngine.Debug.LogError("error: " + wWW.error);
						goto IL_317;
					}
					if (!wWW.isDone)
					{
						goto IL_317;
					}
					if (this.m_assetImportData.m_bEncrypt)
					{
						byte[] binary = SystemManager.DecryptBundleFile(wWW.bytes);
						AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.LoadFromMemoryAsync(binary);
						yield return assetBundleCreateRequest;
						continue;
					}
					assetLoadData2.m_assetBundle = wWW.assetBundle;
					assetLoadData2.m_loadedStatus = AssetStatus.CanUse;
					goto IL_317;
				case 2:
				{
					AssetBundleCreateRequest assetBundleCreateRequest = new AssetBundleCreateRequest();
					if (assetBundleCreateRequest.isDone)
					{
						assetLoadData2.m_assetBundle = assetBundleCreateRequest.assetBundle;
						assetLoadData2.m_loadedStatus = AssetStatus.CanUse;
					}
					assetBundleCreateRequest = null;
					goto IL_317;
				}
				case 3:
					goto IL_34C;
				}
				break;
				IL_373:
				if (num2 >= num3)
				{
					goto Block_16;
				}
				if (this.m_dicLoadAssetData.ContainsKey(array[num2]))
				{
					assetLoadData2 = this.m_dicLoadAssetData[array[num2]];
					if (assetLoadData2.m_loadedStatus == AssetStatus.CanUse)
					{
						goto IL_361;
					}
				}
				else
				{
					assetLoadData2 = new AssetLoadData();
					this.m_dicLoadAssetData.Add(array[num2], assetLoadData2);
				}
				assetLoadData2.m_strAssetPath = array[num2];
				string text = this.m_localAssetBundlePath + assetLoadData2.m_strAssetPath;
				if (assetLoadData2.m_loadedStatus == AssetStatus.NotReady)
				{
					assetLoadData2.m_loadedStatus = AssetStatus.Downloading;
					if (!text.Contains("://"))
					{
						UnityEngine.Debug.LogError("WWW加载路径错误，strAssetPath = : " + text);
					}
					wWW = new WWW(text);
					yield return wWW;
					continue;
				}
				if (assetLoadData2.m_loadedStatus != AssetStatus.Downloading)
				{
					goto IL_35A;
				}
				IL_34C:
				if (assetLoadData2.m_loadedStatus == AssetStatus.CanUse)
				{
					goto IL_35A;
				}
				yield return 1;
				continue;
				IL_361:
				int num4 = num2;
				num2 = num4 + 1;
				goto IL_373;
				IL_35A:
				assetLoadData2 = null;
				goto IL_361;
				IL_317:
				wWW = null;
				goto IL_35A;
			}
			yield break;
			Block_16:
			MonoManager.Instance.StartCoroutine(this.GetAssetBundleFromWWW(assetLoadData, assetName, callback, bUnLoad));
			array = null;
			IL_3B4:
			yield break;
		}

///		[IteratorStateMachine(typeof(AssetLoadManager._003CGetAssetBundleFromMemory_003Ed__35))]
		private IEnumerator GetAssetBundleFromMemory(AssetLoadData assetData, string assetName, AssetLoadManager.AssetLoadOverCall callback, bool bUnLoad)
		{
			int num = 0;
			AssetBundleCreateRequest assetBundleCreateRequest = null;
			while (num == 0)
			{
				if (assetData.m_loadedStatus != AssetStatus.NotReady)
				{
					if (assetData.m_loadedStatus == AssetStatus.Downloading && callback != null)
					{
						if (!this.m_dicCacheLoadingCallBackFunc.ContainsKey(assetData.m_strAssetPath))
						{
							Dictionary<string, AssetLoadManager.AssetLoadOverCall> arg_1E1_0 = this.m_dicCacheLoadingCallBackFunc;
							string arg_1E1_1 = assetData.m_strAssetPath;
							AssetLoadManager.AssetLoadOverCall arg_1E1_2;
							if ((arg_1E1_2 = AssetLoadManager.__c.__9__35_0) == null)
							{
								arg_1E1_2 = (AssetLoadManager.__c.__9__35_0 = new AssetLoadManager.AssetLoadOverCall(AssetLoadManager.__c.__9._GetAssetBundleFromMemory_b__35_0));
							}
							arg_1E1_0.Add(arg_1E1_1, arg_1E1_2);
						}
						Dictionary<string, AssetLoadManager.AssetLoadOverCall> dicCacheLoadingCallBackFunc = this.m_dicCacheLoadingCallBackFunc;
						string strAssetPath = assetData.m_strAssetPath;
						dicCacheLoadingCallBackFunc[strAssetPath] = (AssetLoadManager.AssetLoadOverCall)Delegate.Combine(dicCacheLoadingCallBackFunc[strAssetPath], callback);
					}
				//IL_21D:
					yield break;
				}
				assetData.m_loadedStatus = AssetStatus.Downloading;
				string text = this.m_localAssetFromFilePath + assetData.m_strAssetPath;
				if (!File.Exists(text))
				{
					UnityEngine.Debug.LogError("资源不存在, assetPath = " + text);
				}
				FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.ReadWrite);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, (int)fileStream.Length);
				fileStream.Close();
				byte[] binary = array;
				if (this.m_assetImportData.m_bEncrypt)
				{
					binary = SystemManager.DecryptBundleFile(array);
				}
				assetBundleCreateRequest = AssetBundle.LoadFromMemoryAsync(binary);
				yield return assetBundleCreateRequest;
			}
			if (num != 1)
			{
				yield break;
			}
			if (assetBundleCreateRequest.isDone)
			{
				if (!string.IsNullOrEmpty(assetName))
				{
					assetData.m_assetObject = assetBundleCreateRequest.assetBundle.LoadAsset(assetName);
				}
				if (bUnLoad)
				{
					assetBundleCreateRequest.assetBundle.Unload(false);
				}
				else
				{
					assetData.m_assetBundle = assetBundleCreateRequest.assetBundle;
				}
				assetData.m_loadedStatus = AssetStatus.CanUse;
				if (callback != null)
				{
					callback(assetData);
				}
			}
			assetBundleCreateRequest = null;
//			goto IL_21D;
		}

//		[IteratorStateMachine(typeof(AssetLoadManager._003CGetAssetBundleFromWWW_003Ed__36))]
		private IEnumerator GetAssetBundleFromWWW(AssetLoadData assetData, string assetName, AssetLoadManager.AssetLoadOverCall callback, bool bUnLoad)
		{
			WWW wWW = null;
			AssetBundleCreateRequest assetBundleCreateRequest = null;
			while (true)
			{
				int num = 0;
				switch (num)
				{
				case 0:
				{
					string text = this.m_localAssetBundlePath + assetData.m_strAssetPath;
					if (assetData.m_loadedStatus == AssetStatus.NotReady)
					{
						assetData.m_loadedStatus = AssetStatus.Downloading;
						if (!text.Contains("://"))
						{
							UnityEngine.Debug.LogError("WWW加载路径错误，strAssetPath = : " + text);
						}
						wWW = new WWW(text);
						yield return wWW;
						continue;
					}
					goto IL_262;
				}
				case 1:
					if (wWW.error != null)
					{
						goto Block_4;
					}
					if (!wWW.isDone)
					{
						goto IL_256;
					}
					if (this.m_assetImportData.m_bEncrypt)
					{
						byte[] binary = SystemManager.DecryptBundleFile(wWW.bytes);
						assetBundleCreateRequest = AssetBundle.LoadFromMemoryAsync(binary);
						yield return assetBundleCreateRequest;
						continue;
					}
					goto IL_1D2;
				case 2:
					goto IL_12B;
				}
				break;
			}
			yield break;
			Block_4:
			UnityEngine.Debug.LogError("error: " + wWW.error);
			goto IL_256;
			IL_12B:
			if (assetBundleCreateRequest.isDone)
			{
				if (!string.IsNullOrEmpty(assetName))
				{
					assetData.m_assetObject = assetBundleCreateRequest.assetBundle.LoadAsset(assetName);
				}
				if (bUnLoad)
				{
					assetBundleCreateRequest.assetBundle.Unload(false);
				}
				else
				{
					assetData.m_assetBundle = assetBundleCreateRequest.assetBundle;
				}
				assetData.m_loadedStatus = AssetStatus.CanUse;
				if (callback != null)
				{
					callback(assetData);
				}
			}
			assetBundleCreateRequest = null;
			goto IL_256;
			IL_1D2:
			if (!string.IsNullOrEmpty(assetName))
			{
				assetData.m_assetObject = wWW.assetBundle.LoadAsset(assetName);
			}
			if (bUnLoad)
			{
				wWW.assetBundle.Unload(false);
			}
			else
			{
				assetData.m_assetBundle = wWW.assetBundle;
			}
			assetData.m_loadedStatus = AssetStatus.CanUse;
			if (callback != null)
			{
				callback(assetData);
			}
			IL_256:
			wWW = null;
			goto IL_302;
			IL_262:
			if (assetData.m_loadedStatus == AssetStatus.Downloading && callback != null)
			{
				if (!this.m_dicCacheLoadingCallBackFunc.ContainsKey(assetData.m_strAssetPath))
				{
					Dictionary<string, AssetLoadManager.AssetLoadOverCall> arg_2C6_0 = this.m_dicCacheLoadingCallBackFunc;
					string arg_2C6_1 = assetData.m_strAssetPath;
					AssetLoadManager.AssetLoadOverCall arg_2C6_2;
					if ((arg_2C6_2 = AssetLoadManager.__c.__9__36_0) == null)
					{
						arg_2C6_2 = (AssetLoadManager.__c.__9__36_0 = new AssetLoadManager.AssetLoadOverCall(AssetLoadManager.__c.__9._GetAssetBundleFromWWW_b__36_0));
					}
					arg_2C6_0.Add(arg_2C6_1, arg_2C6_2);
				}
				Dictionary<string, AssetLoadManager.AssetLoadOverCall> dicCacheLoadingCallBackFunc = this.m_dicCacheLoadingCallBackFunc;
				string strAssetPath = assetData.m_strAssetPath;
				dicCacheLoadingCallBackFunc[strAssetPath] = (AssetLoadManager.AssetLoadOverCall)Delegate.Combine(dicCacheLoadingCallBackFunc[strAssetPath], callback);
			}
			IL_302:
			yield break;
		}
	}
}
