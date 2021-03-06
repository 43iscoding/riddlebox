﻿using System.Security.Cryptography;
using UnityEngine;

public class RotateWidget : MonoBehaviour
{
	public AudioClip onStart;
	GameObject handle;
	Plane plane;

	void Start()
	{
		foreach (var componentsInChild in GetComponentsInChildren<Collider>())
		{
			componentsInChild.gameObject.tag = "Widget";
		}
		GetComponent<Rigidbody>().isKinematic = false;
	}

	public void OnMouseDown()
	{
		if (!enabled)
		{
			return;
		}
		Destroy(handle);

		if (onStart != null)
		{
			SoundUtils.PlaySound(onStart);
		}

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			handle = new GameObject("handle");
			handle.transform.parent = transform;
			handle.transform.position = hit.point;
			plane = new Plane(ray.direction, hit.point);
		}
	}

	public void OnMouseUp()
	{
		Destroy(handle);
	}

	public void OnMouseDrag()
	{
		if (!enabled)
		{
			return;
		}
		if (handle != null)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float enter;
			plane.Raycast(ray, out enter);
			Vector3 newMouse = ray.GetPoint(enter);

			Vector3 vector3 = newMouse - handle.transform.position;
			GetComponent<Rigidbody>().AddForceAtPosition(vector3 * Time.deltaTime * 0.5f * 100, handle.transform.position, ForceMode.VelocityChange);
		}
	}
}
