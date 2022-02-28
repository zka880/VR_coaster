using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Tracktrigger : MonoBehaviour
{
    public GameObject player_csv;

    void OnTriggerEnter(Collider e)
    {
        if (e.CompareTag("Player"))
        {
            if (transform.parent.CompareTag("Track"))//父类
            {
                player_csv.GetComponent<csv>().count_trigger++;
                player_csv.GetComponent<Ardcontrol>().trig_ac = 1;
            }
        }
    }
}
