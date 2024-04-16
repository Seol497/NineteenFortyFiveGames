using System.Collections;
using UnityEngine;


public class Spawn : MonoBehaviour
{
    GameManager gameManager;
    public GameObject monster_5;
    public GameObject monster_5_H;
    public GameObject boss;
    public bool scoreSpawn;
    public bool stage1 = false;
    public bool stage2 = false;
    public bool countDown = false;

    public float countdownTimer = 100f;
    int score = 0;
    int goal = 0;
    float delay = 1f;

    bool isBossSpawn = false;
    bool spawnMonster_5 = false;
    bool spawnMonster_5_H = false;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        if (gameManager != null)
        {
            score = gameManager.score; 
            goal = gameManager.goal;
        }
    }

    void Start()
    {
        if (stage2)
        {
            stage1 = false;
            StartCoroutine(Spawn_Monster_5());
            StartCoroutine(Spawn_Monster_5_H());
        }
        else if (stage1)
            StartCoroutine(Spawn_Monster_5());
        if (countDown)
        {
            Invoke("OffSpawnManager", countdownTimer);
        }
    }

    private void Update()
    {
        score = gameManager.score;
        if(score > goal && !isBossSpawn && scoreSpawn)
        {
            isBossSpawn = true;
            SpawnBoss();          
        }
    }

    public void OffSpawnManager()
    {
        spawnMonster_5 = false;
        spawnMonster_5 = false;
        StopCoroutine(Spawn_Monster_5());
        StopCoroutine(Spawn_Monster_5_H());
    }

    public void SpawnBoss()
    {
        if (stage1)
        {
            Vector3 pos = new Vector3(0, 10, 0);
            Instantiate(boss, pos, Quaternion.identity);
            spawnMonster_5 = false;
            StopCoroutine(Spawn_Monster_5());
            StartCoroutine(Spawn_Monster_5_H());
        }
        if (stage2)
        {
            Vector3 pos = new Vector3(0, 11, 0);
            Instantiate(boss, pos, Quaternion.identity);
            delay = 4f;
            StartCoroutine(Spawn_Monster_5_H());
            StartCoroutine(Spawn_Monster_5());
        }
    }

    //IEnumerator Spawn_Meteor()
    //{
    //    while (true)
    //    {
    //        if (GameManager.Instance.score > 200)
    //        {
    //            int count = Random.Range(1, 8);
    //            count = 7;
    //            Meteor_Pattern(count);
    //        }
    //        yield return new WaitForSeconds(GameManager.Instance.meteorSpawntime);
    //    }
    //}
    //void Meteor_Pattern(int count)
    //{
    //    meteorPattern.Add(meteor);
    //    if (meteorPattern.Count > 0)
    //    {
    //        float otpos;
    //        float plus;
    //        Vector3 pos = meteorPattern[0].transform.position;

    //        for (int i = 1; i < count; i++)
    //        {
    //            meteorPattern.Add(meteorPattern[0]);
    //        }
    //        otpos = 5f / count;
    //        plus = otpos;

    //        if (count % 2 == 0 && count != 1)
    //        {
    //            float harp = otpos / 2;
    //            pos = new Vector3(pos.x + harp, pos.y, pos.z);
    //            Instantiate(meteorPattern[0], pos, Quaternion.identity);
    //            pos = new Vector3(pos.x - otpos, pos.y, pos.z);
    //            Instantiate(meteorPattern[1], pos, Quaternion.identity);
    //            otpos = otpos + plus;
    //            for (int i = 2; i < count; i++)
    //            {
    //                pos = new Vector3(pos.x + otpos, pos.y, pos.z);
    //                Instantiate(meteorPattern[i], pos, Quaternion.identity);
    //                otpos = -(otpos + plus);
    //                plus = -plus;
    //            }
    //        }
    //        else
    //        {
    //            Instantiate(meteorPattern[0], pos, Quaternion.identity);
    //            for (int i = 1; i < count; i++)
    //            {
    //                pos = new Vector3(pos.x + otpos, pos.y, pos.z);
    //                Instantiate(meteorPattern[i], pos, Quaternion.identity);
    //                otpos = -(otpos + plus);
    //                plus = -plus;
    //            }
    //        }
    //        if (count > 3)
    //        {
    //            GameObject[] meteorObjects = GameObject.FindGameObjectsWithTag("Meteor");
    //            int how = Random.Range(1, 5);
    //            for (int i = 0; i < how; i++)
    //            {
    //                int much = Random.Range(0, count);
    //                Destroy(meteorObjects[much]);
    //            }
    //        }
    //        otpos = 0f;
    //        meteorPattern.Clear();
    //    }
    //}

    IEnumerator Spawn_Monster_5()
    {
        spawnMonster_5 = true;
        while (spawnMonster_5)
        {
            Vector2 pos = new Vector2(Random.Range(-2.45f, 2.45f), Random.Range(10, 25));
            Instantiate(monster_5, pos, Quaternion.identity);
            yield return new WaitForSeconds(delay);
        }
        yield break;
    }
    IEnumerator Spawn_Monster_5_H()
    {
        spawnMonster_5_H = true;
        while (spawnMonster_5_H)
        {
            yield return new WaitForSeconds(0.2f);
            Vector2 pos = new Vector2(Random.Range(-2.45f, 2.45f), Random.Range(10, 20));
            Instantiate(monster_5_H, pos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(delay * 2.5f, delay * 3.5f));
        }
        yield break;
    }
}