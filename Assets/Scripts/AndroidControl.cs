using System;
using UnityEngine;
using Util;

public class AndroidControl : ControlsBase<AndroidControl>
{
	public delegate void Callback();

	private bool m_IsInit;

	public AndroidControl.Callback AdBuySucCallback;

	public AndroidControl.Callback AdNoAdCallback;

	public AndroidControl.Callback AdCompleteCallback;

	public AndroidControl.Callback AdSkipCallback;

	public AndroidControl.Callback AdCloseCallback;

	public AndroidControl.Callback AdErrorCallback;

	public AndroidControl.Callback AdReadyCallback;

	public void Init()
	{
		if (!this.m_IsInit)
		{
			this.m_IsInit = true;
		}
	}

	public void CallAndroidFunc(string funcName, string para, AndroidControl.Callback complete, AndroidControl.Callback close, AndroidControl.Callback error)
	{
        /*
		this.AdCompleteCallback = complete;
		this.AdCloseCallback = close;
		this.AdErrorCallback = error;
		if (Application.platform != RuntimePlatform.Android)
		{
			this.CompleteCallback();
			return;
		}
		AndroidJavaObject @static = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
		if (para.Length <= 0)
		{
			@static.Call(funcName, Array.Empty<object>());
			return;
		}
		@static.Call(funcName, new object[]
		{
			para
		});
		*/
	}

	public void CallAndroidFunc(string funcName, string para, AndroidControl.Callback complete, AndroidControl.Callback error)
	{
        /*
		this.AdCompleteCallback = complete;
		this.AdErrorCallback = error;
		if (Application.platform != RuntimePlatform.Android)
		{
			this.CompleteCallback();
			return;
		}
		AndroidJavaObject @static = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
		if (para.Length <= 0)
		{
			@static.Call(funcName, Array.Empty<object>());
			return;
		}
		@static.Call(funcName, new object[]
		{
			para
		});
		*/
	}

	public void CallAndroidFunc(string funcName, string para, AndroidControl.Callback ready)
	{
        /*
		this.AdErrorCallback = null;
		this.AdReadyCallback = ready;
		if (Application.platform == RuntimePlatform.Android)
		{
			AndroidJavaObject @static = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			if (para.Length <= 0)
			{
				@static.Call(funcName, Array.Empty<object>());
				return;
			}
			@static.Call(funcName, new object[]
			{
				para
			});
		}
		*/
	}

	public void CallAndroidStartMissionFunc(string funcName, string para)
	{
        /*
		if (Application.platform == RuntimePlatform.Android)
		{
			new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Call(funcName, new object[]
			{
				para
			});
		}
		*/
	}

	public void CallAndroidEndMissionFunc(string funcName, string para, bool isWin, string failPara)
	{
        /*
		if (Application.platform == RuntimePlatform.Android)
		{
			new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Call(funcName, new object[]
			{
				para,
				isWin,
				failPara
			});
		}
		*/
	}

	public void CallAndroidUseToolsFunc(string funcName, string para)
	{
        /*
		if (Application.platform == RuntimePlatform.Android)
		{
			new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Call(funcName, new object[]
			{
				para
			});
		}
		*/
	}

	public void CallAndroidNoAdFunc(string funcName, string para, AndroidControl.Callback noad)
	{
        /*
		this.AdNoAdCallback = noad;
		if (Application.platform != RuntimePlatform.Android)
		{
			this.AdNoAdCallback();
			return;
		}
		AndroidJavaObject @static = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
		if (para.Length <= 0)
		{
			@static.Call(funcName, Array.Empty<object>());
			return;
		}
		@static.Call(funcName, new object[]
		{
			para
		});
		*/
	}

	public void CallAndroidBuyFunc(string funcName, string para, AndroidControl.Callback noad)
	{
        /*
		this.AdBuySucCallback = noad;
		if (Application.platform != RuntimePlatform.Android)
		{
			this.AdBuySucCallback();
			return;
		}
		AndroidJavaObject @static = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
		if (para.Length <= 0)
		{
			@static.Call(funcName, Array.Empty<object>());
			return;
		}
		@static.Call(funcName, new object[]
		{
			para
		});
		*/
	}

	public void CompleteCallback()
	{
        /*
		UnityEngine.Debug.Log("AndroidControl call succ");
		if (this.AdCompleteCallback != null)
		{
			this.AdCompleteCallback();
		}
		*/
	}

	public void SkipCallback()
	{
		UnityEngine.Debug.Log("AndroidControl call Fail");
		if (this.AdSkipCallback != null)
		{
			this.AdSkipCallback();
		}
	}

	public void ErrorCallback()
	{
        /*
		UnityEngine.Debug.Log("AndroidControl call Fail");
		if (this.AdErrorCallback != null)
		{
			this.AdErrorCallback();
		}
		*/
	}

	public void CloseCallback()
	{
		if (this.AdCloseCallback != null)
		{
			this.AdCloseCallback();
		}
	}

	public void ReadyCallback()
	{
		if (this.AdReadyCallback != null)
		{
			this.AdReadyCallback();
		}
	}

	public void BuySucCallback()
	{
        /*
		if (this.AdBuySucCallback != null)
		{
			this.AdBuySucCallback();
		}
		*/
	}

	public void NoAdCallback()
	{
        /*
		if (this.AdNoAdCallback != null)
		{
			this.AdNoAdCallback();
		}
		*/
	}

	public void PlayShock(int val)
	{
        /*
		if (!Singleton<GameManager>.Instance.m_UserInfo.YaoState)
		{
			return;
		}
		long[] array = new long[]
		{
			0L,
			(long)val
		};
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity").Call("StartShock", new object[]
			{
				array,
				-1
			});
		}
		*/
	}

	public void CallAndroidSkipMarketFunc(string funcName)
	{
        /*
		if (Application.platform == RuntimePlatform.Android)
		{
			new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Call(funcName, Array.Empty<object>());
		}
		*/
	}
}
