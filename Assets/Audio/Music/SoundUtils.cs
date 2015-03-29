using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

internal static class SoundUtils
{
	static Dictionary<AudioClip, int> lastPlayFrame = new Dictionary<AudioClip, int>();
	static Dictionary<AudioClip[], float> lastPlayFrameArray = new Dictionary<AudioClip[], float>();

	public static void InitSounds()
	{
		SetSoundsVolume(GetSoundsVolume());
		SetMusicVolume(GetMusicVolume());
	}

	public static void SetSoundsVolume(float soundVolume)
	{
		AudioListener.volume = soundVolume;
		PlayerPrefs.SetFloat("GenericVolume", soundVolume);
	}

	public static void MuteSounds()
	{
		//SetMusicVolume(0);
		SetSoundsVolume(0);
	}

	public static void MuteMusic()
	{
		SetMusicVolume(0);
	}

	public static void UnMuteMusic()
	{
		SetMusicVolume(0.5f);
	}

	public static void UnMuteSounds()
	{
		//SetMusicVolume(0.5f);
		SetSoundsVolume(1);
	}

	public static void ToggleSound()
	{
		if (!IsSoundMuted())
		{
			MuteSounds();
		}
		else
		{
			UnMuteSounds();
		}
	}

	public static void ToggleMusic()
	{
		if (!IsMusicMuted())
		{
			MuteMusic();
		}
		else
		{
			UnMuteMusic();
		}
	}

	public static void PlaySound(AudioClip sound, float volume = 1.0f)
	{
		if (IsAllowedToPlayNowAndUpdate(sound))
		{
			NGUITools.PlaySound(sound, GetSoundsVolume() * volume);
		}
	}

	public static void PlaySound(AudioClip[] sound, float volume = 1.0f)
	{
		if (IsAllowedToPlayNowAndUpdate(sound))
		{
			NGUITools.PlaySound(sound[Random.Range(0, sound.Length)], GetSoundsVolume() * volume);
		}
	}

	static bool IsAllowedToPlayNowAndUpdate(AudioClip sound)
	{
		if (sound == null)
		{
			return false;
		}
		int last;
		if (lastPlayFrame.TryGetValue(sound, out last))
		{
			lastPlayFrame[sound] = Time.frameCount;
			return last < Time.frameCount;
		}
		lastPlayFrame.Add(sound, Time.frameCount);
		return true;
	}

	static bool IsAllowedToPlayNowAndUpdate(AudioClip[] sound)
	{
		if (sound == null)
		{
			return false;
		}
		float last;
		if (lastPlayFrameArray.TryGetValue(sound, out last))
		{
			lastPlayFrameArray[sound] = Time.realtimeSinceStartup;
			return last + 0.02f < Time.realtimeSinceStartup;
		}
		lastPlayFrameArray.Add(sound, Time.realtimeSinceStartup);
		return true;
	}

	public static void PlaySound(AudioSource audioSource, List<AudioClip> soundToPlay)
	{
		if (soundToPlay == null || soundToPlay.Count == 0)
		{
			return;
		}

		PlaySound(audioSource, soundToPlay[Random.Range(0, soundToPlay.Count)]);
	}

	public static void PlaySound(AudioSource audioSource, AudioClip soundToPlay)
	{
		if (audioSource != null)
		{
			audioSource.clip = soundToPlay;
			audioSource.Play();
		}
		else
		{
			PlaySound(soundToPlay);
		}
	}

	public static float GetSoundsVolume()
	{
		return PlayerPrefs.GetFloat("GenericVolume", 1);
	}

	public static float GetMusicVolume()
	{
		return PlayerPrefs.GetFloat("MusicVolume", 0.5f);
	}

	public static bool IsSoundMuted()
	{
		var f = GetSoundsVolume();
		return f <= 0;
	}

	public static bool IsMusicMuted()
	{
		var f = GetMusicVolume();
		return f <= 0;
	}

	public static void SetMusicVolume(float value)
	{
		Music.SetVolume(value);
		PlayerPrefs.SetFloat("MusicVolume", value);
	}

	public static void PlaySoundDontDestroy(AudioClip clip, GameObject go)
	{
		GameObject.DontDestroyOnLoad(go);
		AudioSource source = go.GetComponent<AudioSource>();
		if (source == null) source = go.AddComponent<AudioSource>();
		source.pitch = 1f;
		source.PlayOneShot(clip, 1f);
	}
}
