using System.Collections;
using System.Collections.Generic;
using System.Text;
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

    public int score = 0;
    public int goal = 2000;
    private int targetScore = 10000;
    private int lifes = 3;
    private int credits = 0;
    private int bombs = 2;
    
    private bool isDead = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift) && credits < 9 && score >= targetScore)
        {
            creditImage.enabled = true;
            credits += 2;
            creditText.text = $"{credits}";
            targetScore += targetScore;
        }  
        if((Input.GetKeyDown(KeyCode.KeypadEnter) && lifes == 0 && credits > 0 ))
        {
            ReStart();
        }
    }

    public void Score(int num)
    {
        score += num;
        scoreText.text = $"{score}";
    }

    public void UseBomb()
    {
        bombs--;
        if(bombs == 0)
            bomb1.enabled = false;

        if(bombs == 1)
            bomb2.enabled = false;
    }




    private void ReStart()
    {
        credits--;
        creditText.text = $"{credits}";
        lifes = 3;
        Live();
    }

    private void Live()
    {
        Instantiate(player, player.transform.position, Quaternion.identity);
        if(lifes == 1)
        {
            life1.enabled = false;
        }
        else if(lifes == 2)
        {
            life2.enabled = false;
        }
        else if (lifes == 3)
        {
            life1.enabled = true;
            life2.enabled = true;
        }
        bombs = 2;
        bomb1.enabled = true;
        bomb2.enabled = true;
    }

    public void Dead()
    {
        isDead = true;
        lifes--;
        if(lifes> 0)
        {
            isDead = false;
            Invoke("Live", 1);
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
