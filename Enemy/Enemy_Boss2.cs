using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss2 : Enemy
{

    public GameObject bullet1pos;
    public GameObject bullet2pos;
    public int ranAction;
    public Enemy_BossSkill skill;
    public Animator anim;
    GameObject find2;
    public Player player;
    void Start() {

        Debug.Log("start");
        int ranPattern = Random.Range(0, 3);
        //���۽� ���� 
        //Invoke("Fire", ran);
        //
        StartCoroutine(ThinkPattern());
        skill.Ondamage(30);


    }

    IEnumerator ThinkPattern()
    {
        yield return new WaitForSeconds(0.1f);
        ranAction = Random.Range(0, 5);
        switch (ranAction)
        {
            case 0:
            case 1:
                StartCoroutine(Teleport());
                break;
            case 2:
            case 3:
                StartCoroutine(Bomb());
                break;
            case 4:
                StartCoroutine(SpeedUp());
                break;
        }
    }

    IEnumerator Teleport() {
        Debug.Log("Teleport");
        GameObject find = GameObject.FindWithTag("Player");
        int ran = Random.Range(0, 3);
        if (ran == 0) {
            transform.position = find.transform.position + new Vector3(0, 5);
        }else if(ran == 1) {
            transform.position = find.transform.position + new Vector3(0, -5);
        }
        else if (ran == 2)
        {
            transform.position = find.transform.position + new Vector3(5, 0);
        }
        else if (ran == 3)
        {
            transform.position = find.transform.position + new Vector3(-5, 0);
        }


        yield return new WaitForSeconds(5f);
        StartCoroutine(ThinkPattern());
    }

    IEnumerator Bomb() {
        anim.SetTrigger("doSkill");
        Debug.Log("Bomb");
        ranx = 0;
        rany = 0;
        yield return new WaitForSeconds(3f);
        skill.gameObject.SetActive(true);                

        yield return new WaitForSeconds(5f);
        skill.gameObject.SetActive(false);
        StartCoroutine(ThinkPattern());
    }
    IEnumerator SpeedUp() {


        Debug.Log("SpeedUp");
        speed = 5;
        yield return new WaitForSeconds(5f);

        speed = 1;
        StartCoroutine(ThinkPattern());
    }


/*
    void Think()
    {
        ranx = Random.Range(-1, 2);
        rany = Random.Range(-1, 2);
        //��ͷ� ��� ������ ����
        Invoke("Think", 3);
    }*/




    // public Player

    private void FixedUpdate()
    {
        transform.position += new Vector3(ranx, rany, 0).normalized * speed * Time.deltaTime;



        if (!(ranx == 0))
        {

            anim.SetBool("isWalk", !(ranx == 0));
        }
        if (isFire)
        {
            GameObject find = GameObject.FindWithTag("Player");

            TargerPlayer(find.transform);
        }
        if (ranx < 0)
        {
            sr.flipX = true;
        }
        else
        {

            sr.flipX = false;
        }
    }




    public void Fire()
    {
        if (isFire)
        {


            isFire = false;
            GameObject bullet = Instantiate(bulletPre, transform.position, transform.rotation, bulletparent);
            Rigidbody2D rigid1 = bullet.GetComponent<Rigidbody2D>();
            //rigid1.AddForce(new Vector3(ranx, rany, 0) * bulletspeed, ForceMode.Impulse);

            GameObject find = GameObject.FindWithTag("Player");

            //�÷��̾� ����
            Vector3 direction = find.transform.position - transform.position;
            //�÷��̾� �������� ���� �߻�
            rigid1.AddForce(direction.normalized * bulletspeed, ForceMode2D.Impulse);
            Bullet bullet_1 = bullet.GetComponent<Bullet>();
            bullet_1.Ondamage(damage);
            bullet_1.range = this.range;



            GameObject bullet2 = Instantiate(bulletPre, bullet1pos.transform.position, bullet1pos.transform.rotation, bulletparent);
            Rigidbody2D rigid2 = bullet2.GetComponent<Rigidbody2D>();
            //rigid1.AddForce(new Vector3(ranx, rany, 0) * bulletspeed, ForceMode.Impulse);

           
            find2 = GameObject.FindWithTag("Player");

            //�÷��̾� ����
            Vector3 direction2 = find2.transform.position+new Vector3(-1,0) - bullet1pos.transform.position;
            //�÷��̾� �������� ���� �߻�
            rigid2.AddForce(direction2.normalized * bulletspeed, ForceMode2D.Impulse);
            Bullet bullet_2 = bullet2.GetComponent<Bullet>();
            bullet_2.Ondamage(damage);
            bullet_2.range = this.range;

            GameObject bullet3 = Instantiate(bulletPre, bullet2pos.transform.position, bullet2pos.transform.rotation, bulletparent);
            Rigidbody2D rigid3 = bullet3.GetComponent<Rigidbody2D>();
            //rigid1.AddForce(new Vector3(ranx, rany, 0) * bulletspeed, ForceMode.Impulse);

            GameObject find3 = GameObject.FindWithTag("Player");

            //�÷��̾� ����
            Vector3 direction3 = find3.transform.position + new Vector3(1, 0) - bullet2pos.transform.position;
            //�÷��̾� �������� ���� �߻�
            rigid3.AddForce(direction3.normalized * bulletspeed, ForceMode2D.Impulse);
            Bullet bullet_3 = bullet3.GetComponent<Bullet>();
            bullet_3.Ondamage(damage);
            bullet_3.range = this.range;

            
            Invoke("Fire", 0.5f);
        }
        //���� ����


    }
    public void TargerPlayer(Transform find)
    {

        ranx = -(transform.position.x - find.position.x);
        rany = -(transform.position.y - find.position.y);

    }

    public override void onDamage(int damage)
    {
        //ü���� ������ ������Ʈ ����
        curhp -= damage;
        sr.material.color = Color.red;
        audio.clip = onDamageSound;
        audio.Play();
        Invoke("ReturnColor", 1);

        if (curhp <= 0)
        {

            int ran = Random.Range(0, 100 - gm.luck);

            Instantiate(coin, transform.position, transform.rotation, pickparent);
            Instantiate(coin, new Vector3(transform.position.x + 1, transform.position.y), transform.rotation, pickparent);
            Instantiate(coin, new Vector2(transform.position.x - 1, transform.position.y), transform.rotation, pickparent);
            Instantiate(coin, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation, pickparent);
            Instantiate(coin, new Vector2(transform.position.x, transform.position.y - 1), transform.rotation, pickparent);

            player.curhp = player.maxhp;
            if (ran == 30)
            {
                Instantiate(luck, transform.position, transform.rotation, pickparent);
            }
            gm.curHp = gm.maxHp;
            gm.score += 100;
            gm.experience += Random.Range(20, 25);
            audio.clip = deathSound;
            audio.Play();
            Destroy(gameObject);
        }

    }

}
