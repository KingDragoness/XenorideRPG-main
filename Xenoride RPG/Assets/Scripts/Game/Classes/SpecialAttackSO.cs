using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Xenoride
{

	[CreateAssetMenu(fileName = "Revive", menuName = "Xenoride/Special Attack", order = 1)]

	public class SpecialAttackSO : ScriptableObject
	{

		public string ID = "Revive";
		public string NameDisplay = "Revive";
		public int SPCost = 4;
		public TBC.TBC_Action actionPrefab;

	}


}