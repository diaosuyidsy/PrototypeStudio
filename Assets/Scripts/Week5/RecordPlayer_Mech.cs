using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPlayer_Mech : MonoBehaviour
{
	private Transform _parent;
	private Vector3 _originalParentRotation;
	private Vector3 _originalSelfRotation;
	private Vector3 _mouseDownPositoin;

	private void Awake()
	{
		_parent = transform.parent;
		_originalParentRotation = _parent.rotation.eulerAngles;
		_originalSelfRotation = transform.rotation.eulerAngles;
	}

	private void OnMouseDown()
	{
		_mouseDownPositoin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	private void OnMouseDrag()
	{
		Vector3 curMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float xDiff = curMousePos.x - _mouseDownPositoin.x;
		float yDiff = curMousePos.y - _mouseDownPositoin.y;
		Vector3 diff = curMousePos - _parent.position;
		diff.Normalize();

		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		_parent.rotation = Quaternion.Euler(0f, 0f, (rot_z + 90));
	}

	private void OnMouseUp()
	{
		_parent.eulerAngles = _originalParentRotation;
		transform.eulerAngles = _originalSelfRotation;
	}
}
