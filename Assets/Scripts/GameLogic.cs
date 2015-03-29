using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{
	public GameObject GameUI;
	//List<Box> boxes = new List<Box>();
	int currentBoxIndex;
	Box currentBox;

	void Awake()
	{
		if (The.gameLogic != null)
		{
			Destroy(gameObject);
			return;
		}

		//DontDestroyOnLoad(this);
		Debug.Log("Starting gamelogic!");
		Music.Play(MusicTrackKind.InGame);
		The.gameLogic = this;
		Instantiate(GameUI);
		AddAllBoxes();
		
		//ShowNextBox();
	}

	static void OnBoxLoaded(Box box)
	{
		//box.gameObject.SetActive(false);
	}

	public void OnBoxUnlocked()
	{
		currentBoxIndex++;
		Debug.Log("Current index = " + currentBoxIndex);

		if (The.box != null)
		{
			//The.box.gameObject.SetActive(false); // TODO: fade animation
			TweenScale.Begin(The.box.gameObject, 0.5f, Vector3.zero);
			The.box = null;
		}
		if (currentBoxIndex >= 2)
		{
			currentBoxIndex = 0;
			LoadNextScene();
			//Win();
		}
		else
		{
			LoadNextScene();
		}
	}

	void LoadNextScene()
	{
		int levelIndex = currentBoxIndex + 1;
		PreloadScreen.Load(levelIndex,null,true);


		//StartCoroutine(LoadNextSceneC());
	}

	IEnumerator LoadNextSceneC()
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
				Debug.Log("Destry " + c);
				Destroy(c.gameObject);
			}
		}
		Destroy(currentBox);
		yield return null;
		AddAllBoxes();
		Debug.Log("Loaded scene " + levelIndex);
	}

	void AddAllBoxes()
	{
		currentBox = FindObjectOfType<Box>();
	}
}
