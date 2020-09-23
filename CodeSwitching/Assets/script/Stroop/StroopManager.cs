﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StroopManager : MonoBehaviour
{

    private string getUrl;
    public GameObject selectPanel, PlayPanel, endPanel, blockPanel2, PracticeEndPanel;
    public List<string[]> Data = new List<string[]>();
    public Text Description;
    // Start is called before the first frame update
    private string leveltext;
    void Start()
    {
        getUrl = "faulty337.cafe24.com/dataget.php";
        selectPanel.SetActive(true);
        endPanel.SetActive(false);
        PlayPanel.SetActive(false);
        ScreenSetting(GameManager.Level);
        StartCoroutine(DataGet());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScreenSetting(int level){
        switch(level){
            case 1:
                leveltext = "1단계에서는 제시되는 모든 단어의 색깔을 고르세요.";
                break;
            case 2:
                leveltext = "2단계에서는 "+GameManager.Lan_2+" 단어 색깔만 고르세요. "+GameManager.Lan_1+" 단어는 PASS 하세요.";
                break;
            case 3:
                leveltext = "3단계에서는 "+GameManager.Lan_1+" 단어 색깔만 고르세요. "+GameManager.Lan_2+" 단어는 PASS 하세요.";
                break;
            default:
                leveltext = "1단계에서는 제시되는 모든 단어의 색깔을 고르세요.";
                break;
        }
        Description.text = "단어가 어떤 색깔로 \"쓰여져\" 있는지 고르세요.\n "+leveltext+"\n\n게임설명을 \"꼭\" 보세요.";
    }


    IEnumerator DataGet()
    {


        WWWForm form = new WWWForm();
        form.AddField("input_Subject", GameManager.Subject); //
        form.AddField("Lan1", GameManager.Lan_1);
        form.AddField("Lan2", GameManager.Lan_2);
        WWW web = new WWW(getUrl, form);
        do
        {
            yield return null;
        }
        while (!web.isDone);
        print(web.text);
        if (web.error != null)
        {
            Debug.LogError("web.error=" + web.error);
            yield break;
        }
        string[] ex;
        string[] data = web.text.Split(',');
        for (int i = 0; i < data.Length - 1; i += 2)
        {
            ex = new string[2] { data[i], data[i + 1] };
            Data.Add(ex);
        }
    }

    public void gameStart()
    {
        blockPanel2.SetActive(false);
        PracticeEndPanel.SetActive(false);
        PlayPanel.SetActive(true);
        PlayPanel.GetComponent<StroopPlay>().StartSetting(GameManager.Level, 40, Data);
        GameManager.state = 10;
    }

    public void gameEnd(){
        PlayPanel.SetActive(false);
        blockPanel2.SetActive(true);
        endPanel.SetActive(true);
        endPanel.GetComponent<StroopEnd>().EndSetting();
        GameManager.state = 13;
    }

    public void PracticeGameStart(){
        blockPanel2.SetActive(false);
        PracticeEndPanel.SetActive(false);
        PlayPanel.SetActive(true);
        PlayPanel.GetComponent<StroopPlay>().StartSetting(1, 10, practiceQuestion());
        GameManager.state = 9;
    }
    public void PracticeGameEnd(){
        blockPanel2.SetActive(true);
        PlayPanel.SetActive(false);
        PracticeEndPanel.SetActive(true);
        GameManager.state = 12;
    }
    public void gotohome()
    {
        SceneManager.LoadScene("Main");
        GameManager.state = 3;
    }
    public void retry(){
        blockPanel2.SetActive(false);
        PracticeEndPanel.SetActive(false);
        endPanel.SetActive(false);
        selectPanel.SetActive(true);
        ScreenSetting(GameManager.Level);
        GameManager.state = 7;
    }

    public void nextLevel(){
        if(GameManager.Level<3){
            GameManager.Level +=1;
        }
        retry();
    }

    public List<string[]> practiceQuestion(){
        List<string[]> PData = new List<string[]>();
        PData.Add(new string[]{"일", "one"});
        PData.Add(new string[]{"이", "two"});
        PData.Add(new string[]{"삼", "three"});
        PData.Add(new string[]{"사", "four"});
        PData.Add(new string[]{"오", "five"});
        PData.Add(new string[]{"육", "six"});
        PData.Add(new string[]{"칠", "seven"});
        PData.Add(new string[]{"팔", "eight"});
        PData.Add(new string[]{"구", "nine"});
        PData.Add(new string[]{"십", "ten"});
        PData.Add(new string[]{"십일", "eleven"});
        PData.Add(new string[]{"십이", "twelve"});
        PData.Add(new string[]{"십삼", "thirteen"});
        PData.Add(new string[]{"십사", "fourteen"});
        PData.Add(new string[]{"십오", "fifteen"});
        PData.Add(new string[]{"십육", "sixteen"});
        PData.Add(new string[]{"십칠", "seventeen"});
        PData.Add(new string[]{"십팔", "eighteen"});
        PData.Add(new string[]{"십구", "nineteen"});
        PData.Add(new string[]{"이십", "twenty"});
        return PData;
    }

}
