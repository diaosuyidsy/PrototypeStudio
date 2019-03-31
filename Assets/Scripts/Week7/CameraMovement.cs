using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week7
{
	public class CameraMovement : MonoBehaviour
	{
		public GameObject Target;

		private float YOffset;
		private float ZOffset;
		private Vector3 velocity;
		private void Awake()
		{
			YOffset = transform.position.y - Target.transform.position.y;
			ZOffset = transform.position.z - Target.transform.position.z;
		}

		private void Update()
		{
			var pos = transform.position;
			pos.y = Target.transform.position.y + YOffset;
			pos.z = Target.transform.position.z + ZOffset;

			////transform.position = Vector3.Slerp(transform.position, pos, Time.deltaTime * 5f);
			//transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, 1f);
			transform.position = pos;
		}
	}
}

