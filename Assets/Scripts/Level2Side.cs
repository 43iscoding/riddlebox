using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Side : Side
{
	public List<Handle> handles;
	public GameObject bottom;

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
					h.GetComponent<RotateWidget>().enabled = false;
					h.GetComponent<Rigidbody>().isKinematic = true;
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
			h.GetComponent<RotateWidget>().enabled = false;
			h.GetComponent<Rigidbody>().isKinematic = true;
		}
		Debug.Log("Unlocked");
		bottom.GetComponent<RotateWidget>().enabled = true;
	}
}
