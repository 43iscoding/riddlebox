using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {
	public bool ccw = false;
	public int duration = 2;

	void Awake()
	{
		
		if (!ccw)
		{
			TweenRotation tweenPosition = TweenRotation.Begin(gameObject, duration, Quaternion.Euler(0, 0, 180));
			tweenPosition.from = new Vector3(0, 0, 180);
			tweenPosition.to = new Vector3(0, 0, -180);
			tweenPosition.style = UITweener.Style.Loop;
		}
		else
		{
			TweenRotation tweenPosition = TweenRotation.Begin(gameObject, duration, Quaternion.Euler(0, 0, 180));
			tweenPosition.from = new Vector3(0, 0, -180);
			tweenPosition.to = new Vector3(0, 0, 180);
			tweenPosition.style = UITweener.Style.Loop;
		}
	}
}
