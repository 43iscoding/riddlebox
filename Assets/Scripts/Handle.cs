using UnityEngine;
using System.Collections;

public class Handle : MonoBehaviour
{
	public float needAngle;
	public float treshold;

	public bool IsOk()
	{
		return Mathf.Abs(transform.localRotation.z - needAngle) < treshold;
	}
}
