using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //싱글톤
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

            }
            return instance;
        }
    }

    #region SerializeField 
    [Header("씬")]
    [SerializeField]
    private GameObject uI;
    [SerializeField]
    private GameObject deadScene;
    [SerializeField]
    private GameObject playScene;
    [Header("Text")]
    [SerializeField]
    private Text creditText1;
    [SerializeField]
    private Text creditText2;
    [SerializeField]
    private Text countDown;
    [Header("라이프")]
    [SerializeField]
    private Image life1;
    [SerializeField]
    private Image life2;
    [Header("폭탄")]
    [SerializeField]
    private Image bomb1;
    [SerializeField]
    private Image bomb2;
    [Header("코인")]
    [SerializeField]
    private Image creditImage;
    [Header("스코어")]
    [SerializeField]
    private List<Sprite> score_Sprite;
    [SerializeField]
    private List<Image> score_Image;
    [Header("아이템")]
    [SerializeField]
    private GameObject item_PowerUp;
    [SerializeField]
    private GameObject item_Bomb;

    public string nextSceneName;

    [SerializeField]
    private GameObject player;

    #endregion
    private int targetScore = 10000;

    [Range(0,9999999)] public int score = 0;
    public int goal = 2000;
    public int level = 1;
    public int powerUp = 0;
    public int bombs = 2;

    private int lifes = 3;
    private int credits = 0;
    private float currentTime = 10;
    private bool isCounting = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift) && credits < 9 && score >= targetScore)
        {
            creditImage.enabled = true;
            credits++;
            creditText1.text = $"{credits}";
            creditText2.text = $"{credits}";
            targetScore += 10000 + targetScore / 4;
        }
        if ((Input.GetKeyDown(KeyCode.R) && lifes == 0 && credits > 0))
        {
            deadScene.SetActive(false);
            playScene.SetActive(true);
            currentTime = 10;
            isCounting = false;
            ResumeGame();
            ReStart();
        }
        if (isCounting)
        {

            currentTime -= Time.unscaledDeltaTime;

            // UI에 시간 표시
            countDown.text = Mathf.CeilToInt(currentTime).ToString();

            // 시간이 0이 되면 카운트다운 종료
            if (currentTime <= 0)
            {
                currentTime = 10;
                isCounting = false;
                Destroy(gameObject);
                Destroy(uI.gameObject);
                ResumeGame();
                SceneManager.LoadScene("Main");
            }
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
        for (int i = 0; i < scoreString.Length; i++)
        {
            score_Image[i].enabled = true;
            for (int j = 0; j < 11; j++)
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
        if (bombs == 0)
            bomb1.enabled = false;

        if (bombs == 1)
            bomb2.enabled = false;
    }

    public void EatItem(int num, int num2)
    {
        bombs += num;
        switch (bombs)
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
        if (level == 5)
        {
            powerUp++;
            if (powerUp > 5)
                powerUp = 5;
            level = 1;
        }
    }

    public void SpawnItem()
    {

        float percentage = level * powerUp;
        float randomValue = Random.Range(0, percentage);

        for (int i = 0; i < randomValue; i++)
            Instantiate(item_PowerUp, transform.position, Quaternion.identity);

        for (int i = 0; i < randomValue / 2; i++)
            Instantiate(item_Bomb, transform.position, Quaternion.identity);


    }

    private void ReStart()
    {
        credits--;
        creditText1.text = $"{credits}";
        lifes = 3;
        Live();
    }

    private void Live()
    {
        level = 1;
        powerUp = 0;
        bombs = 2;
        GameObject playerSpawn = GameObject.Find("PlayerSpawn");
        Instantiate(player, playerSpawn.transform.position, Quaternion.identity);
        if (lifes == 1)
        {
            life1.enabled = false;
        }
        else if (lifes == 2)
        {
            life2.enabled = false;
        }
        else if (lifes == 3)
        {
            life1.enabled = true;
            life2.enabled = true;
        }
        bomb1.enabled = true;
        bomb2.enabled = true;
    }

    public void Dead()
    {
        lifes--;
        if (lifes > 0)
        {
            Invoke("Live", 1);
        }
        else
        {
            StartCoroutine(ShowDeadScene());
        }
    }

    IEnumerator ShowDeadScene()
    {
        yield return new WaitForSeconds(2);
        playScene.SetActive(false);
        deadScene.SetActive(true);
        creditText2.text = $"{credits}";
        isCounting = true;
        PauseGame();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("게임 일시정지");
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        Debug.Log("게임 재개");
    }

    private void Awake()
    {
        deadScene.gameObject.SetActive(false);
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(uI.gameObject);

        if (instance == null)   //null 체크
        {
            instance = this;   //자기자신 인스턴스해서 저장
        }
    }

    void Start()
    {
        if (credits > 0)
        {
            creditText1.text = $"{credits}";
            creditImage.enabled = true;
        }
        else
        {
            creditText1.text = "";
            creditText2.text = "0";
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
