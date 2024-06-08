using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using ToolBox.Pools;

namespace Xenoride.TBC
{
	public class TBC_UIButton_TurnOrder : MonoBehaviour
	{

        public TBC_UI_TurnOrder turnOrderScript;
		public Image imagePortrait;
        public Animator animatorButton;
        public RectTransform rectTransform;
        public TBC.TurnOrder assignedTurn;
        public float TimeToHide = 0.3f;
		public bool deleteTurn = false;

		public int index = 0;
        private float _timer = 0.3f;

        private void Awake()
        {
            _timer = TimeToHide;
        }

        public void SetDeleteTurn()
        {
            deleteTurn = true;
        }

        private void Update()
        {
            if (deleteTurn == true)
            {
                _timer -= Time.deltaTime;
                animatorButton.SetBool("Hide", true);
            }
            else
            {
                animatorButton.SetBool("Hide", false);
            }

            if (_timer <= 0f)
            {
                gameObject.Release();
                turnOrderScript.allTurnOrderButtons.Remove(this);
                deleteTurn = false;
                _timer = TimeToHide;
            }
        }

    }
}