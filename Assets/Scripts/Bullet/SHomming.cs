using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHomming : MonoBehaviour
{
    GameObject target;  //플레이어
    public float speed = 3f;

    Vector2 dir;
    Vector2 dirNo;

    void Start()
    {
        Destroy(gameObject, 6f);
        //플레이어 태그로 찾기
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            if (target == null)
            {
                dirNo = Vector2.down;
            }
            else
            {
                //A - B  -> A를 바라보는 벡터나온다.
                dir = target.transform.position - transform.position;
                //방향벡터만 구하기 단위벡터 1의크기로 만든다.
                dirNo = dir.normalized;
            }
        }

    }

    void Update()
    {
            transform.Translate(dirNo * speed * Time.deltaTime);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
