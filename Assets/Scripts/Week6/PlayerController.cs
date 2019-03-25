using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

namespace Week6
{
	public class PlayerController : MonoBehaviour
	{
		public int PlayerID;
		public float Speed = 5f;
		public float CheckRadius = 0.2f;
		public LayerMask WhatIsGround;
		public float JumpForce = 5f;
		public Transform FeetPosition;
		public float JumpTime = 0.2f;
		public bool HasControl = true;
		public Transform Canvas;

		private Player _player;
		private Rigidbody2D _rb;
		private float moveHorizontal;
		private bool _isGrounded;
		private bool _isJumping;
		private float _jumpTimeCounter;

		private void Awake()
		{
			_jumpTimeCounter = JumpTime;
			_player = ReInput.players.GetPlayer(PlayerID);
			_rb = GetComponent<Rigidbody2D>();
		}

		void Update()
		{
			_checkJump();
		}

		private void FixedUpdate()
		{
			_checkMovement();
		}

		private void _checkJump()
		{
			_isGrounded = Physics2D.OverlapCircle(FeetPosition.position, CheckRadius, WhatIsGround);

			if (_isGrounded && _player.GetButtonDown("Jump") && HasControl)
			{
				_isJumping = true;
				_jumpTimeCounter = JumpTime;
				_rb.velocity = new Vector2(_rb.velocity.x, Vector2.up.y * JumpForce);
				GameManager.GM.JumpStart(PlayerID);
			}

			if (_player.GetButton("Jump") && _isJumping)
			{
				if (_jumpTimeCounter > 0)
				{
					_rb.velocity = new Vector2(_rb.velocity.x, Vector2.up.y * JumpForce);
					_jumpTimeCounter -= Time.deltaTime;
				}
				else
				{
					_isJumping = false;
				}
			}

			if (_player.GetButtonUp("Jump"))
			{
				_isJumping = false;
			}
		}

		private void _checkMovement()
		{
			moveHorizontal = _player.GetAxis("Move Horizontal");
			if (moveHorizontal > 0)
			{
				transform.eulerAngles = Vector3.zero;
				Canvas.localScale = new Vector3(1, 1, 1);
			}
			else if (moveHorizontal < 0)
			{
				transform.eulerAngles = new Vector3(0f, 180f);
				Canvas.localScale = new Vector3(-1, 1, 1);
			}
			if (HasControl)
				_rb.velocity = new Vector2(Speed * moveHorizontal, _rb.velocity.y);
		}

		public void LoseControl(float time)
		{
			StartCoroutine(_lc(time));
		}

		IEnumerator _lc(float time)
		{
			HasControl = false;
			yield return new WaitForSeconds(time);
			HasControl = true;
		}
	}
}

