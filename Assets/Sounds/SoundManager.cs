using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;

	public AudioClip otletaetKrizhka;


	void Awake()
	{
		instance = this;
	}
}
