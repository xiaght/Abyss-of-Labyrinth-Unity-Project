using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopColl : MonoBehaviour
{

    public BoxCollider2D shopColl;
    public RectTransform ShopUi;






    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            ShopUi.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShopUi.gameObject.SetActive(false);
        }

    }









}
