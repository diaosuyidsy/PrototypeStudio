using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPlayer_Mech : MonoBehaviour
{
	public RecordPlayer_Record Record;
	public AudioClip Music;

	private Transform _parent;
	private Vector3 _originalParentRotation;
	private Vector3 _originalSelfRotation;
	private Vector3 _mouseDownPositoin;
	private float _originalMouseDistance;
	private bool canInteract = true;

	private void Awake()
	{
		_parent = transform.parent;
		_originalParentRotation = _parent.rotation.eulerAngles;
		_originalSelfRotation = transform.rotation.eulerAngles;
		canInteract = true;
	}

	private void OnMouseDown()
	{
		if (!canInteract) return;
		_mouseDownPositoin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		_originalMouseDistance = Vector3.Distance(_mouseDownPositoin, _parent.position);
	}

	private void OnMouseDrag()
	{
		if (!canInteract) return;

		Vector3 curMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float curDistance = Vector3.Distance(curMousePos, _parent.position);
		float distanceDiff = _originalMouseDistance - curDistance;
		distanceDiff = distanceDiff > 0 ? distanceDiff : 0f;
		Vector3 diff = curMousePos - _parent.position;
		diff.Normalize();

		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 90f;
		var minClamp = (transform.localEulerAngles.x < 10f && (Mathf.Abs(_parent.eulerAngles.z - 360f + 9.7f) <= 5f)) ? -9.7f : -30f;
		rot_z = Mathf.Clamp(rot_z, minClamp, 9.9f);
		_parent.rotation = Quaternion.Euler(0f, 0f, rot_z);
		transform.localEulerAngles = new Vector3(distanceDiff * 80f, transform.localEulerAngles.y, transform.localEulerAngles.z);

	}

	private void OnMouseUp()
	{
		if (!canInteract) return;

		if (_parent.eulerAngles.z - 360f < -12f && Record.IsRotationFastEnough)
		{
			Camera.main.GetComponent<AudioSource>().clip = Music;
			Camera.main.GetComponent<AudioSource>().Play();
			Record.canInteract = false;
			canInteract = false;
			StartCoroutine(StartEnding());
		}
		else
		{
			_parent.eulerAngles = _originalParentRotation;
			transform.eulerAngles = _originalSelfRotation;
		}
	}

	IEnumerator StartEnding()
	{
		yield return new WaitForSeconds(6.5f);
		GetComponentInParent<Animator>().SetTrigger("RecordExit");
	}
}
