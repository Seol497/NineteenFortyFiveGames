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
    private List<Sprite> score_Sprite;
    [SerializeField]
    private List<Image> score_Image;

    [SerializeField]
    private GameObject player;
    #endregion
    private int targetScore = 10000;

    public int level = 1;
    public int score = 0;
    public int goal = 2000;
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
        if (score >= 9999999)
        {
            score = 9999999;
        }
        string scoreString = score.ToString();
        for(int i = 0; i < scoreString.Length; i++)
        {
            score_Image[i].enabled = true;
            for(int j = 0; j < 11; j++)
            {
                int l = int.Parse(scoreString[i].ToString());
                if (l == j)
                {
                    score_Image[i].sprite = score_Sprite[j];
                }
            }
        }
        
    }

    public void UseBomb()
    {
        bombs--;
        if(bombs == 0)
            bomb1.enabled = false;

        if(bombs == 1)
            bomb2.enabled = false;
    }

    public void EatItem(int num, int num2)
    {
        bombs += num;
        switch(bombs)
        {
            case 1:
                bomb1.enabled = true;
                bomb2.enabled = false;
                break;
            case 2:
                bomb1.enabled = true;
                bomb2.enabled = true;
                break;
        }
        level += num2;
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
        lifes--;
        if(lifes> 0)
        {
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
        if (credits > 0)
        {
            creditText.text = $"{credits}";
            creditImage.enabled = true;
        }
        else
        {
            creditText.text = "";
            creditImage.enabled = false;
        }
        foreach(Image image in score_Image)
        {
            image.enabled = false;
        }
        score_Image[0].enabled = true;
        score_Image[0].sprite = score_Sprite[0];
    }







   
}
