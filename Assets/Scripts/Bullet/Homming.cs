using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homminh : MonoBehaviour
{
    GameObject target;  //플레이어
    public float Speed = 3f;
    float speed;

    Vector2 dir;
    Vector2 dirNo;

    void Start()
    {
        Destroy(gameObject, 6f);
        speed = Random.Range(Speed - 2f, Speed);
    }

    void Update()
    {
        //플레이어 태그로 찾기
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            if (target == null)
            {
                return;
            }
        }

        //A - B  -> A를 바라보는 벡터나온다.
        dir = target.transform.position - transform.position;
        //방향벡터만 구하기 단위벡터 1의크기로 만든다.
        dirNo = dir.normalized;
        transform.Translate(dirNo * speed * Time.deltaTime);
    }
}
