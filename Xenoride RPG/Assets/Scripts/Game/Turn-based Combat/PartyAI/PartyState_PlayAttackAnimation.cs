using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Xenoride.TBC
{
    public class PartyState_PlayAttackAnimation : TBC_PartyState
    {


        public bool attemptAttacking = false;

        public override void OnEnterState()
        {

        }

        public override void OnExitState()
        {

        }

        public override void OnState()
        {
            //partyScript.PlayAnimationMob();
            var commandOrder = partyScript.CurrentRunningOrder().commandOrder;
            var targetToAttack = partyScript.CurrentRunningOrder().target;
            commandOrder.targetedUnit = targetToAttack;
            partyScript.agent.enabled = true;

            if (commandOrder.IsRanged)
            {
                partyScript.PlayAnimationMob(commandOrder.clip_Attack);
                attemptAttacking = true;
            }
            else
            {
                float dist = Vector3.Distance(transform.position, targetToAttack.transform.position);

                if (dist > partyScript.meleeDistance)
                {
                    partyScript.agent.isStopped = false;
                    partyScript.agent.SetDestination(targetToAttack.transform.position);
                    partyScript.PlayAnimationMob(partyScript.animationClips[2]);
                    attemptAttacking = false;
                }
                else
                {
                    //Debug.Log("attacking");
                    partyScript.agent.isStopped = true;
                    attemptAttacking = true;
                    partyScript.PlayAnimationMob(commandOrder.clip_Attack);
                }
            }

        }
    }
}