using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Xenoride.TBC
{
	public class TBC_PointInvestUIButton : MonoBehaviour
	{

		public TBC_VictoryUI victoryUI; 
		public TypeBattleStat statType = TypeBattleStat.DEX;
		public Button button_SkillUp;
		public Button button_SkillDown;
		public Text label_CurrentSkill;
		public Text label_NetSkill;
		public Text label_FinalSkill;

		public int netPoints = 0;

		public void Up_Skill()
        {
			victoryUI.SkillUp(this);

		}

		public void Down_Skill()
        {
			victoryUI.SkillDown(this);

		}

	}
}