using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_5 : MonoBehaviour
{
    protected float health;
    protected float speed;
    protected float delay;
    public Transform ms1;
    public Transform ms2;
    public GameObject bullet;
    public GameObject explosion;
    protected bool isVisible = false;
    private Renderer myRenderer;

    void CreateBullet()
    {
        Instantiate(bullet, ms1.position, Quaternion.identity);
        Instantiate(bullet, ms2.position, Quaternion.identity);

        //재귀호출
        Invoke("CreateBullet", delay);

    }   

    protected void Dead(Collider2D collision)
    {
        GameObject go = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(go, 1);


        GameManager.Instance.Score(250);
        if (collision.gameObject.CompareTag("PBullet"))
            Destroy(collision.gameObject);

        Destroy(gameObject);
    }

    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        //아래 방향으로 움직여라
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnBecameVisible()
    {
        isVisible = true;
        Invoke("CreateBullet", delay);

    }


    private void OnBecameInvisible()
    {
        if(transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }

}
