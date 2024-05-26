using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using ToolBox.Pools;

namespace Xenoride.TBC
{
	public class TBC_UI_TurnOrder : MonoBehaviour
	{

		public TBC_UIButton_TurnOrder prefab;
        public Transform parentButton;
		public List<TBC_UIButton_TurnOrder> allTurnOrderButtons = new List<TBC_UIButton_TurnOrder>();
        public int maxTurnsShown = 8;

        private void Awake()
        {
            prefab.gameObject.Populate(8);
        }

        private void Update()
        {
            allTurnOrderButtons.ReleasePoolObject();

            int index = 0;

            foreach (var turn in TurnBasedCombat.Turn.AllCurrentTurnOrders)
            {
                if (index >= maxTurnsShown) break;
                var button = prefab.gameObject.Reuse<TBC_UIButton_TurnOrder>(parentButton); //Instantiate(prefab, parentButton);
                button.gameObject.SetActive(true);
                button.imagePortrait.sprite = turn.party.partyMemberSO.sprite_wide_201px;
                index++;
                allTurnOrderButtons.Add(button);
            }

        }

    }
}