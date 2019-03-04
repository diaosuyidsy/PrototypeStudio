﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using Week4;

[RequireComponent(typeof(BehaviorTree))]
public class CarInitializer : MonoBehaviour
{
	#region Publicly Set Variables
	[Tooltip("Starts with 0")]
	public int LaneNum;
	public GameObject ExplosionVFX;
	#endregion

	private Transform PatrolPointsHolder;
	private List<GameObject> _pps;
	private BehaviorTree _bt;

	private void Start()
	{
		PatrolPointsHolder = GameManager.GM.LaneWaypointHolders[LaneNum];
		_bt = GetComponent<BehaviorTree>();
		_pps = new List<GameObject>();
		List<GameObject> PP = new List<GameObject>();
		for (int i = 0; i < PatrolPointsHolder.childCount; i++)
		{
			PP.Add(PatrolPointsHolder.GetChild(i).gameObject);
		}
		_bt.SetVariableValue("Patrol Points", PP);
		_bt.SetVariableValue("LaneNumber", LaneNum);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Car"))
		{
			Instantiate(ExplosionVFX, transform.position, ExplosionVFX.transform.rotation);
			Destroy(gameObject);
		}
	}
}
