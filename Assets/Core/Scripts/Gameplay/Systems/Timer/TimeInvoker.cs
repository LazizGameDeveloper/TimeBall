using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeInvoker : MonoBehaviour
{
    public static TimeInvoker Instance {
        get {
            if (_instance == null) {
                
            }
            return _instance;
        }
    }

    public static TimeInvoker _instance;

    // Update is called once per frame
    void Update()
    {
        
    }
}
