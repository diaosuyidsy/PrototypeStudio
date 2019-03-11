using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Click : MonoBehaviour
{
	public GameObject NextObject;

	private Vector3 _originalScale;
	private float _holdDownTime;
	private Transform _parent;
	private float _releaseTime;
	private float _releaseSpeed;
	private bool _hasReleased;
	private bool _canUpdate;

	private void Awake()
	{
		_originalScale = transform.localScale;
		_parent = transform.parent;
		_canUpdate = true;
	}

	private void Update()
	{
		if (!_canUpdate) return;
		var time = Time.time - _releaseTime;
		if (_releaseSpeed > 0f || _parent.localScale.x > 1f)
		{
			_releaseSpeed -= (9.8f * time);
			_parent.localScale += new Vector3(Time.deltaTime * _releaseSpeed * time, Time.deltaTime * _releaseSpeed * time, Time.deltaTime * _releaseSpeed * time);
			if (_parent.localScale.x > 10f)
			{
				_canUpdate = false;
				StartCoroutine(waitsec(1f));
			}
		}
		else
		{
			if (_hasReleased)
			{
				_hasReleased = false;
				Camera.main.GetComponent<CameraShake>().shakeDuration = 0.1f;
			}
			_parent.localScale = Vector3.one;
			_releaseSpeed = 0f;
		}
	}

	private void OnMouseDown()
	{
		_holdDownTime = Time.time;
		transform.localScale *= 0.8f;
	}

	private void OnMouseUp()
	{
		var curTime = Time.time;
		transform.localScale = _originalScale;
		_releaseSpeed = (Time.time - _holdDownTime) * 100f;
		_releaseTime = Time.time;
		_hasReleased = true;
	}

	IEnumerator waitsec(float time)
	{
		yield return new WaitForSeconds(time);
		NextObject.SetActive(true);
		_parent.gameObject.SetActive(false);
	}
}
