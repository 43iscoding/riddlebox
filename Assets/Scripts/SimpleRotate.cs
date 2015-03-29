using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
	public Axis axis;

	Vector3 prevMousePosition;
	Vector3 currentMousePosition;

	void Start()
	{
		prevMousePosition = currentMousePosition = Input.mousePosition;
	}

	void OnMouseDown()
	{
		prevMousePosition = currentMousePosition = Input.mousePosition;
	}

	void OnMouseDrag()
	{
		prevMousePosition = currentMousePosition;
		currentMousePosition = Input.mousePosition;

		Vector3 delta = currentMousePosition - prevMousePosition;
		float v = delta.x;
		Vector3 rotation = new Vector3(delta.y, -delta.x);
		//transform.Rotate(rotation * rotationSpeed, Space.Self);
		transform.localRotation *= Quaternion.Euler(axis == Axis.X ? v : 0, axis == Axis.Y ? v : 0, axis == Axis.Z ? v : 0);
	}
}
