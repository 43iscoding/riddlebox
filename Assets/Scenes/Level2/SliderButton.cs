using UnityEngine;
using System.Collections;

public class SliderButton : MonoBehaviour
{
	public int scale;
	public Slider slider;

	public void OnMouseDown()
	{
		if (!Slider.passed)
		{
			SoundUtils.PlaySound(SoundManager.instance.lampochka_var2);
			slider.SetNewValue(scale * SliderValueSet.currentDelta);
		}
	}
}
