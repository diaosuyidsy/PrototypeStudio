using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6
{
	public class Ability_Boop : Ability
	{
		public float BoopForceImpulse = 10f;
		public float Angle = 60f;
		public float Range = 3f;

		public LayerMask EnemyMask;

		public override void OnPressedDownAbility()
		{
			nextReadyTime = BaseCoolDown + Time.time;
			coolDownTimeLeft = BaseCoolDown;
			_boopEnemy();
		}

		private void _boopEnemy()
		{
			print("Boop");
			// First check if there is a enemy to kill
			Collider2D[] hitcolliders = Physics2D.OverlapCircleAll(transform.position, Range, EnemyMask);
			for (int i = 0; i < hitcolliders.Length; i++)
			{
				var hit = hitcolliders[i].gameObject;
				if (hit == gameObject) continue;
				float angle = _angleBetween(hit);
				print(angle);
				if (angle < Angle)
				{
					//hit.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * BoopForceImpulse, ForceMode2D.Impulse);
					print("Added force");
					hit.gameObject.GetComponent<PlayerController>().LoseControl(0.5f);
					hit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.right.x * BoopForceImpulse, hit.gameObject.GetComponent<Rigidbody2D>().velocity.y);

				}
			}
		}

		private float _angleBetween(GameObject go)
		{
			Vector3 targetDir = go.transform.position - transform.position;
			float angle = Vector3.Angle(targetDir, transform.right);
			return angle;
		}
	}
}

