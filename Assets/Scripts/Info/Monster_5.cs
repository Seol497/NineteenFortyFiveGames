using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Monster_5 : Monster
{      
    public int score = 100;
    protected override void CreateBullet()
    {
        Instantiate(bullet, ms1.position, Quaternion.identity);
        Instantiate(bullet, ms2.position, Quaternion.identity);

        //���ȣ��
        Invoke("CreateBullet", delay);
    }   

    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
        hp = 100;
        speed = 2;
        delay = 1.5f;
    }

    void Update()
    {
        //�Ʒ� �������� ��������
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
            SpawnItem(0.09f);
            Dead(score);
        }
    }

    private void OnBecameVisible()
    {

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
