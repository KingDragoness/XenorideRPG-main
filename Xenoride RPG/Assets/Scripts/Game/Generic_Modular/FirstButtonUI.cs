using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Xenoride
{
	public class FirstButtonUI : MonoBehaviour
	{

		public Button firstButton;
		public bool saveLastSelectedButton = false;

		private GameObject _previousButtonSelected;
		private List<GameObject> allPreviousSelectedButtons = new List<GameObject>(); //this is retarded

		public int limitFrame = 10;

		public EventSystem _eventSystem
        {
            get
            {
				return FindObjectOfType<EventSystem>();
            }
        }

		private void OnEnable()
		{
			allPreviousSelectedButtons.Clear();
			for (int x = 0; x < limitFrame; x++) allPreviousSelectedButtons.Add(null);

			if (saveLastSelectedButton && _previousButtonSelected != null)
			{
				if (_previousButtonSelected.activeInHierarchy == false)
				{
					StartCoroutine(SelectContinueButtonLater(firstButton.gameObject));
				}
				else
				{
					StartCoroutine(SelectContinueButtonLater(_previousButtonSelected));
				}
			}
			else
			{
				StartCoroutine(SelectContinueButtonLater(firstButton.gameObject));
			}
		}

        private void Update()
        {
			int index = Mathf.FloorToInt((Time.time * limitFrame) % limitFrame);

			if (index < allPreviousSelectedButtons.Count)
            {
				allPreviousSelectedButtons[index] = _eventSystem.currentSelectedGameObject;
			}
        }

        private void OnDisable()
        {
            if (saveLastSelectedButton)
            {
				if (_eventSystem != null) _previousButtonSelected = _eventSystem.currentSelectedGameObject;
            }
        }

		public void SANITYCHECK()
        {

		}

		IEnumerator SelectContinueButtonLater(GameObject buttonTarget)
		{
			yield return null;
			_eventSystem.SetSelectedGameObject(null);
			_eventSystem.SetSelectedGameObject(buttonTarget);
		}
	}
}