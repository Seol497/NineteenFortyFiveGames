using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Stage02Event : MonoBehaviour
{
    public GameObject dangerous;
    public GameObject explosion;

    private GameObject target;

    private float countTime = 15f;
    private int difficult = 1;
    private float delay = 3f;

    private List<Vector2> pos = new List<Vector2>();
    private void Start()
    {
        StartCoroutine(Warning());
    }

    IEnumerator Warning()
    {
        yield return new WaitForSeconds(7f);
        while (countTime > 0)
        {
            yield return new WaitForSeconds(delay);
            for (int i = 0; i < difficult; i++)
            {
                pos.Add(new Vector2(Random.Range(-2f, 2f), Random.Range(-4.8f, 4.8f)));
            }
            if (difficult >= 5)
            {
                if (target == null)
                {
                    target = GameObject.FindGameObjectWithTag("Player");
                    if (target == null)
                    {
                        
                    }
                }
                if(target != null)
                {
                    int j = Random.Range(0, difficult);
                    pos[j] = pos[j] = new Vector2(target.transform.position.x, target.transform.position.y);
                }
            }
            for (int i = 0 ; i < difficult; i++)
            {        
                GameObject warning = Instantiate(dangerous, pos[i], Quaternion.identity);
                Destroy(warning, delay / 5 * 4);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(delay);
            for(int i = 0 ; i < difficult; i++)
            {
                GameObject explosions = Instantiate(explosion, pos[i], Quaternion.identity);
                Destroy(explosions, 0.7f);
            }
            countTime -= 2 * delay + 0.05f * difficult;
            if (countTime <= 0)
            {
                difficult++;
                delay -= 0.29f;
                countTime = 30 * difficult / 4;
            }
            if(difficult >= 10)
                countTime = 0;
            pos.Clear();
        }
    }
}
