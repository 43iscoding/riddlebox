using UnityEngine;
using System.Collections;

public class Slider : Side
{
	int value = 4;
	const int max = 50;

	public Renderer lamp;
	public Material green;

	public Renderer clock;
	public Material clockMaterial;

	internal static bool passed;


	void Start()
	{
		SetPosition();
	}

	void SetPosition()
	{
		transform.localPosition = new Vector3((float)value/max, 0, 0);
	}

	public void SetNewValue(int delta)
	{
		int newValue = value + delta;
		if (newValue >= -max && newValue <= max)
		{
			value = newValue;
			SetPosition();
		}
		if (value == 0)
		{
			OnUnlock();
		}
	}

	public override void OnUnlock()
	{
		base.OnUnlock();
		{
			var mats = lamp.materials;
			mats[1] = green;
			lamp.materials = mats;
		}

		{
			var mats = clock.materials;
			mats[0] = clockMaterial;
			clock.materials = mats;
		}

		passed = true;
	}
}
