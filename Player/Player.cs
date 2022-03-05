using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed=1;

    public int maxhp=200;
    public int curhp;

    public int coin;
    public int luck;



    Rigidbody2D rigid;
    float hAxis;
    float vAxis;
    bool fDown;
    bool spaceDown;
    bool skill1Down;

    bool isDodge;
    bool isWalk;
    bool isAttack;



    public GameObject bulletPre;
    public float curShotDelay = 0;
    public float MaxShotDelay = 0;
    public float delay = 1;

    public float damage;
    public float range;
    public float bulletspeed;

    public GameManager gm;
    public ButtonManager bm;

    public Animator anim;
    public SpriteRenderer sr;
    public CapsuleCollider2D coll;

    public Player_Skill1 skill1;
    public float Skill1damage;
    public bool skill1CoolTime;
    public GameObject skill2;
    public GameObject skill3;


    public Camera camera;
    Vector2 point;
    Vector2 moveVec;
    Vector3 dodgeVec;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        curhp = maxhp;

        Skill1damage = 30;
        skill1CoolTime = true;
        skill1.currentDamageTime = 2;
        skill1.Ondamage(Skill1damage);

        speed = 5;
        damage = 100;
        range = 1;
        bulletspeed = 8;
        MaxShotDelay = 0.5f;
        luck = 0;

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();

        Fire();
        Relode();

        Dodge();
        skill1Play();

    }

    public void GetLife() { 
    
    }
    public void skill1Play() {
        if (skill1Down&&skill1CoolTime) {
            skill1.gameObject.SetActive(true);
            skill1CoolTime = false;
            Invoke("Skill1Out", 2f);
            Invoke("Skill1CoolOut", 5f);

        }

    }
    public void Skill1Out() {

        skill1.gameObject.SetActive(false);
    }
    public void Skill1CoolOut()
    {
        skill1CoolTime = true;
    }
    void Dodge()
    {
        if (spaceDown && !isDodge && moveVec != Vector2.zero)
        {
            speed *= 2;
            anim.SetTrigger("isDash");
            gameObject.layer = 9;
     //       coll.size = new Vector2(0.1f, 0.1f);

            isDodge = true;
            Invoke("DodgeOut", 0.5f);

        }
    }
    void DodgeOut()
    {
        gameObject.layer = 3;
    //    coll.size = new Vector2(0.45f, 0.75f);
        dodgeVec = moveVec;
        isDodge = false;
        coll.enabled = true;
        speed *= 0.5f;
    }


    void Fire()
    {
        //´«¹° µô·¹ÀÌ
        if (curShotDelay < MaxShotDelay)
            return;
        if (Input.GetMouseButton(0)) {
//            Debug.Log("1");

  //          anim.SetTrigger("isAttack");

            point = Input.mousePosition;
            
            Vector2 ray= camera.ScreenToWorldPoint(point);



            GameObject bullet = Instantiate(bulletPre, transform.position, transform.rotation);
            //¹ß»ç
            Rigidbody2D rigid1 = bullet.GetComponent<Rigidbody2D>();

            Vector2 fireVec = ray - (Vector2)transform.position;

            if (fireVec.x < 0)
            {

                sr.flipX = true;
            }
            else {

                sr.flipX = false;
            }

            rigid1.AddForce(fireVec.normalized * bulletspeed, ForceMode2D.Impulse);
            Bullet_Player bullet1 = bullet.GetComponent<Bullet_Player>();
            //´«¹° ½ºÅÝ ¼³Á¤
            bullet1.Ondamage((float)damage);
            bullet1.range = this.range;

        }
            curShotDelay = 0;
    }


    void Relode()
    {
        curShotDelay += Time.deltaTime * delay;
    }
    void GetInput()
    {

        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        fDown = Input.GetMouseButton(0);
        spaceDown = Input.GetKeyDown(KeyCode.Space);
        skill1Down = Input.GetKeyDown(KeyCode.E);
    }
    void Move()
    {
        moveVec = new Vector3(hAxis, vAxis).normalized;
        rigid.velocity = moveVec*speed;
        if (isDodge)
        {
            //moveVec = dodgeVec;
        }
        //transform.LookAt(transform.position +(Vector3) moveVec);
/*        if (hAxis!=0&& !(curShotDelay < MaxShotDelay)) {
            sr.flipX = hAxis == -1;
        }*/
        if (hAxis != 0 )
        {
            sr.flipX = hAxis == -1;
        }    
        anim.SetBool("isRun", moveVec != Vector2.zero);
    }

    public void onDamage(int damage) {
        curhp -= damage;
        if (curhp <= 0) {
            bm.GameOver();


        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp")) {
            PickUp item = collision.GetComponent<PickUp>();
            switch (item.type) {

                case PickUp.Type.Coin:
                    coin += item.value;
                    gm.coin += item.value;
                    Destroy(collision.gameObject);
                    break;
                case PickUp.Type.Heart:
                    curhp += item.value;
                    if (curhp > maxhp)
                    {
                        curhp = maxhp;
                    }
                    Destroy(collision.gameObject);
                    break;
                case PickUp.Type.Luck:
                    luck++;
                    gm.luck++;
                    if (luck >= 20)
                        luck = 20;
                    Destroy(collision.gameObject);
                    break;

            }

        }
        /*if (collision.CompareTag("Enemy")) {
            Debug.Log("¸÷ÇÇ°Ý");
            Vector2 back = transform.position - collision.transform.position;
            // rigid.velocity = back*10;
            Debug.Log(back);
            rigid.AddForce(new Vector2(back.x,back.y) *11000, ForceMode2D.Impulse);


        }*/

    }



}
