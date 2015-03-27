using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class DateTimeTimer
{
	internal float duration;
	internal DateTime endTime;

	public DateTimeTimer()
	{
		duration = 0;
		endTime = DateTime.Now;
	}

	public DateTimeTimer(float duration)
	{
		this.duration = duration;
		this.endTime = DateTime.UtcNow + TimeSpan.FromSeconds(duration);
	}

	public DateTimeTimer(float duration, float initialPercent)
	{
		this.duration = duration;
		this.endTime = DateTime.UtcNow + TimeSpan.FromSeconds(duration * (1 - initialPercent));
	}

	public bool IsEnded()
	{
		//return (duration == 0 && endTime==0) || (Time.time > endTime);
		return DateTime.UtcNow > endTime;
	}

	public float GetCompletePercent()
	{
		if (duration > 0)
		{
			return Mathf.Clamp01((float) (1 - ((endTime - DateTime.UtcNow).TotalSeconds) / duration));
		}
		else
		{
			return 1;
		}
	}

	public float Left()
	{
		return Mathf.Max(0, (float)(endTime - DateTime.UtcNow).TotalSeconds);
	}

	public string LeftString()
	{
		int leftSeconds = (int)Left();
		return string.Format("{0}:{1}", leftSeconds / 60, Utils.Format00(leftSeconds%60));
	}

	public void Shift(float timeAdd)
	{
		endTime = endTime.AddSeconds(timeAdd);
	}
}
