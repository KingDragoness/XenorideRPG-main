using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using MackySoft.Choice;
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
		public List<TBC_Action> allAvailableCommands = new List<TBC_Action>();
		public List<TBC.OrderToken> currentQueuedOrders = new List<TBC.OrderToken>();

		public Transform originalPost;
		[Space]
		[FoldoutGroup("References")] public AnimancerPlayer animPlayer;
		[FoldoutGroup("References")] public NavMeshAgent agent;
		[FoldoutGroup("States")] public List<ClipTransition> animationClips = new List<ClipTransition>();
		[FoldoutGroup("States")] public List<TBC_PartyState> allPartyStates = new List<TBC_PartyState>();
		[FoldoutGroup("States")] public TBC_PartyState currentState;
		[FoldoutGroup("States")] public UnityEvent OnDead;
		[FoldoutGroup("Stats")] public float meleeDistance = 2f;
		[FoldoutGroup("Stats")] public float timeToDecide = 1f;
		[FoldoutGroup("Run-time")] public bool isDecisionMaking = false;

		private float _timeToAIRunning = 1f;
		private bool _deadOrFallen = false;

		public bool IsPartyMember
		{
			get
			{
				return partyMemberSO.alliance == Party.Alliance.Party ? true : false;
			}
		}
		public bool DeadOrFallen { get => _deadOrFallen; }

		public static List<TBC_Party> GetAllPartyMembers()
		{
			return TurnBasedCombat.Instance.sceneSpace.GetComponentsInChildren<TBC_Party>().ToList();
		}


		[FoldoutGroup("DEBUG")] [Button("Copy Party Stat")]
		public void CopyPartyStat()
        {
			partyStat.battleStat = partyMemberSO.battleStat;
        }

        #region States

		public T GetPartyStateByClass<T>() where T : TBC_PartyState
        {

			foreach (var state in allPartyStates)
			{
				if (state is T)
                {
					return state as T;
                }
            }

			return null;
        }

        #endregion


        private void Update()
        {
			if (_deadOrFallen)
            {
				currentQueuedOrders.Clear();
			}

			if (CurrentRunningOrder() != null)
            {
				PartyState_PlayAttackAnimation paaScript = GetPartyStateByClass<PartyState_PlayAttackAnimation>();
				currentState = paaScript;
            }
            else
            {
				PartyState_Idle idleScript = GetPartyStateByClass<PartyState_Idle>();
				currentState = idleScript;

			}

			if (currentState != null)
            {	
				currentState.OnState();
            }


			if (isDecisionMaking && IsPartyMember == false)
			{
				ProcessAIDecision();
			}
            else
            {
				_timeToAIRunning = 0f;

			}
		}

		public void ProcessAIDecision()
        {
			_timeToAIRunning += Time.deltaTime;

			if (_timeToAIRunning >= timeToDecide)
            {
				List<TargetTags> targetTags = new List<TargetTags>();
				targetTags.Add(TargetTags.Party);
				targetTags.Add(TargetTags.Single);

				var allPartyList = TurnBasedCombat.Turn.GetTargetables(targetTags);
				TBC_Action selectedAction = allAvailableCommands.ToWeightedSelector(x => x.weight).SelectItemWithUnityRandom();
				TBC_Party randomParty = allPartyList[Random.Range(0, allPartyList.Count)];

				IssueOrder(new TBC.OrderToken(selectedAction, randomParty));
				_timeToAIRunning = 0f;
			}
		}

		[FoldoutGroup("DEBUG")]
		[Button("Kill party")]
		public void DEBUG_KillParty()
		{
			TBC.EffectToken token = new TBC.EffectToken();
			token.effectType = EffectType.DamageDeal;
			token.origin = this;
			token.Value = 999;

			ReceivedEffect(token);
		}

		public void ReceivedEffect(TBC.EffectToken effectToken)
		{
			if (effectToken.effectType == EffectType.DamageDeal)
            {
				partyStat.currentHP -= Mathf.RoundToInt(effectToken.Value);
				PlayAnimationMob(animationClips[1], 0.1f);

				TurnBasedCombat.UI.outputNumber.OneTimeDisplayText_1($"{Mathf.RoundToInt(effectToken.Value)}", Color.white, transform.position);
            }

			if (partyStat.currentHP <= 0f)
            {
				_deadOrFallen = true;
				OnDead?.Invoke();
			}
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
			currentQueuedOrders.Add(command);
			IssueOrder(currentQueuedOrders);
		}
		public void IssueOrder(List<TBC.OrderToken> commands)
		{
			currentQueuedOrders = commands;
		}

		public void CompleteOrder()
        {
			if (currentQueuedOrders.Count <= 0) return;
			currentQueuedOrders.RemoveAt(0);
        }


		#endregion

		public string GetReport()
        {
			string str = "";
			str = $"{partyStat.currentHP}/{partyStat.MaxHitpoint} HP | {partyStat.currentSP}/{partyStat.MaxSP} SP";

			return str;
        }

	}
}