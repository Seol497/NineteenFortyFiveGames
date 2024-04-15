using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Boss_Boat : Monster
{
    public Transform CP1;
    public Transform CP2;
    public Transform CP3;
    public Transform MS_HR;
    public Transform MS_HL;
    public Transform MS_SH_R1;
    public Transform MS_SH_R2;
    public Transform MS_SH_L1;
    public Transform MS_SH_L2;
    public GameObject Bullet_SH;
    public GameObject Bullet_UH;
    public GameObject BossBullet;
    public GameObject explosion2;
    public GameObject explosion3;
    public GameObject explosion4;

    private float delay1 = 0.9f;
    private float delay2 = 1.6f;
    private float delay3 = 20f;
    private float delay4 = 25f;

    private bool isDead = true;
    private void Awake()
    {
        hp = 100000;
        myRenderer = GetComponent<Renderer>();
    }

    public override void GetDamage(int num)
    {
        base.GetDamage(num);
        if (hp > 0 && !isDead)
        {
            StartCoroutine(Hit());
        }
        else if (hp <= 0 && !isDead)
        {
            StartCoroutine(Dead());
        }
        Debug.Log(hp);
    }

    #region �� �߻�
    protected override void CreateBullet()
    {
        StartCoroutine(CreateBullets());
        StartCoroutine(CreateBullet_SH());
        StartCoroutine(CreateBullet_UH());
        StartCoroutine(CicleFire());
    }

    IEnumerator CreateBullets()
    {
        while (!isDead)
        {
            Instantiate(bullet, ms1.position, Quaternion.identity);
            Instantiate(bullet, ms2.position, Quaternion.identity);

            yield return new WaitForSeconds(delay1);
        }
    }

    IEnumerator CreateBullet_SH()
    {
        while (!isDead)
        {
            Instantiate(Bullet_SH, MS_SH_R1.position, Quaternion.identity);
            Instantiate(Bullet_SH, MS_SH_R2.position, Quaternion.identity);
            Instantiate(Bullet_SH, MS_SH_L1.position, Quaternion.identity);
            Instantiate(Bullet_SH, MS_SH_L2.position, Quaternion.identity);
            yield return new WaitForSeconds(delay2);
        }
    }

    IEnumerator CreateBullet_UH()
    {
        while (!isDead)
        {
            Instantiate(Bullet_UH, MS_HR.position, Quaternion.identity);
            Instantiate(Bullet_UH, MS_HL.position, Quaternion.identity);

            yield return new WaitForSeconds(delay3);
        }
    }

    IEnumerator CicleFire()
    {
        int count = 30;
        //�߻�ü ������ ����
        float intervalAngle = 360 / count;
        //���ߵǴ� ����(�׻� ���� ��ġ�� �߻����� �ʵ��� ����
        float weightAngle = 0f;

        //�� ���·� ����ϴ� �߻�ü ����(count ���� ��ŭ)
        while (!isDead)
        {
            for (int i = 0; i < count; ++i)
            {
                //�߻�ü ����
                GameObject clone = Instantiate(BossBullet, CP1.position, Quaternion.identity);

                //�߻�ü �̵� ����(����)
                float angle = weightAngle + intervalAngle * i;
                //�߻�ü �̵� ����(����)
                //Cos(����)���� ������ ���� ǥ���� ����  pi/ 180 �� ����
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                //sin(����)���� ������ ���� ǥ���� ���� PI/100�� ����
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);


                //�߻�ü �̵� ���� ����
                clone.GetComponent<BossBullet>().Move(new Vector2(x, y));

                yield return new WaitForSeconds(0.1f);
            }

            //�߻�ü�� �����Ǵ� ���� ���� ������ ���� ����
            weightAngle += 1;

            //3�ʸ��� �̻��� �߻�
            yield return new WaitForSeconds(delay4);
        }
    }
    #endregion

    IEnumerator Dead()
    {
        isDead = true;
        for (int i = 0; i < 5; i++)
        {
            Vector2 pos = new Vector2(transform.position.x + 1.751f, transform.position.y + 0.719f);
            GameObject go = Instantiate(explosion, pos, Quaternion.identity);
            Destroy(go, 1);
            yield return new WaitForSeconds(0.3f);
            pos = new Vector2(transform.position.x - 1.68f, transform.position.y + 0.878f);
            GameObject go2 = Instantiate(explosion2, pos, Quaternion.identity);
            Destroy(go2, 1);
            yield return new WaitForSeconds(0.3f);
            pos = new Vector2(transform.position.x, transform.position.y + 0.256f);
            GameObject go3 = Instantiate(explosion3, pos, Quaternion.identity);
            Destroy(go3, 1);
        }
        Vector2 pos2 = new Vector2(transform.position.x, transform.position.y + 0.256f);
        GameObject go4 = Instantiate(explosion4, pos2, Quaternion.identity);
        Destroy(go4, 1.5f);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 5; i++)
            SpawnItem(0.7f);
        GameManager.Instance.Score(5000);
        Destroy(gameObject);
        AllDestory();
        yield break;
    }

    IEnumerator MoveObject()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 2)
        {
            float newY = Mathf.Lerp(10, 2.35f, elapsedTime / 2);

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = new Vector3(transform.position.x, 2.35f, transform.position.z);
        yield return new WaitForSeconds(2f);
        isDead = false;
        CreateBullet();
    }

    void Start()
    {
        StartCoroutine(MoveObject());   
    }


    void Update()
    {
        
    }
}
