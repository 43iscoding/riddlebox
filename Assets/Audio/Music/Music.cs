using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum MusicTrackKind
{
	None = 0,

	MainMenu = 1,
	InGame = 2,
}

public class Music : MonoBehaviour
{
	internal static Dictionary<MusicTrackKind, MusicConfig> musics;

	MusicConfig CurrentTracks;
	MusicConfig BackupTracks;
	float BackupTime;

	/// <summary>
	/// Only one instance will be master.
	/// </summary>
	bool Master;

	static Music singleton;

	void Awake()
	{
		if (Master)
		{
			return;
		}
		if (singleton != null && singleton != this)
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);

		RegisterSigleton();
		SoundUtils.InitSounds();
	}

	void Update()
	{
		if (!GetComponent<AudioSource>().isPlaying)
		{
			if (BackupTracks != null)
			{
				RestorePreviousPrivate();
			}
		}

		if (!GetComponent<AudioSource>().isPlaying)
		{
			PlayNextTrack();
		}
	}

	void PlayNextTrack()
	{
		if (CurrentTracks != null)
		{
			CurrentTracks.SetNextTrack();
			Play();
		}
	}

	void Play(float time = 0)
	{
		GetComponent<AudioSource>().clip = CurrentTracks.CurrentTrack;
		GetComponent<AudioSource>().time = time;
		GetComponent<AudioSource>().Play();
	}

	public static void SetVolume(float volume)
	{
		if (singleton != null)
		{
			singleton.GetComponent<AudioSource>().volume = volume;
		}
	}

	public static void RestorePrevious()
	{
//		Debug.Log("RestorePrevious");
		if (singleton != null)
		{
			singleton.RestorePreviousPrivate();
		}
	}

	void RestorePreviousPrivate()
	{
		if (BackupTracks != null)
		{
			CurrentTracks = BackupTracks;
			BackupTracks = null;
			GetComponent<AudioSource>().Stop();
			Play(BackupTime);
		}
	}

	public static void Play(MusicTrackKind musicKind, bool keepPrevious = false)
	{
		MusicConfig music;
		if (musics == null)
		{
			Debug.LogWarning("Cannot play " + musicKind);
			return;
		}
		if (musics.TryGetValue(musicKind, out music))
		{
			Play(music, keepPrevious);
		}
	}

	public static void Play(MusicConfig music, bool keepPrevious = false)
	{
		if (singleton == null)
		{
			singleton = (Instantiate(Resources.Load("Music", typeof(Transform)) as Transform) as Transform).GetComponent<Music>();
			singleton.RegisterSigleton();
		}

		singleton.PlayPrivate(music, keepPrevious);
	}

	void PlayPrivate(MusicConfig music, bool keepPrevious)
	{
		if (CurrentTracks == null || !music.Tracks.Contains(CurrentTracks.CurrentTrack))
		{
			if (keepPrevious)
			{
				BackupMusic();
			}

			CurrentTracks = music;
			music.Reset();
			Play();
		}
	}
	/*
	 * nado li voobshe etot funkcional?
	public static void Stop()
	{
		if (singleton == null)
		{
			singleton = (Instantiate(Resources.Load("Music", typeof(Transform)) as Transform) as Transform).GetComponent<Music>();
			singleton.RegisterSigleton();
		}

		singleton.StopPrivate();
	}

	void StopPrivate()
	{
		BackupTracks = null;
		CurrentTracks = null;
		BackupTime = 0;
		audio.Pause();
		Debug.Log("Stop");
	}
	*/
	void RegisterSigleton()
	{
		singleton = this;
		DontDestroyOnLoad(singleton);
		singleton.Master = true;
	}

	void BackupMusic()
	{
		BackupTracks = CurrentTracks;
		BackupTime = GetComponent<AudioSource>().time;
	}
}
