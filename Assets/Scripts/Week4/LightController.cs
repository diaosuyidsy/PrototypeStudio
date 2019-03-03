using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week4
{
	public class LightController : MonoBehaviour
	{
		public int LaneNum;

		private enum LightState
		{
			Red,
			Yellow,
			Green,
		}
		private LightState _LS;
		private Light _light;

		private void Awake()
		{
			_LS = LightState.Red;
			_light = GetComponent<Light>();
			_light.color = Color.red;

		}

		private void OnMouseUpAsButton()
		{
			if (_LS == LightState.Red)
			{
				_LS = LightState.Green;
				_light.color = Color.green;
				GameManager.GM.LaneRed[LaneNum] = false;
			}
			else if (_LS == LightState.Green)
			{
				StartCoroutine(_turnRed(1f));
			}
		}

		IEnumerator _turnRed(float time)
		{
			_LS = LightState.Yellow;
			_light.color = Color.yellow;
			yield return new WaitForSeconds(time);
			_LS = LightState.Red;
			_light.color = Color.red;
			GameManager.GM.LaneRed[LaneNum] = true;
		}
	}

}
