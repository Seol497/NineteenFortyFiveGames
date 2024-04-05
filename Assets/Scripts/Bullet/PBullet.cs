using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBullet : MonoBehaviour
{
    public float Speed = 4.0f;

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
        //자기 자신 지우기
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<Monster>().GetDamage(10);
            Destroy(gameObject);
        }
    }


}