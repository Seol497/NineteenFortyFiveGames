using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MidAirPlan : MonoBehaviour
{
    public float health = 10000;
    public float delay1 = 1;
    public float delay2 = 15;
    public Transform ms1;
    public Transform ms2;
    public GameObject bullet;
    public GameObject homming;
    public GameObject hommingEx;
    public GameObject explosion;
    public GameObject explosion2;
    public GameObject explosion3;
    public GameObject explosion4;
    private Renderer myRenderer;
    private float pattern;
    private bool isDead;
    private bool skill = false;


    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
        pattern = health / 2;
    }

    private void Start()
    {
        StartCoroutine(MoveObject());
    }

    IEnumerator MoveObject()
    {
        float elapsedTime = 0f; // 경과 시간 초기화

        while (elapsedTime < 2)
        {
            // 보간법을 사용하여 y값 계산
            float newY = Mathf.Lerp(10, 3.6f, elapsedTime / 2);

            // 현재 위치를 새로운 위치로 업데이트
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            // 경과 시간 업데이트
            elapsedTime += Time.deltaTime;

            yield return null; // 한 프레임 대기
        }

        // 최종 위치 설정
        transform.position = new Vector3(transform.position.x, 3.6f, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("PBullet") && !isDead)
        {
            health -= 20;
            if (health > 0)
            {
                Destroy(collision.gameObject);
                StartCoroutine(Hit());
            }
            if (health <= 0 && !isDead)
            {
                StartCoroutine(Dead());
            }
        }
        if (collision.gameObject.CompareTag("Bomb") && !isDead)
        {
            health -= 500;
            if (health > 0)
            {
                StartCoroutine(Hit());
            }
            if (health <= 0 && !isDead)
            {
                StartCoroutine(Dead());
            }
        }
        if (health < pattern && !skill)
        {
            skill = true;
            StartCoroutine(Skill());
        }
        Debug.Log(health);
    }

    void CreateBullet()
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
            yield return new WaitForSeconds(10);
        }
        yield break;
    }

    IEnumerator Hit()
    {
        Color currentColor = new Color(200f / 255f, 20f / 255f, 20f / 255f, 255f / 255f);
        myRenderer.material.color = currentColor;
        yield return new WaitForSeconds(0.05f);
        currentColor = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        myRenderer.material.color = currentColor;
        yield break;
    }

    IEnumerator Dead()
    {
        isDead = true;
        float elapsedTime = 0f;
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
        GameManager.Instance.Score(5000);
        Destroy(gameObject);
        yield break;
    }

    private void OnBecameVisible()
    {
        Invoke("CreateBullet", delay1);
        Invoke("CreateHomming", delay2);
    }

}
