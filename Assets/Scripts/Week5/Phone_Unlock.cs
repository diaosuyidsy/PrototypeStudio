using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week5
{
	public class Phone_Unlock : MonoBehaviour
	{
		public float RightOffset = 1f;
		public Animator PhoneExit;
		public SpriteRenderer FinalScreen;

		private Vector3 _originalPosition;

		private void Awake()
		{
			_originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		}

		private void OnMouseDrag()
		{
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var pos = transform.position;
			pos.x = mousePosition.x;
			if (pos.x - _originalPosition.x <= RightOffset && pos.x > _originalPosition.x)
				transform.position = pos;
			else if (pos.x - _originalPosition.x > RightOffset)
			{
				pos.x = _originalPosition.x + RightOffset;
				transform.position = pos;
			}
		}

		private void OnMouseUp()
		{
			if (Mathf.Abs(transform.position.x - _originalPosition.x - RightOffset) < 0.05f)
			{
				StartCoroutine(StartScreen(1f));
				GetComponent<SpriteRenderer>().enabled = false;
			}
			else transform.position = _originalPosition;
		}

		IEnumerator StartScreen(float time)
		{
			float elapsedTime = 0f;
			float initialColorAlpha = 0f;
			while (elapsedTime <= time)
			{
				float a = Mathf.Lerp(initialColorAlpha, 1f, elapsedTime / time);
				Color temp = FinalScreen.color;
				temp.a = a;
				FinalScreen.color = temp;
				elapsedTime += Time.deltaTime;
				print(elapsedTime);
				yield return new WaitForEndOfFrame();
			}
			PhoneExit.SetTrigger("PhoneExit");
			StartCoroutine(SetInactive(0.8f));
		}

		IEnumerator SetInactive(float time)
		{
			yield return new WaitForSeconds(time);
			transform.parent.gameObject.SetActive(false);
		}
	}
}

