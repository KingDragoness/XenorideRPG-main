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
            PlayAnim //plays attack animation
        }


        [FoldoutGroup("Scene")] [SerializeField] private List<TBC.TurnOrder> allCurrentTurnOrders = new List<TBC.TurnOrder>();
        [FoldoutGroup("Scene")] [SerializeField] private List<TBC_Party> allPartyMembers = new List<TBC_Party>();
        //private List<Dialogue> insert dialogues here
        [FoldoutGroup("Scene")] [SerializeField] private TurnState turnState;

        [Space]
        [SerializeField] private bool isDebugScreen = false;


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

        public List<TBC_Party> GetAllPartyMembers()
        {
            return allPartyMembers.FindAll(x => x.IsPartyMember);
        }

        public List<TBC_Party> GetAllEnemies()
        {
            return allPartyMembers.FindAll(x => x.IsPartyMember == false);

        }

        public void ChangeCurrentState(TurnState _state1)
        {
            turnState = _state1;
        }

        public TurnState CurrentState { get => turnState; }

        private void Update()
        {
            if (TurnBasedCombat.Turn.CurrentState == TurnState.PlayAnim)
            {
                if (CurrentTurn.party.CurrentRunningOrder() == null)
                {
                    TurnBasedCombat.Turn.EndTurn();
                }
            }

            if (CurrentTurn.party.CurrentRunningOrder() != null)
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

        public void EndTurn()
        {
            allCurrentTurnOrders.RemoveAt(0);
        }


        private void OnGUI()
        {
            if (isDebugScreen == false) return;

            GUI.color = Color.black;
            GUI.Label(new Rect(10, 10, 200, 20), $"Current turn: {CurrentTurn.party.partyMemberSO.NameDisplay}");
        }

    }
}