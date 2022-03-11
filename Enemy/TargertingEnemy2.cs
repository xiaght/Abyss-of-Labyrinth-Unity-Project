using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargertingEnemy2 : MonoBehaviour
{
    public Enemy2 enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.isTargert = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.isTargert = true;

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.isTargert = false;

        }
    }
}
