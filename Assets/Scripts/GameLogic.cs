using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{
	public List<Box> boxes;
	int currentBoxIndex;
	Box currentBox;

	void Start()
	{
		The.gameLogic = this;

		foreach (var box in boxes)
		{
			box.gameObject.SetActive(false);
		}
		
		ShowNextBox();
	}

	public void OnBoxUnlocked()
	{
		currentBoxIndex++;
		if (currentBoxIndex >= boxes.Count)
		{
			Win();
		}
		else
		{
			ShowNextBox();
		}
	}

	void ShowNextBox()
	{
		currentBox = boxes[currentBoxIndex];
		currentBox.transform.localPosition = Vector3.zero;
		currentBox.Show();
	}

	void Win()
	{
		Debug.Log("Win");
	}
}
