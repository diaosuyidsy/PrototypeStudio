using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Week3
{
	/// <summary>
	/// Three types of failure:
	/// 1. The KeyNote Collided with TriggerPoint
	/// 2. Player pressed the TriggerPoint at the wrong time
	/// 3. Player pressed the TriggerPoint at the right time, but the wrong note
	/// </summary>
	public class TriggerPointController : MonoBehaviour
	{

		// Update is called once per frame
		void Update()
		{
			if (GameManager.GM.State == GameState.Start)
				_checkInput();
		}

		private void _checkInput()
		{
			bool upPressed = Input.GetKeyDown(KeyCode.UpArrow);
			bool downPressed = Input.GetKeyDown(KeyCode.DownArrow);
			bool leftPressed = Input.GetKeyDown(KeyCode.LeftArrow);
			bool rightPressed = Input.GetKeyDown(KeyCode.RightArrow);

			if (upPressed || downPressed || leftPressed || rightPressed)
			{
				Collider2D collider = Physics2D.OverlapCircle(transform.position, 0.2f);
				if (collider != null && collider.CompareTag("KeyNote"))
				{
					// If we hit the key note, check for the right sign
					KeyCode correctKey = collider.GetComponent<KeyNoteController>().RightKeyCode;
					if (Input.GetKeyDown(correctKey))
					{
						GameManager.GM.OnKeyNoteHitSuccessful();
						Destroy(collider.gameObject);
					}
					else
					{
						GameManager.GM.OnKeyNoteMissed();
						GameManager.GM.NPCSayFreedom();
					}
				}
				else
				{
					GameManager.GM.OnKeyNoteMissed();
				}
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.transform.CompareTag("KeyNote"))
			{
				GameManager.GM.OnKeyNoteMissed();
				GameManager.GM.NPCSayFreedom();
			}
		}
	}
}

