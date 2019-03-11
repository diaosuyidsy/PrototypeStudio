using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons_Click : MonoBehaviour
{
	public bool StartTremble;

	private Buttons_Control BC;
	private int index;

	private Vector3 _parentOriginalPosition;
	private Vector3 _originalScale;

	public bool canclick = true;

	private void Awake()
	{
		BC = GetComponentInParent<Buttons_Control>();
		index = transform.parent.parent.childCount - 1 - transform.parent.GetSiblingIndex();
		_parentOriginalPosition = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z);
		_originalScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
		canclick = true;
	}

	private void OnEnable()
	{
		_parentOriginalPosition = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z);
		canclick = true;
	}

	private void Update()
	{
		if (StartTremble)
		{
			var pos = _parentOriginalPosition;
			pos.x = _parentOriginalPosition.x + Mathf.Sin(Time.time * 100f) * 0.08f;
			pos.y = _parentOriginalPosition.y + Mathf.Cos(Time.time * 100f) * 0.08f;
			transform.parent.position = pos;
		}
	}

	private void OnMouseDown()
	{
		if (!canclick) return;
		transform.localScale *= 0.8f;
	}

	private void OnMouseUp()
	{
		if (!canclick) return;
		if (index == BC.CurClickIndex++)
		{
			StartTremble = true;
			canclick = false;
			if (BC.CurClickIndex == BC.MaxClicks)
			{
				// Meaning we are ready to pop off
				BC.Popoff();
			}
		}
		else
		{
			BC.ResetAllButtons();
		}
	}

	public void ResetScale()
	{
		transform.localScale = _originalScale;
	}


}
