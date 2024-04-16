using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MidAirPlan_1 : Monster
{
    public Vector2 pos;
    public bool flip;
    float angle = 360f;
    bool isShoot;
    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
        hp = 1000;
        speed = 45f;
        CreateBullet();
    }
    protected override void CreateBullet()
    {
        StartCoroutine(Shoot());
    }
    public override void GetDamage(int num)
    {
        base.GetDamage(num);
        if (hp > 0)
        {
            StartCoroutine(Hit());
        }
        else if (hp <= 0)
        {
            SpawnItem(0.25f);
            Dead(2575);
        }
    }
    IEnumerator Shoot()
    {
        while(true)
        {
            if(isShoot)
            {
            Instantiate(bullet, ms1.position, Quaternion.identity);
            Instantiate(bullet, ms2.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(3.5f);
        }
    }
    private void Update()
    {
        angle += Time.deltaTime * speed;
        angle %= 360f;
        float x = 3 * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = 3 * Mathf.Sin(angle * Mathf.Deg2Rad);
        if (flip)
            x = -x;
        Vector2 newPosition = new Vector2(pos.x + x, pos.y + y);
        transform.position = newPosition;
    }
    private void OnBecameVisible()
    {
        isShoot = true;
        speed = 20f;
    }
    private void OnBecameInvisible()
    {
        isShoot = false;
        speed = 45f;
    }
}
