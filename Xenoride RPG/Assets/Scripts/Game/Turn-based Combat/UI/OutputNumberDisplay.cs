using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using ToolBox.Pools;


//Modular
namespace Xenoride
{
    public class OutputNumberDisplay : MonoBehaviour
    {

        public OutputNumberDisplay_Label label_static;
        public OutputNumberDisplay_Label prefab_label;
        public RectTransform CanvasRect;
        public Vector2 randomized_Offset = new Vector2(20, 15);
        public Vector2 offset = new Vector2(0, 20);


        private void Awake()
        {
            label_static.gameObject.SetActive(false);
            prefab_label.gameObject.SetActive(false);
            prefab_label.gameObject.Populate(10);
        }


        public void ShowingText_1(string text, Color color)
        {
            label_static.gameObject.SetActive(true);
            label_static.RectTransform.anchoredPosition = GetPositionUI(label_static.RectTransform);
            label_static.label.text = text;
            label_static.label.color = color;
        }

        public void DisableText_1()
        {
            label_static.gameObject.SetActive(false);
        }



        /// <summary>
        /// Slowly disappears for 1s (going up).
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public void OneTimeDisplayText_1(string text, Color color, Vector3 labelPositionOverride = new Vector3())
        {
            var newLabel = prefab_label.gameObject.Reuse<OutputNumberDisplay_Label>(transform);
            //newLabel.cg.alpha = 1f;
            newLabel.label.text = text;
            //newLabel.label.color = color;

            if (labelPositionOverride == Vector3.zero)
            {
                Vector2 WorldObject_ScreenPosition = GetPositionUI(newLabel.RectTransform);
                WorldObject_ScreenPosition += new Vector2(Random.Range(-randomized_Offset.x, randomized_Offset.x), Random.Range(-randomized_Offset.y, randomized_Offset.y));

                 newLabel.RectTransform.anchoredPosition = WorldObject_ScreenPosition;
            }
            else
            {
                Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(labelPositionOverride);
                Vector2 WorldObject_ScreenPosition = new Vector2(
                ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
                ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));
                WorldObject_ScreenPosition += new Vector2(Random.Range(-randomized_Offset.x, randomized_Offset.x), Random.Range(-randomized_Offset.y, randomized_Offset.y));

                newLabel.RectTransform.anchoredPosition = WorldObject_ScreenPosition;
            }
            //TimedFunction.Start(newLabel.DisableDisplay, 1f);
        }

        public Vector2 GetPositionUI(RectTransform rectTransform)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector2 posTarget = new Vector2(mousePos.x, mousePos.y) + offset;
            posTarget.x = Mathf.Floor(posTarget.x);
            posTarget.y = Mathf.Floor(posTarget.y - rectTransform.sizeDelta.y);

            if (posTarget.x < 0f) posTarget.x = 0f;
            if (posTarget.y < 0f) posTarget.y = 0f;
            if (posTarget.x > Screen.width - rectTransform.sizeDelta.x) posTarget.x = Screen.width - rectTransform.sizeDelta.x;
            if (posTarget.y > Screen.height - rectTransform.sizeDelta.y) posTarget.y = Screen.height - rectTransform.sizeDelta.y;

            return posTarget;
        }

    }

}