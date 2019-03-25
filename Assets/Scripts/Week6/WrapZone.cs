using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapZone : MonoBehaviour
{
	public Transform OtherWrapZone;
	public Vector2 Offset;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			collision.gameObject.transform.position = new Vector2(OtherWrapZone.position.x + Offset.x, collision.gameObject.transform.position.y);
		}
	}
}
