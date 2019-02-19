using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week3
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager GM;

		public GameState State;

		public Transform KeyNoteGenerationPoint;
		public GameObject KeyNotePrefab;

		private void Awake()
		{
			GM = this;
			State = GameState.PreStart;
		}

		public void OnKeyNoteMissed()
		{
			print("You just fucking missed");
		}

		public void OnKeyNoteHitSuccessful()
		{
			print("Successful Hit");
		}
	}

	public enum GameState
	{
		PreStart,
		Start,
		End,
	}

}
