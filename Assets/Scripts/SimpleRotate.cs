using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
	const float rotationSpeed = 0.3f;

	Vector3 prevMousePosition;
	Vector3 currentMousePosition;

	void Start()
	{
		prevMousePosition = currentMousePosition = Input.mousePosition;
	}

	void OnMouseDown()
	{
		Debug.Log("OnMouseDown");
		prevMousePosition = currentMousePosition;
	}

	void OnMouseDrag()
	{
		prevMousePosition = currentMousePosition;
		currentMousePosition = Input.mousePosition;

		Vector3 delta = currentMousePosition - prevMousePosition;
		Vector3 rotation = new Vector3(delta.y, -delta.x);
		Debug.Log(rotation);
		transform.Rotate(rotation * rotationSpeed, Space.Self);
	}
}
