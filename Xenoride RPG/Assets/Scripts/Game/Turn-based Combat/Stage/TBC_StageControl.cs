using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Xenoride.TBC
{
	public class TBC_StageControl : MonoBehaviour
	{

		public List<Transform> allCameraPositions = new List<Transform>();
		public List<TBC_StageSpawner> allSpawners = new List<TBC_StageSpawner>();


		//only execute after complex animation
		public void SwapCameraPos()
        {

        }

	}
}