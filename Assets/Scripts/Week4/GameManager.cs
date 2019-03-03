using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week4
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager GM;
		public GameState gamestate;

		public bool[] LaneRed;
		public Transform[] LaneWaypointHolders;
		public GameObject CarPrefab;

		private void Awake()
		{
			GM = this;
			gamestate = GameState.Prepare;
		}

		private void _SpawnOneCar()
		{
			// Spawn from a lane from 0-7
			int lane = Random.Range(0, 8);
			GameObject car = Instantiate(CarPrefab, LaneWaypointHolders[lane].position, Quaternion.identity);
			car.GetComponent<CarInitializer>().LaneNum = lane++;
		}

		private void _OnGameStart()
		{
			InvokeRepeating("_SpawnOneCar", 0f, 1f);
		}

		public void TriggerGameStart()
		{
			EventManager.TriggerEvent("Game Start");
		}

		private void OnEnable()
		{
			EventManager.StartListening("Game Start", _OnGameStart);
		}

		private void OnDisable()
		{
			EventManager.StopListening("Game Start", _OnGameStart);
		}
	}

	public enum GameState
	{
		Prepare,
		Start,
		Pause,
		End,
	}
}

