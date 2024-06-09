using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Xenoride
{
	public class GenericUI_Description : MonoBehaviour
	{

		public Text label_BodyText;
		public GameObject parent;

        public static GenericUI_Description Instance; //only exist per scene

        private GenericUI_DescriptionButton currentButton;

        private void Awake()
        {
            Instance = this;
        }



        public void ShowDescription(GenericUI_DescriptionButton button)
        {
            gameObject.EnableGameobject(true);
            currentButton = button;
            label_BodyText.text = $"{button.descriptionTooltipText}";
        }

		public void HideDescription(GenericUI_DescriptionButton button = null)
        {
            if (currentButton != null)
            {
                if (button == currentButton)
                {
                    gameObject.EnableGameobject(false);
                }
            }
            else
            {
                gameObject.EnableGameobject(false);
            }

        }

    }
}