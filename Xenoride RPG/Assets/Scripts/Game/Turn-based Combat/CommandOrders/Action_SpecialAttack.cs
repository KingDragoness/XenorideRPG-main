using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Xenoride.TBC
{

    public class Action_SpecialAttack : TBC_Action
    {

        public SpecialAttackSO attachedSO;
        [InfoBox("Action_SpecialAttack is generic. Must Execute(); and EndOrder(); to fully execute.")]
        public List<EffectUnit> allEffectUnits = new List<EffectUnit>();
        public bool allowEndOrderUponExecution = false;

        public override void Execute()
        {

            if (targetedUnit != null)
            {

                foreach (var effectUnit in allEffectUnits)
                {
                    TBC.EffectToken effect = new TBC.EffectToken();
                    effect.Value = effectUnit.Value;
                    effect.origin = partyScript;
                    effect.effectType = effectUnit.EffectType;
                    targetedUnit.ReceivedEffect(effect);
                }

            }

            partyScript.partyStat.currentSP -= attachedSO.SPCost;
            targetedUnit = null;
            if (allowEndOrderUponExecution)
            {
                EndOrder();
            }
        }

        public void EndOrder()
        {
            partyScript.CompleteOrder();
        }

    }
}