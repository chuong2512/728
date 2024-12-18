using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Engine
{
	public class SystemManager
	{
		private sealed class _DelayToAction_d__39 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int __1__state;

			private object __2__current;

			public float delaySeconds;

			public Action action;

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

			public _DelayToAction_d__39(int __1__state)
			{
				this.__1__state = __1__state;
			}

			void IDisposable.Dispose()
			{
			}

			bool IEnumerator.MoveNext()
			{
				int num = this.__1__state;
				if (num == 0)
				{
					this.__1__state = -1;
					this.__2__current = new WaitForSeconds(this.delaySeconds);
					this.__1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.__1__state = -1;
				this.action();
				return false;
			}

			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		private static int MAX_CHAR_COUNT = 100000;

		private static string WEB_PID_SIGN = "jq2016!@#";

		private static byte[] s_abyDecryptMap = new byte[]
		{
			81,
			161,
			158,
			176,
			30,
			131,
			28,
			45,
			233,
			119,
			61,
			19,
			147,
			16,
			69,
			255,
			109,
			201,
			32,
			47,
			27,
			130,
			26,
			125,
			245,
			207,
			82,
			168,
			210,
			164,
			180,
			11,
			49,
			151,
			87,
			25,
			52,
			223,
			91,
			65,
			88,
			73,
			170,
			95,
			10,
			239,
			136,
			1,
			220,
			149,
			212,
			175,
			123,
			227,
			17,
			142,
			157,
			22,
			97,
			140,
			132,
			60,
			31,
			90,
			2,
			79,
			57,
			254,
			4,
			7,
			92,
			139,
			238,
			102,
			51,
			196,
			200,
			89,
			181,
			93,
			194,
			108,
			246,
			77,
			251,
			174,
			74,
			75,
			243,
			53,
			44,
			202,
			33,
			120,
			59,
			3,
			253,
			36,
			189,
			37,
			55,
			41,
			172,
			78,
			249,
			146,
			58,
			50,
			76,
			218,
			6,
			94,
			0,
			148,
			96,
			236,
			23,
			152,
			215,
			62,
			203,
			106,
			169,
			217,
			156,
			187,
			8,
			143,
			64,
			160,
			111,
			85,
			103,
			135,
			84,
			128,
			178,
			54,
			71,
			34,
			68,
			99,
			5,
			107,
			240,
			15,
			199,
			144,
			197,
			101,
			226,
			100,
			250,
			213,
			219,
			18,
			122,
			14,
			216,
			126,
			153,
			209,
			232,
			214,
			134,
			39,
			191,
			193,
			110,
			222,
			154,
			9,
			13,
			171,
			225,
			145,
			86,
			205,
			179,
			118,
			12,
			195,
			211,
			159,
			66,
			182,
			155,
			229,
			35,
			167,
			173,
			24,
			198,
			244,
			184,
			190,
			21,
			67,
			112,
			224,
			231,
			188,
			241,
			186,
			165,
			166,
			83,
			117,
			228,
			235,
			230,
			133,
			20,
			72,
			221,
			56,
			42,
			204,
			127,
			177,
			192,
			113,
			150,
			248,
			63,
			40,
			242,
			105,
			116,
			104,
			183,
			163,
			80,
			208,
			121,
			29,
			252,
			206,
			138,
			141,
			46,
			98,
			48,
			234,
			237,
			43,
			38,
			185,
			129,
			124,
			70,
			137,
			115,
			162,
			247,
			114
		};

		private static byte[] s_abyEncryptMap = new byte[]
		{
			112,
			47,
			64,
			95,
			68,
			142,
			110,
			69,
			126,
			171,
			44,
			31,
			180,
			172,
			157,
			145,
			13,
			54,
			155,
			11,
			212,
			196,
			57,
			116,
			191,
			35,
			22,
			20,
			6,
			235,
			4,
			62,
			18,
			92,
			139,
			188,
			97,
			99,
			246,
			165,
			225,
			101,
			216,
			245,
			90,
			7,
			240,
			19,
			242,
			32,
			107,
			74,
			36,
			89,
			137,
			100,
			215,
			66,
			106,
			94,
			61,
			10,
			119,
			224,
			128,
			39,
			184,
			197,
			140,
			14,
			250,
			138,
			213,
			41,
			86,
			87,
			108,
			83,
			103,
			65,
			232,
			0,
			26,
			206,
			134,
			131,
			176,
			34,
			40,
			77,
			63,
			38,
			70,
			79,
			111,
			43,
			114,
			58,
			241,
			141,
			151,
			149,
			73,
			132,
			229,
			227,
			121,
			143,
			81,
			16,
			168,
			130,
			198,
			221,
			255,
			252,
			228,
			207,
			179,
			9,
			93,
			234,
			156,
			52,
			249,
			23,
			159,
			218,
			135,
			248,
			21,
			5,
			60,
			211,
			164,
			133,
			46,
			251,
			238,
			71,
			59,
			239,
			55,
			127,
			147,
			175,
			105,
			12,
			113,
			49,
			222,
			33,
			117,
			160,
			170,
			186,
			124,
			56,
			2,
			183,
			129,
			1,
			253,
			231,
			29,
			204,
			205,
			189,
			27,
			122,
			42,
			173,
			102,
			190,
			85,
			51,
			3,
			219,
			136,
			178,
			30,
			78,
			185,
			230,
			194,
			247,
			203,
			125,
			201,
			98,
			195,
			166,
			220,
			167,
			80,
			181,
			75,
			148,
			192,
			146,
			76,
			17,
			91,
			120,
			217,
			177,
			237,
			25,
			233,
			161,
			28,
			182,
			50,
			153,
			163,
			118,
			158,
			123,
			109,
			154,
			48,
			214,
			169,
			37,
			199,
			174,
			150,
			53,
			208,
			187,
			210,
			200,
			162,
			8,
			243,
			209,
			115,
			244,
			72,
			45,
			144,
			202,
			226,
			88,
			193,
			24,
			82,
			254,
			223,
			104,
			152,
			84,
			236,
			96,
			67,
			15
		};

		private static string m_EncryptSign = "JQEF";

		public static string EncryptWebPID(int dwUserID)
		{
			byte[] bytes = BitConverter.GetBytes(dwUserID);
			byte[] bytes2 = Encoding.GetEncoding("utf-8").GetBytes(SystemManager.WEB_PID_SIGN);
			int num = bytes.Length + bytes2.Length;
			byte[] array = new byte[num];
			Array.Copy(bytes, array, bytes.Length);
			Array.Copy(bytes2, 0, array, bytes.Length, bytes2.Length);
			byte b = 151;
			for (int i = 0; i < num; i++)
			{
				array[i] ^= b;
				b += 18;
			}
			char[] array2 = new char[]
			{
				'0',
				'1',
				'2',
				'3',
				'4',
				'5',
				'6',
				'7',
				'8',
				'9',
				'A',
				'B',
				'C',
				'D',
				'E',
				'F'
			};
			char[] array3 = new char[num * 2];
			for (int i = 0; i < num; i++)
			{
				byte expr_95 = array[i];
				byte b2 = (byte)(expr_95 & 15);
				byte b3 = (byte)((expr_95 & 240) >> 4);
				array3[i * 2] = array2[(int)b3];
				array3[i * 2 + 1] = array2[(int)b2];
			}
			return new string(array3);
		}

		public static bool DecryptWebPID(string str, ref int dwUserID)
		{
			string arg_05_0 = string.Empty;
			char[] array = str.ToCharArray();
			int num = array.Length / 2;
			byte[] array2 = new byte[num];
			for (int i = 0; i < num; i++)
			{
				char c = array[i * 2];
				char c2 = array[i * 2 + 1];
				byte b = (byte)((c <= '9') ? (c - '0') : (c - 'A' + '\n'));
				byte b2 = (byte)((int)((byte)((c2 <= '9') ? (c2 - '0') : (c2 - 'A' + '\n')) & 15) | (int)(b & 15) << 4);
				array2[i] = b2;
			}
			if (num <= 4)
			{
				dwUserID = 0;
				return false;
			}
			byte b3 = 151;
			for (int i = 0; i < num; i++)
			{
				array2[i] ^= b3;
				b3 += 18;
			}
			byte[] array3 = new byte[array2.Length - 4];
			Array.Copy(array2, 4, array3, 0, array3.Length);
			dwUserID = BitConverter.ToInt32(array2, 0);
			return Encoding.UTF8.GetString(array3).Trim(new char[1]).Trim() == SystemManager.WEB_PID_SIGN;
		}

		public static bool DecryptData(byte[] pData, int startIndex, int nLen, byte byCheckSum)
		{
			for (int i = startIndex; i < nLen; i++)
			{
				byte b = SystemManager.s_abyDecryptMap[(int)pData[i]];
				byCheckSum += b;
				pData[i] = b;
			}
			return byCheckSum == 0;
		}

		public static byte EncryptData(byte[] pData, int startIndex, int nLen)
		{
			byte b = 0;
			for (int i = startIndex; i < nLen; i++)
			{
				byte b2 = pData[i];
				b += b2;
				pData[i] = SystemManager.s_abyEncryptMap[(int)b2];
			}
			return (byte)(~b + 1);
		}

		public static int EncryptBundleFile(string assetBundlePath, string strBundleFilePath, bool bNeedDelete = true)
		{
			byte[] array = File.ReadAllBytes(assetBundlePath);
			byte[] array2 = SystemManager.StringDefaultByteArray(SystemManager.m_EncryptSign);
			int num = array.Length;
			int num2 = array2.Length;
			if (array.Length > num2)
			{
				byte[] array3 = new byte[num2];
				Array.Copy(array, 0, array3, 0, num2);
				string b = SystemManager.ByteArrayDefaultString(array3);
				if (SystemManager.m_EncryptSign == b)
				{
					return num;
				}
			}
			if (bNeedDelete)
			{
				File.Delete(assetBundlePath);
			}
			FileStream arg_83_0 = new FileStream(strBundleFilePath, FileMode.Create);
			SystemManager.EncryptResData(array, num);
			byte[] array4 = new byte[num2 + num];
			Array.Copy(array2, 0, array4, 0, num2);
			Array.Copy(array, 0, array4, num2, num);
			arg_83_0.Write(array4, 0, array4.Length);
			arg_83_0.Close();
			return array4.Length;
		}

		public static byte[] DecryptBundleFile(byte[] buff)
		{
			if (buff.Length < 4)
			{
				return buff;
			}
			byte[] array = new byte[4];
			Array.Copy(buff, 0, array, 0, array.Length);
			string text = SystemManager.ByteArrayDefaultString(array);
			if (SystemManager.m_EncryptSign != text)
			{
				UnityEngine.Debug.Log("解密标记不对，不需要解密" + text);
				return buff;
			}
			byte[] array2 = new byte[buff.Length - 4];
			Array.Copy(buff, 4, array2, 0, array2.Length);
			SystemManager.DecryptResData(array2, array2.Length);
			return array2;
		}

		private static void EncryptResData(byte[] pbyData, int nLen)
		{
			byte b = 152;
			for (int i = 0; i < nLen; i++)
			{
				pbyData[i] ^= b;
			}
		}

		private static void DecryptResData(byte[] pbyData, int nLen)
		{
			byte b = 152;
			for (int i = 0; i < nLen; i++)
			{
				pbyData[i] ^= b;
			}
		}

		public static string GBKArrayToString(byte[] data)
		{
			if (data == null)
			{
				UnityEngine.Debug.LogWarning("data is null!");
				return "";
			}
			byte[] bytes = SystemManager.GBK_To_UTF16(data);
			return Encoding.Unicode.GetString(bytes).Trim(new char[1]).Trim();
		}

		public static string ByteArrayToString(byte[] data)
		{
			if (data == null)
			{
				UnityEngine.Debug.LogWarning("data is null!");
				return "";
			}
			return Encoding.Unicode.GetString(data).Trim(new char[1]).Trim();
		}

		public static string ByteArrayDefaultString(byte[] data)
		{
			if (data == null)
			{
				UnityEngine.Debug.LogWarning("data is null!");
				return "";
			}
			return Encoding.Default.GetString(data).Trim(new char[1]).Trim();
		}

		public static string ShotArrayToString(ushort[] shotsArray)
		{
			return SystemManager.GBKArrayToString(SystemManager.Shorts2Bytes(shotsArray));
		}

		public static byte[] Shorts2Bytes(ushort[] s)
		{
			byte b = 2;
			byte[] array = new byte[s.Length * (int)b];
			for (int i = 0; i < s.Length; i++)
			{
				byte[] bytes = SystemManager.getBytes(s[i], false);
				for (int j = 0; j < (int)b; j++)
				{
					array[i * (int)b + j] = bytes[j];
				}
			}
			return array;
		}

		public static byte[] getBytes(ushort s, bool bBigEnding)
		{
			byte[] array = new byte[2];
			if (bBigEnding)
			{
				for (int i = array.Length - 1; i >= 0; i--)
				{
					array[i] = (byte)(s & 255);
					s = (ushort)(s >> 8);
				}
			}
			else
			{
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = (byte)(s & 255);
					s = (ushort)(s >> 8);
				}
			}
			return array;
		}

		public static byte[] StringDefaultByteArray(string msg)
		{
			return Encoding.Default.GetBytes(msg);
		}

		public static byte[] StringToByteArray(string msg)
		{
			return Encoding.Unicode.GetBytes(msg);
		}

		public static byte[] StringToGBKArray(string msg)
		{
			return SystemManager.UTF16_To_GBK(SystemManager.ShortConverter(SystemManager.StringToByteArray(msg)));
		}

		public static byte[] StringToByteArray(string msg, int length)
		{
			byte[] array = new byte[length];
			byte[] bytes = Encoding.Unicode.GetBytes(msg);
			for (int i = 0; i < bytes.Length; i++)
			{
				array[i] = bytes[i];
			}
			return array;
		}

		public static string AddStringColor(string content, string color)
		{
			return string.Concat(new string[]
			{
				"[",
				color,
				"]",
				content,
				"[-]"
			});
		}

		public static string Base64Encode(string s)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(s)).Replace("+", "%2b").Replace("=", "%3d");
		}

		public static byte[] GBK_To_UTF16(byte[] szGBK)
		{
			List<ushort> list = new List<ushort>();
			if (szGBK == null || list == null)
			{
				return SystemManager.ByteConverter(list);
			}
			int num = szGBK.Length;
			if (num > SystemManager.MAX_CHAR_COUNT)
			{
				return SystemManager.ByteConverter(list);
			}
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				byte b = szGBK[i];
				if (b < 128)
				{
					list.Add((ushort)b);
					num2++;
				}
				else
				{
					i++;
					byte b2 = 0;
					if (i < num)
					{
						b2 = szGBK[i];
					}
					int num3 = (int)b + (int)b2 * 256;
					ushort item = Tab_GBK_To_UTF16.m_aCharTable_GBK_To_UTF16[num3];
					list.Add(item);
					num2++;
				}
			}
			list.Add(0);
			return SystemManager.ByteConverter(list);
		}

		public static byte[] UTF16_To_GBK(ushort[] szUTF16)
		{
			List<byte> list = new List<byte>();
			if (szUTF16 == null || list == null)
			{
				return SystemManager.ByteConverter(list);
			}
			int num = SystemManager.Wcslen_UTF16(szUTF16);
			if (num > SystemManager.MAX_CHAR_COUNT)
			{
				return SystemManager.ByteConverter(list);
			}
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				ushort num3 = szUTF16[i];
				ushort num4 = Tab_UTF16_To_GBK.m_aCharTable_UTF16_To_GBK[(int)num3];
				if (num4 < 256)
				{
					list.Add((byte)num4);
					num2++;
				}
				else
				{
					byte[] bytes = BitConverter.GetBytes(num4);
					list.Add(bytes[0]);
					list.Add(bytes[1]);
					num2++;
					num2++;
				}
			}
			list.Add(0);
			return SystemManager.ByteConverter(list);
		}

		public static int Wcslen_UTF16(ushort[] szUTF16)
		{
			int num = 0;
			int num2 = szUTF16.Length;
			while (num < num2 && szUTF16[num] != 0)
			{
				num++;
			}
			return num;
		}

		public static ushort[] ShortConverter(byte[] source)
		{
			List<ushort> list = new List<ushort>();
			int num = source.Length;
			for (int i = 0; i < num; i++)
			{
				byte[] expr_15 = new byte[2];
				expr_15[0] = ((byte)((i >= num) ? 0 : source[i]));
				i++;
				expr_15[1] = ((byte)((i >= num) ? 0 : source[i]));
				ushort item = BitConverter.ToUInt16(expr_15, 0);
				list.Add(item);
			}
			int count = list.Count;
			ushort[] array = new ushort[count];
			for (int j = 0; j < count; j++)
			{
				array[j] = list[j];
			}
			return array;
		}

		public static byte[] ByteConverter(List<byte> source)
		{
			int count = source.Count;
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = source[i];
			}
			return array;
		}

		public static byte[] ByteConverter(List<ushort> source)
		{
			List<byte> list = new List<byte>();
			int i = 0;
			int count = source.Count;
			while (i < count)
			{
				if (source[i] == 0)
				{
					list.Add(0);
					break;
				}
				byte[] bytes = BitConverter.GetBytes(source[i]);
				list.AddRange(bytes);
				i++;
			}
			int count2 = list.Count;
			byte[] array = new byte[count2];
			for (int j = 0; j < count2; j++)
			{
				array[j] = list[j];
			}
			return array;
		}

		public static int ReturnFrontValueInByte(byte value)
		{
			return (value & 240) >> 4;
		}

		public static int ReturnBackValueInByte(byte value)
		{
			return (int)(value & 15);
		}

		public static int ReturnFrontValueInShort(short value)
		{
			return ((int)value & 65280) >> 8;
		}

		public static int ReturnBackValueInShort(short value)
		{
			return (int)(value & 255);
		}

		public static void DestroyGameObject(GameObject obj, bool bUnloadAsset = false)
		{
			UnityEngine.Object.Destroy(obj);
			if (bUnloadAsset)
			{
				Resources.UnloadUnusedAssets();
			}
		}

		public static void DestroyImmediate(GameObject obj, bool bUnloadAsset = false)
		{
			UnityEngine.Object.DestroyImmediate(obj);
			if (bUnloadAsset)
			{
				Resources.UnloadUnusedAssets();
			}
		}

		public static Transform GetChildrenByName(Transform go, string name)
		{
			foreach (Transform transform in go)
			{
				if (transform.name == name)
				{
					return transform;
				}
			}
			return null;
		}

		public static int GetLocSystemTime()
		{
			DateTime d = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			return (int)(DateTime.Now - d).TotalSeconds;
		}

//		[IteratorStateMachine(typeof(SystemManager._003CDelayToAction_003Ed__39))]
		public static IEnumerator DelayToAction(Action action, float delaySeconds)
		{
			yield return new WaitForSeconds(delaySeconds);
			action();
			yield break;
		}

		public static bool CheckIsMobilePlatform()
		{
			return Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
		}

		public static int CalculateString(string inputStr)
		{
			int num = 0;
			for (int i = 0; i < inputStr.Length; i++)
			{
				string s = inputStr.Substring(i, 1);
				if (Encoding.Default.GetBytes(s).Length > 1)
				{
					num++;
				}
			}
			return num;
		}

		public static string Encrypt(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return input;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(input);
			return BitConverter.ToString(MD5.Create().ComputeHash(bytes)).Replace("-", "").ToLower();
		}

		public static string EncryptByMD5(string Source)
		{
			byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(Source);
			byte[] array = new MD5CryptoServiceProvider().ComputeHash(bytes);
			StringBuilder stringBuilder = new StringBuilder(32);
			int i = 0;
			int num = array.Length;
			while (i < num)
			{
				stringBuilder.Append(array[i].ToString("x").PadLeft(2, '0'));
				i++;
			}
			return stringBuilder.ToString();
		}

		public static byte[] EncryptPwd(string Source)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(Source);
			byte[] array = new MD5CryptoServiceProvider().ComputeHash(bytes);
			StringBuilder stringBuilder = new StringBuilder(32);
			int i = 0;
			int num = array.Length;
			while (i < num)
			{
				stringBuilder.Append(array[i].ToString("x").PadLeft(2, '0'));
				i++;
			}
			return SystemManager.StringToGBKArray(stringBuilder.ToString());
		}

		public static DateTime StampToDateTime(string timeStamp)
		{
			DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			long ticks = long.Parse(timeStamp + "0000000");
			TimeSpan value = new TimeSpan(ticks);
			return dateTime.Add(value);
		}

		public static int DateTimeToStamp(DateTime time)
		{
			DateTime d = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			return (int)(time - d).TotalSeconds;
		}
	}
}
