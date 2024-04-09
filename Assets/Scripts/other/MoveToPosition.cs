using UnityEngine;

public class MTP : MonoBehaviour
{
    private void Update()
    {
        // 목표 위치와 현재 위치 사이의 거리를 계산
        Vector2 targetPosition = new Vector2(0, 8);
        float distance = Vector2.Distance(transform.position, targetPosition);

        // 목표 위치에 도달하지 않았다면 이동
        if (distance > 0.1f)
        {
            // 현재 위치에서 목표 위치로 선형 보간하여 이동
            transform.position = Vector2.Lerp(transform.position, targetPosition, 5 * Time.deltaTime);
        }
    }
}

