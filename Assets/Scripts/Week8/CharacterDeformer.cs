using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week8
{
	public class CharacterDeformer : MonoBehaviour
	{
		public float force = 80f;
		public float forceOffset = 0.1f;
		public int PixelsSize = 5;

		private void OnControllerColliderHit(ControllerColliderHit hit)
		{
			ConsoleProDebug.Watch("Hit point Position", hit.point.ToString());
			MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();
			if (deformer)
			{
				Vector3 point = hit.point;
				point += hit.normal * forceOffset;
				deformer.AddDeformingForce(point, force);
			}
		}

		//private void Update()
		//{
		//	if (Input.GetMouseButton(0))
		//		//HandleInput();
		//}

		void HandleInput()
		{
			Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(inputRay, out hit))
			{
				var renderer = hit.collider.GetComponent<Renderer>();
				var meshCollider = hit.collider as MeshCollider;

				Texture2D tex = renderer.material.mainTexture as Texture2D;
				var pixelUV = hit.textureCoord;
				pixelUV.x *= tex.width;
				pixelUV.y *= tex.height;

				var colors = new Color[PixelsSize * PixelsSize];
				// set brush to black
				for (var i = 0; i < PixelsSize * PixelsSize; i++)
				{
					colors[i] = Color.white;
				}

				tex.SetPixels((int)pixelUV.x, (int)pixelUV.y, PixelsSize, PixelsSize, colors);

				tex.Apply();
			}
		}
	}
}

