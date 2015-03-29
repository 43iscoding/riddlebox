// 1, 2, 4, 6

using System.Collections;
using UnityEngine;

public class WheelSide : Side
{
	public MeshRenderer[] lights;
	public WheelButton[] buttons;
	public Texture2D green;

	bool[] combination = // must be closed
		{
			false, // 0
			true, // 1
			false, // 2
			true, // 3
			false, // 4
			false,// 5
			false, //6
			false, //7
			};


	void Start()
	{
		StartCoroutine(Check());
	}

	IEnumerator Check()
	{
		//yield return new WaitForSeconds(1);
		//OnUnlock(); // TEMP:

		while (true)
		{
		next:
			yield return null;
			bool ok = true;

			for (int i = 0; i < 8; i++)
			{
				if (!buttons[i].ready)
				{
					goto next;
				}
			}
			for (int i = 0; i < 8; i++)
			{
				ok = ok && buttons[i].close == combination[i];
				if (buttons[i].close && !combination[i])
				{
					Abort();
					goto next;
				}
			}

			if (ok)
			{
				break;
			}
		}

		Debug.Log("Wheel open");
		OnUnlock();
	}

	void Abort()
	{
		Debug.Log("Abort");
		foreach (var button in buttons)
		{
			button.SetOpen();
		}
	}

	public override void OnUnlock()
	{
		base.OnUnlock();
		foreach (var light in lights)
		{
			light.material.mainTexture = green;
		}
	}
}
