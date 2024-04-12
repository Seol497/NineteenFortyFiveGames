using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using static UnityEditor.PlayerSettings;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class StartManager : MonoBehaviour
{
    public RawImage fir;
    public RawImage main;
    public RawImage sub;
    public RawImage select;

    public VideoClip stop_clip;
    public VideoPlayer player;
    public VideoPlayer ranPlayer;
    public List<VideoClip> left_clips;
    public List<VideoClip> right_clips;

    public Image fadeImage;
    public Image insertImage;
    public Image countImage;
    public Image Hidden;
    public List<Sprite> insertSprites;
    public List<Sprite> countSprites;

    [Range(-1, 6)] private int characterCount = 0;

    private bool isIncert = false;
    private bool isStart = false;
    private bool left = false;
    private bool right = false;
    private bool fade;

    private float countdownTimer = 12f;

    private string secretCode = "À§À§¾Æ·¡¾Æ·¡¿Þ¿À¿Þ¿ÀAB";
    private string inputCode = "";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift) && !isIncert)
        {
            StartCoroutine(FadeOut(main));
            StartCoroutine(FadeIn(sub));
            isIncert = true;
        }
        if (Input.GetKeyDown(KeyCode.R) && isIncert && !isStart)
        {
            isStart = true;
            StartCoroutine(FadeOut(sub));
            StartCoroutine(FadeIn(select));
            StartCoroutine(CountDown());
            StartCoroutine(CountInsert());
            player.clip = stop_clip;
        }

        if (isStart)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                left = true;
                right = false;
                characterCount--;
                if (characterCount == -1)
                    characterCount = 5;
                Select(characterCount);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                left = false;
                right = true;
                characterCount++;
                if (characterCount == 6)
                    characterCount = 0;
                Select(characterCount);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (characterCount == 5)
                {
                    int ran = Random.Range(0, 5);
                    Select(ran);
                }
                player.playbackSpeed = 0;
                ranPlayer.playbackSpeed = 0;
                StartCoroutine(FadeOut(select));
                Invoke("LoadNextScene", 2f);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
                inputCode += "À§";
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                inputCode += "¾Æ·¡";
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                inputCode += "¿Þ";
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                inputCode += "¿À";
            else if (Input.GetKeyDown(KeyCode.A))
                inputCode += "A";
            else if (Input.GetKeyDown(KeyCode.B))
                inputCode += "B";

            if (inputCode.Equals(secretCode.Substring(0, inputCode.Length)))
            {
                if (inputCode.Length == secretCode.Length)
                {
                    ranPlayer.gameObject.SetActive(false);
                    Hidden.gameObject.SetActive(true);
                    player.gameObject.SetActive(false);
                    StartCoroutine(FadeOut(select));
                    Invoke("LoadNextScene", 2f);

                    inputCode = "";
                }
            }
            else
            {

                inputCode = "";
            }
        }
    }

    void Start()
    {
        fir.gameObject.SetActive(true);
        main.gameObject.SetActive(false);
        sub.gameObject.SetActive(false);
        select.gameObject.SetActive(false);
        fadeImage.gameObject.SetActive(false);
        Hidden.gameObject.SetActive(false);
        Invoke("TurnUp", 0.5f);
    }

    private void Select(int num)
    {
        if (left)
            player.clip = left_clips[num];
        if(right)
            player.clip = right_clips[num];
    }

    IEnumerator CountInsert()
    {     
        while (countdownTimer > 0f)
        {
            insertImage.sprite = insertSprites[2];
            yield return new WaitForSeconds(0.5f);
            insertImage.sprite = insertSprites[0];
            yield return new WaitForSeconds(0.5f);
            insertImage.sprite = insertSprites[2];
            yield return new WaitForSeconds(0.5f);
            insertImage.sprite = insertSprites[1];
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator CountDown()
    {
        while (countdownTimer > 0f)
        {
            yield return new WaitForSeconds(1f);
            countdownTimer -= 1f;
            if (countdownTimer < 10f)
            countImage.sprite = countSprites[(int)countdownTimer];
        }
        if (characterCount == 5)
        {
            int ran = Random.Range(0, 5);
            Select(ran);
        }
        player.playbackSpeed = 0;
        ranPlayer.playbackSpeed = 0;
        StartCoroutine(FadeOut(select));
        Invoke("LoadNextScene", 2f);
    }
    private void LoadNextScene()
    {
        SceneManager.LoadScene("Stage1");
    }

    // ±î¸ÅÁü
    IEnumerator FadeOut(RawImage image)
    {
        float startAlpha = fadeImage.color.a;
        float timer = 0f;
        if (fade)
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(FadeOut(image));
        }
        else
        {
            fadeImage.gameObject.SetActive(true);
            fade = true;
            yield return new WaitForSeconds(0.2f);
            while (timer < 1.5f)
            {

                timer += Time.deltaTime;
                float alpha = Mathf.Lerp(startAlpha, 1f, timer / 1.5f);
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
                yield return null;
            }
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);
            image.gameObject.SetActive(false);
            fade = false;
        }
    }

    // ¹à¾ÆÁü
    IEnumerator FadeIn(RawImage image)
    {
        if (fade)
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(FadeIn(image));
        }
        else
        {

            float startAlpha = fadeImage.color.a;
            float timer = 0f;
            fade = true;
            yield return new WaitForSeconds(0.2f);
            image.gameObject.SetActive(true);
            while (timer < 2f)
            {

                timer += Time.deltaTime;
                float alpha = Mathf.Lerp(startAlpha, 0f, timer / 2f);
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
                yield return null;
            }
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0f);
            fadeImage.gameObject.SetActive(false);
            fade = false;
        }
    }

    private void TurnUp()
    {
        fir.gameObject.SetActive(false);
        main.gameObject.SetActive(true);
    }
}
