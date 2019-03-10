using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone_ExitEvent : MonoBehaviour
{
	public GameObject Phone;

	public void PhoneDisappear()
	{
		Phone.SetActive(false);
	}
}
