using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using ToolBox.Pools;

namespace Xenoride.TBC
{
	public class TBC_UI_ChildCommands : MonoBehaviour
	{

		public ActionCommand_Button buttonPrefab;
        public Transform buttonExit; //always last
        public Transform parentButton;
        public Text label_Header;
        public Image icon;

		private List<ActionCommand_Button> allACButtons = new List<ActionCommand_Button>();


        private void Awake()
        {
            buttonPrefab.gameObject.Populate(12);
            buttonPrefab.gameObject.SetActive(false);
        }


        private void TEST()
        {
            buttonPrefab.button.Select();
        }

        public void OpenedChildCommand(TBC.ChildUI_Type uiType)
        {
            foreach (var button in allACButtons)
            {
                button.button.onClick.RemoveAllListeners();
                button.ItemSO = null;
                button.SpecialAttack = null;
            }

            allACButtons.ReleasePoolObject();
            int index = 0;

            if (uiType == TBC.ChildUI_Type.Item)
            {
                label_Header.text = $"Inventory";
                icon.sprite = Engine.Assets.sprite_Inventory;
                var allItems = TurnBasedCombat.Inventory.PartyInventory.allItemDatas;

                foreach(var itemData in allItems)
                {
                    var item = Engine.Assets.GetItem(itemData.ID);
                    if (item == null) continue;
                    if (item.itemType != Item.ItemCategory.Consumable) continue;

                    var button = buttonPrefab.gameObject.Reuse<ActionCommand_Button>(parentButton);
                    button.buttonType = TBC.ChildUI_Type.Item;

                    button.gameObject.SetActive(true);
                    button.label_Left.text = $"{item.NameDisplay}";
                    button.label_Right.text = $"x{itemData.Count}";
                    button.icon_BlackandWhite.gameObject.SetActive(false);
                    button.icon_Colored.gameObject.SetActive(false);
                    button.ItemSO = item;
                    //button.icon_Colored.sprite = ...; insert sprite

                    if (button.CheckAnyTargetAvailable(item.targetTags) == false)
                    {
                        button.button.interactable = false;
                    }
                    else
                    {
                        button.button.interactable = true;
                    }

                    allACButtons.Add(button);
                    index++;
                }
            }
			else if (uiType == TBC.ChildUI_Type.SpecialAttack)
            {
                label_Header.text = $"Special Abilities";
                icon.sprite = Engine.Assets.sprite_SpecialAttack;

                var currentParty = TurnBasedCombat.Turn.CurrentTurn.party;
                var allSpecialAtks = currentParty.GetAction_SpecialAttackType();
                
                foreach(var specialAtk in allSpecialAtks)
                {
                    if (specialAtk.attachedSO == null) continue;
                    if (currentParty.partyStat.Level < specialAtk.LevelThreshold) continue;
                    if (specialAtk.attachedSO.SPCost > currentParty.partyStat.currentSP) continue;

                    var button = buttonPrefab.gameObject.Reuse<ActionCommand_Button>(parentButton);
                    button.buttonType = TBC.ChildUI_Type.SpecialAttack;

                    button.gameObject.SetActive(true);
                    button.label_Left.text = $"{specialAtk.attachedSO.NameDisplay}";
                    button.label_Right.text = $"[{specialAtk.attachedSO.SPCost} SP]";
                    button.icon_BlackandWhite.gameObject.SetActive(false);
                    button.icon_Colored.gameObject.SetActive(false);
                    button.SpecialAttack = specialAtk;

                    if (button.CheckAnyTargetAvailable(specialAtk.targetTags) == false)
                    {
                        button.button.interactable = false;
                    }
                    else
                    {
                        button.button.interactable = true;
                    }

                    allACButtons.Add(button);
                    index++;
                }

            }

            buttonExit.transform.SetAsLastSibling();
            TimedFunction.Start(buttonExit.transform.SetAsLastSibling, 0.02f);
        }

        private void Update()
        {
            
        }


    }
}