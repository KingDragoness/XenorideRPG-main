using System.Collections;
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
        public GameObject ui_targeting;

        [FoldoutGroup("Targeting")] [SerializeField] private TBC_Action hoveringCommandOrder; //current selected command order
        [FoldoutGroup("Targeting")] [SerializeField] private List<TBC_Party> targetingPartyMembers;

        private int currentIndex = 0;


        public bool IsTargetingMode
        {
            get { return hoveringCommandOrder != null ? true : false; }
        }

        private void Update()
        {


            if (TurnBasedCombat.Turn.CurrentState == TBC_TurnSystem.TurnState.WaitInput)
            {
                ui_Parent.gameObject.EnableGameobject(true);

                //SELECTING WHAT COMMAND TO ISSUE
                if (IsTargetingMode == false)
                {
                    var currentTurn = TurnBasedCombat.Turn.CurrentTurn;

                    if (currentTurn.party.IsPartyMember)
                    {
                        TurnBasedCombat.Camera.LookAtParty(currentTurn.party);
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
            List<TBC_Party> allTargetables = new List<TBC_Party>();

            if (tags.Contains(TargetTags.Enemy))
            {
                allTargetables = TurnBasedCombat.Turn.GetAllEnemies();
                //list is modifyable
            }
            else if (tags.Contains(TargetTags.Party))
            {
                allTargetables = TurnBasedCombat.Turn.GetAllPartyMembers();

            }

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

            EventSystem.current.SetSelectedGameObject(null);

            var currentTargetParty = allTargetables[currentIndex];

            if (currentTargetParty.IsPartyMember) 
                TurnBasedCombat.Camera.LookAtParty(currentTargetParty);
            else 
                TurnBasedCombat.Camera.LookAtEnemy(currentTargetParty);
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

    }

}