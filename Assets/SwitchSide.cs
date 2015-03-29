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
		//StartCoroutine(CheckC());
	}

	IEnumerator CheckC()
	{
		yield return new WaitForSeconds(1);
		OnUnlock(); // TEMP:
	}

	public override void OnUnlock()
	{
		SoundUtils.PlaySound(SoundManager.instance.otkrivanie_dverki);
		door.SetTrigger("open");
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
