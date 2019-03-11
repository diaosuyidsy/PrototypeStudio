using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RecordPlayer_ExitEvent : MonoBehaviour
{

	public RectTransform ToBeContinue;

	void NextObject()
	{

	}

	public void MoveInToBeContinue()
	{
		ToBeContinue.DOAnchorPosX(0f, 13f);
	}
}
