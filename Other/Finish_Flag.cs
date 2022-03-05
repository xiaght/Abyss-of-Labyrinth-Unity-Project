using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish_Flag : MonoBehaviour
{
    public GameManager gm;
    public MapGenerator map;
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Player")
        {

            map.StartRandomMap();
            gm.stage++;
        }

    }
}
