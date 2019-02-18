using UnityEngine;
using SonicBloom.Koreo;

namespace Week3
{
	public class EventSubscriber : MonoBehaviour
	{
		// Start is called before the first frame update
		void Start()
		{
			Koreographer.Instance.RegisterForEvents("TestEventID", FireEventDebugLog);
		}

		private void FireEventDebugLog(KoreographyEvent koreoEvent)
		{
			Debug.Log(Time.timeSinceLevelLoad);
		}
	}
}

