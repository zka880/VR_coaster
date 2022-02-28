using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scenestop : MonoBehaviour
{
    public bool paused = true;

    private void Update()
    {
        /*  if (Input.GetKeyUp(KeyCode.P))
              paused = !paused; 
              */

        if (paused)
            Time.timeScale = 0;
        else
        {
            if (Time.timeScale == 0)
                GetComponent<Serial>().Write("1");
            Time.timeScale = 1;
        }


    }

   public void bottonclick()
    {
            paused = !paused;
    }
}
