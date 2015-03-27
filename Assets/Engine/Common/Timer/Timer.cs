using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Timer
{
	float duration;
	float endTime;

	public Timer()
	{
		duration = 0;
		endTime = 0;
	}

	public Timer(float duration)
	{
		this.duration = duration;
		this.endTime = Time.time + duration;
	}

	public Timer(float duration, float initialPercent)
	{
		this.duration = duration;
		this.endTime = Time.time + duration * (1 - initialPercent);
	}

	public bool IsEnded()
	{
		//return (duration == 0 && endTime==0) || (Time.time > endTime);
		return Time.time >= endTime;
	}

	public float GetCompletePercent()
	{
		if (duration > 0)
		{
			return Mathf.Clamp01(1 - (endTime - Time.time) / duration);
		}
		else
		{
			return 1;
		}
	}

	public float Left()
	{
		return Mathf.Max(0, endTime - Time.time);
	}

	public string LeftString()
	{
		int leftSeconds = (int)Left();
		return string.Format("{0}:{1}", leftSeconds / 60, Utils.Format00(leftSeconds%60));
	}

	public void Speedup(float times)
	{
		float oldEnd = endTime;
		endTime = Time.time + Left() / times;
		duration -= oldEnd - endTime;
	}
}
