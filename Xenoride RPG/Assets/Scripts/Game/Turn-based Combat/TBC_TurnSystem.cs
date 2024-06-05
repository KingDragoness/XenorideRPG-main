using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Xenoride.TBC
{
	public class TBC_TurnSystem : MonoBehaviour
	{

        public enum TurnState
        {
            DialoguePause, //wait dialogue text, usually camera cutscene
            WaitInput, //wait player's input
            PlayAnim, //plays attack animation
            Gameover, //game over
            VictoryScreen //victory!
        }


        [FoldoutGroup("Scene")] [SerializeField] private List<TBC.TurnOrder> allCurrentTurnOrders = new List<TBC.TurnOrder>();
        [FoldoutGroup("Scene")] [SerializeField] private List<TBC_Party> allPartyMembers = new List<TBC_Party>();
        //private List<Dialogue> insert dialogues here
        [FoldoutGroup("Scene")] [SerializeField] private TurnState turnState;
        public float TimeTriggerGameover = 4f;
        public GameEvent event_OnTurnEnd;

        [FoldoutGroup("Victory")] public int TotalXPGain = 0;

        [Space]
        [SerializeField] private bool isDebugScreen = false;
        private float _timerGameover = 4f;


        [FoldoutGroup("DEBUG")]
        [Button("DEBUG_InitializeRound")]
        public void Debug_initializeRound()
        {
            allCurrentTurnOrders.Clear();

            var allParty = TurnBasedCombat.Instance.sceneSpace.GetComponentsInChildren<TBC_Party>().ToList();
            allPartyMembers = allParty;

            foreach (var party in allParty)
            {
                allCurrentTurnOrders.Add(new TBC.TurnOrder(party));
            }
        }

        public TBC.TurnOrder CurrentTurn
        {
            get
            {
                return allCurrentTurnOrders[0];
            }
        }


        public List<TBC_Party> GetAllUnits()
        {
            return allPartyMembers;
        }


        public List<TBC_Party> GetAllPartyMembers()
        {
            return allPartyMembers.FindAll(x => x.IsPartyMember);
        }

        public List<TBC_Party> GetAllEnemies()
        {
            return allPartyMembers.FindAll(x => x.IsPartyMember == false);

        }

        public List<TBC_Party> GetTargetables(List<TargetTags> targetTags)
        {
            var allParties = new List<TBC_Party>();
            allParties.AddRange(GetAllUnits());

            if (targetTags.Contains(TargetTags.Enemy))
            {
                allParties.RemoveAll(x => x.IsPartyMember);
            }
            else if (targetTags.Contains(TargetTags.Party))
            {
                allParties.RemoveAll(x => x.IsPartyMember == false);
            }

            if (targetTags.Contains(TargetTags.Revive))
            {
                allParties.RemoveAll(x => x.DeadOrFallen == false);
            }
            else
            {
                allParties.RemoveAll(x => x.DeadOrFallen);
            }


            return allParties;
        }

        public void ChangeCurrentState(TurnState _state1)
        {
            turnState = _state1;
        }

        public TurnState CurrentState { get => turnState; }
        public List<TBC.TurnOrder> AllCurrentTurnOrders { get => allCurrentTurnOrders; }

        private void Update()
        {

            if (TurnBasedCombat.Turn.CurrentState == TurnState.Gameover)
            {
                _timerGameover -= Time.deltaTime;
                TurnBasedCombat.Instance.IsGameOver = true;

                if (_timerGameover <= 0f)
                {
                    Time.timeScale = 0f;
                }
            }
            else if (TurnBasedCombat.Turn.CurrentState == TurnState.VictoryScreen)
            {
                TurnBasedCombat.Instance.IsVictory = true;
            }

            var currentParty = TurnBasedCombat.Turn.CurrentTurn.party;

            if (TurnBasedCombat.Turn.CurrentState == TurnState.WaitInput)
            {
                currentParty.isDecisionMaking = true;
            }
            else
            {
                currentParty.isDecisionMaking = false;
            }


            if (TurnBasedCombat.Turn.CurrentState == TurnState.PlayAnim)
            {
                if (CurrentTurn.party.CurrentRunningOrder() == null)
                {
                    TurnBasedCombat.Turn.EndTurn();
                }
            }

            allCurrentTurnOrders.RemoveAll(x => x.party.DeadOrFallen);

            if (IsAllPartyDead())
            {
                ChangeCurrentState(TurnState.Gameover);
            }
            else if (IsAllEnemyDead())
            {
                ChangeCurrentState(TurnState.VictoryScreen);
            }
            else if (CurrentTurn.party.CurrentRunningOrder() != null)
            {
                ChangeCurrentState(TurnState.PlayAnim);
            }
            else if (false) //If dialogue ctuscene queued
            {
                //turnstate.dialogue/cutscene
            }
            else
            {
                ChangeCurrentState(TurnState.WaitInput);
            }

  
        }

        public bool IsAllPartyDead()
        {
            var partyMembers = GetAllPartyMembers();
            bool isAllDead = true;

            foreach(var party in partyMembers)
            {
                if (party.DeadOrFallen == false)
                    return false;
            }

            return isAllDead;
        }

        public bool IsAllEnemyDead()
        {
            var enemies = GetAllEnemies();
            bool isAllDead = true;

            foreach (var party in enemies)
            {
                if (party.DeadOrFallen == false)
                    return false;
            }

            return isAllDead;
        }

        public void EndTurn()
        {
            allCurrentTurnOrders.Add(GenerateTurnOrder(allCurrentTurnOrders[0].party));
            allCurrentTurnOrders.RemoveAt(0);
            event_OnTurnEnd.Raise();
        }

        public void QueueTurnOrder(TBC_Party party)
        {
            if (allCurrentTurnOrders.Find(x => x.party == party) != null) return; //already exists!

            allCurrentTurnOrders.Add(GenerateTurnOrder(party));

        }

        private TBC.TurnOrder GenerateTurnOrder(TBC_Party party)
        {
            TBC.TurnOrder turn = new TBC.TurnOrder(party);
            return turn;
        }


        private void OnGUI()
        {
            if (isDebugScreen == false) return;

            GUI.color = Color.black;
            GUI.Label(new Rect(10, 10, 200, 20), $"Current turn: {CurrentTurn.party.partyMemberSO.NameDisplay}");
        }

    }
}