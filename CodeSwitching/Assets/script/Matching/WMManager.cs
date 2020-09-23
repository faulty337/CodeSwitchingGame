using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WMManager : MonoBehaviour
{

    public GameObject PlayPanel, SelectPanel, EndPanel, blockpanel, blockPanel2, PracticeEndPanel;
    public List<string[]> Data = new List<string[]>();
    public string getUrl;
    public int failCount, touchCount;
    public float playtime;

    public Text Description;
    private string leveltext;


    void Start()
    {
        getUrl = "faulty337.cafe24.com/dataget.php";
        blockpanel.SetActive(false);
        PlayPanel.SetActive(false);
        EndPanel.SetActive(false);
        SelectPanel.SetActive(true);
        StartCoroutine(DataGet());
        ScreenSetting(GameManager.Level);

    }

    public void ScreenSetting(int level){
        switch(level){//GameManager.Level
            case 1:
                leveltext = "1단계에서는 모두 3쌍을";
                break;
            case 2:
                leveltext = "2단계에서는 모두 6쌍을";
                break;
            case 3:
                leveltext = "3단계에서는 모두 8쌍을";
                break;
            default:
                leveltext = "1단계에서는 모두 4쌍을";
                break;
        }
        Description.text = "서로 같은 뜻의 단어쌍을 찾으세요. "+leveltext+" 찾으면 됩니다.\n\n게임설명을 \"꼭\" 보세요.";
    }
    
    public void PracticeGameStart(){
        blockPanel2.SetActive(false);
        PracticeEndPanel.SetActive(false);
        PlayPanel.SetActive(true);
        PlayPanel.GetComponent<WMplay>().StartSetting(1, practiceQuestion());
        GameManager.state = 9;
    }
    public void PracticeGameEnd(){
        blockPanel2.SetActive(true);
        PlayPanel.SetActive(false);
        PracticeEndPanel.SetActive(true);
        GameManager.state = 12;
    }

    public void GameStart()
    {
        blockPanel2.SetActive(false);
        PracticeEndPanel.SetActive(false);
        PlayPanel.SetActive(true);
        PlayPanel.GetComponent<WMplay>().StartSetting(GameManager.Level, Data);
        SelectPanel.SetActive(false);
        GameManager.state = 10;
    }

    public void GameEnd(){
        PlayPanel.SetActive(false);
        SelectPanel.SetActive(true);
        blockPanel2.SetActive(true);
        EndPanel.SetActive(true);
        EndPanel.GetComponent<WMEnd>().EndSetting();
        GameManager.state = 13;
    }

    public void retry(){
        blockPanel2.SetActive(false);
        PracticeEndPanel.SetActive(false);
        EndPanel.SetActive(false);
        SelectPanel.SetActive(true);
        ScreenSetting(GameManager.Level);
        GameManager.state = 8;
    }

    public void nextLevel(){
        if(GameManager.Level<3){
            GameManager.Level +=1;
        }
        retry();

    }

    IEnumerator DataGet()
    {
        WWWForm form = new WWWForm();
        form.AddField("input_Subject", GameManager.Subject);
        form.AddField("Lan1", GameManager.Lan_1);
        form.AddField("Lan2", GameManager.Lan_2);
        WWW web = new WWW(getUrl, form);
        do
        {
            yield return null;
        }
        while (!web.isDone);
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

    public void gotohome()
    {
        SceneManager.LoadScene("Main");
        GameManager.state = 3;
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
