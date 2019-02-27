using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using Week4;

public class IsLaneRed : Conditional
{
	public SharedInt LaneNumber;

	public override TaskStatus OnUpdate()
	{
		if (GameManager.GM.LaneRed[LaneNumber.Value]) return TaskStatus.Success;
		return TaskStatus.Failure;
	}
}
