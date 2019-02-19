﻿using System.Collections;
using UnityEngine;

namespace Week3
{
	public class OtherNPCController : MonoBehaviour
	{
		public GameObject Canvas;

		public void SpeakForFreedom()
		{
			StartCoroutine(SpeechBubble(0.3f));
		}

		IEnumerator SpeechBubble(float time)
		{
			Canvas.SetActive(true);
			yield return new WaitForSeconds(time);
			Canvas.SetActive(false);
		}
	}

}
