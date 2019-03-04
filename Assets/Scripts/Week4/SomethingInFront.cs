using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SomethingInFront : Conditional
{
	public LayerMask objectLayerMask;
	public SharedString targetTag;
	public SharedFloat viewDistance = 50;

	public override TaskStatus OnUpdate()
	{
		RaycastHit hit;
		if (Physics.SphereCast(transform.position, 0.5f, transform.forward, out hit, viewDistance.Value, objectLayerMask))
		{
			if (hit.collider.CompareTag(targetTag.Value)) return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
