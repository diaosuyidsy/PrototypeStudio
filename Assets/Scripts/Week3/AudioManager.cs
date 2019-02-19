using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week3
{
	public class AudioManager : MonoBehaviour
	{
		public static AudioManager AM;
		public float DelayedTime = 5f;

		private AudioSource _audioSource;

		private void Awake()
		{
			AM = this;
			_audioSource = GetComponent<AudioSource>();
		}

		public void StartAudioWithDelay()
		{
			_audioSource.PlayDelayed(DelayedTime);
		}
	}
}

