using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestScripts : MonoBehaviour
{
    private string secretCode = "�����Ʒ��Ʒ��޿��޿�AB"; // ������ ���� ����
    private string inputCode = ""; // �Էµ� ������ ������ ����
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Debug.Log("up");

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("up2");

            inputCode += "��";
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            inputCode += "�Ʒ�";
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            inputCode += "��";
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            inputCode += "��";
        else if (Input.GetKeyDown(KeyCode.A))
            inputCode += "A";
        else if (Input.GetKeyDown(KeyCode.B))
            inputCode += "B";

        if (inputCode.Equals(secretCode.Substring(0, inputCode.Length)))
        {
            if (inputCode.Length == secretCode.Length)
            {
                Debug.Log("up3");
                inputCode = "";
            }
        }
        else
        {

            inputCode = "";
        }
    }


}
