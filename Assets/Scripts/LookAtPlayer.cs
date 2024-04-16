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

        // Ÿ�ٰ��� �Ÿ��� ���
        Vector3 distanceVector = targetPosition - currentPosition;

        // �Ÿ� ���͸� ����Ͽ� ������ ���
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        angle += 90;
        Quaternion newRotation = Quaternion.Euler(0f, 0f, angle);

        // ȸ������ ����
        transform.rotation = newRotation;
    }
}
