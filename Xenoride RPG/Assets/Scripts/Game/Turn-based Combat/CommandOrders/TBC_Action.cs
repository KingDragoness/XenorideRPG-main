using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Animancer;

namespace Xenoride.TBC
{
	public abstract class TBC_Action : MonoBehaviour
	{

		[FoldoutGroup("AI")] public int weight = 10;
		[FoldoutGroup("AI")] public int HP_Threshold = 200; //set to zero if available at anytime.

		public bool IsRanged = true; //ignores running animation
		[Range(1,5)] public int TargetCount = 1;
		public List<TargetTags> targetTags = new List<TargetTags>();

		public ClipTransition clip_Attack;
		[HideIf("IsRanged")] public ClipTransition clip_Running;

		internal TBC_Party partyScript;

		public abstract void Execute();



		public virtual void Awake()
        {
			partyScript = GetComponentInParent<TBC_Party>();
        }

		public bool IsTagChecked(TargetTags tag)
        {
			return targetTags.FindIndex(x => x == tag) == -1 ? false : true;
        }

	}
}