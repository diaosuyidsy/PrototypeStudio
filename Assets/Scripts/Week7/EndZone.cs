using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week7
{
	public class EndZone : MonoBehaviour
	{
		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.CompareTag("Player") && GameManager.GM.State != GameState.End)
			{
				GameManager.GM.EndGame();
			}
		}
	}
}

