using System.Collections;
using UnityEngine;

public class MidAirPlan : Monster
{
    public float delay1 = 1;
    public float delay2 = 15;
    public GameObject homming;
    public GameObject hommingEx;
    public GameObject explosion2;
    public GameObject explosion3;
    public GameObject explosion4;
    private float pattern;
    private bool isDead;
    private bool skill = false;


    private void Awake()
    {
        hp = 100000;
        myRenderer = GetComponent<Renderer>();
        pattern = hp / 2;
    }

    private void Start()
    {
        StartCoroutine(MoveObject());
    }

    IEnumerator MoveObject()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 2)
        {
            float newY = Mathf.Lerp(10, 3.6f, elapsedTime / 2);

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 최종 위치 설정
        transform.position = new Vector3(transform.position.x, 3.6f, transform.position.z);
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
        if (hp < pattern && !skill)
        {
            StartCoroutine(Skill());
        }
    }

    protected override void CreateBullet()
    {
        if (!isDead)
        {
            Instantiate(bullet, ms1.position, Quaternion.identity);
            Instantiate(bullet, ms2.position, Quaternion.identity);
            
            Invoke("CreateBullet", delay1);
        }

        
    }

    void CreateHomming()
    {
        if (!isDead && !skill)
        {
            Instantiate(homming, ms1.position, Quaternion.identity);
            Instantiate(homming, ms2.position, Quaternion.identity);

            Invoke("CreateHomming", delay2);
        }

    }

    IEnumerator Skill()
    {
        skill = true;
        while (!isDead)
        {
            Instantiate(hommingEx, ms1.position, Quaternion.identity);
            Instantiate(hommingEx, ms2.position, Quaternion.identity);
            yield return new WaitForSeconds(1);
            Instantiate(hommingEx, ms1.position, Quaternion.identity);
            Instantiate(hommingEx, ms2.position, Quaternion.identity);
            yield return new WaitForSeconds(1);
            Instantiate(hommingEx, ms1.position, Quaternion.identity);
            Instantiate(hommingEx, ms2.position, Quaternion.identity);
            yield return new WaitForSeconds(12);
        }
        yield break;
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

    private void OnBecameVisible()
    {
        Invoke("CreateBullet", delay1);
        Invoke("CreateHomming", delay2);
    }

}
