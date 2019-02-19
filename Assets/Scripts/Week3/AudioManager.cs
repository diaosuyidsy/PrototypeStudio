using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Week3
{
	public class AudioManager : MonoBehaviour
	{
		public static AudioManager AM;
		public float DelayedTime = 5f;
		public GameObject Koreographer;
		public AudioClip KillSound;

		private AudioSource _audioSource;

		private void Awake()
		{
			AM = this;
			_audioSource = GetComponent<AudioSource>();
		}

		public void StartAudioWithDelay()
		{
			Koreographer.SetActive(true);
			_audioSource.PlayDelayed(DelayedTime);
		}

		public void StartKillSound()
		{
			_audioSource.clip = KillSound;
			_audioSource.Play();
		}
	}
}

