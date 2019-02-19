using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SonicBloom.Koreo;
using UnityEngine.SceneManagement;

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

		private GameObject[] NPCs;
		private GameObject Player;

		private void Awake()
		{
			GM = this;
			State = GameState.PreStart;
			KeyNoteDistance = Mathf.Abs(TriggerPoint.position.x - KeyNoteGenerationPoint.position.x);
			NPCs = GameObject.FindGameObjectsWithTag("OtherNPC");
			Player = GameObject.FindGameObjectWithTag("Player");
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
			Player.SendMessage("SayAntiFreedom");
			StartCoroutine(StartOver(2f));
		}

		public void OnKeyNoteHitSuccessful()
		{
			NPCSayFreedom();
			Player.SendMessage("SpeakForFreedom");
		}

		public void NPCSayFreedom()
		{
			foreach (var npc in NPCs)
			{
				npc.SendMessage("SpeakForFreedom");
			}
		}

		private void FireKeyNote(KoreographyEvent koreoEvent)
		{
			Instantiate(KeyNotePrefab, KeyNoteGenerationPoint.transform.position, KeyNotePrefab.transform.rotation);
		}

		IEnumerator StartOver(float time)
		{
			yield return new WaitForSeconds(time);
			SceneManager.LoadScene("Week3_America");
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
