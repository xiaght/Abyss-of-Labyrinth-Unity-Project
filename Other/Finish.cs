using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{

    public Transform EnemyZone;

    public GameObject finish_temp;

    private void Update()
    {
        if (EnemyZone.childCount == 0)
        {
            finish_temp.gameObject.SetActive(true);
        }
        else {

            finish_temp.gameObject.SetActive(false);
        }

    }


}
