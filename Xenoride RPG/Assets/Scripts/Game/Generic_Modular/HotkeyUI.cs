using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Xenoride
{
	public class HotkeyUI : MonoBehaviour
	{

		public List<Button> allButtons = new List<Button>();

        private void Update()
        {
            if (GetButtonByIndex(0) != null && Input.GetKeyUp(KeyCode.Q))
            {
                var button = GetButtonByIndex(0);
                button.onClick.Invoke();
            }
            else if (GetButtonByIndex(1) != null && Input.GetKeyUp(KeyCode.W))
            {
                var button = GetButtonByIndex(1);
                button.onClick.Invoke();
            }
            else if (GetButtonByIndex(2) != null && Input.GetKeyUp(KeyCode.E))
            {
                var button = GetButtonByIndex(2);
                button.onClick.Invoke();
            }
            else if (GetButtonByIndex(3) != null && Input.GetKeyUp(KeyCode.R))
            {
                var button = GetButtonByIndex(3);
                button.onClick.Invoke();
            }
            else if (GetButtonByIndex(4) != null && Input.GetKeyUp(KeyCode.T))
            {
                var button = GetButtonByIndex(4);
                button.onClick.Invoke();
            }
        }

        public Button GetButtonByIndex(int index)
        {
			if (allButtons.Count > index)
            {
				return allButtons[index];
            }

			return null;
        }

	}
}