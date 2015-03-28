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
			if (Physics.Raycast(ray, out hitInfo))
			{
				if (hitInfo.collider == null || hitInfo.collider.tag != "Widget")
				{
					dragging = true;
				}
				else
				{
					dragging = false;
					//selected = hitInfo.collider.gameObject;
					//selected.SendMessage("OnMouseDown");
				}
			}
			else
			{
				dragging = true;
			}
		}

		if (!Input.GetMouseButton(0)) 
		{
			//if (selected != null)
			//{
			//	//selected.SendMessage("OnMouseUp");
			//	selected = null;
			//}
			dragging = false;
		}
		else
		{
			//if (selected != null)
			//{
			//	selected.SendMessage("OnMouseDrag");
			//}
		}

		if (dragging) 
		{
			Vector3 delta = currentMousePosition - prevMousePosition;
			Vector3 rotation = new Vector3(delta.y, -delta.x);
			transform.Rotate(rotation * rotationSpeed, Space.World);
		}
	}
}
