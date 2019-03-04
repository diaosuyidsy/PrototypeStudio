using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedAutoDestruct : MonoBehaviour
{
	private void OnEnable()
	{
		Destroy(gameObject, 15f);
	}

}
