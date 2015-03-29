using UnityEngine;
using System.Collections;

public class MouseDownProxy : MonoBehaviour
{
	GameObject go;

	void OnMouseDown()
	{
		go = null;
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
		{
			go = hit.collider.gameObject;
			if (go != gameObject)
			{
				go.SendMessage("OnMouseDown");
			}
			else
			{
				go = null;
			}
		}
	}
	void OnMouseDrag()
	{
		if (go != null)
		{
			go.SendMessage("OnMouseDrag");
		}
	}
}
