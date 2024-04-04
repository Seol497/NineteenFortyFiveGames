using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    protected float health;
    protected float speed;
    protected float delay;
    protected bool isVisible;

    protected IEnumerator Hit()
    {
        Color currentColor = new Color(200f / 255f, 20f / 255f, 20f / 255f, 255f / 255f);
        myRenderer.material.color = currentColor;
        yield return new WaitForSeconds(0.05f);
        currentColor = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
        myRenderer.material.color = currentColor;
        yield break;
    }
}
