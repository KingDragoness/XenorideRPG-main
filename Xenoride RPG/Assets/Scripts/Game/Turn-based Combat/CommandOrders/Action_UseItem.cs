using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Xenoride.TBC
{
    public class Action_UseItem : TBC_Action
    {

        public ItemSO targetItem;
        //Do not use 'partyScript' for UseItem

        public override void Execute()
        {
            Debug.Log("Executed item use animation.");

            if (targetedUnit != null)
            {

                foreach(var effectUnit in targetItem.allEffectUnits)
                {
                    TBC.EffectToken effect = new TBC.EffectToken();
                    effect.Value = effectUnit.Value;
                    effect.origin = partyScript;
                    effect.effectType = effectUnit.EffectType;
                    targetedUnit.ReceivedEffect(effect);
                }
      
            }

            TurnBasedCombat.Inventory.PartyInventory.RemoveItem(targetItem.ID, 1);
            partyScript.CompleteOrder();
            targetedUnit = null;
        }


    }
}