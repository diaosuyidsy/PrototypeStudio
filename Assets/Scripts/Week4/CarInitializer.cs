using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

[RequireComponent(typeof(BehaviorTree))]
public class CarInitializer : MonoBehaviour
{
	#region Publicly Set Variables
	public Transform PatrolPointsHolder;
	#endregion

	private List<GameObject> _pps;
	private BehaviorTree _bt;

	private void Awake()
	{
		_bt = GetComponent<BehaviorTree>();
		_pps = new List<GameObject>();
		for (int i = 0; i < PatrolPointsHolder.childCount; i++)
		{
			GameObject go = new GameObject();
			go.transform.position = PatrolPointsHolder.GetChild(i).position;
			_pps.Add(go);
		}
		_bt.SetVariableValue("Patrol Points", _pps);
	}

}
