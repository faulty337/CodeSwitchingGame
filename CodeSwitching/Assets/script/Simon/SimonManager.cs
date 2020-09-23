using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimonManager : MonoBehaviour
{

    public List<string[]> Data = new List<string[]>();
    public GameObject SelectPanel, PlayPanel, EndPanel, blockPanel2, PracticeEndPanel;
    private string getUrl = "faulty337.cafe24.com/dataget.php";
    public Text Description;
    private string level;
    // Start is called before the first frame update
    void Start()
    {
        SelectPanel.SetActive(true);
        PlayPanel.SetActive(false);
        EndPanel.SetActive(false);
        StartCoroutine(DataGet());
        blockPanel2.SetActive(false);
        Description.text = GameManager.Lan_1+" 단어가 제시되면, [왼쪽(LEFT)] 버튼을 누르고, "+GameManager.Lan_2+" 단어가 제시되면 [오른쪽(RIGHT)] 버튼을 누르세요. \n\n게임설명을 \"꼭\" 보세요.";
    }

    // Update is called once per frame
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
            ex = new string[2] {data[i], data[i+1] };
            Data.Add(ex);
        }
    }
    public void gameStart()
    {
        blockPanel2.SetActive(false);
        PracticeEndPanel.SetActive(false);
        PlayPanel.SetActive(true);
        PlayPanel.GetComponent<Simonplay>().GameStart(GameManager.Level, 40, Data);
        GameManager.state = 10;
    }

    public void gameEnd(){
        PlayPanel.SetActive(false);
        EndPanel.SetActive(true);
        blockPanel2.SetActive(true);
        GameManager.state = 13;
        EndPanel.GetComponent<Simonend>().EndSetting();
    }
    public void PracticeGameStart(){
        blockPanel2.SetActive(false);
        PracticeEndPanel.SetActive(false);
        PlayPanel.SetActive(true);
        PlayPanel.GetComponent<Simonplay>().GameStart(1, 10, practiceQuestion());
        GameManager.state = 9;
    }
    public void PracticeGameEnd(){
        blockPanel2.SetActive(true);
        PlayPanel.SetActive(false);
        PracticeEndPanel.SetActive(true);
        GameManager.state = 12;
    }


    public void gotohome(){
        
        SceneManager.LoadScene("Main");
        GameManager.state = 3;
    }
    public void retry(){
        blockPanel2.SetActive(false);
        PracticeEndPanel.SetActive(false);
        // EndPanel.GetComponent<Simonend>().DelRank();
        EndPanel.SetActive(false);
        SelectPanel.SetActive(true);
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
