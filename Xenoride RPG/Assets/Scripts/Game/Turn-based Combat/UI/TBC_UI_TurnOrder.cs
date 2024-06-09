using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using ToolBox.Pools;

namespace Xenoride.TBC
{
	public class TBC_UI_TurnOrder : MonoBehaviour
	{

		public TBC_UIButton_TurnOrder prefab;
        public Transform parentButton;
        public VerticalLayoutGroup verticalLayoutGroup;
        public float speed = 100f;
		public List<TBC_UIButton_TurnOrder> allTurnOrderButtons = new List<TBC_UIButton_TurnOrder>();
        public int maxTurnsShown = 8;


        private void Awake()
        {
            prefab.gameObject.Populate(8);
        }

        private void Update()
        {
            int index = 0;
            var rtVLG = verticalLayoutGroup.GetComponent<RectTransform>();

            //reading buttons
            foreach (var button in allTurnOrderButtons)
            {
                Vector3 targetedPosition = rtVLG.anchoredPosition;

                if (button.deleteTurn)
                {
                    targetedPosition.x += 100f;
                }

                if (index >= maxTurnsShown)
                {
                    button.gameObject.SetActive(false);
                }
                else
                {
                    button.gameObject.SetActive(true);
                }

                targetedPosition.y -= index * (button.rectTransform.sizeDelta.y);
                button.imagePortrait.sprite = button.assignedTurn.party.partyMemberSO.sprite_wide_201px;
                button.rectTransform.anchoredPosition = Vector3.MoveTowards(button.rectTransform.anchoredPosition, targetedPosition, Time.deltaTime * speed);

                index++;
            }


        }

        public void CreateButton(TBC.TurnOrder turnOrder, int index = -1)
        {
            var rtVLG = verticalLayoutGroup.GetComponent<RectTransform>();

            if (allTurnOrderButtons.Find(x => x.assignedTurn == turnOrder) != null) return; //already exist turn.

            var button = prefab.gameObject.Reuse<TBC_UIButton_TurnOrder>(parentButton);
            button.assignedTurn = turnOrder;
            button.deleteTurn = false;
            allTurnOrderButtons.Add(button);
            Vector3 targetedPosition = rtVLG.anchoredPosition;
            targetedPosition.x += 100f;
            targetedPosition.y -= index * (button.rectTransform.sizeDelta.y);

            if (index == -1)
            {
                targetedPosition.y -= allTurnOrderButtons.Count * (button.rectTransform.sizeDelta.y);
            }

            //Debug.Log("added");
            button.rectTransform.anchoredPosition = targetedPosition;
        }





    }
}