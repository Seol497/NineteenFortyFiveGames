using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBullet : MonoBehaviour
{
    public float Speed = 4.0f;
    public int damage = 10;

    private void Awake()
    {
        
    }
    void Update()
    {
        //미사일 위쪽방향으로 움직이기
        //위의 방향 * 스피드 타임
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }

    //화면밖으로 나갈경우
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<Monster>().GetDamage(damage);
            Destroy(gameObject);
        }
    }


}