using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if(player == null)
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 targetPosition = player.transform.position;
        Vector3 currentPosition = transform.position;

        // 타겟과의 거리를 계산
        Vector3 distanceVector = targetPosition - currentPosition;

        // 거리 벡터를 사용하여 각도를 계산
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        angle += 90;
        Quaternion newRotation = Quaternion.Euler(0f, 0f, angle);

        // 회전값을 적용
        transform.rotation = newRotation;
    }
}
