using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour
{
	public List<Side> sides;
	HashSet<Side> unlocked = new HashSet<Side>(); 

	void Awake()
	{
		foreach (var side in sides)
		{
			side.SetBox(this);
		}
	}

	public void OnSideUnlocked(Side side)
	{
		unlocked.Add(side);
		bool all = true;
		foreach (var s in sides)
		{
			all = all && unlocked.Contains(s);
		}
		if (all)
		{
			UnlockBox();
		}
	}

	void UnlockBox()
	{
		The.gameLogic.OnBoxUnlocked();

		gameObject.SetActive(false);// TODO: fade animation
	}

	public void Show()
	{
		gameObject.SetActive(true); // TODO: show animation
		StartCoroutine(ShowCoroutine());
	}

	IEnumerator ShowCoroutine()
	{
		Timer t = new Timer(1);

		while (!t.IsEnded())
		{
			transform.localScale = Vector3.Lerp(Vector3.one * 0.75f, Vector3.one, t.GetCompletePercent());
			yield return null;
		}
	}
}
