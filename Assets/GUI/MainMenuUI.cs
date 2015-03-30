using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class MainMenuUI : MonoBehaviour {
	public PreloadScreen PreloadScreenPrefab;
	internal static Dictionary<MusicTrackKind, MusicConfig> musics = new Dictionary<MusicTrackKind, MusicConfig>();

	public List<MusicConfig> MusicTracks;

		
	void Awake(){

		PreloadScreen.prefab = PreloadScreenPrefab;


		foreach (MusicConfig x in MusicTracks)
		{
			AddMusic(x);
		}

		Music.musics = musics;

		Music.Play(MusicTrackKind.MainMenu);
	}


	public static void AddMusic(MusicConfig musicConfig)
	{
		if (musicConfig != null)
		{
			musics[musicConfig.TrackKind] = musicConfig;
		}
		else
		{
			Debug.LogError("Adding null music!");
		}
	}

	public void Play()
	{
		PreloadScreen.Load("Level1", null);
	}


	public void Higscores()
	{
		PreloadScreen.Load("Level1", null);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
