using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour {
	public float speed=5;

	void Update()
	{
		transform.RotateAround(transform.position, Vector3.up, speed * Time.deltaTime);
	}
}
