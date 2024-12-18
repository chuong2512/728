using JsonFx.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Engine
{
	public class AssetDownFileRecord
	{
		private class RecordData
		{
			public string m_strCode = "";

			public string m_strName;

			public bool IsSameName(string _name)
			{
				return this.m_strName == _name;
			}

			public bool IsSameVersion(string code)
			{
				return this.m_strCode == code;
			}
		}

		private object m_objLock;

		private List<AssetDownFileRecord.RecordData> m_lstCacheData;

		private static AssetDownFileRecord m_instance;

		public static AssetDownFileRecord Instance
		{
			get
			{
				if (AssetDownFileRecord.m_instance == null)
				{
					AssetDownFileRecord.m_instance = new AssetDownFileRecord();
				}
				return AssetDownFileRecord.m_instance;
			}
		}

		private AssetDownFileRecord()
		{
			this.m_objLock = new object();
			this.m_lstCacheData = new List<AssetDownFileRecord.RecordData>();
			this.loadCache();
		}

		private string getRecordName(string url)
		{
			string str = "";
			string str2 = "";
			this.setFolderName(url, ref str, ref str2);
			return str + "/" + str2;
		}

		private void setFolderName(string url, ref string floderName, ref string fileName)
		{
			int num = url.LastIndexOf("/");
			fileName = url.Substring(num + 1);
			url = url.Substring(0, num);
			num = url.LastIndexOf("/");
			floderName = url.Substring(num + 1);
		}

		public void CreateSaveFolder(string url)
		{
		}

		public bool HasCache(string url, string code)
		{
			object objLock = this.m_objLock;
			bool result;
			lock (objLock)
			{
				if (this.m_lstCacheData == null)
				{
					result = false;
				}
				else
				{
					string cacheDataSavePath = this.GetCacheDataSavePath(url);
					if (!File.Exists(cacheDataSavePath))
					{
						result = false;
					}
					else
					{
						string recordName = this.getRecordName(url);
						bool flag2 = false;
						AssetDownFileRecord.RecordData recordData = null;
						foreach (AssetDownFileRecord.RecordData current in this.m_lstCacheData)
						{
							if (current.IsSameName(recordName))
							{
								recordData = current;
								if (current.IsSameVersion(code))
								{
									flag2 = true;
									break;
								}
								break;
							}
						}
						if (recordData != null && !flag2)
						{
							this.m_lstCacheData.Remove(recordData);
							File.Delete(cacheDataSavePath);
						}
						result = flag2;
					}
				}
			}
			return result;
		}

		public string GetCacheDataSavePath(string url)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			this.setFolderName(url, ref empty, ref empty2);
			string text = Application.persistentDataPath + "/" + empty;
			string text2 = text + "/" + empty2;
			if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
			{
				text = text.Replace('/', '\\');
				text2 = text2.Replace('/', '\\');
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(text);
			if (!directoryInfo.Exists)
			{
				directoryInfo.Create();
			}
			return text2;
		}

		public void SaveCacheData(string url, string code)
		{
			object objLock = this.m_objLock;
			lock (objLock)
			{
				AssetDownFileRecord.RecordData recordData = new AssetDownFileRecord.RecordData();
				recordData.m_strName = this.getRecordName(url);
				recordData.m_strCode = code;
				this.m_lstCacheData.Add(recordData);
				this.saveCache();
			}
		}

		private void saveCache()
		{
			string arg_13_0 = this.getSaveCachePath();
			string contents = JsonWriter.Serialize(this.m_lstCacheData);
			File.WriteAllText(arg_13_0, contents);
		}

		private void loadCache()
		{
			string saveCachePath = this.getSaveCachePath();
			if (!File.Exists(saveCachePath))
			{
				return;
			}
			AssetDownFileRecord.RecordData[] arg_26_0 = JsonReader.Deserialize<AssetDownFileRecord.RecordData[]>(File.ReadAllText(saveCachePath));
			this.m_lstCacheData.Clear();
			AssetDownFileRecord.RecordData[] array = arg_26_0;
			for (int i = 0; i < array.Length; i++)
			{
				AssetDownFileRecord.RecordData item = array[i];
				this.m_lstCacheData.Add(item);
			}
		}

		private string getSaveCachePath()
		{
			return Application.persistentDataPath + "/_Cachedata.txt";
		}
	}
}
