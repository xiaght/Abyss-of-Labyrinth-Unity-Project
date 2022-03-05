using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargertingEmemy : MonoBehaviour
{
    public Enemy enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.isFire = true;
            enemy.Fire();

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.isFire = true;

        }

    }

}
