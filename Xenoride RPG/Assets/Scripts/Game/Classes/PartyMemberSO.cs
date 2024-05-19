using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Xenoride
{

	[CreateAssetMenu(fileName = "Tomas", menuName = "Xenoride/Party Member", order = 1)]

	public class PartyMemberSO : ScriptableObject
	{

		public string ID = "Tomas";
		public string NameDisplay = "Tomas";
		public Party.Alliance alliance = Party.Alliance.Enemy;
		public Sprite sprite_wide_201px;

		[Space]
		public SaveData.BattleStat battleStat;


		[FoldoutGroup("CalculationTest")] public int HP = 0;
		[FoldoutGroup("CalculationTest")] public int SP = 0;


		[Button("Calculate Stat")]
		public void CalculateStat()
        {
			HP = battleStat.MaxHitpoint;
			SP = battleStat.MaxSP;
        }
	}
}