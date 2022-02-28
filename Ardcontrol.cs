using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ardcontrol : MonoBehaviour
{
    public float trig_ac = 0;

    void Update()
    {
        if (GetComponent<csv>().saves && trig_ac == 1)
        {
           
                GetComponent<Serial>().Write("1");
                Debug.Log("1");
                trig_ac = 0;
                GetComponent<Serial>().Write("0");
        }

        //else if ()
        //    {
        //        GetComponent<Serial>().Write("0");
        //        Debug.Log("0");
        //    }
        
    }
}

