using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Xenoride.TBC
{
	public class PartyState_Idle : TBC_PartyState
	{

        public float rotatingSpeed = 90f;

        public override void OnEnterState()
        {

        }

        public override void OnExitState()
        {

        }

        public override void OnState()
        {
            float dist = Vector3.Distance(transform.position, partyScript.originalPost.position);

            if (partyScript.DeadOrFallen == true)
            {
                partyScript.agent.enabled = false;
                partyScript.PlayAnimationMob(partyScript.animationClips[4]);
            }
            else if (dist > 1f)
            {
                partyScript.agent.enabled = true;
                partyScript.agent.isStopped = false;
                partyScript.agent.SetDestination(partyScript.originalPost.position);
                partyScript.PlayAnimationMob(partyScript.animationClips[2]);
            }
            else
            {
                partyScript.agent.enabled = false;
                partyScript.PlayAnimationMob(partyScript.animationClips[0]);

                Vector3 target = transform.position + (Vector3.forward * 5f);

                if (partyScript.IsPartyMember == false)
                {
                    target = transform.position + (Vector3.back * 5f);
                }
   

                Vector3 relativePos = target - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                partyScript.transform.rotation = Quaternion.Lerp(partyScript.transform.rotation, rotation, Time.deltaTime * rotatingSpeed);
            }


        }

    }
}