using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week5
{
	public class Phone_Open : MonoBehaviour
	{
		public GameObject HomeButton;
		public float NeedToHoldTime = 5f;

		private float HoldDownTime;
		private Vector3 _originalPosition;

		private void Awake()
		{
			_originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		}

		private void OnMouseDown()
		{
			HoldDownTime = Time.time;
			transform.position -= new Vector3(0, 0.05f);
		}

		private void OnMouseDrag()
		{
			if (Time.time >= HoldDownTime + NeedToHoldTime)
			{
				// Unlocked the phone
				HomeButton.SetActive(true);
			}
		}

		private void OnMouseUp()
		{
			HoldDownTime = -NeedToHoldTime;
			transform.position = _originalPosition;
		}
	}

}
