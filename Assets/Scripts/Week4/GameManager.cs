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
		public Transform[] PedLaneWaypointHolders;
		public GameObject CarPrefab;
		public GameObject PedPrefab;

		private void Awake()
		{
			GM = this;
			gamestate = GameState.Prepare;
		}

		private void _SpawnOneCar()
		{
			// Spawn from a lane from 0-7
			int lane = Random.Range(0, 8);
			GameObject car = Instantiate(CarPrefab, LaneWaypointHolders[lane].position, Quaternion.Euler(0f, _uglyFunction(lane), 0f));
			car.GetComponent<CarInitializer>().LaneNum = lane++;
		}

		private void _SpawnOnePed()
		{
			int lane = Random.Range(0, 8);
			GameObject ped = Instantiate(PedPrefab, PedLaneWaypointHolders[lane].position, Quaternion.identity);
			ped.GetComponent<PedInitializer>().LaneNum = lane++;
		}

		private void _OnGameStart()
		{
			InvokeRepeating("_SpawnOneCar", 0f, 1f);
			InvokeRepeating("_SpawnOnePed", 0f, 0.5f);
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

		private float _uglyFunction(int lane)
		{
			if (lane <= 1)
			{
				return 0f;
			}
			else if (lane <= 3)
			{
				return -90f;
			}
			else if (lane <= 5)
			{
				return 180f;
			}
			else
			{
				return 90f;
			}
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

