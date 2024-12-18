using DG.Tweening;
using System;
using UnityEngine;
using Util;

public class SkipView : MonoBehaviour
{
	public Transform m_obj;

	private void Start()
	{
	}

	public void showView()
	{
		this.WinDoScale();
		base.transform.gameObject.SetActive(true);
	}

	public void clickClose()
	{
		base.transform.gameObject.SetActive(false);
	}

	public void clickSkip()
	{
		base.transform.gameObject.SetActive(false);
		ControlsBase<AndroidControl>.Instance.CallAndroidUseToolsFunc("UseTools", "评分跳转");
		Singleton<GameManager>.Instance.m_UserInfo.m_isClickSkip = true;
		Singleton<GameManager>.Instance.OnPause();
		ControlsBase<AndroidControl>.Instance.CallAndroidSkipMarketFunc("skipMarket");
	}

	public void WinDoScale()
	{
		Vector3 endValue = new Vector3(1f, 1f, 1f);
		float duration = 0.3f;
		this.m_obj.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		this.m_obj.DOScale(endValue, duration).SetEase(Ease.OutBack);
	}

	private void Update()
	{
	}
}
