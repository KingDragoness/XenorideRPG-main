﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Cinemachine;

namespace Xenoride.TBC
{


	public class TBC_CameraController : MonoBehaviour
	{

		public enum CameraState
        {
			LookAtParty,
			PlayAnim,
			Victory
        }

		public CameraState currentCamState;
		[FoldoutGroup("Play Anim")] public GameObject playAnimationCamera;
		[FoldoutGroup("Victory/Defeat")] public GameObject victoryCamera;
		[FoldoutGroup("Selector Camera")] public GameObject partySelector_Party;
		[FoldoutGroup("Selector Camera")] public CinemachineVirtualCamera vc_Party;
		[FoldoutGroup("Selector Camera")] public CinemachineTargetGroup targetGroup_LookAt_Party;
		[FoldoutGroup("Selector Camera")] public float radiusTarget = 5f;
		[FoldoutGroup("Selector Camera")] public float followOffsetZ = -10f;
		[FoldoutGroup("Selector Camera")] public float followOffsetY = -10f;
		[FoldoutGroup("Selector Camera")] public float perTargetDistanceZ = -2f;
		[FoldoutGroup("Selector Camera")] public float perTargetDistanceY = -1f;
		[FoldoutGroup("DEBUG")] public List<TBC_Party> DEBUG_PartyMembers = new List<TBC_Party>();

		private TBC_Party[] cachedPartyMembers;

        private void Awake()
        {
			cachedPartyMembers = new TBC_Party[8];
        }

        private void Update()
        {
			if (currentCamState == CameraState.LookAtParty)
            {
				partySelector_Party.gameObject.EnableGameobject(true);
				LookAtParty(TurnBasedCombat.Turn.GetAllUnits().ToArray());
			}

			if (currentCamState == CameraState.PlayAnim)
            {
				partySelector_Party.gameObject.EnableGameobject(false);
				playAnimationCamera.gameObject.EnableGameobject(true);
				HandlePlayAnimationCam();
			}
			else
            {
				playAnimationCamera.gameObject.EnableGameobject(false);
			}

			if (currentCamState == CameraState.Victory)
            {
				victoryCamera.gameObject.EnableGameobject(true);
				partySelector_Party.gameObject.EnableGameobject(false);
				playAnimationCamera.gameObject.EnableGameobject(false);
				SetTargetParty(TurnBasedCombat.Turn.GetAllPartyMembers().ToArray());

			}
		}


		private void HandlePlayAnimationCam()
        {
			var currentParty = TurnBasedCombat.Turn.CurrentTurn.party;
			//vc_AttackAnim.LookAt = currentParty.transform;
			//vc_AttackAnim.Follow = currentParty.transform;

		}

		[FoldoutGroup("DEBUG")]
		[Button("Camera - Look at party")]
		public void DEBUG_PartyLook()
        {
			LookAtParty(DEBUG_PartyMembers.ToArray());
        }

		private void WipeList()
        {
			int i = 0;
			foreach(var c1 in cachedPartyMembers)
            {
				cachedPartyMembers[i] = null;
				i++;
            }

		}

		public void LookAtParty(TBC_Party partyUnit)
        {
			WipeList();
			cachedPartyMembers[0] = partyUnit;
			LookAtParty(cachedPartyMembers);
		}

		public void LookAtParty(TBC_Party[] allParties)
        {
			targetGroup_LookAt_Party.m_Targets = new CinemachineTargetGroup.Target[allParties.Length];

			int index = 0;
			foreach (var partyMember in allParties)
            {
				if (partyMember == null) continue;
				CinemachineTargetGroup.Target t = new CinemachineTargetGroup.Target();
				t.target = partyMember.transform;
				if (partyMember.IsPartyMember == false) t.weight = 1f; else t.weight = 0.8f;
				t.radius = radiusTarget;
				targetGroup_LookAt_Party.m_Targets[index] = t;
				index++;

			}
			currentCamState = CameraState.LookAtParty;
			var cot = vc_Party.GetCinemachineComponent<CinemachineOrbitalTransposer>();
			cot.m_FollowOffset = new Vector3(cot.m_FollowOffset.x, followOffsetY + (perTargetDistanceY * index), followOffsetZ + (perTargetDistanceZ * index));
		}

		private void SetTargetParty(TBC_Party[] allParties)
        {
			targetGroup_LookAt_Party.m_Targets = new CinemachineTargetGroup.Target[allParties.Length];

			int index = 0;
			foreach (var partyMember in allParties)
			{
				if (partyMember == null) continue;
				CinemachineTargetGroup.Target t = new CinemachineTargetGroup.Target();
				t.target = partyMember.transform;
				if (partyMember.IsPartyMember == false) t.weight = 1f; else t.weight = 0.8f;
				t.radius = radiusTarget;
				targetGroup_LookAt_Party.m_Targets[index] = t;
				index++;

			}
		}



	}
}