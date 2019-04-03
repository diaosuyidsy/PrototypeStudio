using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
		public Rigidbody BodyRigidbody;
		public Text Score;
		public float SFloat;
		public Text ForceText;
		public GameObject SpeechBubble;

		public float TotalRotation;
		// Update is called once per frame
		void Update()
		{
			CheckMouseInput();
			if (Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene("Week7");
			}
			SpeechBubble.transform.position = Camera.main.WorldToScreenPoint(BodyRigidbody.transform.position + new Vector3(0f, 1f));
		}

		private void FixedUpdate()
		{
			if (GameManager.GM.State != GameState.Start) return;
			var AngularVelocity = BodyRigidbody.angularVelocity.magnitude * Mathf.Rad2Deg * Time.fixedDeltaTime;
			TotalRotation += AngularVelocity;
			SFloat = TotalRotation / 360f;
			Score.text = SFloat.ToString("F1");
		}

		private void CheckMouseInput()
		{
			if (GameManager.GM.State != GameState.Prestart) return;
			if (Input.GetMouseButton(0))
			{
				ForceText.gameObject.SetActive(true);
				ForceText.rectTransform.position = Input.mousePosition;
				ForceImpulse += 0.5f;
				ForceImpulse = Mathf.Clamp(ForceImpulse, 20f, 90f);
				ForceText.text = ForceImpulse.ToString("F0");

			}
			if (Input.GetMouseButtonUp(0))
			{
				ForceText.gameObject.SetActive(false);
				var v3 = Input.mousePosition;
				v3.z = 4.5f;
				var pos = Camera.main.ScreenToWorldPoint(v3);
				pos.x = 0f;

				//var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				//cube.transform.position = pos;
				Collider[] hits = Physics.OverlapSphere(pos, 0.2f, PlayerMask);
				if (hits.Length == 0) return;

				GameManager.GM.Speech(1);
				GameManager.GM.State = GameState.Start;
				PenguinRagdoll.transform.position = Penguin.transform.position;
				Penguin.SetActive(false);
				PenguinRagdoll.SetActive(true);

				hits = Physics.OverlapSphere(pos, 0.2f, RagdollNoCollisionMask);
				foreach (Collider hit in hits)
				{
					hit.gameObject.GetComponent<Rigidbody>().AddExplosionForce(ForceImpulse, pos, ExplosionRange, UpwardMultiplier, ForceMode.Impulse);
				}

				ForceImpulse = 20f;
			}
		}
	}
}

