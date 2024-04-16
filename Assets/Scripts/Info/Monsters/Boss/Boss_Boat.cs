using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Boat : Monster
{
    public List<Transform> CP;
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
    public GameObject Phase2;

    private float delay1 = 0.9f;
    private float delay2 = 2.5f;
    private float delay3 = 12f;
    private float delay4 = 10f;
    private int level = 1;
    int count = 30;

    private bool isDead = true;
    private bool skill = false;
    private void Awake()
    {
        hp = 200000;
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
        if (hp <= 70000 && level == 1)
        {
            level++;
            count = 35;
            StartCoroutine(CicleFire(1));
            delay4 = 8f;
        }
        if (hp <= 20000 && level == 2)
        {
            level++;
            skill = true;
            count = 40;
            StartCoroutine(CicleFire(2));
            StopCoroutine(CreateBullets());
            StopCoroutine(CreateBullet_SH());
            StopCoroutine(CreateBullet_UH());
            GameObject spawn = GameObject.Find("SpawnManager");
            spawn.SetActive(false);
            delay4 = 0.5f;
        }
}


    #region 총 발사
    protected override void CreateBullet()
    {
        StartCoroutine(CreateBullets());
        StartCoroutine(CreateBullet_SH());
        StartCoroutine(CreateBullet_UH());
        StartCoroutine(CicleFire(0));
    }

    IEnumerator CreateBullets()
    {
        while (!isDead && !skill)
        {
            Instantiate(bullet, ms1.position, Quaternion.identity);
            Instantiate(bullet, ms2.position, Quaternion.identity);

            yield return new WaitForSeconds(delay1);
        }
    }

    IEnumerator CreateBullet_SH()
    {
        while (!isDead && !skill)
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
        while (!isDead && !skill)
        {
            Instantiate(Bullet_UH, MS_HR.position, Quaternion.identity);
            Instantiate(Bullet_UH, MS_HL.position, Quaternion.identity);

            yield return new WaitForSeconds(delay3);
        }
    }

    IEnumerator CicleFire(int num)
    {
        //가중되는 각도(항상 같은 위치로 발사하지 않도록 설정
        float weightAngle = 0f;

        //원 형태로 방사하는 발사체 생성(count 갯수 만큼)
        while (!isDead)
        {
            for (int j = 0; j < count; ++j)
            {
                //발사체 생성
                GameObject clone = Instantiate(BossBullet, CP[num].position, Quaternion.identity);

                //발사체 이동 방향(각도)
                float angle = weightAngle + 360 / count * j;

                //발사체 이동 방향(벡터)
                //Cos(각도)라디안 단위의 각도 표현을 위해  pi/ 180 을 곱함
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                //sin(각도)라디안 단위의 각도 표현을 위해 PI/100을 곱함
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                if(num == 1) { x = -x; }
                if(num == 2)
                {                    
                    switch (Random.Range(0, 3))
                    {
                        case 0:
                            x = -x;
                            break;
                        case 1:
                            y = -y;
                            break;
                        case 2:
                            x = -x;
                            y = -y;
                            break;

                    }
                }

                //발사체 이동 방향 설정
                clone.GetComponent<BossBullet>().Move(new Vector2(x, y));

                yield return new WaitForSeconds(0.1f);
            }

            //발사체가 생성되는 시작 각도 설정을 위한 변수
            weightAngle += 1;

            //3초마다 미사일 발사
            yield return new WaitForSeconds(Random.Range(delay4, delay4 + 1));
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
        Instantiate(Phase2, transform.position, Quaternion.identity);
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
