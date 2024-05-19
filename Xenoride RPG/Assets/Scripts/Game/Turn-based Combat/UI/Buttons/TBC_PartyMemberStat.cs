using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Xenoride.TBC
{
	public class TBC_PartyMemberStat : MonoBehaviour
	{

		public TBC_Party partyMember;
		public Image portrait;
		public Slider hitpoint_slider;
		public Slider sp_slider;
		public Text label_hitpoint;
		public Text label_sp;
		public Text label_partyName;

        private void Update()
        {
			if (partyMember == null) return;

			portrait.sprite = partyMember.partyMemberSO.sprite_wide_201px;
			hitpoint_slider.value = partyMember.partyStat.currentHP;
			hitpoint_slider.maxValue = partyMember.partyStat.MaxHitpoint;
			sp_slider.value = partyMember.partyStat.currentSP;
			sp_slider.maxValue = partyMember.partyStat.MaxSP;
			label_hitpoint.text = Mathf.FloorToInt(partyMember.partyStat.currentHP).ToString();
			label_sp.text = Mathf.FloorToInt(partyMember.partyStat.currentSP).ToString();
			label_partyName.text = partyMember.partyMemberSO.NameDisplay;

		}

	}
}