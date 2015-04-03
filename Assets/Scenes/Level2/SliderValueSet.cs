using UnityEngine;
using System.Collections;

public class SliderValueSet : MonoBehaviour
{
	internal static int currentDelta = 3;
	public int setDelta;

	public Renderer selected;
	public Material selectedMaterial;
	public Renderer old;
	public Material oldMaterial;

	void OnMouseDown()
	{
		if (!Slider.passed)
		{
			selected.material = selectedMaterial;
			old.material = oldMaterial;

			SoundUtils.PlaySound(SoundManager.instance.lampochka_var2);
			currentDelta = setDelta;
		}
	}
}
 