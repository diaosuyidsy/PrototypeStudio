using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week3
{
	public class KeyNoteController : MonoBehaviour
	{
		public float Speed = 10f;
		public KeyCode RightKeyCode;

		private KeyNoteDirection Dir;

		private void Awake()
		{
			_init();
		}

		// Initialize the key note's direction randomly
		private void _init()
		{
			int rand = Random.Range(0, 4);
			Dir = (KeyNoteDirection)rand;
			transform.rotation = Quaternion.Euler(0f, 0f, 90f * (int)Dir);
			switch (Dir)
			{
				case KeyNoteDirection.Down:
					RightKeyCode = KeyCode.DownArrow;
					break;
				case KeyNoteDirection.Up:
					RightKeyCode = KeyCode.UpArrow;
					break;
				case KeyNoteDirection.Left:
					RightKeyCode = KeyCode.LeftArrow;
					break;
				case KeyNoteDirection.Right:
					RightKeyCode = KeyCode.RightArrow;
					break;
			}
		}

		private void Update()
		{
			transform.position -= new Vector3(Speed * Time.deltaTime, 0f);
		}

	}

	public enum KeyNoteDirection
	{
		Right = 0,
		Up = 1,
		Left = 2,
		Down = 3,
	}
}
