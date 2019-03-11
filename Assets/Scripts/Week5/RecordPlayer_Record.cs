using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPlayer_Record : MonoBehaviour
{
	public bool IsRotationFastEnough;
	public bool canInteract = true;

	private Vector3 _lastHoldingPosition;
	private float _rotationSpeed;

	private void Awake()
	{
		canInteract = true;
	}

	private void Update()
	{
		transform.Rotate(-Vector3.forward * Time.deltaTime * _rotationSpeed * 100f);
		IsRotationFastEnough = _rotationSpeed > 3f;
	}

	private void OnMouseDrag()
	{
		if (!canInteract) return;
		var curMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 diff = curMousePos - transform.position;
		diff.Normalize();

		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90f;
		transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
		_lastHoldingPosition = curMousePos;
	}

	private void OnMouseUp()
	{
		if (!canInteract) return;

		var curMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		_rotationSpeed = Vector3.Distance(curMousePos, _lastHoldingPosition);

		if (_lastHoldingPosition.y > transform.position.y)
		{
			if (curMousePos.x > _lastHoldingPosition.x) _rotationSpeed *= 1f;
			else _rotationSpeed *= -1f;
		}
		else
		{
			if (curMousePos.x > _lastHoldingPosition.x) _rotationSpeed *= -1f;
			else _rotationSpeed *= 1f;
		}
	}
}
