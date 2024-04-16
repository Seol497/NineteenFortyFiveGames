using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
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
    [Header("Scene")]
    [SerializeField] GameObject uI;
    [SerializeField] GameObject deadScene;
    [SerializeField] GameObject playScene;

    [Header("Text")]
    [SerializeField] Text creditText1;
    [SerializeField] Text creditText2;
    [SerializeField] Text countDown;

    [Header("Life")]
    [SerializeField] Image life1;
    [SerializeField] Image life2;

    [Header("Bomb")]
    [SerializeField] Image bomb1;
    [SerializeField] Image bomb2;

    [Header("Coin")]
    [SerializeField] Image creditImage;

    [Header("Score")]
    [SerializeField] List<Sprite> score_Sprite;
    [SerializeField] List<Image> score_Image;

    [Header("Item")]
    [SerializeField] GameObject item_PowerUp;
    [SerializeField] GameObject item_Bomb;


    [SerializeField]
    private GameObject player;

    #endregion
    private int targetScore = 10000;
    private string nextSceneName;
    private int SceneCount = 2;

    [Range(0, 9999999)] public int score = 0;
    public int goal = 2000;
    public int level = 1;
    public int powerUp = 0;
    public int bombs = 2;

    private int lifes = 3;
    private int credits = 0;
    private float currentTime = 10;
    private bool isCounting = false;
    private bool gameStart = false;
    private bool easy = false;


    #region UI
    private void Update()
    {
        if (gameStart)
        {
            if (Input.GetKeyDown(KeyCode.RightShift) && credits < 9 && (easy || score >= targetScore))
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

    IEnumerator ShowDeadScene()
    {
        yield return new WaitForSeconds(2);
        playScene.SetActive(false);
        deadScene.SetActive(true);
        creditText2.text = $"{credits}";
        isCounting = true;
        PauseGame();
    }
    #endregion

    #region 플레이어 관련
    public void SpawnItem()
    {

        float percentage = level * powerUp;
        float randomValue = Random.Range(0, percentage);

        for (int i = 0; i < randomValue * 1.5f; i++)
            Instantiate(item_PowerUp, transform.position, Quaternion.identity);

        for (int i = 0; i < randomValue / 3; i++)
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
        GameObject[] helpers = GameObject.FindGameObjectsWithTag("Helper");
        foreach (GameObject helper in helpers)
        {
            Destroy(helper);
        }
        if (lifes > 0)
        {
            Invoke("Live", 1);
        }
        else
        {
            StartCoroutine(ShowDeadScene());
        }
    }
    #endregion

    #region 게임 일시정지/전개
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
    #endregion

    public void LoadNextScene()
    {
        nextSceneName = "Stage" + SceneCount;
        SceneCount++;
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject playerSpawn = GameObject.Find("PlayerSpawn");
        Instantiate(player, playerSpawn.transform.position, Quaternion.identity);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void Awake()
    {
        deadScene.SetActive(false);
        playScene.SetActive(false);
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
        foreach (Image image in score_Image)
        {
            image.enabled = false;
        }
        score_Image[0].enabled = true;
        score_Image[0].sprite = score_Sprite[0];
    }

    public void GameStart(bool difficulty)
    {
        easy = difficulty;
        playScene.SetActive(true);
        gameStart = true;
        Live();
    }

}