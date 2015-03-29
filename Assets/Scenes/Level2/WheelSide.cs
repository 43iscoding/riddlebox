// 1, 2, 4, 6

using System.Collections;
using UnityEngine;

public class WheelSide : Side
{
	public WheelButton[] buttons;
	bool[] combination =
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
		while (true)
		{
			bool ok = true;
			for (int i = 0; i < 8; i++)
			{
				ok = buttons[i].close == combination[i];
				if (buttons[i].close && !combination[i])
				{
					Abort();
					ok = false;
					break;
				}
			}

			if (ok)
			{
				break;
			}
			yield return null;
		}

		Debug.Log("Wheel open");
		//OnUnlock();
	}

	void Abort()
	{
		Debug.Log("Abort");
		foreach (var button in buttons)
		{
			button.SetOpen();
		}
	}
}
