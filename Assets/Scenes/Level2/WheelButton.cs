using UnityEngine;
using System.Collections;

public class WheelButton : MonoBehaviour
{
	public int index;
	public bool open;

	internal bool close {
		get { return !open; }
	}

	void Start()
	{
		gameObject.AddComponent<MeshCollider>();
		open = true;
		UpdateAnimator();
	}

	void OnMouseDown()
	{
		SetClose();
	}

	void SetClose()
	{
		open = false;
		UpdateAnimator();
	}

	void UpdateAnimator()
	{
		var animator = GetComponent<Animator>();
		animator.SetBool("open", open);
	}

	public void SetOpen()
	{
		open = true;
		UpdateAnimator();
	}
}
