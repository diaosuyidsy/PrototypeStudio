using UnityEngine;
using SonicBloom.Koreo;

namespace Week3
{
	public class OtherNPCController : MonoBehaviour
	{
		public void OnGameStart()
		{
			Koreographer.Instance.RegisterForEvents("TestEventID", SpeakForFreedom);
		}

		private void SpeakForFreedom(KoreographyEvent koreoEvent)
		{

		}
	}

}
