using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Xenoride.TBC
{
	public abstract class TBC_PartyState : MonoBehaviour
	{

		public TBC_Party partyScript;

		public abstract void OnEnterState();
		public abstract void OnState();
		public abstract void OnExitState();

	}
}