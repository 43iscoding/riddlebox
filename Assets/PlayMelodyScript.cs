using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayMelodyScript : MonoBehaviour
{
	public string melodyString = "";
	string melodyPlayed = "";
	internal bool complete;
	public List<AudioSource> audioClips;
	
	void Start()
	{
		complete = false;
	}
	
	void OnMouseDown()
	{
		StartCoroutine(PlayCouroutine());	
	}
	
	IEnumerator PlayCouroutine()
	{
		if (melodyString != string.Empty)
		{
			foreach (var clip in audioClips)
			{
				clip.Play();

				while (clip.isPlaying)
				{
					yield return null;
				}
			}
		}
	}

	public void OnPlay(string tone)
	{
		melodyPlayed += tone;

		if (melodyPlayed.Length >= melodyString.Length)
		{
			string substring = melodyPlayed.Substring(melodyPlayed.Length - melodyString.Length);
			Debug.Log(substring);
			if (substring == melodyString)
			{
				complete = true;
				print("complete");
			}
		}
	}
}
