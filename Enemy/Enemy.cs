using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    //������
    public float ranx = 0;
    public float rany = 0;

    //����
    public int maxhp = 200;
    public int curhp;
    public float speed = 0;
    public float bulletspeed = 0;
    public int damage = 0;
    public float range = 0;

    public bool isFire;

    //���� ������
    public GameObject bulletPre;
    public GameObject coin;
    public GameObject heart;
    public GameObject luck;

    public Transform bulletparent;
    public Transform pickparent;

    public SpriteRenderer sr;
    public GameManager gm;

    public CircleCollider2D Targertcoll;
    //public SpriteRenderer tempColor;

    public AudioSource audio;
    public AudioClip onDamageSound;
    public AudioClip deathSound;
    void Awake()
    {
        Debug.Log("Awake");
        // tempColor = sr;
       // gm = GetComponent<GameManager>();
        //gm.GetComponent<GameManager>();
        curhp = maxhp;
        speed = 1;
        bulletspeed = 5;
      //  damage = 10+gm.stage;
        range = 5;
        audio.volume = 0.1f;


        int ran = Random.Range(2, 5);
        //���۽� ���� 
        //Invoke("Fire", ran);
        Invoke("Think", ran);


    }




    public void Fire()
    {
        if (isFire) {
            isFire = false;
            GameObject bullet = Instantiate(bulletPre, transform.position, transform.rotation, bulletparent);
            Rigidbody2D rigid1 = bullet.GetComponent<Rigidbody2D>();
            //rigid1.AddForce(new Vector3(ranx, rany, 0) * bulletspeed, ForceMode.Impulse);

            GameObject find = GameObject.FindWithTag("Player");

            //�÷��̾� ����
            Vector3 direction = find.transform.position - transform.position;
            //�÷��̾� �������� ���� �߻�
            rigid1.AddForce(direction.normalized * bulletspeed, ForceMode2D.Impulse);
            Bullet bullet1 = bullet.GetComponent<Bullet>();
            bullet1.Ondamage(damage);
            bullet1.range = this.range;

            //���
            int ran = Random.Range(1, 5);
            Invoke("Fire", ran);
        }
        //���� ����
        

    }


    /*    private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == "Bullet_Player")
            {
                //�ǰ��Լ�
                onDamage();
            }
        }*/
    public virtual void onDamage(int damage)
    {
        //ü���� ������ ������Ʈ ����
        curhp -= damage;
        sr.material.color = Color.red;
        Invoke("ReturnColor", 1);
        audio.clip = onDamageSound;
        audio.Play();
        if (curhp <= 0)
        {

            int ran = Random.Range(0, 100 - gm.luck);
            if (ran >=0&& ran<20) {
                Instantiate(coin, transform.position, transform.rotation, pickparent);
            }
            else if (ran >= 20 && ran < 30)
            {
                Instantiate(heart, transform.position, transform.rotation, pickparent);
            }
            else if (ran ==30)
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

    void ReturnColor() {

        sr.material.color = Color.white;
    }
    //�̵� �Լ�
    void Think()
    {
        ranx = Random.Range(-1, 2);
        rany = Random.Range(-1, 2);
        //��ͷ� ��� ������ ����
        Invoke("Think", 3);
    }
    //
    private void FixedUpdate()
    {
        //�������� ������
        transform.position += new Vector3(ranx, rany, 0) * speed * Time.deltaTime;

    }


    // Update is called once per frame
    void Update()
    {

    }

    
}




