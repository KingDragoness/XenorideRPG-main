using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Animancer;
using Newtonsoft.Json;

namespace Xenoride.TBC
{

	//Clip indexes:
	//0 idle
	//1 get hit
	//2 running/moving
	//3 tired/low HP
	//4 dead

	public class TBC_Party : MonoBehaviour
	{

		public PartyMemberSO partyMemberSO;
		public SaveData.PartyStat partyStat;
		public List<TBC_Party> targetCommand;
		public List<TBC_Action> allAvailableCommands = new List<TBC_Action>();

		public List<TBC.OrderToken> currentQueuedOrders = new List<TBC.OrderToken>();
		[FoldoutGroup("Animations")] public AnimancerPlayer animPlayer;
		[FoldoutGroup("Animations")] public List<ClipTransition> animationClips = new List<ClipTransition>();
		[FoldoutGroup("States")] public List<TBC_PartyState> allPartyStates = new List<TBC_PartyState>();
		[FoldoutGroup("States")] public TBC_PartyState currentState;

		public bool IsPartyMember
		{
			get
			{
				return partyMemberSO.alliance == Party.Alliance.Party ? true : false;
			}
		}

		public static List<TBC_Party> GetAllPartyMembers()
		{
			return TurnBasedCombat.Instance.sceneSpace.GetComponentsInChildren<TBC_Party>().ToList();
		}


		[FoldoutGroup("DEBUG")] [Button("Copy Party Stat")]
		public void CopyPartyStat()
        {
			partyStat.battleStat = partyMemberSO.battleStat;
        }



		private void Update()
        {

			if (currentState != null)
            {	
				currentState.OnState();
            }

		}
		public void ReceivedEffect(TBC.EffectToken effectToken)
		{

		}

		public void PlayAnimationMob(ClipTransition clipTransition, float time = 0.25f)
		{
			if (animPlayer == null) return;
			if (!animPlayer.IsPlayingClip(clipTransition.Clip)) animPlayer.PlayAnimation(clipTransition, time);

		}



		#region Orders & Turns

		[FoldoutGroup("DEBUG")]
		[Button("Execute order")]
		public void DEBUG_ExecuteOrder()
		{
			var currentOrder = CurrentRunningOrder();
			if (currentOrder == null) return;
			currentOrder.commandOrder.Execute();
		}

		public TBC.OrderToken CurrentRunningOrder()
		{
			if (currentQueuedOrders.Count > 0)
			{
				var order = currentQueuedOrders[0];
				if (order != null)
				{
					return order;
				}
			}

			return null;
		}

		public TBC_Action GenericAttackCommand
		{
			get { return allAvailableCommands.Find(x => x is Action_Attack); }
		}

		public void IssueOrder(TBC.OrderToken command)
		{
			var commandOrders = new List<TBC.OrderToken>();
			commandOrders.Add(command);
			IssueOrder(commandOrders);
		}
		public void IssueOrder(List<TBC.OrderToken> commands)
		{
			currentQueuedOrders = commands;
		}


		#endregion


	}
}