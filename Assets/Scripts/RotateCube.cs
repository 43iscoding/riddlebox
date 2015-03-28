using UnityEngine;
using System.Collections;

public class RotateCube : MonoBehaviour 
{
	public GameObject cube;

	const float rotationSpeed = 0.3f;

	Vector3 prevMousePosition;
	Vector3 currentMousePosition;

	void OnMouseDown()
	{
		prevMousePosition = currentMousePosition = Input.mousePosition;
	}

	void OnMouseDrag()
	{
		prevMousePosition = currentMousePosition;
		currentMousePosition = Input.mousePosition;
		Vector3 delta = currentMousePosition - prevMousePosition;
		Vector3 rotation = new Vector3(delta.y, -delta.x);
		cube.transform.Rotate(rotation * rotationSpeed, Space.World);
	}
}
