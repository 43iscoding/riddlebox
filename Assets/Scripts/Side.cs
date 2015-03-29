using UnityEngine;
using System.Collections;

public class Side : MonoBehaviour
{
	protected internal Box box;

	public void SetBox(Box box1)
	{
		box = box1;
	}

	public virtual void OnUnlock()
	{
		box.OnSideUnlocked(this);
		//gameObject.SetActive(false);
	}
}
