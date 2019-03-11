using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons_Event : MonoBehaviour
{
	public void CameraShake()
	{
		Camera.main.GetComponent<CameraShake>().shakeDuration = 0.1f;
	}
}
