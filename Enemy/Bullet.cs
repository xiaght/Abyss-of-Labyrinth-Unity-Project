using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float damage = 0;
    public float range=0f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyThis", range);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Input.GetAxisRaw();
    }

    
    public void Ondamage(float _damage) {

        damage = _damage;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Wall"))
        {
            DestroyThis();

        }
        if (collision.CompareTag("Player")) {

            Player player=collision.GetComponent<Player>();
            player.onDamage((int)damage);
            DestroyThis();
        }
    }
    
    void DestroyThis()
    {
        Destroy(gameObject);
        
    }



}
