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
        //�̻��� ���ʹ������� �����̱�
        //���� ���� * ���ǵ� Ÿ��
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }

    //ȭ������� �������
    private void OnBecameInvisible()
    {
        //�ڱ� �ڽ� �����
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