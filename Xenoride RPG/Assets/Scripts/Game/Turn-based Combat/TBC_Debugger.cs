using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Xenoride.TBC
{
	public class TBC_Debugger : MonoBehaviour
	{

		public bool isDebuggingScreen = false;

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.F3))
            {
                isDebuggingScreen = !isDebuggingScreen;
            }
        }

        private void OnGUI()
        {
            if (isDebuggingScreen == false) return;

            GUI.color = Color.black;

            foreach(var party in TurnBasedCombat.Turn.GetAllUnits())
            {
                string contentString = "";
                Vector2 ScreenPos = Camera.main.WorldToScreenPoint(party.transform.position);

                contentString += party.GetReport();

                GUI.Label(new Rect(ScreenPos.x, Screen.height - ScreenPos.y, 300, 900), contentString);

            }
        }

    }
}