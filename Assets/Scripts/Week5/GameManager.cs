using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Week5
{
	public class GameManager : MonoBehaviour
	{
		private void Update()
		{
			if (Input.GetKeyUp(KeyCode.R))
			{
				SceneManager.LoadScene("Week5");
			}
		}
	}

}
