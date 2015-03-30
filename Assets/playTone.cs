using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playTone : MonoBehaviour 
{
	public string tone;
	public PlayMelodyScript melodyButtonScript;
	AudioSource audioPlayer;

	void Start () 
	{
		audioPlayer = transform.GetComponent<AudioSource>();
	}
	
	void OnMouseDown() 
	{
		audioPlayer.Play();
		melodyButtonScript.OnPlay(tone);
	}
}
