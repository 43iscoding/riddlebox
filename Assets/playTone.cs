using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playTone : MonoBehaviour 
{
	public string tone;
	public List<AudioClip> audioClips;
	
	public GameObject melodyButton;
	PlayMelodyScript melodyButtonScript;
	
	AudioSource audioPlayer;

	void Start () 
	{
		audioPlayer = transform.GetComponent<AudioSource>();
		melodyButtonScript = melodyButton.GetComponent<PlayMelodyScript>();
	}
	
	void OnMouseDown() 
	{
		if (tone == "a") { audioPlayer.clip = audioClips[0]; }
		if (tone == "s") { audioPlayer.clip = audioClips[1]; }
		if (tone == "d") { audioPlayer.clip = audioClips[2]; }
		if (tone == "f") { audioPlayer.clip = audioClips[3]; }
		if (tone == "g") { audioPlayer.clip = audioClips[4]; }
		
		audioPlayer.Play();
		melodyButtonScript.melodyPlayed += tone;
		
		if (melodyButtonScript.melodyPlayed.Length >= melodyButtonScript.melodyString.Length)
		{
			if (melodyButtonScript.melodyPlayed ==  melodyButtonScript.melodyString)
			{
				melodyButtonScript.complete = true;
				print ("complete");
			}
			else
			{
				melodyButtonScript.melodyPlayed = "";
			}
		}
	}
}
