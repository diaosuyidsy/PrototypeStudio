using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

namespace Week6
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager GM;

		public GameObject Lock;
		public GameObject LostText;
		public GameObject StartText;

		private int TotalPlayerNum;
		private int playerDead;
		private int lockNum;
		private float[] jp;
		private int jpp;

		public enum GameState
		{
			Prestart,
			Start,
			End,
		}

		public GameState State;

		private void Awake()
		{
			GM = this;
			State = GameState.Prestart;
			TotalPlayerNum = GameObject.FindGameObjectsWithTag("Player").Length;
			jp = new float[2] { -1f, -1f };
		}

		public void AddToLock(int num)
		{
			lockNum += num;
			if (TotalPlayerNum == lockNum) Lock.SetActive(false);
		}

		public void PlayerD()
		{
			playerDead++;
			if (playerDead >= TotalPlayerNum)
			{
				State = GameState.End;
				LostText.SetActive(true);
			}
		}

		public void JumpStart(int pn)
		{
			jp[pn] = Time.time;
			if (Mathf.Abs(jp[0] - jp[1]) < 0.5f)
			{
				State = GameState.Start;
				StartText.SetActive(false);
			}
		}

		private void Update()
		{
			if ((ReInput.players.GetPlayer(0).GetButtonDown("R") || ReInput.players.GetPlayer(1).GetButtonDown("R")) && State == GameState.End)
				SceneManager.LoadScene("Week6");
		}

	}
}

