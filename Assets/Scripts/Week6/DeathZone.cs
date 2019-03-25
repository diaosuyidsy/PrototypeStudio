using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6
{
	public class DeathZone : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				collision.gameObject.SetActive(false);
				GameManager.GM.PlayerD();
			}
		}
	}
}

