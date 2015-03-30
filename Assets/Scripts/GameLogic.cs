using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{
	public GameObject GameUI;
	public string nextLevel;
	int currentBoxIndex;
	bool ended;

	void Awake()
	{
		if (The.gameLogic != null)
		{
			Destroy(gameObject);
			return;
		}

		Music.Play(MusicTrackKind.InGame);
		The.gameLogic = this;
		Instantiate(GameUI);
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.M) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
		{
			OnBoxUnlocked();
		}
	}

	public void OnBoxUnlocked()
	{
		if (ended)
		{
			return;
		}
		ended = true;
		//if (The.box != null)
		//{
		//    TweenScale tweenScale = TweenScale.Begin(The.box.gameObject, 0.5f, Vector3.one * 0.001f);
		//    EventDelegate.Set(tweenScale.onFinished, () => UITweener.current.gameObject.SetActive(false));
		//    The.box = null;
		//}

		if (nextLevel != "")
		{
			PreloadScreen.Load(nextLevel);
		}
		else
		{
			PreloadScreen.Load("FinalScreen");
		}
	}
}
