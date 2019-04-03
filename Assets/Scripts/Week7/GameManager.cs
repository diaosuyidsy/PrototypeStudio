using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Week7
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager GM;
		public GameState State;
		public float HighScore;
		public PlayerControl PC;
		public Text HighScoreText;
		public GameObject SpeechBubble;
		public string[] PreGameSentence;
		public string[] HitSentence;
		public string[] EndSentence;

		private void Awake()
		{
			if (GM == null)
				GM = this;
			else if (GM != this)
				Destroy(gameObject);

			DontDestroyOnLoad(gameObject);
			State = GameState.Prestart;
			Speech(0);
		}

		public void EndGame()
		{
			if (State == GameState.End) return;
			if (PC.SFloat > HighScore)
			{
				HighScore = PC.SFloat;
				HighScoreText.text = HighScore.ToString("F1");
			}
			State = GameState.End;
			Speech(2);
		}

		private void OnEnable()
		{
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		private void OnDisable()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}

		void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			PC = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerControl>();
			HighScoreText = GameObject.FindGameObjectWithTag("HighScoreText").GetComponent<Text>();
			//SpeechBubble = GameObject.FindGameObjectWithTag("SpeechBubble");
			//if (SpeechBubble != null)
			//	SpeechBubble.SetActive(false);
			//Speech(0);
			State = GameState.Prestart;
			HighScoreText.text = HighScore.ToString("F1");
		}


		public void Speech(int index)
		{
			//switch (index)
			//{
			//	case 0:
			//		StartCoroutine(Speak(PreGameSentence[Random.Range(0, PreGameSentence.Length)], 1f, 2f));
			//		break;
			//	case 1:
			//		StartCoroutine(Speak(HitSentence[Random.Range(0, HitSentence.Length)], 0f, 0.5f));
			//		break;
			//	case 2:
			//		StartCoroutine(Speak(EndSentence[Random.Range(0, EndSentence.Length)], 2f, 1f));
			//		break;
			//}
		}
		IEnumerator Speak(string str, float preWaitTime, float time)
		{
			yield return new WaitForSeconds(preWaitTime);
			SpeechBubble.GetComponentInChildren<Text>().text = str;
			SpeechBubble.SetActive(true);
			yield return new WaitForSeconds(time);
			SpeechBubble.SetActive(false);
		}
	}

	public enum GameState
	{
		Prestart,
		Start,
		End,
	}
}

