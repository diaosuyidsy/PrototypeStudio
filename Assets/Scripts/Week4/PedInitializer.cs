using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using Week4;

[RequireComponent(typeof(BehaviorTree))]
public class PedInitializer : MonoBehaviour
{
	#region Publicly Set Variables
	[Tooltip("Starts with 0")]
	public int LaneNum;
	public GameObject Normal;
	public GameObject Ragdoll;
	#endregion

	private Transform PatrolPointsHolder;
	private List<GameObject> _pps;
	private BehaviorTree _bt;

	private void Start()
	{
		RandomizeLook();
		PatrolPointsHolder = GameManager.GM.PedLaneWaypointHolders[LaneNum];
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

	private void RandomizeLook()
	{
		int rand = Random.Range(1, 51);
		Normal.transform.GetChild(rand).gameObject.SetActive(true);
		Ragdoll.transform.GetChild(rand).gameObject.SetActive(true);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Car"))
		{
			GameObject a = Instantiate(Ragdoll, transform.position, transform.rotation);
			a.SetActive(true);
			Destroy(gameObject);
		}
	}
}
