using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{


    //스텟

    public float damageTime; // 데미지가 들어갈 딜레이 (매 프레임마다가 아닌 일정 시간마다 데미지를 주기 위하여)
    public float currentDamageTime;


    public bool isTargert;
    GameObject find;

    public CircleCollider2D coll;
    public Vector2 moveVec;
    //눈물 프리펩
 

    void Awake()
    {
        moveVec = new Vector2(ranx, rany);

        // tempColor = sr;
        // gm = GetComponent<GameManager>();
        //gm.GetComponent<GameManager>();
        curhp = maxhp;
        speed = 3;
        damage = 5;
         find = GameObject.FindWithTag("Player");
        audio.volume = 0.1f;
    }

    public void onDamage(int damage)
    {
        //체력이 없으면 오브젝트 삭제
        curhp -= damage;
        sr.material.color = Color.red;
        Invoke("ReturnColor", 1);
        audio.clip = onDamageSound;
        audio.Play();
        if (curhp <= 0)
        {

            int ran = Random.Range(0, 50 - gm.luck);
            if (ran >= 0 && ran < 5)
            {
                Instantiate(coin, transform.position, transform.rotation, pickparent);
            }
            else if (ran >= 5 && ran < 15)
            {
                Instantiate(heart, transform.position, transform.rotation, pickparent);
            }
            else if (ran == 16)
            {
                Instantiate(luck, transform.position, transform.rotation, pickparent);
            }

            gm.score += 10;
            gm.experience += Random.Range(3, 5);
            audio.clip = deathSound;
            audio.Play();
            Destroy(gameObject);
        }

    }

    void ReturnColor()
    {

        sr.material.color = Color.white;
    }
    //이동 함수
    //
    private void FixedUpdate()
    {
        transform.position += new Vector3(ranx, rany, 0).normalized * speed * Time.deltaTime;
        //값에따라 움직임
        if (isTargert)
        { 

            TargerPlayer(find.transform);
            float distance = Vector2.Distance(find.transform.position, transform.position);
            if (distance < 1) {



                Player player= find.GetComponent<Player>();
                if (currentDamageTime <= 0)
                {
                    player.onDamage(damage);
                    currentDamageTime = damageTime;
                }



            }
        }
        else
        {
            ranx = 0;
            rany = 0;
        }
        

    }
    public void TargerPlayer(Transform find)
    {

        ranx = -(transform.position.x - find.position.x);
        rany = -(transform.position.y - find.position.y);

    }

    void Update()
    {
        ElapseTime();
        if (ranx > 0)
        {
            sr.flipX = true;
        }
        else
        {

            sr.flipX = false;
        }
    }

    private void ElapseTime()
    {
        if (currentDamageTime > 0)
            currentDamageTime -= Time.deltaTime;  // 1초에 1씩
    }

   

}
