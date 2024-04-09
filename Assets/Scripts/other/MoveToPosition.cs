using UnityEngine;

public class MTP : MonoBehaviour
{
    private void Update()
    {
        // ��ǥ ��ġ�� ���� ��ġ ������ �Ÿ��� ���
        Vector2 targetPosition = new Vector2(0, 8);
        float distance = Vector2.Distance(transform.position, targetPosition);

        // ��ǥ ��ġ�� �������� �ʾҴٸ� �̵�
        if (distance > 0.1f)
        {
            // ���� ��ġ���� ��ǥ ��ġ�� ���� �����Ͽ� �̵�
            transform.position = Vector2.Lerp(transform.position, targetPosition, 5 * Time.deltaTime);
        }
    }
}

