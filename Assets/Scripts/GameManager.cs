using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //싱글톤
    public static GameManager Instance;

    #region SerializeField 
    [Tooltip("UI 관련")]
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text creditText;
    [SerializeField]
    private Image life1;
    [SerializeField]
    private Image life2;
    [SerializeField]
    private Image bomb1;
    [SerializeField]
    private Image bomb2;
    [SerializeField]
    private Image creditImage;

    [SerializeField]
    private GameObject player;
    #endregion

    private int score = 0;
    private int lifes = 3;
    private int bombs = 2;
    private int credits = 0;
    
    private bool isDead = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift) && credits < 10)
        {
            creditImage.enabled = true;
            credits += 2;
            creditText.text = $"{credits}";
        }      
    }

    private void Live()
    {
        Instantiate(player, player.transform.position, Quaternion.identity);
        lifes--;
        switch (lifes)
        {
            case 0:

                break;
            case 1:
                life1.enabled = false;
                break;
            case 2:
                life2.enabled = false;
                break;
        }
        bombs = 2;
        bomb1.enabled = true;
        bomb2.enabled = true;
    }

    public void Dead()
    {
        isDead = true;
        if(lifes> 0)
        {
            isDead = false;
            Live();
        }
    }
    public void UseBomb()
    {
        bombs--;
        if (bombs > 0)
        {

        }

    }
    private void Awake()
    {
        if(Instance == null)   //null 체크
            Instance = this;   //자기자신 인스턴스해서 저장

    }

    void Start()
    {
        creditText.text = "";
        creditImage.enabled = false;
        scoreText.text = $"{score}";
    }







   
}
