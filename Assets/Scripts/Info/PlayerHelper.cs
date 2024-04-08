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
        // Player를 찾아서 Transform으로 등록
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player를 찾을 수 없습니다. Player에 'Player' 태그를 추가하세요.");
        }
    }
    private void Update()
    {
        // Player의 위치로부터 offsetX, offsetY 만큼 이동한 위치로 이동
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
