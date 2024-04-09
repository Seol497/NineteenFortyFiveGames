using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    protected float hp = 100;
    protected float speed = 1;
    protected float delay = 1;
    protected bool isVisible = false;
    protected int score = 100;
    protected float percentage = 0.3f;
    protected Renderer myRenderer;

    public GameObject bullet;
    public Transform ms1;
    public Transform ms2;
    public GameObject explosion;
    public GameObject item_Bomb;
    public GameObject item_PowerUp;



    private List<GameObject> enemyList = new List<GameObject>();



    protected IEnumerator Hit()
    {
        Color currentColor = new Color(200f / 255f, 20f / 255f, 20f / 255f, 255f / 255f);
        myRenderer.material.color = currentColor;
        yield return new WaitForSeconds(0.05f);
        currentColor = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        myRenderer.material.color = currentColor;
        yield break;
    }

    protected void AllDestory()
    {
        GameObject spawnManager = GameObject.Find("SpawnManager");
        Destroy(spawnManager);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemyList.Add(enemy);
        }
        foreach (GameObject enemy in enemyList)
        {
            enemy.gameObject.GetComponent<Monster>().GetDamage(9999999);
        }
        GameObject player = null;
        Player playerController = null;
        while(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerController = player.GetComponent<Player>();
            }               
        }
        if (playerController != null)
        {
            playerController.StartCoroutine(playerController.End());
        }
    }

    public virtual void GetDamage(int num)
    {
        hp -= num;        
    }

    protected void Dead(int score)
    {
        GameObject go = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(go, 1);


        GameManager.Instance.Score(score);

        Destroy(gameObject);
    }

    protected void SpawnItem(float percentage)
    {
        // ������ ���� �����Ͽ� Ȯ�� ���
        float randomValue = Random.value;

        // ���� Ȯ���� ���Ͽ� GameObject ���� ���� ����
        if (randomValue < percentage)
        {
            // Ȯ���� ���� GameObject ����
            Instantiate(item_Bomb, transform.position, Quaternion.identity);
        }
        if (randomValue * 3 < percentage)
        {
            Instantiate(item_PowerUp, transform.position, Quaternion.identity);
        }
    }

    protected abstract void CreateBullet();   
}
