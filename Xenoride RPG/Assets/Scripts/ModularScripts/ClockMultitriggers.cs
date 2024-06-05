using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ClockMultitriggers : MonoBehaviour
{
    
    [System.Serializable]
    public class ClockTriggerBlock
    {
        public float Timer = 5f;
        public UnityEvent OnTimerOff;

        private float _timer = 0f;
        private bool _hasTriggered = false;

        public void Update()
        {

            if (_timer >= Timer)
            {
                if (_hasTriggered == false)
                {
                    OnTimerOff?.Invoke();
                    _hasTriggered = true;
                }
            }
            else
            {
                _timer += Time.deltaTime;
            }
        }

        public void ResetClock()
        {
            _timer = 0f;
            _hasTriggered = false;
        }
    }

    public List<ClockTriggerBlock> allTriggerBlocks = new List<ClockTriggerBlock>();
    public UnityEvent OnDisableEvent;

    private void Update()
    {
        foreach(var block in allTriggerBlocks)
        {
            block.Update();
        }

    }

    private void OnDisable()
    {
        foreach (var block in allTriggerBlocks)
        {
            block.ResetClock();
        }

        OnDisableEvent?.Invoke();
    }


}
