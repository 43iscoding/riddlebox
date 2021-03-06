﻿using System.Collections;
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
	public List<Animation> doorsAnimation;
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
		SoundUtils.PlaySound(SoundManager.instance.done);
		Debug.Log("Open doors");

		doorsAnimation[0].Play();
		doorsAnimation[1].Play();

		yield return new WaitForSeconds(2);

		//{
		//	var joint = doors[0].gameObject.AddComponent<HingeJoint>();
		//	joint.connectedBody
		//}

		//foreach (var door in doors)
		//{
		//    door.gameObject.AddComponent<MeshCollider>().convex = true;
		//    door.gameObject.AddComponent<RotateWidget>().onStart = SoundManager.instance.krutilki;
		//    door.gameObject.GetComponent<Rigidbody>().isKinematic = false;
		//}

		//while (!doors[0].IsOk() || !doors[1].IsOk())
		//{
		//    if (doors[0].IsOk())
		//    {
		//        doors[0].LockRotation();
		//    }
		//    if (doors[1].IsOk())
		//    {
		//        doors[1].LockRotation();
		//    }
		//    yield return null;
		//}
		Debug.Log("Level completed");
		OnUnlock();
	}

	IEnumerator CheckTopHandles()
	{
		foreach (var h in topHandles)
		{
			h.GetComponent<SimpleRotate>().onRotate = SoundManager.instance.krutilki;
		}
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

		SoundUtils.PlaySound(SoundManager.instance.lampochka_var2);
		foreach (var h in topHandles)
		{
			h.TurnGreen();
			h.GetComponent<MeshRenderer>().materials[1].SetTexture(0, green.mainTexture);
		}
		Debug.Log("Top Handles Unlocked");
		//doors[0].GetComponent<MouseDownProxy>().enabled = false;
		//doors[1].GetComponent<MouseDownProxy>().enabled = false;
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

				if (isOk && h.unlocked)
				{
					h.LockRotation();
					SoundUtils.PlaySound(SoundManager.instance.unlocking);
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
		SoundUtils.PlaySound(SoundManager.instance.done);

		// Rotate bottom
		bottom.GetComponent<RotateWidget>().enabled = true;
		bottom.GetComponent<RotateWidget>().onStart = SoundManager.instance.cube_base_rotation;

		while (!bottom.IsOk())
		{
			yield return null;
		}
		Vector3 velocity = (Camera.main.transform.position - lockUp.transform.position).normalized*2;
		lockUp.AddComponent<Rigidbody>().velocity += velocity;
		lockDown.AddComponent<Rigidbody>().velocity += velocity;

		SoundUtils.PlaySound(SoundManager.instance.otletaetKrizhka);

		// MELODY
		Debug.Log("Play sound");

		while (!PlayMelodyScript.complete)
		{
			yield return null;
		}

		Debug.Log("Viezhaet ruchka");
		SoundUtils.PlaySound(SoundManager.instance.done);
		SoundUtils.PlaySound(SoundManager.instance.unlocking_2);

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
