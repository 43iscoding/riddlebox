using System.Collections.Generic;
using UnityEngine;

public class SwitchSide : Side
{
	public static SwitchSide instance;
	public List<rotateSwitch> allSwitches;
	public Animator door;

	void Awake()
	{
		instance = this;
	}

	public override void OnUnlock()
	{
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
