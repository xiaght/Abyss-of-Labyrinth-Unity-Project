using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skill1 : MonoBehaviour
{

    public float damage = 0;
    public Player player;
  

    public float damageTime; // �������� �� ������ (�� �����Ӹ��ٰ� �ƴ� ���� �ð����� �������� �ֱ� ���Ͽ�)
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
            currentDamageTime -= Time.deltaTime;  // 1�ʿ� 1��
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
