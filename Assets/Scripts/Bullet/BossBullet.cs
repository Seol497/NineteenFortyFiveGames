using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float Speed = 3f;
    Vector2 vec2 = Vector2.down;

    //방향을 전달하는 함수
    public void Move(Vector2 vec)
    {
        vec2 = vec;        
    }

    void Update()
    {
        transform.Translate(vec2 * Speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //미사일지움
            Destroy(gameObject);
        }
    }





}
