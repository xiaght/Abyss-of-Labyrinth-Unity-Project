using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BossSkill : MonoBehaviour
{
    public float damage = 0;
    public Enemy_Boss2 boss;


    public float damageTime; // �������� �� ������ (�� �����Ӹ��ٰ� �ƴ� ���� �ð����� �������� �ֱ� ���Ͽ�)
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
            currentDamageTime -= Time.deltaTime;  // 1�ʿ� 1��
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
