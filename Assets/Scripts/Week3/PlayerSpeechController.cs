using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Week3
{
	public class PlayerSpeechController : MonoBehaviour
	{
		public GameObject Canvas;
		public Text text;

		public void SpeakForFreedom()
		{
			text.text = "Freedom";
			StartCoroutine(SpeechBubble(0.3f));
		}

		public void SayAntiFreedom()
		{
			text.text = "I Don't like Homosexual People";
			Canvas.SetActive(true);
		}

		IEnumerator SpeechBubble(float time)
		{
			Canvas.SetActive(true);
			yield return new WaitForSeconds(time);
			Canvas.SetActive(false);
		}
	}
}

