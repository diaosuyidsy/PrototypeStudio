using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone_ExitEvent : MonoBehaviour
{
	public GameObject Phone;
	public GameObject NextObject;

	public void PhoneDisappear()
	{
		Phone.SetActive(false);
	}

	public void Next()
	{
		NextObject.SetActive(true);
	}
}
