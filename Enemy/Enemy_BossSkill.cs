using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BossSkill : MonoBehaviour
{
    public float damage = 0;
    public Enemy_Boss2 boss;


    public float damageTime; // 데미지가 들어갈 딜레이 (매 프레임마다가 아닌 일정 시간마다 데미지를 주기 위하여)
    public float currentDamageTime;


    public void Ondamage(float _damage)
    {

        damage = _damage;
    }



    void Update()
    {
        ElapseTime();

    }

    private void ElapseTime()
    {
        if (currentDamageTime > 0)
            currentDamageTime -= Time.deltaTime;  // 1초에 1씩
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (currentDamageTime <= 0)
            {
                Player player = collision.GetComponent<Player>();
                player.onDamage((int)damage);
                currentDamageTime = damageTime;
            }


        }
    }
}
