using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Xenoride.TBC
{
	public class TBC_Victory_PartyXP : MonoBehaviour
	{

		public Image portrait;
		public Text label_Level;
		public Text label_XP;
		public Slider slider_XP;
		public TBC_Party partyTarget;
        public GameObject levelUp;


		[Space]
		public float XPLeftToRun = 0f;
        public float SpeedToDrainXP = 50f;

        private int prev_Level = 1;

        public void OverrideStart(int level)
        {
            prev_Level = level;
        }

		public void Refresh()
        {
            if (partyTarget == null) return;
            if (gameObject.activeInHierarchy == false) return;

            var party = partyTarget;
            int originalXP = party.partyStat.TotalXP;
            int projectedXP = party.partyStat.TotalXP + TurnBasedCombat.Turn.TotalXPGain;
            int xp_Running = projectedXP - Mathf.RoundToInt(XPLeftToRun);

            int demLevel = SaveData.PartyStat.GetCurrentLevel(originalXP + xp_Running);
            int demXP = SaveData.PartyStat.GetCurrentXP(originalXP + xp_Running);
            int maxXPLevel = SaveData.PartyStat.GetNextLevelUp(demLevel);

            if (XPLeftToRun >= 0f) XPLeftToRun -= Time.deltaTime * SpeedToDrainXP * demLevel;
            if (demLevel > prev_Level)
            {
                levelUp.gameObject.EnableGameobject(true);
                //trigger animation for level up
            }

            portrait.sprite = party.partyMemberSO.sprite_wide_201px;
            label_Level.text = $"LV. {demLevel}";
            label_XP.text = $"{demXP}/{maxXPLevel}";
            slider_XP.value = demXP;
            slider_XP.maxValue = maxXPLevel;
            prev_Level = demLevel;
        }

	}
}