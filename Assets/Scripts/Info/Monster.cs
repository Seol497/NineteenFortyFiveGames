using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float Speed = 3;
    public float Delay = 1f;
    public Transform ms1;
    public Transform ms2;
    public GameObject bullet;
    public GameObject explosion;
    private bool isVisible = false;

    void CreateBullet()
    {
        Instantiate(bullet, ms1.position, Quaternion.identity);
        Instantiate(bullet, ms2.position, Quaternion.identity);

        //재귀호출
        Invoke("CreateBullet", Delay);

    }

    void Update()
    {
        //아래 방향으로 움직여라
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isVisible)
        {
            if (collision.gameObject.CompareTag("PBullet"))
            {
                GameObject go = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(go, 1);

                Destroy(collision.gameObject);

                Destroy(gameObject);
            }
        }
    }

    private void OnBecameVisible()
    {
        isVisible = true;
        Invoke("CreateBullet", Delay);

    }


    private void OnBecameInvisible()
    {
        if(transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }

}
