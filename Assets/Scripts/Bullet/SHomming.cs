using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHomming : MonoBehaviour
{
    GameObject target;  //�÷��̾�
    public float speed = 3f;

    Vector2 dir;
    Vector2 dirNo;

    void Start()
    {
        Destroy(gameObject, 6f);
        //�÷��̾� �±׷� ã��
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            if (target == null)
            {
                return;
            }
            //A - B  -> A�� �ٶ󺸴� ���ͳ��´�.
            dir = target.transform.position - transform.position;
            //���⺤�͸� ���ϱ� �������� 1��ũ��� �����.
            dirNo = dir.normalized;
        }

    }

    void Update()
    {
        if (target != null)
            transform.Translate(dirNo * speed * Time.deltaTime);
        else
            transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
