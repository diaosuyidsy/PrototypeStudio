using UnityEngine;

namespace Week3
{
	public class DeathZone : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D collision)
		{
			Destroy(collision.gameObject);
		}
	}
}

