using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homminh : MonoBehaviour
{
    GameObject target;  //�÷��̾�
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
        //�÷��̾� �±׷� ã��
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            if (target == null)
            {
                return;
            }
        }

        //A - B  -> A�� �ٶ󺸴� ���ͳ��´�.
        dir = target.transform.position - transform.position;
        //���⺤�͸� ���ϱ� �������� 1��ũ��� �����.
        dirNo = dir.normalized;
        transform.Translate(dirNo * speed * Time.deltaTime);
    }
}
