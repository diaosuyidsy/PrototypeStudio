using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

namespace Week6
{
	[RequireComponent(typeof(PlayerController))]
	public abstract class Ability : MonoBehaviour
	{
		public string ButtonName;
		public float BaseCoolDown = 1f;
		public Text CountDown;

		protected float coolDownTimeLeft = 0f;
		protected float nextReadyTime;
		protected Player _player;
		protected bool _isUsingOtherAbility;
		protected int PlayerID;

		public virtual void Awake()
		{
			PlayerID = GetComponent<PlayerController>().PlayerID;
			_player = ReInput.players.GetPlayer(PlayerID);
		}

		public virtual void Update()
		{
			bool coolDownComplete = Time.time > nextReadyTime;
			if (coolDownComplete)
			{
				CountDown.gameObject.SetActive(false);
				if (_player.GetButtonDown(ButtonName)) OnPressedDownAbility();
			}
			else CoolDown();
		}
		/// <summary>
		/// Pressed Down the Button
		/// </summary>
		public abstract void OnPressedDownAbility();

		/// <summary>
		/// Holding the button
		/// </summary>
		public virtual void OnHoldAbility() { }

		/// <summary>
		/// Lift Up the Button
		/// </summary>
		public virtual void OnLiftUpAbility() { }

		/// <summary>
		/// Cool Down Should run in Update
		/// </summary>
		public virtual void CoolDown()
		{
			CountDown.gameObject.SetActive(true);
			coolDownTimeLeft -= Time.deltaTime;
			CountDown.text = coolDownTimeLeft.ToString("F1");
		}
	}
}

