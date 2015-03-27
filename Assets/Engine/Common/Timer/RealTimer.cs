using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RealTimer
{
	internal float duration;
	float endTime;

	public RealTimer()
	{
		duration = 0;
		endTime = 0;
	}

	public RealTimer(float duration)
	{
		this.duration = duration;
		this.endTime = Time.realtimeSinceStartup + duration;
	}

	public RealTimer(float duration, float initialPercent)
	{
		this.duration = duration;
		this.endTime = Time.realtimeSinceStartup + duration * (1 - initialPercent);
	}

	public bool IsEnded()
	{
		//return (duration == 0 && endTime==0) || (Time.time > endTime);
		return Time.realtimeSinceStartup >= endTime;
	}

	public float GetCompletePercent()
	{
		if (duration > 0)
		{
			return Mathf.Clamp01(1 - (endTime - Time.realtimeSinceStartup) / duration);
		}
		else
		{
			return 1;
		}
	}

	public float Left()
	{
		return Mathf.Max(0, endTime - Time.realtimeSinceStartup);
	}

	public float Passed()
	{
		return Mathf.Max(0, duration - (endTime - Time.realtimeSinceStartup));
	}

	public string LeftString()
	{
		int leftSeconds = (int)Left();
		return string.Format("{0}:{1}", leftSeconds / 60, Utils.Format00(leftSeconds%60));
	}

	public void Speedup(float times)
	{
		float oldEnd = endTime;
		endTime = Time.realtimeSinceStartup + Left() / times;
		duration -= oldEnd - endTime;
	}

	public void Shift(float timeAdd)
	{
		endTime += timeAdd;
	}
}
