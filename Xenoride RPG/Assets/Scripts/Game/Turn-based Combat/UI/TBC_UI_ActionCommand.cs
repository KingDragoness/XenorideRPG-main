using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Xenoride.TBC
{
	public class TBC_UI_ActionCommand : MonoBehaviour
	{

		public GameObject blackedOut;

		private TBC_Party GetCurrentParty
        {
            get { return TurnBasedCombat.Turn.CurrentTurn.party; }
        }


		public void Command_Attack()
        {
			TurnBasedCombat.UI.OpenTargetSelection(GetCurrentParty.GenericAttackCommand);

        }
		public void Command_Reload()
		{

		}

		public void OpenMenu_SpecialAttack()
        {
			TurnBasedCombat.UI.OpenAC_SpecialAttacks();
        }

		public void OpenMenu_Items()
		{

		}

		public void Command_Flee()
		{

		}
	}
}