using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bomb : MonoBehaviour
{
    public GameObject explosion;
    public float Speed = 1.0f;

    private void Start()
    {
        StartCoroutine(Expolision());
    }    

    void Update()
    {
        if (Time.timeScale != 0)
        {
            Vector3 currentScale = transform.localScale;
            currentScale.x -= 0.002f;
            currentScale.y -= 0.002f;
            currentScale.z -= 0.002f;
            transform.localScale = currentScale;
            transform.Translate(Vector2.up * Speed * Time.deltaTime);
        }
    }

    IEnumerator Expolision()
    {
        yield return new WaitForSeconds(2);
        GameObject go = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(go, 1);
        Destroy(gameObject);
        yield break;
    }
}
