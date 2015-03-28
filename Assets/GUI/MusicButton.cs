using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MusicButton : MonoBehaviour {
	public Image icon;
	public Sprite on;
	public Sprite off;
	// Use this for initialization
	bool musicOn=true;

	public void Toggle () {
		if (musicOn) {
			icon.sprite = on;
			musicOn=false;
		} else {
			icon.sprite = off;
			musicOn=true;
		}
	}

}
