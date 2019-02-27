using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week4
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager GM;

		public bool[] LaneRed;

		private void Awake()
		{
			GM = this;
		}

	}
}

