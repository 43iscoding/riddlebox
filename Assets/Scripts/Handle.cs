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
	public GameObject AllOkGameObject;
	public GameObject ErrorGameObject;
	internal bool unlocked = true;

	public void TurnRed()
	{
		if (ErrorGameObject != null)
			ErrorGameObject.SetActive(true);
		if (AllOkGameObject != null)
			AllOkGameObject.SetActive(false);
	}
	public void TurnGreen()
	{
		if (ErrorGameObject != null)
			ErrorGameObject.SetActive(false);
		if (AllOkGameObject != null)
			AllOkGameObject.SetActive(true);
	}

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
//			Debug.Log(name + " " + localAngle);
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
		if (unlocked)
		{
			unlocked = false;
			GetComponent<RotateWidget>().enabled = false;
			GetComponent<Rigidbody>().isKinematic = true;
		}
	}
}
