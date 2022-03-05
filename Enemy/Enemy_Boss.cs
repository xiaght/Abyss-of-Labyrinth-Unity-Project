using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss : Enemy
{

    public GameObject bullet1pos;
    public GameObject bullet2pos;


   // public Player





    public void Fire()
    {
        if (isFire)
        {
            isFire = false;
            GameObject bullet = Instantiate(bulletPre, transform.position, transform.rotation, bulletparent);
            Rigidbody2D rigid1 = bullet.GetComponent<Rigidbody2D>();
            //rigid1.AddForce(new Vector3(ranx, rany, 0) * bulletspeed, ForceMode.Impulse);

            GameObject find = GameObject.FindWithTag("Player");

            //플레이어 추적
            Vector3 direction = find.transform.position - transform.position;
            //플레이어 방향으로 눈물 발사
            rigid1.AddForce(direction.normalized * bulletspeed, ForceMode2D.Impulse);
            Bullet bullet_1 = bullet.GetComponent<Bullet>();
            bullet_1.Ondamage(damage);
            bullet_1.range = this.range;


            GameObject bullet2 = Instantiate(bulletPre,bullet1pos.transform.position, bullet1pos.transform.rotation, bulletparent);
            Rigidbody2D rigid2 = bullet2.GetComponent<Rigidbody2D>();
            //rigid1.AddForce(new Vector3(ranx, rany, 0) * bulletspeed, ForceMode.Impulse);

            GameObject find2 = GameObject.FindWithTag("Player");

            //플레이어 추적
            Vector3 direction2 = find2.transform.position - bullet1pos.transform.position;
            //플레이어 방향으로 눈물 발사
            rigid2.AddForce(direction2.normalized * bulletspeed, ForceMode2D.Impulse);
            Bullet bullet_2 = bullet2.GetComponent<Bullet>();
            bullet_2.Ondamage(damage);
            bullet_2.range = this.range;

            GameObject bullet3 = Instantiate(bulletPre, bullet2pos.transform.position, bullet2pos.transform.rotation, bulletparent);
            Rigidbody2D rigid3 = bullet3.GetComponent<Rigidbody2D>();
            //rigid1.AddForce(new Vector3(ranx, rany, 0) * bulletspeed, ForceMode.Impulse);

            GameObject find3 = GameObject.FindWithTag("Player");

            //플레이어 추적
            Vector3 direction3 = find3.transform.position - bullet2pos.transform.position;
            //플레이어 방향으로 눈물 발사
            rigid3.AddForce(direction3.normalized * bulletspeed, ForceMode2D.Impulse);
            Bullet bullet_3 = bullet3.GetComponent<Bullet>();
            bullet_3.Ondamage(damage);
            bullet_3.range = this.range;

            //재귀
            int ran = Random.Range(1, 5);
            Invoke("Fire", ran);
        }
        //눈물 생성


    }



}
