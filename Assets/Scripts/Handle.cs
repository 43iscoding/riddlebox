using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Collections;

public enum Axis
{
	X,Y,Z
}

public class Handle : MonoBehaviour
{
	public List<float> needAngle;
	public float treshold;
	public Axis axis = Axis.Z;

	public bool IsOk()
	{
		foreach (var needAngl in needAngle)
		{
			float localAngle = 0;
			switch (axis)
			{
				case Axis.X:
					localAngle = transform.localEulerAngles.x;
					break;
				case Axis.Y:
					localAngle = transform.localEulerAngles.y;
					break;
				case Axis.Z:
					localAngle = transform.localEulerAngles.z;
					break;
			}
			
			float f = Mathf.Abs(localAngle - needAngl);
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

	public void LockRotation()
	{
		GetComponent<RotateWidget>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
	}
}
