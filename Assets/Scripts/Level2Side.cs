using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Level2Side : Side
{
	public List<Handle> handles;
	public Handle bottom;
	public GameObject lockUp;
	public GameObject lockDown;

	void Start()
	{
		StartCoroutine(CheckBottom());


	}

	IEnumerator CheckBottom()
	{
		for (;;)
		{
			bool allOk = true;
			foreach (var h in handles)
			{
				bool isOk = h.IsOk();
				allOk = isOk && allOk;

				if (isOk)
				{
					h.LockRotation();
				}
			}

			if (allOk)
			{
				break;
			}
			yield return null;
		}

		foreach (var h in handles)
		{
			h.LockRotation();
		}
		Debug.Log("Unlocked");
		bottom.GetComponent<RotateWidget>().enabled = true;

		while (!bottom.IsOk())
		{
			yield return null;
		}
		Destroy(lockUp);
		Destroy(lockDown);
	}
}
