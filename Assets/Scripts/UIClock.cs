using Engine;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIClock : MonoBehaviour
{
	public delegate void TimeOutEvent();

	public Text m_Label_ClockTime;

	private UIClock.TimeOutEvent m_onTimeOut;

	private int m_clockTime;

	private bool m_bAddZero;

	private int m_endTime;

	private void Start()
	{
	}

	public void ResetTimeOutEvent()
	{
		this.m_onTimeOut = null;
	}

	public void StartClock(int time, UIClock.TimeOutEvent timeOutEvent, bool isAddZero = false)
	{
		this.m_clockTime = time;
		this.m_bAddZero = isAddZero;
		if (time > 0)
		{
			this.UpdateLabelClockTime();
		}
		base.CancelInvoke("ReduceTime");
		this.m_onTimeOut = null;
		if (timeOutEvent != null)
		{
			this.m_onTimeOut = timeOutEvent;
		}
		if (time > 0)
		{
			this.m_endTime = SystemManager.GetLocSystemTime() + time;
			this.m_Label_ClockTime.gameObject.SetActive(true);
			base.Invoke("ReduceTime", 1f);
			return;
		}
		this.m_Label_ClockTime.text = CommonManager.GetStringFormatByNum(0);
	}

	public void StopClock()
	{
		this.m_onTimeOut = null;
		this.m_clockTime = 0;
		if (base.IsInvoking("ReduceTime"))
		{
			base.CancelInvoke("ReduceTime");
		}
	}

	private void ReduceTime()
	{
		this.m_clockTime--;
		int locSystemTime = SystemManager.GetLocSystemTime();
		if (this.m_endTime - locSystemTime < this.m_clockTime)
		{
			this.m_clockTime = this.m_endTime - locSystemTime;
		}
		if (this.m_clockTime >= 0)
		{
			this.UpdateLabelClockTime();
			base.Invoke("ReduceTime", 1f);
			return;
		}
		if (this.m_bAddZero)
		{
			int arg_60_0 = this.m_clockTime;
		}
		if (this.m_onTimeOut != null)
		{
			this.m_onTimeOut();
		}
		this.m_Label_ClockTime.text = CommonManager.GetStringFormatByNum(0);
	}

	public int GetClockTime()
	{
		return this.m_clockTime;
	}

	private void UpdateLabelClockTime()
	{
		if (this.m_bAddZero && this.m_clockTime < 10 && this.m_clockTime <= 5)
		{
			int arg_23_0 = this.m_clockTime;
		}
		this.m_Label_ClockTime.text = CommonManager.GetStringFormatByNum(this.m_clockTime);
	}
}
