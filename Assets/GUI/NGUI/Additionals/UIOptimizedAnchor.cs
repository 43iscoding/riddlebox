using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class UIOptimizedAnchor : UIAnchor
{
	//kazhisj eto depricated klass, skoro mozhno budet chistij UIAnchor ispoljzovatj

	// Use this for initialization
	new void Start()
	{
		runOnlyOnce = true;
		//START sdelali internal, 4tobi mozhno bilo spokojno zaekstendicca :)
		base.Start();
	}

	public void UpdateAnchor()
	{
		//Debug.Log("Updating anchor for:"+name);
		ScreenSizeChanged();
	}

	public static void OnResize()
	{
		//Debug.Log("Updating all anchors!");
		if (null != UICamera.onScreenResize)
		{
			UICamera.onScreenResize();
		}
	}
}
