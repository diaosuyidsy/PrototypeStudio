using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons_Control : MonoBehaviour
{
	public int CurClickIndex;
	public int MaxClicks;
	public GameObject BurstParticle;
	public GameObject NextObject;

	private GameObject[] _buttons;

	private void Awake()
	{
		_buttons = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
		{
			_buttons[i] = transform.GetChild(i).gameObject;
		}
		MaxClicks = _buttons.Length;
		StartCoroutine(enableButtons(0.5f));
	}

	IEnumerator enableButtons(float time)
	{
		var times = _buttons.Length;
		for (int i = 0; i < times; i++)
		{
			float spawnY = Random.Range
				(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + 1.2f, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y - 1.2f);
			float spawnX = Random.Range
				(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + 1.2f, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 1.2f);
			_buttons[i].transform.position = new Vector3(spawnX, spawnY);
			_buttons[i].SetActive(true);
			_buttons[i].GetComponent<Animator>().SetTrigger("ButtonFall");
			yield return new WaitForSeconds(time);
		}
	}

	public void ResetAllButtons()
	{
		foreach (var button in _buttons)
		{
			button.GetComponentInChildren<Buttons_Click>().StartTremble = false;
			button.GetComponentInChildren<Buttons_Click>().ResetScale();
			button.SetActive(false);
		}
		CurClickIndex = 0;
		StartCoroutine(enableButtons(0.5f));
	}

	public void Popoff()
	{
		StartCoroutine(PopOff(2f));
	}

	IEnumerator PopOff(float time)
	{
		yield return new WaitForSeconds(time);
		for (int i = 0; i < _buttons.Length; i++)
		{
			Instantiate(BurstParticle, _buttons[i].transform.position, Quaternion.identity);
			_buttons[i].SetActive(false);
			yield return new WaitForSeconds(0.25f);
		}

		NextObject.SetActive(true);
		gameObject.SetActive(false);
	}

}
