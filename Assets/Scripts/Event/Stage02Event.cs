using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage02Event : MonoBehaviour
{
    public GameObject dangerous;
    public GameObject explosion;
    public Spawn spawn;

    private GameObject target;

    private int plus = 0;
    public float countTime = 20f;
    private float maxcountTime;
    private int difficult = 1;
    private float delay = 2f;

    private bool explosiveEnd;

    private List<Vector2> pos = new List<Vector2>();

    private BackGround background;

    private void Start()
    {
        background = GameObject.Find("BackGround").GetComponent<BackGround>();
        StartCoroutine(Warning());
    }

    IEnumerator ChangeAnimation()
    {
        GameObject player = null;
        while (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            yield return null;
        }
        float elapsedTime = 0f;
        float duration = 5f;
        Vector3 playerstartScale = player.transform.localScale;
        Vector3 targetScale = new Vector3(1f, 0.5f, 1f);
        StartCoroutine(background.TranslateBackGround(elapsedTime, duration));
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            Vector3 newScale = Vector3.Lerp(playerstartScale, targetScale, t);

            player.transform.localScale = newScale;

            yield return null;
        }
        player.transform.localScale = targetScale;
        yield return new WaitForSeconds(0.1f);
        playerstartScale = player.transform.localScale;
        elapsedTime = 0f;
        duration = 2f;
        targetScale = new Vector3(1f, 1f, 1f);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            Vector3 newScale = Vector3.Lerp(playerstartScale, targetScale, t);

            player.transform.localScale = newScale;

            yield return null;
        }
        player.transform.localScale = targetScale;
        yield return new WaitForSeconds(1.4f);
        spawn.SpawnBoss();
    }

    IEnumerator Warning()
    {
        maxcountTime = countTime;
        yield return new WaitForSeconds(7f);
        while (countTime > 0)
        {
            explosiveEnd = false;
            yield return new WaitForSeconds(delay / 2);
            for (int i = 0; i < difficult + plus; i++)
            {
                pos.Add(new Vector2(Random.Range(-3.6f, 3.6f), Random.Range(-6.4f, 6.4f)));
            }
            if (difficult >= 4)
            {
                if (target == null)
                {
                    target = GameObject.FindGameObjectWithTag("Player");
                }
                if (target != null)
                {
                    int i = Random.Range(0, 5);
                    if (i == 2 || i == 3 || i == 4)
                    {
                        int j = Random.Range(0, difficult);
                        pos[j] = new Vector2(target.transform.position.x, target.transform.position.y);
                    }
                }
            }
            for (int i = 0; i < difficult + plus; i++)
            {
                GameObject warning = Instantiate(dangerous, pos[i], Quaternion.identity);
                StartCoroutine(Explos(i));
                Destroy(warning, Mathf.Clamp(delay * 1.5f, 0.05f, 1f));
                yield return new WaitForSeconds(0.05f);
            }
            while (!explosiveEnd)
                yield return new WaitForSeconds(0.05f);
            countTime -= 2 * delay + 0.05f * difficult;
            if (countTime <= 0)
            {
                difficult++;
                if (difficult >= 3)
                    plus += 2;
                if (difficult >= 6)
                    plus++;
                if (difficult >= 8)
                    plus++;
                delay -= 0.195f;
                countTime = maxcountTime - difficult;
            }
            if (difficult >= 10)
                countTime = 0;
            yield return new WaitForSeconds(delay);
            pos.Clear();
        }
        StartCoroutine(ChangeAnimation());
    }
    IEnumerator Explos(int i)
    {
        yield return new WaitForSeconds(Mathf.Clamp(delay * 1.5f, 0.05f, 1f));
        GameObject explosions = Instantiate(explosion, pos[i], Quaternion.identity);
        Destroy(explosions, 0.7f);
        if (i == difficult + plus - 1)
            explosiveEnd = true;
    }

}
