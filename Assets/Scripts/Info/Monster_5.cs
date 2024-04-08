using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class Monster_5 : Monster
{      
    protected override void CreateBullet()
    {
        Instantiate(bullet, ms1.position, Quaternion.identity);
        Instantiate(bullet, ms2.position, Quaternion.identity);

        //재귀호출
        Invoke("CreateBullet", delay);
    }   

    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
        hp = 100;
        speed = 2;
        delay = 1;
    }

    void Update()
    {
        //아래 방향으로 움직여라
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    public override void GetDamage(int num)
    {
        base.GetDamage(num);
        if (hp > 0)
        {
            StartCoroutine(Hit());
        }
        else if (hp <= 0)
        {
            SpawnItem(0.05f);
            Dead(score);
        }
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
