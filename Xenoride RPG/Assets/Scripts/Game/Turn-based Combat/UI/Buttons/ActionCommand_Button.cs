using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Xenoride.TBC
{

	//for generic buttons (items, special attacks)

	public class ActionCommand_Button : MonoBehaviour
	{

		public Button button;
		public Text label_Left;
		public Text label_Right;
		public Image icon_Colored;
		public Image icon_BlackandWhite;


		public TBC.ChildUI_Type buttonType;

		private TBC_Party GetCurrentParty
		{
			get { return TurnBasedCombat.Turn.CurrentTurn.party; }
		}

        public ItemSO ItemSO { get => _itemSO; set => _itemSO = value; }

        private ItemSO _itemSO;

		public void Execute()
        {
			if (buttonType == TBC.ChildUI_Type.Item)
            {
				//override Action_UseItem
				var itemCommand = GetCurrentParty.UseItemCommand;
				itemCommand.targetItem = ItemSO;
				itemCommand.targetTags = ItemSO.targetTags;
				TurnBasedCombat.UI.OpenTargetSelection(GetCurrentParty.UseItemCommand);


			}
			else if (buttonType ==  TBC.ChildUI_Type .SpecialAttack)
            {
				//select action

			}
		}

		public bool CheckAnyTargetAvailable(List<TargetTags> tags)
        {
			var allTargets = TurnBasedCombat.Turn.GetTargetables(tags);
			allTargets.RemoveAll(x => x == null);

			if (allTargets.Count == 0)
            {
				return false;
            }

			return true;
		}

	}
}