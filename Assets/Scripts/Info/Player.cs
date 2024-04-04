using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Renderer myRenderer;
    private Collider2D[] colliders;

    public static Player Instance;

    public float moveSpeed = 5f;

    private int bomb = 2;


    private bool isBlinking = false;
    private bool isDead = false;
    private bool isPlaying = false;
    private bool isShoot = false;

    Animator ani; //애니메이터를 가져올 변수

    public Transform pos = null;
    public Transform pos2 = null;
    public Transform skillPos = null;
    public GameObject bullet;
    public GameObject skill;
    public GameObject explosion;


    void Awake()
    {
        myRenderer = GetComponent<Renderer>();
        colliders = GetComponentsInChildren<Collider2D>();
        ani = GetComponent<Animator>();

        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
    }


    void Start()
    {
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        StartCoroutine(BlinkAlpha());
    }

    IEnumerator BlinkAlpha()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 1.5f)
        {
            isBlinking = !isBlinking; // 깜빡임 상태 변경

            // isBlinking이 true면 알파값을 0으로, false면 알파값을 1로 설정
            Color currentColor = myRenderer.material.color;
            currentColor.a = isBlinking ? 0f : 1f;
            myRenderer.material.color = currentColor;
            if (elapsedTime < 0.5f)
            {
                float up = 8 * Time.deltaTime;
                transform.Translate(0, up, 0);
            }
            if (elapsedTime > 0.5f)
                isPlaying = true;
            // 지정한 간격만큼 대기
            yield return new WaitForSeconds(Random.Range(0.005f, 0.05f));

            elapsedTime += Time.deltaTime;
        }
        Color originalColor = myRenderer.material.color;
        originalColor.a = 1f;
        myRenderer.material.color = originalColor;           
        ColliderOn();
        yield break;
    }

    private void ColliderOn()
    {
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("EBullet") && !isDead)
        {
            isDead = true;
            foreach (Collider2D collider in colliders)
            {
                collider.enabled = true;
            }
            GameObject go = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(go, 1);

            Destroy(collision.gameObject);

            GameManager.Instance.Dead();
            Destroy(gameObject);
        }     
    }

    IEnumerator Shoot()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(bullet, pos.position, Quaternion.identity);
            Instantiate(bullet, pos2.position, Quaternion.identity);
            yield return new WaitForSeconds(0.04f);
        }        
        yield return new WaitForSeconds(0.05f);
        isShoot = false;
        yield break;
    }

    void Update()
    {
        if (isPlaying)
        {
            float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            float moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");


            //-1 0 1


            if (Input.GetAxis("Horizontal") <= -0.5f)
                ani.SetBool("left", true);
            else
                ani.SetBool("left", false);

            if (Input.GetAxis("Horizontal") >= 0.5f)
                ani.SetBool("right", true);
            else
                ani.SetBool("right", false);
            if (Input.GetAxis("Vertical") >= 0.5f)
            {
                ani.SetBool("left", false);
                ani.SetBool("right", false);
                ani.SetBool("up", true);
            }
            else
            {
                ani.SetBool("up", false);
            }


            if (Input.GetKeyDown(KeyCode.S) && !isShoot)
            {
                isShoot = true;
                StartCoroutine(Shoot());
            }

            if (Input.GetKeyDown(KeyCode.X ) && bomb > 0)
            {
                bomb--;
                Instantiate(skill, skillPos.position, Quaternion.identity);
                GameManager.Instance.UseBomb();
            }

            transform.Translate(moveX, moveY, 0);


            //캐릭터의 월드 좌표를 뷰포트 좌표계로 변환해준다.
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
            viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
            Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);//다시월드좌표로 변환
            transform.position = worldPos; //좌표를 적용한다.

        }
    }
}