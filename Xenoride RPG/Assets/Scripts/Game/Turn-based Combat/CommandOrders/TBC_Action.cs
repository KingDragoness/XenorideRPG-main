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

		public int LevelThreshold = -1;
		public bool IsRanged = true; //ignores running animation
		public List<TargetTags> targetTags = new List<TargetTags>(); //to override tag
		public TBC_Party targetedUnit;

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

		public void BlockedAction()
        {
			TurnBasedCombat.UI.outputNumber.OneTimeDisplayText_1($"BLOCK", Color.red, transform.position);

		}

		public void CounteredAction(float damage)
        {
			TurnBasedCombat.UI.outputNumber.OneTimeDisplayText_1($"COUNTER", Color.red, transform.position);
			TBC.EffectToken effect = new TBC.EffectToken();
			effect.Value = damage;
			effect.origin = partyScript;
			effect.effectType = EffectType.DamageDeal;
			partyScript.ReceivedEffect(effect);
		}

	}
}