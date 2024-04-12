using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestScripts : MonoBehaviour
{
    private string secretCode = "위위아래아래왼오왼오AB"; // 실행할 패턴 설정
    private string inputCode = ""; // 입력된 패턴을 저장할 변수
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Debug.Log("up");

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("up2");

            inputCode += "위";
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            inputCode += "아래";
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            inputCode += "왼";
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            inputCode += "오";
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
