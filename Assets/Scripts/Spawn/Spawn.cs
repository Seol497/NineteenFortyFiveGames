using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Spawn : MonoBehaviour
{
    GameManager gameManager;
    public GameObject monster_5;
    public GameObject midAirPlan;
    int score = 0;
    int goal = 0;
    bool isBossSpawn = false;

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
        //StartCoroutine("Spawn_Meteor");
        StartCoroutine("Spawn_Monster_5");
    }

    private void Update()
    {
        score = gameManager.score;
        if(score > goal && !isBossSpawn)
        {
            isBossSpawn = true;
            Spawn_MidAirPlan();          
        }
    }

    void Spawn_MidAirPlan()
    {
        Vector3 pos = new Vector3(0, 10, 0);
        Instantiate(midAirPlan, pos, Quaternion.identity);
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
        while (true)
        {
            Vector3 pos = new Vector3(Random.Range(-2.45f, 2.45f), 10, 0);
            Instantiate(monster_5, pos, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }
}