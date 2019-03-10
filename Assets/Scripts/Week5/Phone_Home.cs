using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week5
{
	public class Phone_Home : MonoBehaviour
	{
		public GameObject DragButton;
		public float PressedScale = 0.8f;
		public float TimeBetweenPress = 0.1f;

		private Vector3 _originalScale;
		private float LastTimePress;

		private void Awake()
		{
			_originalScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}

		private void OnMouseDown()
		{
			transform.localScale = _originalScale * PressedScale;
		}

		private void OnMouseUp()
		{
			transform.localScale = _originalScale;
			if (Mathf.Abs(Time.time - LastTimePress) < TimeBetweenPress)
			{
				DragButton.SetActive(true);
			}
			LastTimePress = Time.time;
		}
	}
}

