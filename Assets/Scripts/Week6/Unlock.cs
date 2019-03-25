using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6
{
	public class Unlock : MonoBehaviour
	{
		public int AcceptablePlayerID;
		public GameObject ColorCode;
		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				var g = collision.gameObject.GetComponent<PlayerController>();
				if (g.PlayerID == AcceptablePlayerID)
				{
					GameManager.GM.AddToLock(1);
					ColorCode.SetActive(false);
				}
			}
		}

		private void OnCollisionExit2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				var g = collision.gameObject.GetComponent<PlayerController>();
				if (g.PlayerID == AcceptablePlayerID)
				{
					ColorCode.SetActive(true);
					GameManager.GM.AddToLock(-1);
				}
			}
		}
	}
}

