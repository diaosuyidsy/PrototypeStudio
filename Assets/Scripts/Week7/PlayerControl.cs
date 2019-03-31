using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Week7
{
	public class PlayerControl : MonoBehaviour
	{
		public float ForceImpulse = 10f;
		public float ExplosionRange = 0.5f;
		public float UpwardMultiplier = 0.2f;
		public LayerMask PlayerMask;
		public LayerMask RagdollNoCollisionMask;
		public GameObject PenguinRagdoll;
		public GameObject Penguin;
		// Update is called once per frame
		void Update()
		{
			CheckMouseInput();
			if (Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene("Week7");
			}
		}

		private void CheckMouseInput()
		{
			if (Input.GetMouseButtonDown(0))
			{
				var v3 = Input.mousePosition;
				v3.z = 4.5f;
				var pos = Camera.main.ScreenToWorldPoint(v3);
				pos.x = 0f;

				Collider[] hits = Physics.OverlapSphere(pos, 0.2f, PlayerMask);
				if (hits.Length == 0) return;

				PenguinRagdoll.transform.position = Penguin.transform.position;
				Penguin.SetActive(false);
				PenguinRagdoll.SetActive(true);

				hits = Physics.OverlapSphere(pos, 0.2f, RagdollNoCollisionMask);
				foreach (Collider hit in hits)
				{
					hit.gameObject.GetComponent<Rigidbody>().AddExplosionForce(ForceImpulse, pos, ExplosionRange, UpwardMultiplier, ForceMode.Impulse);
				}
			}
		}
	}
}

