using System;
using UnityEngine;

namespace Engine
{
	public class ReadCsvTools
	{
		private string[][] m_strArray;

		private int m_headIndex;

		private int m_dataIndex;

		private string m_path;

		private int m_count;

		private int m_colCount;

		public ReadCsvTools(string pathOrData, int headIndex = 2, int dataIndex = 4, int loadType = 0)
		{
			this.m_headIndex = headIndex - 1;
			this.m_dataIndex = dataIndex - 1;
			if (loadType == 0)
			{
				this.LoadCvs(pathOrData);
				return;
			}
			this.LoadCsvByData(pathOrData);
		}

		private void LoadCvs(string path)
		{
			TextAsset textAsset = ResourcesLoad.Load(path, typeof(TextAsset)) as TextAsset;
			if (!textAsset)
			{
				return;
			}
			this.LoadCsvByData(textAsset.text);
		}

		private void LoadCsvByData(string data)
		{
			string[] array = data.Split(new char[]
			{
				'\r',
				'\n'
			}, StringSplitOptions.RemoveEmptyEntries);
			this.m_strArray = new string[array.Length][];
			for (int i = 0; i < array.Length; i++)
			{
				this.m_strArray[i] = array[i].Split(new char[]
				{
					','
				});
			}
			if (this.m_strArray.Length != 0)
			{
				this.m_colCount = this.m_strArray[0].Length;
				this.m_count = array.Length - this.m_dataIndex;
			}
		}

		public int GetRowsCount()
		{
			return this.m_count;
		}

		public string GetDataByRowAndCol(int nRow, int nCol)
		{
			if (this.m_strArray.Length == 0 || nRow > this.m_strArray.Length)
			{
				return "";
			}
			if (nCol >= this.m_strArray[0].Length)
			{
				return "";
			}
			if (this.m_strArray[nRow + this.m_dataIndex][nCol] == "")
			{
				return "0";
			}
			return this.m_strArray[nRow + this.m_dataIndex][nCol];
		}

		public string GetDataByRowAndName(int nRow, string strName)
		{
			if (this.m_strArray.Length == 0)
			{
				return "";
			}
			int i = 0;
			while (i < this.m_colCount)
			{
				if (this.m_strArray[this.m_headIndex][i] == strName)
				{
					if (this.m_strArray[nRow + this.m_dataIndex][i] == "")
					{
						return "0";
					}
					return this.m_strArray[nRow + this.m_dataIndex][i];
				}
				else
				{
					i++;
				}
			}
			return "";
		}

		public int GetColumnCount()
		{
			return this.m_colCount;
		}
	}
}
