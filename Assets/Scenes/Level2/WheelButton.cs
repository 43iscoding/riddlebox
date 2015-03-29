using UnityEngine;
using System.Collections;

public class WheelButton : MonoBehaviour
{
	public int index;
	internal bool open;
	internal bool ready;

	internal bool close {
		get { return !open; }
	}

	void Start()
	{
		gameObject.AddComponent<MeshCollider>();
		SetOpen();
	}

	void OnMouseDown()
	{
		SetClose();
	}

	void SetClose()
	{
		open = false;
		UpdateAnimator();
		SetReady();
	}

	void SetReady()
	{
		StartCoroutine(SetReaadyC());
	}

	IEnumerator SetReaadyC()
	{
		ready = false;
		yield return new WaitForSeconds(0.5f);
		ready = true;
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
		SetReady();
	}
}
