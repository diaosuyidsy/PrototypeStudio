using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SonicBloom.Koreo;
using DG.Tweening;

namespace Week3
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager GM;

		public GameState State;
		public float KeyNoteDistance;
		public Transform KeyNoteGenerationPoint;
		public Transform TriggerPoint;
		public GameObject KeyNotePrefab;
		public Text CountDownText;

		private void Awake()
		{
			GM = this;
			State = GameState.PreStart;
			KeyNoteDistance = Mathf.Abs(TriggerPoint.position.x - KeyNoteGenerationPoint.position.x);
		}

		public void StartGame()
		{
			State = GameState.CountDown;
			StartCoroutine(StartCountDown(5f));
			AudioManager.AM.StartAudioWithDelay();
			Koreographer.Instance.RegisterForEvents("TestEventID", FireKeyNote);

		}

		IEnumerator StartCountDown(float time)
		{
			CountDownText.gameObject.SetActive(true);
			float elapedTime = time;
			while (elapedTime > 0f)
			{
				elapedTime -= Time.deltaTime;
				CountDownText.text = Mathf.RoundToInt(elapedTime).ToString();
				yield return new WaitForEndOfFrame();
			}
			CountDownText.gameObject.SetActive(false);
			State = GameState.Start;
		}

		public void OnKeyNoteMissed()
		{
			print("You just fucking missed");
		}

		public void OnKeyNoteHitSuccessful()
		{
			print("Successful Hit");
		}

		private void FireKeyNote(KoreographyEvent koreoEvent)
		{
			Instantiate(KeyNotePrefab, KeyNoteGenerationPoint.transform.position, KeyNotePrefab.transform.rotation);
		}
	}

	public enum GameState
	{
		PreStart,
		CountDown,
		Start,
		End,
	}

}
