using UnityEngine;
using System.Collections;

public class MainMenuUI : MonoBehaviour {
	public PreloadScreen PreloadScreenPrefab;

	void Awake(){

		PreloadScreen.prefab = PreloadScreenPrefab;
	}
	public void Play()
	{
		PreloadScreen.Load("Level1", null);
	}


	public void Higscores()
	{
		PreloadScreen.Load("Level1", null);
	}
}
