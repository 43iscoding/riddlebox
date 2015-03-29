using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour {
	
		
	void Awake(){



		Music.Play(MusicTrackKind.InGame);

	}



	public void ToMenu()
	{
		PreloadScreen.Load("MainMenu", null);
	}


}
