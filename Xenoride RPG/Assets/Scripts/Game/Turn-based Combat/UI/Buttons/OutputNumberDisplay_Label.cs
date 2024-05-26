using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using ToolBox.Pools;

namespace Xenoride
{
    public class OutputNumberDisplay_Label : MonoBehaviour
    {

        public Text label;
        public float moveUpSpeed = 50f;
        public float timeToDisappear = 2f;
        public bool isStaticLabel = false;

        private RectTransform _rectTransform;
        private float _timer = 0f;

        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
                return _rectTransform;
            }
        }


        private void Update()
        {
            if (isStaticLabel == true)
            {
                return;
            }

            var pos = _rectTransform.anchoredPosition;
            _timer += Time.deltaTime;
            pos.y += moveUpSpeed * Time.unscaledDeltaTime;

            if (_timer >= timeToDisappear)
            {
                DisableDisplay();
            }

            _rectTransform.anchoredPosition = pos;

        }

        public bool DisableDisplay()
        {
            gameObject.Release();
            _timer = 0f;

            return true;
        }

    }
}