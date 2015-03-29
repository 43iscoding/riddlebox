using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Level1Side : Side
{
	public List<Handle> handles;
	public Handle bottom;
	public GameObject lockUp;
	public GameObject lockDown;

	public List<Handle> doors;
	public PlayMelodyScript melody;
	public List<Handle> topHandles;
	public Material green;

	public List<GameObject> ruchka;
	bool ruckaReady;
	bool topHandlesReady;

	void Start()
	{
		StartCoroutine(CheckBottom());
		StartCoroutine(CheckTopHandles());
		StartCoroutine(CheckOpenDoors());
	}

	IEnumerator CheckOpenDoors()
	{
		while (!ruckaReady || !topHandlesReady)
		{
			yield return null;
		}
		//// DOORS
		Debug.Log("Open doors");

		//{
		//	var joint = doors[0].gameObject.AddComponent<HingeJoint>();
		//	joint.connectedBody
		//}

		foreach (var door in doors)
		{
			door.gameObject.AddComponent<MeshCollider>().convex = true;
			door.gameObject.AddComponent<RotateWidget>();
			door.gameObject.GetComponent<Rigidbody>().isKinematic = false;
		}

		while (!doors[0].IsOk() || !doors[1].IsOk())
		{
			if (doors[0].IsOk())
			{
				doors[0].LockRotation();
			}
			if (doors[1].IsOk())
			{
				doors[1].LockRotation();
			}
			yield return null;
		}
		Debug.Log("Level completed");
		OnUnlock();
	}

	IEnumerator CheckTopHandles()
	{
		for (; ; )
		{
			bool allOk = true;
			foreach (var h in topHandles)
			{
				bool isOk = h.IsOk();
				allOk = isOk && allOk;

				//if (isOk)
				//{
				//	h.LockRotation();
				//}
			}

			if (allOk)
			{
				break;
			}
			yield return null;
		}

		foreach (var h in topHandles)
		{
			h.TurnGreen();
			h.GetComponent<MeshRenderer>().materials[1].SetTexture(0, green.mainTexture);
		}
		Debug.Log("Top Handles Unlocked");
		doors[0].GetComponent<MouseDownProxy>().enabled = false;
		doors[1].GetComponent<MouseDownProxy>().enabled = false;
		topHandlesReady = true;
	}

	IEnumerator CheckBottom()
	{
		//yield return new WaitForSeconds(1);
		//OnUnlock(); // temp:

		// 4 HANDLES
		for (; ; )
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
		Vector3 velocity = (Camera.main.transform.position - lockUp.transform.position).normalized*2;
		lockUp.AddComponent<Rigidbody>().velocity += velocity;
		lockDown.AddComponent<Rigidbody>().velocity += velocity;

		SoundUtils.PlaySound(SoundManager.instance.otletaetKrizhka);

		//Destroy(lockUp);
		//Destroy(lockDown);
		// TODO: drop down

		// MELODY
		Debug.Log("Play sound");

		while (!melody.complete)
		{
			yield return null;
		}

		Debug.Log("Viezhaet ruchka");

		{
			// Viezhaet ruchka
			Timer t = new Timer(0.5f);
			Vector3 from0 = ruchka[0].transform.localPosition;
			Vector3 from1 = ruchka[1].transform.localPosition;
			Vector3 to0 = ruchka[0].transform.localPosition + new Vector3(0, 0, -0.0001f);
			Vector3 to1 = ruchka[1].transform.localPosition + new Vector3(0, 0, -0.0001f);

			while (!t.IsEnded())
			{
				ruchka[0].transform.localPosition = Vector3.Lerp(from0, to0, t.GetCompletePercent());
				ruchka[1].transform.localPosition = Vector3.Lerp(from1, to1, t.GetCompletePercent());
				yield return null;
			}
			ruckaReady = true;
		}
	}
}
