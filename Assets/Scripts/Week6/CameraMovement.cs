using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week6
{
	public class CameraMovement : MonoBehaviour
	{
		public float Speed;
		// Update is called once per frame
		void Update()
		{
			if (GameManager.GM.State == GameManager.GameState.Start && transform.position.y < 18.46f)
			{
				transform.position += new Vector3(0f, Time.deltaTime * Speed);
			}
		}
	}
}

