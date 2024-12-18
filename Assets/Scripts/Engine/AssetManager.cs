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
	public class AssetManager
	{
		public delegate void AssetCompleteCallBack(AssetData data);

		public delegate void CsvLoadOverCallBack();

		[Serializable]
		private sealed class __c
		{
			public static readonly AssetManager.__c __9 = new AssetManager.__c();

			public static AssetManager.AssetCompleteCallBack __9__29_0;

			public static AssetManager.AssetCompleteCallBack __9__31_0;

			internal void _GetAssetBundleFromMemory_b__29_0(AssetData data)
			{
			}

			internal void _GetAssetBundleFromWWW_b__31_0(AssetData data)
			{
			}
		}

		private sealed class _GetAssetBundleFromMemory_d__29 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int __1__state;

			private object __2__current;

			public AssetData assetData;

			public AssetManager __4__this;

			public AssetManager.AssetCompleteCallBack callback;

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

			public _GetAssetBundleFromMemory_d__29(int __1__state)
			{
				this.__1__state = __1__state;
			}

			void IDisposable.Dispose()
			{
			}

			bool IEnumerator.MoveNext()
			{
				int num = this.__1__state;
				AssetManager assetManager = this.__4__this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.__1__state = -1;
					if (this._asset_5__2.isDone)
					{
						this.assetData.m_assetBundleLoad = this._asset_5__2.assetBundle;
						if (this.assetData.m_assetType == AssetType.Prefab || this.assetData.m_assetType == AssetType.Audio || this.assetData.m_assetType == AssetType.Texture)
						{
							this.assetData.m_mainAsset = this.assetData.m_assetBundleLoad.mainAsset;
						}
						this.assetData.m_loadedStatus = AssetStatus.CanUse;
						this.callback(this.assetData);
					}
					this._asset_5__2 = null;
				}
				else
				{
					this.__1__state = -1;
					if (this.assetData.m_loadedStatus == AssetStatus.NotReady)
					{
						this.assetData.m_loadedStatus = AssetStatus.Downloading;
						FileStream fileStream = new FileStream(assetManager.m_localAssetFromFilePath + assetManager.getTypePath(this.assetData.m_assetType) + this.assetData.m_strAssetPath + AssetManager.BundleExtensionName(), FileMode.Open, FileAccess.ReadWrite);
						byte[] array = new byte[fileStream.Length];
						fileStream.Read(array, 0, (int)fileStream.Length);
						fileStream.Close();
						byte[] binary = SystemManager.DecryptBundleFile(array);
						this._asset_5__2 = AssetBundle.LoadFromMemoryAsync(binary);
						this.__2__current = this._asset_5__2;
						this.__1__state = 1;
						return true;
					}
					if (this.assetData.m_loadedStatus == AssetStatus.Downloading)
					{
						string saveKey = this.assetData.GetSaveKey();
						if (!assetManager.m_dicCacheLoadingCallBackFunc.ContainsKey(saveKey))
						{
							Dictionary<string, AssetManager.AssetCompleteCallBack> arg_1A7_0 = assetManager.m_dicCacheLoadingCallBackFunc;
							string arg_1A7_1 = saveKey;
							AssetManager.AssetCompleteCallBack arg_1A7_2;
							if ((arg_1A7_2 = AssetManager.__c.__9__29_0) == null)
							{
								arg_1A7_2 = (AssetManager.__c.__9__29_0 = new AssetManager.AssetCompleteCallBack(AssetManager.__c.__9._GetAssetBundleFromMemory_b__29_0));
							}
							arg_1A7_0.Add(arg_1A7_1, arg_1A7_2);
						}
						Dictionary<string, AssetManager.AssetCompleteCallBack> dicCacheLoadingCallBackFunc = assetManager.m_dicCacheLoadingCallBackFunc;
						string key = saveKey;
						dicCacheLoadingCallBackFunc[key] = (AssetManager.AssetCompleteCallBack)Delegate.Combine(dicCacheLoadingCallBackFunc[key], this.callback);
					}
				}
				return false;
			}

			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		private sealed class _GetAssetBundleFromWWW_d__31 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int __1__state;

			private object __2__current;

			public AssetManager __4__this;

			public AssetData assetData;

			public AssetManager.AssetCompleteCallBack callback;

			private string _strAssetPath_5__2;

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

			public _GetAssetBundleFromWWW_d__31(int __1__state)
			{
				this.__1__state = __1__state;
			}

			void IDisposable.Dispose()
			{
			}

			bool IEnumerator.MoveNext()
			{
				int num = this.__1__state;
				AssetManager assetManager = this.__4__this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.__1__state = -1;
					this.assetData.m_assetBundleLoad = AssetBundleManager.GetAssetBundle(this._strAssetPath_5__2, this.assetData.m_strCode);
					if (this.assetData.m_assetType == AssetType.Prefab || this.assetData.m_assetType == AssetType.Audio || this.assetData.m_assetType == AssetType.Texture)
					{
						this.assetData.m_mainAsset = this.assetData.m_assetBundleLoad.mainAsset;
					}
					this.assetData.m_loadedStatus = AssetStatus.CanUse;
					this.callback(this.assetData);
				}
				else
				{
					this.__1__state = -1;
					this._strAssetPath_5__2 = assetManager.GetAssetPath(this.assetData);
					if (this.assetData.m_loadedStatus == AssetStatus.NotReady)
					{
						this.assetData.m_loadedStatus = AssetStatus.Downloading;
						if (!this._strAssetPath_5__2.Contains("://"))
						{
							UnityEngine.Debug.LogError("WWW加载路径错误，strAssetPath = : " + this._strAssetPath_5__2);
						}
						this.__2__current = MonoManager.Instance.StartCoroutine(AssetBundleManager.DownloadAssetBundle(this._strAssetPath_5__2, this.assetData.m_strCode, this.assetData.m_bEncrypt));
						this.__1__state = 1;
						return true;
					}
					if (this.assetData.m_loadedStatus == AssetStatus.Downloading)
					{
						string saveKey = this.assetData.GetSaveKey();
						if (!assetManager.m_dicCacheLoadingCallBackFunc.ContainsKey(saveKey))
						{
							Dictionary<string, AssetManager.AssetCompleteCallBack> arg_18A_0 = assetManager.m_dicCacheLoadingCallBackFunc;
							string arg_18A_1 = saveKey;
							AssetManager.AssetCompleteCallBack arg_18A_2;
							if ((arg_18A_2 = AssetManager.__c.__9__31_0) == null)
							{
								arg_18A_2 = (AssetManager.__c.__9__31_0 = new AssetManager.AssetCompleteCallBack(AssetManager.__c.__9._GetAssetBundleFromWWW_b__31_0));
							}
							arg_18A_0.Add(arg_18A_1, arg_18A_2);
						}
						Dictionary<string, AssetManager.AssetCompleteCallBack> dicCacheLoadingCallBackFunc = assetManager.m_dicCacheLoadingCallBackFunc;
						string key = saveKey;
						dicCacheLoadingCallBackFunc[key] = (AssetManager.AssetCompleteCallBack)Delegate.Combine(dicCacheLoadingCallBackFunc[key], this.callback);
					}
				}
				return false;
			}

			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		private Dictionary<string, AssetManager.AssetCompleteCallBack> m_dicCacheLoadingCallBackFunc = new Dictionary<string, AssetManager.AssetCompleteCallBack>();

		private List<string> m_lstCallBackDeleteKey = new List<string>();

		private Dictionary<string, AssetData> m_dicAssetData = new Dictionary<string, AssetData>();

		private bool m_bDataInit;

		private AssetManager.CsvLoadOverCallBack m_csvLoadOverCall;

		private string m_assetBundlePath;

		private string m_localAssetBundlePath;

		private string m_localAssetFromFilePath;

		private static AssetManager m_instance;

		public static AssetManager Instance
		{
			get
			{
				if (AssetManager.m_instance == null)
				{
					AssetManager.m_instance = new AssetManager();
				}
				return AssetManager.m_instance;
			}
		}

		public string LocalAssetBundlePath
		{
			get
			{
				return this.m_localAssetBundlePath;
			}
		}

		public static string BundleExtensionName()
		{
			return ".u3d";
		}

		public string GetConfigData(string fileName)
		{
			StreamReader expr_1B = File.OpenText(this.m_localAssetBundlePath + "Config/" + fileName);
			string result = expr_1B.ReadToEnd();
			expr_1B.Close();
			return result;
		}

		public void Run()
		{
			foreach (KeyValuePair<string, AssetManager.AssetCompleteCallBack> current in this.m_dicCacheLoadingCallBackFunc)
			{
				if (this.m_dicAssetData[current.Key].m_loadedStatus == AssetStatus.CanUse)
				{
					this.m_lstCallBackDeleteKey.Add(current.Key);
					if (current.Value != null)
					{
						current.Value(this.m_dicAssetData[current.Key]);
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
			else if (Application.platform == RuntimePlatform.Android && Application.platform != RuntimePlatform.WindowsEditor)
			{
				this.m_assetBundlePath = httpResourcesIP + "AssetbundlesAndroid/";
				this.m_localAssetBundlePath = Application.streamingAssetsPath + "/AssetbundlesAndroid/";
			}
			else
			{
				this.m_assetBundlePath = httpResourcesIP + "AssetbundlesWindows/";
				this.m_localAssetBundlePath = "file://" + Application.streamingAssetsPath + "/AssetbundlesWindows/";
			}
			this.m_localAssetFromFilePath = this.m_localAssetBundlePath.Substring(this.m_localAssetBundlePath.IndexOf("://") + 3);
		}

		public void LoadAssetData(AssetManager.CsvLoadOverCallBack csvLoadOverCall, string fileName)
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
				this.m_dicAssetData = this.AnalysisAssetJsonData(assetVersionData);
				this.m_bDataInit = true;
				this.GetAssetData(AssetType.TextData, "CSV", new AssetManager.AssetCompleteCallBack(this.onLoadCsvDataOver));
			}
		}

		private void onLoadCsvDataOver(AssetData assetData)
		{
			if (this.m_csvLoadOverCall != null)
			{
				this.m_csvLoadOverCall();
			}
		}

		public void LoadConfigData(TXTFileLoader.OnFinishLoadTXTCall callbacK)
		{
			string url = this.m_localAssetBundlePath + "../../../Config.txt";
			TXTFileLoader.Instance.LoadTXTFileByName(callbacK, url, "utf-8");
		}

		private Dictionary<string, AssetData> AnalysisAssetJsonData(string data)
		{
			Dictionary<string, AssetData> dictionary = new Dictionary<string, AssetData>();
			Hashtable hashtable = JsonReader.Deserialize<Hashtable>(data);
			if (hashtable == null)
			{
				UnityEngine.Debug.LogError("load json data error! : " + data);
				return dictionary;
			}
			if (hashtable.ContainsKey("scene"))
			{
				AssetData[] array = JsonReader.Deserialize<AssetData[]>(hashtable["scene"].ToString());
				for (int i = 0; i < array.Length; i++)
				{
					AssetData assetData = array[i];
					string saveKey = assetData.GetSaveKey();
					dictionary.Add(saveKey, assetData);
				}
			}
			if (hashtable.ContainsKey("prefab"))
			{
				AssetData[] array = JsonReader.Deserialize<AssetData[]>(hashtable["prefab"].ToString());
				for (int i = 0; i < array.Length; i++)
				{
					AssetData assetData2 = array[i];
					string saveKey2 = assetData2.GetSaveKey();
					dictionary.Add(saveKey2, assetData2);
				}
			}
			if (hashtable.ContainsKey("textdata"))
			{
				AssetData[] array = JsonReader.Deserialize<AssetData[]>(hashtable["textdata"].ToString());
				for (int i = 0; i < array.Length; i++)
				{
					AssetData assetData3 = array[i];
					string saveKey3 = assetData3.GetSaveKey();
					dictionary.Add(saveKey3, assetData3);
				}
			}
			if (hashtable.ContainsKey("texture"))
			{
				AssetData[] array = JsonReader.Deserialize<AssetData[]>(hashtable["texture"].ToString());
				for (int i = 0; i < array.Length; i++)
				{
					AssetData assetData4 = array[i];
					string saveKey4 = assetData4.GetSaveKey();
					dictionary.Add(saveKey4, assetData4);
				}
			}
			if (hashtable.ContainsKey("audio"))
			{
				AssetData[] array = JsonReader.Deserialize<AssetData[]>(hashtable["audio"].ToString());
				for (int i = 0; i < array.Length; i++)
				{
					AssetData assetData5 = array[i];
					string saveKey5 = assetData5.GetSaveKey();
					dictionary.Add(saveKey5, assetData5);
				}
			}
			return dictionary;
		}

		public void GetAssetData(string path, AssetManager.AssetCompleteCallBack callback)
		{
			if (!this.m_dicAssetData.ContainsKey(path))
			{
				UnityEngine.Debug.LogError("AssetTotal里未找到此资源:" + path.ToString());
				return;
			}
			if (this.m_dicAssetData[path].m_loadedStatus == AssetStatus.CanUse)
			{
				callback(this.m_dicAssetData[path]);
				return;
			}
			if (!this.m_dicAssetData[path].m_bCompress && !this.m_dicAssetData[path].m_bEncrypt && !this.m_dicAssetData[path].m_bUpdateFromHTTP && !SystemManager.CheckIsMobilePlatform())
			{
				this.GetAssetBundleFromFile(this.m_dicAssetData[path], callback);
				return;
			}
			if (SystemManager.CalculateString(this.m_localAssetBundlePath) > 0 && !this.m_dicAssetData[path].m_bUpdateFromHTTP)
			{
				MonoManager.Instance.StartCoroutine(this.GetAssetBundleFromMemory(this.m_dicAssetData[path], callback));
				return;
			}
			MonoManager.Instance.StartCoroutine(this.GetAssetBundleFromWWW(this.m_dicAssetData[path], callback));
		}

		public void GetAssetData(AssetType type, string name, AssetManager.AssetCompleteCallBack callback)
		{
			string assetDataKey = this.GetAssetDataKey(type, name);
			this.GetAssetData(assetDataKey, callback);
		}

		public bool HasDicAssetData(string name, AssetType type)
		{
			string assetDataKey = this.GetAssetDataKey(type, name);
			return this.HasDicAssetData(assetDataKey);
		}

		public bool HasDicAssetData(string path)
		{
			return this.m_dicAssetData.ContainsKey(path);
		}

//		[IteratorStateMachine(typeof(AssetManager._003CGetAssetBundleFromMemory_003Ed__29))]
		private IEnumerator GetAssetBundleFromMemory(AssetData assetData, AssetManager.AssetCompleteCallBack callback)
		{
			int num = 0;
			AssetBundleCreateRequest assetBundleCreateRequest = null;
			while (num == 0)
			{
				if (assetData.m_loadedStatus != AssetStatus.NotReady)
				{
					if (assetData.m_loadedStatus == AssetStatus.Downloading)
					{
						string saveKey = assetData.GetSaveKey();
						if (!this.m_dicCacheLoadingCallBackFunc.ContainsKey(saveKey))
						{
							Dictionary<string, AssetManager.AssetCompleteCallBack> arg_1A7_0 = this.m_dicCacheLoadingCallBackFunc;
							string arg_1A7_1 = saveKey;
							AssetManager.AssetCompleteCallBack arg_1A7_2;
							if ((arg_1A7_2 = AssetManager.__c.__9__29_0) == null)
							{
								arg_1A7_2 = (AssetManager.__c.__9__29_0 = new AssetManager.AssetCompleteCallBack(AssetManager.__c.__9._GetAssetBundleFromMemory_b__29_0));
							}
							arg_1A7_0.Add(arg_1A7_1, arg_1A7_2);
						}
						Dictionary<string, AssetManager.AssetCompleteCallBack> dicCacheLoadingCallBackFunc = this.m_dicCacheLoadingCallBackFunc;
						string key = saveKey;
						dicCacheLoadingCallBackFunc[key] = (AssetManager.AssetCompleteCallBack)Delegate.Combine(dicCacheLoadingCallBackFunc[key], callback);
					}
				//IL_1DA:
					yield break;
				}
				assetData.m_loadedStatus = AssetStatus.Downloading;
				FileStream fileStream = new FileStream(this.m_localAssetFromFilePath + this.getTypePath(assetData.m_assetType) + assetData.m_strAssetPath + AssetManager.BundleExtensionName(), FileMode.Open, FileAccess.ReadWrite);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, (int)fileStream.Length);
				fileStream.Close();
				byte[] binary = SystemManager.DecryptBundleFile(array);
				assetBundleCreateRequest = AssetBundle.LoadFromMemoryAsync(binary);
				yield return assetBundleCreateRequest;
			}
			if (num != 1)
			{
				yield break;
			}
			if (assetBundleCreateRequest.isDone)
			{
				assetData.m_assetBundleLoad = assetBundleCreateRequest.assetBundle;
				if (assetData.m_assetType == AssetType.Prefab || assetData.m_assetType == AssetType.Audio || assetData.m_assetType == AssetType.Texture)
				{
					assetData.m_mainAsset = assetData.m_assetBundleLoad.mainAsset;
				}
				assetData.m_loadedStatus = AssetStatus.CanUse;
				callback(assetData);
			}
			assetBundleCreateRequest = null;
		//	goto IL_1DA;
		}

		private void GetAssetBundleFromFile(AssetData assetData, AssetManager.AssetCompleteCallBack callback)
		{
			if (assetData.m_loadedStatus == AssetStatus.NotReady)
			{
				string path = this.m_localAssetFromFilePath + this.getTypePath(assetData.m_assetType) + assetData.m_strAssetPath + AssetManager.BundleExtensionName();
				assetData.m_assetBundleLoad = AssetBundle.LoadFromFile(path);
				if (assetData.m_assetType == AssetType.Prefab || assetData.m_assetType == AssetType.Audio || assetData.m_assetType == AssetType.Texture)
				{
					assetData.m_mainAsset = assetData.m_assetBundleLoad.mainAsset;
				}
				assetData.m_loadedStatus = AssetStatus.CanUse;
				callback(assetData);
			}
		}

//		[IteratorStateMachine(typeof(AssetManager._003CGetAssetBundleFromWWW_003Ed__31))]
		private IEnumerator GetAssetBundleFromWWW(AssetData assetData, AssetManager.AssetCompleteCallBack callback)
		{
			int num = 0;
			string assetPath = null;
			while (num == 0)
			{
				assetPath = this.GetAssetPath(assetData);
				if (assetData.m_loadedStatus != AssetStatus.NotReady)
				{
					if (assetData.m_loadedStatus == AssetStatus.Downloading)
					{
						string saveKey = assetData.GetSaveKey();
						if (!this.m_dicCacheLoadingCallBackFunc.ContainsKey(saveKey))
						{
							Dictionary<string, AssetManager.AssetCompleteCallBack> arg_18A_0 = this.m_dicCacheLoadingCallBackFunc;
							string arg_18A_1 = saveKey;
							AssetManager.AssetCompleteCallBack arg_18A_2;
							if ((arg_18A_2 = AssetManager.__c.__9__31_0) == null)
							{
								arg_18A_2 = (AssetManager.__c.__9__31_0 = new AssetManager.AssetCompleteCallBack(AssetManager.__c.__9._GetAssetBundleFromWWW_b__31_0));
							}
							arg_18A_0.Add(arg_18A_1, arg_18A_2);
						}
						Dictionary<string, AssetManager.AssetCompleteCallBack> dicCacheLoadingCallBackFunc = this.m_dicCacheLoadingCallBackFunc;
						string key = saveKey;
						dicCacheLoadingCallBackFunc[key] = (AssetManager.AssetCompleteCallBack)Delegate.Combine(dicCacheLoadingCallBackFunc[key], callback);
					}
				//IL_1B9:
					yield break;
				}
				assetData.m_loadedStatus = AssetStatus.Downloading;
				if (!assetPath.Contains("://"))
				{
					UnityEngine.Debug.LogError("WWW加载路径错误，strAssetPath = : " + assetPath);
				}
				yield return MonoManager.Instance.StartCoroutine(AssetBundleManager.DownloadAssetBundle(assetPath, assetData.m_strCode, assetData.m_bEncrypt));
			}
			if (num != 1)
			{
				yield break;
			}
			assetData.m_assetBundleLoad = AssetBundleManager.GetAssetBundle(assetPath, assetData.m_strCode);
			if (assetData.m_assetType == AssetType.Prefab || assetData.m_assetType == AssetType.Audio || assetData.m_assetType == AssetType.Texture)
			{
				assetData.m_mainAsset = assetData.m_assetBundleLoad.mainAsset;
			}
			assetData.m_loadedStatus = AssetStatus.CanUse;
			callback(assetData);
		//goto IL_1B9;
		}

		public string GetAssetDataKey(AssetType type, string name)
		{
			return this.getTypePath(type) + name;
		}

		public void ReleaseAssetMemory(AssetType type, string name, bool unLoadAllObjects = true)
		{
			string assetDataKey = this.GetAssetDataKey(type, name);
			if (this.m_dicAssetData.ContainsKey(assetDataKey))
			{
				AssetData assetData = this.m_dicAssetData[assetDataKey];
				assetData.m_mainAsset = null;
				assetData.m_assetBundleLoad = null;
				assetData.m_loadedStatus = AssetStatus.NotReady;
				AssetBundleManager.Unload(this.GetAssetPath(assetData), assetData.m_strCode, unLoadAllObjects);
			}
		}

		public float GetDownloadProgress(AssetData assetData)
		{
			return AssetBundleManager.GetDownloadProgress(this.GetAssetPath(assetData), assetData.m_strCode);
		}

		public bool CheckAssetDataCanUse(AssetType type, string _name)
		{
			string assetDataKey = this.GetAssetDataKey(type, _name);
			if (!this.m_dicAssetData.ContainsKey(assetDataKey))
			{
				UnityEngine.Debug.LogWarning("no find key:" + assetDataKey);
				return true;
			}
			return this.m_dicAssetData[assetDataKey].m_loadedStatus == AssetStatus.CanUse;
		}

		public AssetData GetAssetDataCanUse(AssetType type, string _name)
		{
			string assetDataKey = this.GetAssetDataKey(type, _name);
			if (!this.m_dicAssetData.ContainsKey(assetDataKey))
			{
				UnityEngine.Debug.LogWarning("no get assetdata:" + assetDataKey);
				return null;
			}
			return this.m_dicAssetData[assetDataKey];
		}

		public string GetCsvData(string name)
		{
			string assetDataKey = this.GetAssetDataKey(AssetType.TextData, "CSV");
			if (!this.m_dicAssetData.ContainsKey(assetDataKey))
			{
				UnityEngine.Debug.LogError("加载CSV数据error!!! : " + assetDataKey);
				return "";
			}
			TextAsset textAsset = this.m_dicAssetData[assetDataKey].m_assetBundleLoad.LoadAsset(name) as TextAsset;
			if (textAsset == null)
			{
				UnityEngine.Debug.LogError("未找到CSV数据: " + name);
				return "";
			}
			return textAsset.text;
		}

		private string GetAssetPath(AssetData data)
		{
			string text = data.m_bUpdateFromHTTP ? this.m_assetBundlePath : this.m_localAssetBundlePath;
			if (data.m_strAssetPath != "")
			{
				text = text + this.getTypePath(data.m_assetType) + data.m_strAssetPath + AssetManager.BundleExtensionName();
			}
			return text;
		}

		private string getTypePath(AssetType type)
		{
			string result = "";
			switch (type)
			{
			case AssetType.Scene:
				result = "Scene/";
				break;
			case AssetType.Prefab:
				result = "Prefab/";
				break;
			case AssetType.TextData:
				result = "TextData/";
				break;
			case AssetType.Texture:
				result = "Texture/";
				break;
			case AssetType.Audio:
				result = "Audio/";
				break;
			}
			return result;
		}

		public string LocalAssetFromFilePath(string path)
		{
			return this.m_localAssetFromFilePath + path + AssetManager.BundleExtensionName();
		}
	}
}
