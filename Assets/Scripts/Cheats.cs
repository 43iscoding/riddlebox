using System;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour {


	void Awake()
	{
		DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {


		if (Utils.GetKeyDown(KeyCode.KeypadPlus))
		{
			float timescale = Mathf.Clamp(Time.timeScale * 1.5f, 0.011f, 99);
			Utils.SetTimeScale(timescale);
		}
		else if (Utils.GetKeyDown(KeyCode.KeypadMinus))
		{
			float timescale = Mathf.Clamp(Time.timeScale / 1.5f, 0.01f, 100);
			Utils.SetTimeScale(timescale);
		}
		
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			if (Math.Abs(Time.timeScale - 0) > Utils.Epsilon)
			{
				Utils.SetTimeScale(0.1f);
			}
		}
		if (Input.GetKeyDown(KeyCode.RightShift))
		{
			if (Math.Abs(Time.timeScale - 0) > Utils.Epsilon)
			{
				Utils.SetTimeScale(10);
			}
		}
		if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
		{
			if (Math.Abs(Time.timeScale - 0) > Utils.Epsilon)
			{
				Utils.SetTimeScale(1);
			}
		}
	}
}
