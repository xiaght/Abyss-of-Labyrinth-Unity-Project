using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShpoButton : MonoBehaviour
{

    public Player player;
    public GameManager gm;

    public enum Type
    {
        Power, Speed, Luck, BulletSpeed, Hp, MaxShotDelay, Reborn
    }
    public Type type;

    public int price;
    public float value;

    public void OnClickBuyButton()
    {
        ShpoButton shop = GetComponent<ShpoButton>();
        if (shop.price > gm.coin) {
            Debug.Log("x");
            return;
        }

        Time.timeScale = 1;
        switch (shop.type)
        {

            case ShpoButton.Type.Hp:
                Debug.Log("coin" + player.coin);
                gm.coin -= shop.price;
                player.maxhp += (int)shop.value;
                break;

            case ShpoButton.Type.Luck:
                gm.coin -= shop.price;
                player.luck += (int)shop.value;
                break;
            case ShpoButton.Type.Power:
                gm.coin -= shop.price;
                player.damage += shop.value;

                break;
            case ShpoButton.Type.MaxShotDelay:
                gm.coin -= shop.price;
                player.MaxShotDelay = player.MaxShotDelay * shop.value;
                if (player.MaxShotDelay <= 0.1f)
                {

                    player.MaxShotDelay = 0.1f;
                }
                break;
            case ShpoButton.Type.Speed:
                gm.coin -= shop.price;
                player.speed += shop.value;
                if (player.speed >= 15)
                {

                    player.speed = 15;
                }
                break;
            case ShpoButton.Type.BulletSpeed:
                gm.coin -= shop.price;
                player.bulletspeed += shop.value;
                break;

            case ShpoButton.Type.Reborn:
                gm.coin -= shop.price;
                player.GetLife();
                break;

        }
    }
}
