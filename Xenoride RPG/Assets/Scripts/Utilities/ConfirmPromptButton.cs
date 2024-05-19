using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace ImorUtility
{
    public class ConfirmPromptButton : MonoBehaviour
    {

        public WindowConfirmPrompt confirmUI;
        public Text label;
        public Button button;
        public int index = 0;

        public void Execute()
        {
            confirmUI.ExecuteCommand(this);
        }

    }
}