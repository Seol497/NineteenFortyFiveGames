using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Renderer myRenderer;
    private Collider2D[] colliders;



    public float moveSpeed = 5f;

    private int level = 1;
    private int bomb = 2;
    private int summonCount = 0;
    private int powerUp= 0;
    private int helperLv = 0;

    private bool isBlinking = false;
    private bool isDead = false;
    private bool isPlaying = false;
    private bool isShoot = false;
    private bool isDesh = false;
    private bool isInvincible = true;

    Animator ani; //�ִϸ����͸� ������ ����

    public List<Transform> pos;
    public Transform skillPos = null;
    public List <GameObject> bullet;
    public List <GameObject> Helper;
    public GameObject skill;
    public GameObject explosion;


    void Awake()
    {
        myRenderer = GetComponent<Renderer>();
        colliders = GetComponentsInChildren<Collider2D>();
        ani = GetComponent<Animator>();
        level = GameManager.Instance.level;
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
            isBlinking = !isBlinking; // ������ ���� ����

            // isBlinking�� true�� ���İ��� 0����, false�� ���İ��� 1�� ����
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
            // ������ ���ݸ�ŭ ���
            yield return new WaitForSeconds(Random.Range(0.005f, 0.05f));

            elapsedTime += Time.deltaTime;
        }
        Color originalColor = myRenderer.material.color;
        originalColor.a = 1f;
        myRenderer.material.color = originalColor;
        isInvincible = false;
        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("EBullet") && !isDead && !isInvincible)
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
        if (collision.gameObject.CompareTag("PowerItem"))
        {
            GameManager.Instance.EatItem(0, 1);
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.CompareTag("BombItem"))
        {
            if(bomb < 2)
            {
                bomb++;
                GameManager.Instance.EatItem(1, 0);
                Destroy(collision.gameObject);
            }           
        }
    }

    IEnumerator Shoot()
    {

        for (int i = 0; i < 3; i++)
        {
            if (level == 1 || level == 3)
                Instantiate(bullet[powerUp], pos[0].position, Quaternion.identity);
            if (level == 2)
            {
                Instantiate(bullet[powerUp], pos[1].position, Quaternion.identity);
                Instantiate(bullet[powerUp], pos[2].position, Quaternion.identity);
            }
            if (level == 4 && summonCount == 0)
            {
                Instantiate(Helper[0], pos[3].position, Quaternion.identity);
                Instantiate(Helper[0], pos[4].position, Quaternion.identity);
                summonCount++;
            }
            if (level == 5 && summonCount == 1)
            {
                Instantiate(Helper[0], pos[5].position, Quaternion.identity);
                Instantiate(Helper[0], pos[6].position, Quaternion.identity);
                summonCount++;
            }
            if (level == 6)
            {
                GameObject[] helperObjects = GameObject.FindGameObjectsWithTag("Helper");
                foreach (GameObject helperObject in helperObjects)
                {
                    Destroy(helperObject);
                }
                powerUp++;
                summonCount = 0;                
                level = 1;
            }
            if (powerUp == 2)
            {
                helperLv++;
            }

            yield return new WaitForSeconds(0.04f);
        }        
        yield return new WaitForSeconds(0.05f);
        isShoot = false;
        yield break;
    }

    IEnumerator Desh()
    {
        isInvincible = true;
        moveSpeed = 15;
        yield return new WaitForSeconds(0.2f);
        moveSpeed = 7.5f;
        yield return new WaitForSeconds(0.4f);
        isInvincible = false;
        ani.SetBool("boost", false);
        yield return new WaitForSeconds(1f);
        isDesh = false;
    }

    void Update()
    {
        if (isPlaying)
        {


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

            if (Input.GetKeyDown(KeyCode.Space) && !isDesh)
            {
                isDesh = true;
                ani.SetBool("boost", true);
                StartCoroutine(Desh());
            }

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


            transform.Translate(moveX, moveY, 0);


            //ĳ������ ���� ��ǥ�� ����Ʈ ��ǥ��� ��ȯ���ش�.
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            viewPos.x = Mathf.Clamp01(viewPos.x); //x���� 0�̻�, 1���Ϸ� �����Ѵ�.
            viewPos.y = Mathf.Clamp01(viewPos.y); //y���� 0�̻�, 1���Ϸ� �����Ѵ�.
            Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);//�ٽÿ�����ǥ�� ��ȯ
            transform.position = worldPos; //��ǥ�� �����Ѵ�.

        }
    }
}