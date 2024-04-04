using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expolision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x += 0.003f;
        currentScale.y += 0.003f;
        currentScale.z += 0.003f;
        transform.localScale = currentScale;
    }
}
