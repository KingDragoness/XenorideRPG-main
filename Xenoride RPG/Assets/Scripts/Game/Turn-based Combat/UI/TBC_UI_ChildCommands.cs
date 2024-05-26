using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Xenoride.TBC
{
	public class TBC_UI_ChildCommands : MonoBehaviour
	{

		public GameObject blackedOut;
		public ActionCommand_Button buttonPrefab;
		public Transform parentButton;

		private List<ActionCommand_Button> allACButtons = new List<ActionCommand_Button>();



	}
}