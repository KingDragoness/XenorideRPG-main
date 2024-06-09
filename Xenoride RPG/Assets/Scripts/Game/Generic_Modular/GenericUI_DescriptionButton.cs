using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Xenoride
{
	public class GenericUI_DescriptionButton : MonoBehaviour
	{

        public GenericUI_Description localScript;
		[TextArea(2,4)]
		public string descriptionTooltipText = "";

        private void OnDisable()
        {
            HideTooltip();
        }

        public void ShowTooltip()
        {
            if (localScript == null)
            {
                GenericUI_Description.Instance.ShowDescription(this);
            }
            else
            {
                localScript.ShowDescription(this);
            }
        }

		public void HideTooltip()
        {
            if (localScript == null)
            {
                GenericUI_Description.Instance.HideDescription(this);
            }
            else
            {
                localScript.HideDescription(this);
            }
        }

	}
}