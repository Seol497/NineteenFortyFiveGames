using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelper : MonoBehaviour
{
    public float offsetX;
    public float offsetY;

    public GameObject bullet;
    public Transform ms;

    public float followSpeed = 5f;

    private Transform playerTransform;
    private bool isShoot = false;

    private void Start()
    {
        // Player�� ã�Ƽ� Transform���� ���
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player�� ã�� �� �����ϴ�. Player�� 'Player' �±׸� �߰��ϼ���.");
        }
    }
    private void Update()
    {
        // Player�� ��ġ�κ��� offsetX, offsetY ��ŭ �̵��� ��ġ�� �̵�
        if (playerTransform != null)
        {
            Vector3 targetPosition = new Vector3(playerTransform.position.x + offsetX, playerTransform.position.y + offsetY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.S) && !isShoot)
        {
            isShoot = true;
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(bullet, ms.position, Quaternion.identity);
            yield return new WaitForSeconds(0.04f);
        }
        yield return new WaitForSeconds(0.05f);
        isShoot = false;
        yield break;
    }
}
