using UnityEngine;
using System.Collections;

public class Rotatable : MonoBehaviour
{
	const float rotationSpeed = 0.3f;

	Vector3 prevMousePosition;
	Vector3 currentMousePosition;

	bool dragging;

	void Start() 
	{
		prevMousePosition = currentMousePosition = Input.mousePosition;
	}
	
	void Update() 
	{
		prevMousePosition = currentMousePosition;
		currentMousePosition = Input.mousePosition;

		if (Input.GetMouseButtonDown(0))
		{
			prevMousePosition = currentMousePosition;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			Physics.Raycast (ray, out hitInfo);
			if (hitInfo.collider == null || hitInfo.collider.tag != "Widget")
			{
				dragging = true;
			} 
			else 
			{
				dragging = false;
			}
			Debug.Log("GetMouseButtonDown");
		}

		if (!Input.GetMouseButton(0)) 
		{
			dragging = false;
		}
		else
		{
			Debug.Log("GetMouseButton");
		}

		if (dragging) 
		{
			Vector3 delta = currentMousePosition - prevMousePosition;
			Vector3 rotation = new Vector3(delta.y, -delta.x);
			transform.Rotate(rotation * rotationSpeed, Space.World);

		//	transform.Rotate(Input.GetAxis("Mouse Y")  *rotationSpeed, -Input.GetAxis("Mouse X")*rotationSpeed, Time.deltaTime, Space.World);
		}
	}
}
