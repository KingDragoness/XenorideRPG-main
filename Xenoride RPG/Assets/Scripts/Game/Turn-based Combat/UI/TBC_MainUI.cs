﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;


namespace Xenoride.TBC
{

    public class TBC_MainUI : MonoBehaviour
    {



        public TBC_UI_ActionCommand ui_Parent;
        public TBC_UI_ChildCommands ui_Children;
        public TBC_UI_PartyStats ui_PartyStats;
        public GameObject gameOverScreen;
        public GameObject victoryScreen;
        public OutputNumberDisplay outputNumber;
        public GameObject ui_targeting;

        [FoldoutGroup("Targeting")] [SerializeField] private TBC_Action hoveringCommandOrder; //current selected command order
        [FoldoutGroup("Targeting")] [SerializeField] private List<TBC_Party> targetingPartyMembers;
        [FoldoutGroup("Targeting")] [SerializeField] private TBC_UI_TargetCursor cursor; //needs to be list for multiple enemies (all/parties)
        private int currentIndex = 0;


        public bool IsTargetingMode
        {
            get { return hoveringCommandOrder != null ? true : false; }
        }

        private void Update()
        {

            if (TurnBasedCombat.Instance.IsGameOver)
            {
                GameOver();
            }
            else if (TurnBasedCombat.Instance.IsVictory)
            {
                VictoryScreen();
            }
            else
            {
                GameRunning();
            }


        }

        private void GameOver()
        {
            gameOverScreen.gameObject.EnableGameobject(true);
            ui_Parent.gameObject.EnableGameobject(false);
            ui_PartyStats.gameObject.EnableGameobject(false);
            ui_Children.gameObject.EnableGameobject(false);
            ui_targeting.gameObject.EnableGameobject(false);

        }

        private void VictoryScreen()
        {
            victoryScreen.gameObject.EnableGameobject(true);
            ui_Parent.gameObject.EnableGameobject(false);
            ui_PartyStats.gameObject.EnableGameobject(false);
            ui_Children.gameObject.EnableGameobject(false);
            ui_targeting.gameObject.EnableGameobject(false);
        }

        private void GameRunning()
        {
            if (TurnBasedCombat.Turn.CurrentState == TBC_TurnSystem.TurnState.WaitInput)
            {
                if (TurnBasedCombat.Turn.CurrentTurn.party.IsPartyMember)
                {
                    ui_Parent.gameObject.EnableGameobject(true);
                }
                else
                {
                    ui_Parent.gameObject.EnableGameobject(false);
                }


                //SELECTING WHAT COMMAND TO ISSUE
                if (IsTargetingMode == false)
                {
                    var currentTurn = TurnBasedCombat.Turn.CurrentTurn;

                    if (currentTurn.party.IsPartyMember)
                    {
                        //TurnBasedCombat.Camera.LookAtParty(currentTurn.party);
                    }
                    else
                    {
                        //shouldn't be allowed.
                    }

                    if (ui_Children.gameObject.activeSelf)
                    {
                        ui_Parent.blackedOut.EnableGameobject(true);
                    }
                    else
                    {
                        ui_Parent.blackedOut.EnableGameobject(false);
                    }

                    ui_Children.blackedOut.EnableGameobject(false);
                    ui_targeting.gameObject.EnableGameobject(false);
                }
                //SELECTING TARGET
                else if (IsTargetingMode)
                {
                    SelectingTarget();
                    ui_Parent.blackedOut.EnableGameobject(true);
                    ui_Children.blackedOut.EnableGameobject(true);
                    ui_targeting.gameObject.EnableGameobject(true);
                }

                TurnBasedCombat.Camera.currentCamState = TBC_CameraController.CameraState.LookAtParty;

            }
            else if (TurnBasedCombat.Turn.CurrentState == TBC_TurnSystem.TurnState.PlayAnim)
            {
                ui_Parent.gameObject.EnableGameobject(false);
                ui_Children.gameObject.EnableGameobject(false);
                ui_targeting.gameObject.EnableGameobject(false);

                if (TurnBasedCombat.Turn.CurrentState == TBC_TurnSystem.TurnState.PlayAnim)
                {
                    TurnBasedCombat.Camera.currentCamState = TBC_CameraController.CameraState.PlayAnim;
                }

            }
        }

        private void SelectingTarget()
        {
            var tags = hoveringCommandOrder.targetTags;
            var currentParty = TurnBasedCombat.Turn.CurrentTurn.party;
            List<TBC_Party> allTargetables = TurnBasedCombat.Turn.GetTargetables(tags);
            targetingPartyMembers = new List<TBC_Party>();



            if (Input.GetMouseButtonUp(1))
            {
                //CYCLE, only one way
                currentIndex--;
            }
            else if (Input.GetKeyUp(KeyCode.Space) | Input.GetKeyUp(KeyCode.Return))
            {              
                //EXECUTE
                currentParty.IssueOrder(new TBC.OrderToken(hoveringCommandOrder, allTargetables[currentIndex]));
                hoveringCommandOrder = null;
            }
            else if (Input.GetKeyUp(KeyCode.Escape))
            {
                Cancel();
            }

            if (currentIndex >= allTargetables.Count)
            {
                currentIndex = 0;
            }
            if (currentIndex < 0)
            {
                currentIndex = allTargetables.Count - 1;
            }

            if (allTargetables[currentIndex] != null)
            {
                targetingPartyMembers.Add(allTargetables[currentIndex]);
            }

            EventSystem.current.SetSelectedGameObject(null);
            ShowCursors();

        }

        private void ShowCursors()
        {
            if (targetingPartyMembers.Count > 0)
            {
                cursor.targetedParty = targetingPartyMembers[0];
                cursor.gameObject.EnableGameobject(true);
            }
        }




        #region Targeting

        public void OpenTargetSelection(TBC_Action commandOrder)
        {
            hoveringCommandOrder = commandOrder;
            ui_targeting.gameObject.EnableGameobject(true);

        }

        public void Cancel()
        {
            hoveringCommandOrder = null;
            ui_targeting.gameObject.EnableGameobject(false);

        }

        #endregion

        #region Sub-menus (Action)

        public void OpenAC_SpecialAttacks()
        {
            var currentParty = TurnBasedCombat.Turn.CurrentTurn.party;



        }

        #endregion

    }

}