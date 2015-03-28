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
			float f = (transform.localRotation.z * Mathf.Rad2Deg - f1);
			Debug.Log(f);
			if (Mathf.Abs(f) < treshold)
			{
				return true;
			}
		}
		return false;
	}
}
