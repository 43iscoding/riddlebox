using System.Collections.Generic;
using UnityEngine;

public class MusicConfig : MonoBehaviour
{
	public MusicTrackKind TrackKind;
#if SOUNDS_MONO || UNITY_EDITOR
	public List<AudioClip> Mono;
	public int MonoLoopFrom = 0;
#endif
#if !SOUNDS_MONO || UNITY_EDITOR
	public List<AudioClip> Stereo;
	public int StereoLoopFrom = 0;
#endif

	internal int CurrentTrackNumber;

	public List<AudioClip> Tracks
	{
		get
		{
#if SOUNDS_MONO
			return Mono;
#else
			return Stereo;
#endif
		}
	}
	public int LoopFrom
	{
		get
		{
#if SOUNDS_MONO
			return MonoLoopFrom;
#else
			return StereoLoopFrom;
#endif
		}
	}

	public void SetNextTrack()
	{
		CurrentTrackNumber++;
		if (CurrentTrackNumber >= Tracks.Count)
		{
			CurrentTrackNumber = LoopFrom;
		}
	}

	public AudioClip CurrentTrack
	{
		get { return Tracks[CurrentTrackNumber]; }
	}

	public void Reset()
	{
		CurrentTrackNumber = 0;
	}
}
