using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{
	List<Box> boxes = new List<Box>();
	int currentBoxIndex;
	Box currentBox;

	void Start()
	{
		The.gameLogic = this;

		AddAllBoxes();
		
		ShowNextBox();
	}

	static void OnBoxLoaded(Box box)
	{
		box.gameObject.SetActive(false);
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

		PrepareNextBox();
	}

	void PrepareNextBox()
	{
		StartCoroutine(LoadNextScene());
	}

	IEnumerator LoadNextScene()
	{
		Camera main = Camera.main;
		int levelIndex = currentBoxIndex + 1;
		if (!Application.CanStreamedLevelBeLoaded(levelIndex))
		{
			Debug.Log("No more scenes");
			yield break;
		}

		var async = Application.LoadLevelAdditiveAsync(levelIndex);
		async.allowSceneActivation = false;

		Debug.Log("Loading " + async);

		while (async.progress < 0.9f)
		{
			yield return null;
		}
		async.allowSceneActivation = true;
		yield return async;

		foreach (Camera c in FindObjectsOfType<Camera>())
		{
			if (main != c)
			{
				Destroy(c.gameObject);
			}
		}
		AddAllBoxes();
		Debug.Log("Loaded scene " + currentBoxIndex + 1);
	}

	void AddAllBoxes()
	{
		foreach (Box box in FindObjectsOfType<Box>())
		{
			if (!boxes.Contains(box))
			{
				boxes.Add(box);
				OnBoxLoaded(box);
			}
		}
	}

	void Win()
	{
		Debug.Log("Win");
	}
}
