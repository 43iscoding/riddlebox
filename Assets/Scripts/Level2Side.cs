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
	public Rigidbody topBox;

	public List<Handle> doors;
	public PlayMelodyScript melody;

	void Start()
	{
		StartCoroutine(CheckBottom());


	}

	IEnumerator CheckBottom()
	{
		// 4 HANDLES
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
		Debug.Log("Rotate bottom");

		// Rotate bottom
		bottom.GetComponent<RotateWidget>().enabled = true;

		while (!bottom.IsOk())
		{
			yield return null;
		}
		Destroy(lockUp);
		Destroy(lockDown);
		
		// MELODY
		Debug.Log("Play sound");



		while (!melody.complete)
		{
			yield return null;
		}


		// DOORS
		Debug.Log("Open doors");

		foreach (var door in doors)
		{
			door.gameObject.AddComponent<MeshCollider>().convex = true;
			door.gameObject.AddComponent<RotateWidget>();
		}

		while (!doors[0].IsOk() || !doors[1].IsOk())
		{
			yield return null;
		}
		Debug.Log("Level completed");
	}
}
