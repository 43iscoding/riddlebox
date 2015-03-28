using UnityEngine;

public class RotateProxy : MonoBehaviour
{
	public RotateWidget rotateWidget;

	public void OnMouseDown()
	{
		if (rotateWidget.enabled)
		{
			rotateWidget.OnMouseDown();
		}
	}
	public void OnMouseUp()
	{
		if (rotateWidget.enabled)
			rotateWidget.OnMouseUp();
	}

	public void OnMouseDrag()
	{
		if (rotateWidget.enabled)
			rotateWidget.OnMouseDrag();
	}
}
