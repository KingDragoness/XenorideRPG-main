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

        public override void Execute()
        {
            RefreshWeapon();

            //Get current party member's hold weapon
            //calculate damage output
            Debug.Log("Executed attack animation.");
            float variableDmg = Mathf.Round(Damage/9f) + 1f;

            if (targetedUnit != null)
            {
                TBC.EffectToken effect = new TBC.EffectToken();
                effect.Value = Damage + Random.Range(-variableDmg, variableDmg);
                effect.origin = partyScript;
                effect.effectType = EffectType.DamageDeal;
                targetedUnit.ReceivedEffect(effect);
            }

            partyScript.CompleteOrder();
            targetedUnit = null;

        }

        private void RefreshWeapon()
        {
            var weaponItem = partyScript.GetWeaponItem();
            if (weaponItem == null) 
            {
                //MELEE
                Damage = 4 + Mathf.Round(partyScript.partyStat.battleStat.STR/1.9f);
                IsRanged = false;
                return;
            }

            Damage = weaponItem.baseDamage;

            if (weaponItem.category == Item.WeaponCategory.Gun)
            {
                IsRanged = true;
            }
            else
            {
                IsRanged = false;
            }
        }


    }
}