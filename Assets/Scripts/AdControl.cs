/*
using System;

public class AdControl : ControlsBase<AdControl>
{
	private AdModels m_AdInfo;

	private bool m_IsInit;

	private bool m_IsAdInit;

	public void Init()
	{
		if (!this.m_IsInit)
		{
			this.m_IsInit = true;
			this.m_AdInfo = new AdModels();
			this.InitAdSDK();
			this.InitEventListener();
		}
	}

	public void InitEventListener()
	{
	}

	public void RemoveEventListener()
	{
	}

	private void InitAdSDK()
	{
		this.setAdIsLoad(true);
	}

	public void ShowNoAd(string id, AndroidControl.Callback succ)
	{
		ControlsBase<AndroidControl>.Instance.CallAndroidNoAdFunc("noAd", id, succ);
	}

	public void BuyItem(string id, AndroidControl.Callback succ)
	{
		ControlsBase<AndroidControl>.Instance.CallAndroidBuyFunc("buyItem", id, succ);
	}

	public void SetNoAd(AndroidControl.Callback succ)
	{
		ControlsBase<AndroidControl>.Instance.AdNoAdCallback = succ;
	}

	public void ShowIntAd(string id, AndroidControl.Callback succ, AndroidControl.Callback fail)
	{
		ControlsBase<AndroidControl>.Instance.CallAndroidFunc("showIntAd", id, succ, fail);
	}

	public void ShowRwAd(string id, AndroidControl.Callback complete, AndroidControl.Callback close, AndroidControl.Callback fail)
	{
		ControlsBase<AndroidControl>.Instance.CallAndroidFunc("showRwAd", id, complete, close, fail);
	}

	public void setAdIsLoad(bool isLoad)
	{
		this.m_AdInfo.IsLoad = isLoad;
	}

	public bool getAdIsLoad()
	{
		return this.m_AdInfo.IsLoad;
	}

	public void setAdIsShow(bool isShow)
	{
		this.m_AdInfo.IsShow = isShow;
	}

	public bool getAdIsShow()
	{
		return this.m_AdInfo.IsShow;
	}
}
*/