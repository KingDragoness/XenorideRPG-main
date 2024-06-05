using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimedFunctionScript : MonoBehaviour
{

    public float time = 1f;
    public TimedFunction.OnFunction function;
    public TimedFunction.OnFunction1 function1;
    public TimedFunction.OnFunction2<bool> function2;
    public UnityEvent OnEvent;

    private void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0f)
        {
            function?.Invoke();
            function1?.Invoke();
            function2?.Invoke();
            OnEvent?.Invoke();
            Destroy(gameObject);
        }
    }


}
