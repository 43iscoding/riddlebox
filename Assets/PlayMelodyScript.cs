using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayMelodyScript : MonoBehaviour {

	public string melodyString = "";
	public string melodyPlayed = "";
	public bool complete;
	public List<AudioClip> audioClips;
	
	AudioSource audioPlayer;

	// Use this for initialization
	void Start () {
		audioPlayer = transform.GetComponent<AudioSource>();
		complete = false;

	}
	
	public void Play()
	{
		StartCoroutine(PlayCouroutine ());	
	}

	
	IEnumerator PlayCouroutine ()
	{
		if (melodyString != string.Empty)
		{
			for (int i = 0; i < melodyString.Length; i++)
			{
				string sound = melodyString.Substring(i, 1);
				
				if (sound == "a") { audioPlayer.clip = audioClips[0]; }
				if (sound == "s") { audioPlayer.clip = audioClips[1]; }
				if (sound == "d") { audioPlayer.clip = audioClips[2]; }
				if (sound == "f") { audioPlayer.clip = audioClips[3]; }
				if (sound == "g") { audioPlayer.clip = audioClips[4]; }
	
				audioPlayer.Play();
				
				while (audioPlayer.isPlaying)
				{
					yield return null;
				}
			}
		}
	}
	
}
