using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MusicButton : MonoBehaviour {
	public Image icon;
	public Sprite on;
	public Sprite off;


	void Awake()
	{

		SyncSound();
	}

	public void Toggle () {


		SoundUtils.ToggleMusic();
		SyncSound();

		
	}

	void SyncSound()
	{

		if (!SoundUtils.IsMusicMuted())
		{
			icon.sprite = on;
		}
		else
		{
			icon.sprite = off;
		}
	}

}
