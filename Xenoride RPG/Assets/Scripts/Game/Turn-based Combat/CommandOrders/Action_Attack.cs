using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Xenoride.TBC
{

    //derivatives CO_Sword & CO_Gun is useless
	public class Action_Attack : TBC_Action
	{

        public float Damage = 8f;
        public int VariableDamage = 1;

        public override void Execute()
        {
            //Get current party member's hold weapon
            //calculate damage output
            Debug.Log("Executed attack animation.");
            if (targetedUnit != null)
            {
                TBC.EffectToken effect = new TBC.EffectToken();
                effect.Value = Damage + Random.Range(-VariableDamage, VariableDamage);
                effect.origin = partyScript;
                effect.effectType = EffectType.DamageDeal;
                targetedUnit.ReceivedEffect(effect);
            }

            partyScript.CompleteOrder();
            targetedUnit = null;

        }



    }
}