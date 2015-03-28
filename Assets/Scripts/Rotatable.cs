using UnityEngine;
using System.Collections;

public class Rotatable : MonoBehaviour {
	
	public float rotationSpeed;

	private Vector3 prevMousePosition;
	private Vector3 currentMousePosition;

	private bool dragging;

	void Start() {
		prevMousePosition = currentMousePosition = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update () {
		prevMousePosition = currentMousePosition;
		currentMousePosition = Input.mousePosition;

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitInfo;
			Physics.Raycast (ray, out hitInfo);
			if (hitInfo.collider == null || hitInfo.collider.tag != "Widget") {
				dragging = true;
			} else {
				dragging = false;
			}
		}

		if (!Input.GetMouseButton (0)) {
			dragging = false;
		}

		if (dragging) {
			Vector3 delta = currentMousePosition - prevMousePosition;
			Vector3 rotation = new Vector3(delta.y, -delta.x);
			transform.Rotate(rotation * rotationSpeed, Space.World);
		}



	}
}
