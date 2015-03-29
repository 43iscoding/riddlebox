using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;

	public AudioClip otletaetKrizhka;
	public AudioClip cube_base_rotation;
	public AudioClip electro;
	public AudioClip krutilki;
	public AudioClip nazhatie_knopki;
	public AudioClip open_gates;
	public AudioClip otkrivanie_dverki;
	public AudioClip Otzhalisj_knopki;
	public AudioClip unlocking;
	public AudioClip unlocking_2;
	public AudioClip Vikruchivaetsa_mehanizm;
	public AudioClip vkljuchaetsa_lampochka;
	public AudioClip lampochka_var2;

	void Awake()
	{
		instance = this;
	}
}
