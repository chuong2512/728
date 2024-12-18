using System;

public class AndroidHelper : ViewBase<AndroidHelper>
{
	public static bool m_isNoAd;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void AdNoAdCallback()
	{
		AndroidHelper.m_isNoAd = true;
		ControlsBase<AndroidControl>.Instance.NoAdCallback();
	}

	public void AdBuySucCallback()
	{
		ControlsBase<AndroidControl>.Instance.BuySucCallback();
	}

	public void AdCompleteCallback()
	{
		ControlsBase<AndroidControl>.Instance.CompleteCallback();
	}

	public void AdSkipCallback()
	{
		ControlsBase<AndroidControl>.Instance.SkipCallback();
	}

	public void AdCloseCallback()
	{
		ControlsBase<AndroidControl>.Instance.CloseCallback();
	}

	public void AdErrorCallback()
	{
		ControlsBase<AndroidControl>.Instance.ErrorCallback();
	}

	public void AdReadyCallback()
	{
		ControlsBase<AndroidControl>.Instance.ReadyCallback();
	}
}
