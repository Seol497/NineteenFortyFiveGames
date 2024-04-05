using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{


    public float moveSpeed = 5f;
    private bool isBlinking;
    private Renderer myRenderer;


    void Start()
    {

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        myRenderer = GetComponent<Renderer>();

        Vector2 randomDirection = Random.insideUnitSphere.normalized;


        rb.velocity = randomDirection * moveSpeed;
        StartCoroutine(BlinkAlpha());
    }


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == "Wall")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null && rb.velocity.x != 0)
            {
                rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            }
        }

        else if (other.gameObject.name == "Floor")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null && rb.velocity.y != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
            }
        }
    }

    IEnumerator BlinkAlpha()
    {
        yield return new WaitForSeconds(7);
        float elapsedTime = 0f;
        float blinkSpeed = 0.1f;
        
        while (elapsedTime < 1.5f)
        {
            isBlinking = !isBlinking;


            Color currentColor = myRenderer.material.color;
            currentColor.a = isBlinking ? 0f : 1f;
            myRenderer.material.color = currentColor;
            if (elapsedTime > 0.5f)
            yield return new WaitForSeconds(Random.Range(blinkSpeed / 2, blinkSpeed));

            elapsedTime += Time.deltaTime;
            blinkSpeed -= 0.00005f;
        }
        Color originalColor = myRenderer.material.color;
        originalColor.a = 0f;
        myRenderer.material.color = originalColor;
        Destroy(gameObject);
        yield break;
    }
}
