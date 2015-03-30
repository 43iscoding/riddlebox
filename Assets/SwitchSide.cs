using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSide : Side
{
	public static SwitchSide instance;
	public List<rotateSwitch> allSwitches;
	public Animator door;

	void Start()
	{
		instance = this;
		door.SetTrigger("open");
		//StartCoroutine(CheckC());
	}

	IEnumerator CheckC()
	{
		yield return new WaitForSeconds(1);
		OnUnlock(); // TEMP:
	}

	public override void OnUnlock()
	{
		StartCoroutine(WaitForWheel());

	}

	IEnumerator WaitForWheel()
	{
		Debug.Log("Wait for wheel");
		while (!box.IsUnlocked(box.sides[0]))
		{
			yield return null;
		}
		Debug.Log("Door opens");
		SoundUtils.PlaySound(SoundManager.instance.otkrivanie_dverki);
		door.SetTrigger("open");
		yield return new WaitForSeconds(2);
		base.OnUnlock();
	}

	public void Check()
	{
		bool ok = true;
		foreach (var s in allSwitches)
		{
			ok = ok && s.IsOk();
		}
		if (ok)
		{
			Debug.Log("Switch ok");
			OnUnlock();
		}
	}
}
