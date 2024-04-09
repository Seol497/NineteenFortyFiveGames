using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public RawImage fir;
    public RawImage main;
    public RawImage sub;
    public Image select;
    public List<Sprite> characterSprites;

    [Range(0, 4)]private int characterCount = 0;

    private bool isIncert = false;
    private bool isStart = false;


    void Start()
    {
        select.enabled = false;
        fir.gameObject.SetActive(true);
        main.gameObject.SetActive(false);
        sub.gameObject.SetActive(false);
        Invoke("TurnUp", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightShift) && !isIncert)
        {
            Destroy(main.gameObject);
            sub.gameObject.SetActive(true);
            isIncert = true;
        }
        if (Input.GetKeyDown(KeyCode.R) && isIncert)
        {
            isStart = true;
            Destroy(sub.gameObject);
            Select();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && isStart)
        {
            characterCount--;
            Select();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && isStart)
        {
            characterCount++;
            Select();
        }
    }
    private void TurnUp()
    {
        Destroy(fir.gameObject);
        main.gameObject.SetActive(true);
    }
    private void Select()
    {
        select.enabled = true;
        select.sprite = characterSprites[characterCount];
    }
}
