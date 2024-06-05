using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Xenoride.TBC
{
	public class TBC_VictoryUI : MonoBehaviour
	{

		public Transform parentPartyXP;
		public List<TBC_Victory_PartyXP> victoryButtons = new List<TBC_Victory_PartyXP>();

        [FoldoutGroup("DEBUG")]
        [Button("Test_XP Run")]
        public void Inject_TestXP(int xp)
        {
            Debug.Log(SaveData.PartyStat.GetCurrentXP(xp));
        }

        [FoldoutGroup("DEBUG")]
        [Button("Test_LV Run")]
        public void Inject_TestLV(int xp)
        {
            Debug.Log(SaveData.PartyStat.GetCurrentLevel(xp));
        }

        private void OnEnable()
        {
            foreach (var button in victoryButtons)
            {
                button.gameObject.EnableGameobject(false);
            }

            var parties = TurnBasedCombat.Turn.GetAllPartyMembers();

            int i = 0;

            foreach (var button in victoryButtons)
            {
                if (i >= parties.Count) break;
                var party = parties[i];
                button.partyTarget = party;
                button.gameObject.EnableGameobject(true);
                button.XPLeftToRun = TurnBasedCombat.Turn.TotalXPGain;
                button.OverrideStart(party.partyStat.Level);

                i++;
            }
        }

        private void Update()
        {


            foreach (var button in victoryButtons)
            {
                button.Refresh();

            }
        }

    }
}