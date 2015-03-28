using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Handle : MonoBehaviour
{
	public List<float> needAngle;
	public float treshold;

	public bool IsOk()
	{
		foreach (var f1 in needAngle)
		{
			float f2 = transform.localEulerAngles.z;
			float f = Mathf.Abs(f2 - f1);
			if (f > 360)
			{
				f -= 360;
			}
			if (Mathf.Abs(f) < treshold)
			{
				return true;
			}
		}
		return false;
	}
}
