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
		public float unitSize = 1f; //affects camera's size
		public Party.Alliance alliance = Party.Alliance.Enemy;
		public Sprite sprite_wide_201px;

		[Space]
		public SaveData.BattleStat battleStat;
		public int startingLevel = 1;


		[FoldoutGroup("CalculationTest")] public int HP = 0;
		[FoldoutGroup("CalculationTest")] public int SP = 0;
		[FoldoutGroup("Enemy")] public int Enemy_XPReward = 16;

		[Button("Calculate Stat")]
		public void CalculateStat()
        {
			HP = battleStat.MaxHitpoint;
			SP = battleStat.MaxSP;
        }
	}
}