using Engine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class CommonManager
{
	private sealed class _CaptureScreenshot_d__14 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int __1__state;

		private object __2__current;

		public Rect rect;

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

		public _CaptureScreenshot_d__14(int __1__state)
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
				this.__2__current = new WaitForEndOfFrame();
				this.__1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.__1__state = -1;
			Texture2D expr_51 = new Texture2D((int)this.rect.width, (int)this.rect.height, TextureFormat.RGB24, false);
			expr_51.ReadPixels(this.rect, 0, 0);
			expr_51.Apply();
			byte[] bytes = expr_51.EncodeToPNG();
			UnityEngine.Object.Destroy(expr_51);
			string str = "/Screenshot" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".png";
			File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + str, bytes);
			return false;
		}

		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	public static string GetStringFormatByNum(int value)
	{
		if (value >= 10 || value <= -10)
		{
			return value.ToString();
		}
		return "0" + value;
	}

	public static string GetStringFormatByWan(int value)
	{
		string result = string.Empty;
		if (value > 100000000)
		{
			if (value % 100000000 == 0)
			{
				result = (value / 100000000).ToString() + "亿";
			}
			else
			{
				result = (value / 100000000).ToString("f2") + "亿";
			}
		}
		else if (value > 10000)
		{
			if (value % 10000 == 0)
			{
				result = (value / 10000).ToString() + "万";
			}
			else
			{
				result = ((float)value / 10000f).ToString("f2") + "万";
			}
		}
		else
		{
			result = value.ToString();
		}
		return result;
	}

	public static bool IsNumString(string str)
	{
		return Regex.IsMatch(str, "^[0-9]+$");
	}

	public static bool IsIPString(string address)
	{
		string[] array = address.Split(new char[]
		{
			'.'
		});
		for (int i = 0; i < array.Length; i++)
		{
			if (!string.IsNullOrEmpty(array[i]))
			{
				int num = 0;
				if (!int.TryParse(array[i], out num))
				{
					return false;
				}
			}
		}
		return true;
	}

	public static string GetIPStr(string ipstring)
	{
		string[] array = ipstring.Split(new char[]
		{
			'.'
		});
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < array.Length; i++)
		{
			if (!string.IsNullOrEmpty(array[i]))
			{
				stringBuilder.Append(array[i]);
				stringBuilder.Append(".");
			}
		}
		string text = stringBuilder.ToString();
		return text.Substring(0, text.Length - 1);
	}

	public static IPAddress GetIPAddress(string ipStr)
	{
		ipStr = ipStr.Trim();
		char[] array = ipStr.ToCharArray();
		string text = string.Empty;
		int num = 0;
		while (num < array.Length && array[num].GetHashCode() != 0)
		{
			text += array[num].ToString();
			num++;
		}
		UnityEngine.Debug.Log(" oldip:" + ipStr + " newIPStr:" + text);
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			" oldLength:",
			ipStr.Length,
			" newLength:",
			text.Length
		}));
		return IPAddress.Parse(text);
	}

	public static string TrimByteString(string trimStr)
	{
		char[] array = trimStr.Trim().ToCharArray();
		string text = string.Empty;
		int num = 0;
		while (num < array.Length && array[num].GetHashCode() != 0)
		{
			text += array[num].ToString();
			num++;
		}
		return text;
	}

	public static string CutCharFromString(string cutStr, int len)
	{
		int num = 0;
		string text = string.Empty;
		string text2 = string.Empty;
		for (int i = 0; i < cutStr.Length; i++)
		{
			text = cutStr.Substring(i, 1);
			num++;
			if (Encoding.Default.GetBytes(text).Length > 1)
			{
				num++;
			}
			if (num > len)
			{
				break;
			}
			text2 += text;
		}
		return text2;
	}

	public static string CutCharFromStringEx(string cutStr, int len)
	{
		int num = 0;
		string text = string.Empty;
		string text2 = string.Empty;
		for (int i = 0; i < cutStr.Length; i++)
		{
			text = cutStr.Substring(i, 1);
			num++;
			if (Encoding.Default.GetBytes(text).Length > 1)
			{
				num++;
			}
			if (num > len)
			{
				text2 += "...";
				break;
			}
			text2 += text;
		}
		return text2;
	}

	public static SpawnPool GetSenceEffectPool()
	{
		return PoolManager.Pools.Create("SenceEffect");
	}

	public static SpawnPool GetAudioObjectPool()
	{
		return PoolManager.Pools.Create("AudioManager");
	}

	public static string StrHelper(int time)
	{
		int num = time / 60;
		int num2 = time % 60;
		return ((num < 10) ? ("0" + num) : num.ToString()) + ":" + ((num2 < 10) ? ("0" + num2) : num2.ToString());
	}

	public static string parseTimeSeconds(int t, int type = 0)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		if (t >= 86400)
		{
			num = (int)Convert.ToInt16(t / 86400);
			num2 = (int)Convert.ToInt16(t % 86400 / 3600);
			num3 = (int)Convert.ToInt16(t % 86400 % 3600 / 60);
			Convert.ToInt16(t % 86400 % 3600 % 60);
		}
		else if (t >= 3600)
		{
			num2 = (int)Convert.ToInt16(t / 3600);
			num3 = (int)Convert.ToInt16(t % 3600 / 60);
			Convert.ToInt16(t % 3600 % 60);
		}
		else if (t >= 60)
		{
			num3 = (int)Convert.ToInt16(t / 60);
			Convert.ToInt16(t % 60);
		}
		else
		{
			Convert.ToInt16(t);
		}
		string result;
		if (type == 0)
		{
			result = string.Format("{0}天{1}时{2}分", num, num2, num3);
		}
		else
		{
			result = string.Format("{0}年{1}月{2}日{3}时{4}分", num, num2, num3);
		}
		return result;
	}

	public static void parseTimeSeconds(int t, ref int totalday, ref int hours, ref int min)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		if (t >= 86400)
		{
			num = (int)Convert.ToInt16(t / 86400);
			num2 = (int)Convert.ToInt16(t % 86400 / 3600);
			num3 = (int)Convert.ToInt16(t % 86400 % 3600 / 60);
			Convert.ToInt16(t % 86400 % 3600 % 60);
		}
		else if (t >= 3600)
		{
			num2 = (int)Convert.ToInt16(t / 3600);
			num3 = (int)Convert.ToInt16(t % 3600 / 60);
			Convert.ToInt16(t % 3600 % 60);
		}
		else if (t >= 60)
		{
			num3 = (int)Convert.ToInt16(t / 60);
			Convert.ToInt16(t % 60);
		}
		else
		{
			Convert.ToInt16(t);
		}
		totalday = num;
		hours = num2;
		min = num3;
	}

//	[IteratorStateMachine(typeof(CommonManager._003CCaptureScreenshot_003Ed__14))]
	public static IEnumerator CaptureScreenshot(Rect rect)
	{
		yield return new WaitForEndOfFrame();
		Texture2D expr_51 = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
		expr_51.ReadPixels(rect, 0, 0);
		expr_51.Apply();
		byte[] bytes = expr_51.EncodeToPNG();
		UnityEngine.Object.Destroy(expr_51);
		string str = "/Screenshot" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".png";
		File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + str, bytes);
		yield break;
	}
}
