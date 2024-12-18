using System;
using UnityEngine;

public class TimeUtil : MonoBehaviour
{
	private static DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

	public static long GetTimeStamp(bool bflag = true)
	{
		TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
		long result;
		if (bflag)
		{
			result = Convert.ToInt64(timeSpan.TotalSeconds);
		}
		else
		{
			result = Convert.ToInt64(timeSpan.TotalMilliseconds);
		}
		return result;
	}

	public static string GetSecondString(long second)
	{
		if (second >= 86400L)
		{
			return string.Concat(new object[]
			{
				second / 86400L,
				"d ",
				string.Format("{0:D2}", second % 86400L / 3600L),
				":",
				string.Format("{0:D2}", second % 3600L / 60L),
				":",
				string.Format("{0:D2}", second % 60L)
			});
		}
		return string.Concat(new string[]
		{
			string.Format("{0:D2}", second / 3600L),
			":",
			string.Format("{0:D2}", second % 3600L / 60L),
			":",
			string.Format("{0:D2}", second % 60L)
		});
	}

	public static string GetSecondStrings(long second)
	{
		if (second >= 3600L)
		{
			return string.Concat(new string[]
			{
				string.Format("{0:D2}", second / 3600L),
				" : ",
				string.Format("{0:D2}", second % 3600L / 60L),
				" : ",
				string.Format("{0:D2}", second % 60L)
			});
		}
		return string.Format("{0:D2}", second % 3600L / 60L) + " : " + string.Format("{0:D2}", second % 60L);
	}

	public static string NormalizeTimpstamp0(long timpStamp)
	{
		long ticks = timpStamp * 10000000L;
		TimeSpan value = new TimeSpan(ticks);
		return TimeUtil.dtStart.Add(value).ToString("yyyy-MM-dd");
	}
}
