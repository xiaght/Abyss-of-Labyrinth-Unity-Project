using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skill1 : MonoBehaviour
{

    public float damage = 0;
    public Player player;
  

    public float damageTime; // 데미지가 들어갈 딜레이 (매 프레임마다가 아닌 일정 시간마다 데미지를 주기 위하여)
    public float currentDamageTime;


    public void Ondamage(float _damage)
    {

        damage = _damage;
    }



    void Update()
    {
            ElapseTime();
        transform.position=player.transform.position;
        
    }

    private void ElapseTime()
    {
        if (currentDamageTime > 0)
            currentDamageTime -= Time.deltaTime;  // 1초에 1씩
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (currentDamageTime <= 0) {
                Enemy enemy = collision.GetComponent<Enemy>();
                enemy.onDamage((int)damage);
                currentDamageTime = damageTime;
            }


        }
    }
}
