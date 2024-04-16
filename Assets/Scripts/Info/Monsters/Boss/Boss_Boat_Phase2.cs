using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Boss_Boat_Phase2 : Monster
{
    public List<Transform> CP;
    public GameObject MonsterL;
    public GameObject MonsterR;
    public GameObject BossBullet;
    public GameObject explosion2;
    public GameObject explosion3;
    public GameObject explosion4;


    private bool isDead = true;
    float skill01;
    float count = 100f;

    private void Awake()
    {       
        hp = 150000;
        skill01 = 120000;
        myRenderer = GetComponent<Renderer>();
    }
    private void Start()
    {
        StartCoroutine(MoveObject());
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
        if(hp <= skill01)
        {
            SpawnMonster();
            skill01 -= 30000;
        }
    }

    protected override void CreateBullet()
    {
        StartCoroutine(CicleFire(0));
        StartCoroutine(CicleFire(1));
        StartCoroutine(CicleFire(2));
        StartCoroutine(ShotBullet());
    }

    void SpawnMonster()
    {
        Instantiate(MonsterL);
        Instantiate(MonsterR);
    }

    IEnumerator ShotBullet()
    {
        while (!isDead)
        {
            Instantiate(bullet, ms1.position, Quaternion.identity);
            Instantiate(bullet, ms2.position, Quaternion.identity);
            yield return new WaitForSeconds(10f);
        }
    }

    IEnumerator CicleFire(int num)
    {
        //가중되는 각도(항상 같은 위치로 발사하지 않도록 설정
        float weightAngle = 0f;
        delay = 1f;
        //원 형태로 방사하는 발사체 생성(count 갯수 만큼)
        while (!isDead)
        {
            for (int j = 0; j < count; ++j)
            {

                    //발사체 생성
                GameObject clone = Instantiate(BossBullet, CP[num].position, Quaternion.identity);

                //발사체 이동 방향(각도)
                float angle = weightAngle + 360f / count * j;

                //발사체 이동 방향(벡터)
                //Cos(각도)라디안 단위의 각도 표현을 위해  pi/ 180 을 곱함
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                //sin(각도)라디안 단위의 각도 표현을 위해 PI/100을 곱함
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                if (num == 2)
                    x = -x;                

                //발사체 이동 방향 설정
                clone.GetComponent<BossBullet>().Move(new Vector2(x, y));

                if(num == 0 || num == 2)
                    yield return new WaitForSeconds(0.01f);
            }

            //발사체가 생성되는 시작 각도 설정을 위한 변수
            weightAngle += 1;

            //3초마다 미사일 발사
            if(num == 1)
                yield return new WaitForSeconds(3f);

            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator MoveObject()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 2)
        {
            float newY = Mathf.Lerp(2.35f, 2.87f, elapsedTime / 2);

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = new Vector3(transform.position.x, 2.87f, transform.position.z);
        yield return new WaitForSeconds(2f);
        isDead = false;
        CreateBullet();
    }
   
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
}
