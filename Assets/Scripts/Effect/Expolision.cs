using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expolision : MonoBehaviour
{
    
    public float scaleX = 0.003f;
    public float scaleY = 0.003f;
    public float scaleZ = 0.003f;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x += scaleX;
        currentScale.y += scaleY;
        currentScale.z += scaleZ;
        transform.localScale = currentScale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EBullet"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<Monster>().GetDamage(2000);
        }

    }
}
