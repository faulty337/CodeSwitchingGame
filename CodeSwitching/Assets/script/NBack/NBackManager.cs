using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NBackManager : MonoBehaviour
{
    public int TotalStage; //총 스테이지 길이
    public GameObject panel;
    public List<string[]> Q; //문제
    public int[] data;  //사용자 입력
    public int N;
    public Text Description;

    private string level; 

    public GameObject playpanel, endpanel, selectpanel, blockPanel, blockPanel2, PracticeEndPanel;
    public string getUrl;
    // Start is called before the first frame update
    void Start()
    {
        Q = new List<string[]>();
        getUrl = "faulty337.cafe24.com/dataget.php";
        StartCoroutine(DataGet());
        N = GameManager.Level;
        TotalStage = 40;
        data = new int[TotalStage];
        selectpanel.SetActive(true);
        playpanel.SetActive(false);
        endpanel.SetActive(false);
        blockPanel.SetActive(false);
        blockPanel2.SetActive(false);
        PracticeEndPanel.SetActive(false);
        ScreenSetting(GameManager.Level);
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
    
    public void ScreenSetting(int level){
        
        switch(level){
            case 1:
                this.level = "1단계에서는 바로 전에";
                break;
            case 2:
                this.level = "2단계에서는 두번 전에";
                break;
            case 3:
                this.level = "3단계에서는 세번 전에";
                break;
            default:
                this.level = "1단계에서는 바로 전에";
                break;
        }
        Description.text = "뜻이 같은 단어가 나오면 \"YES\" \n\n"+this.level+" 제시된 단어와 지금 제시된 단어가 뜻이 같을 때, YES를 누르세요. 그렇지 않은 경우엔 모두 NO를 누르세요.\n\n게임설명을 \"꼭\" 보세요.";
    }

    IEnumerator DataGet()
    {
        blockPanel.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("input_Subject", GameManager.Subject);//
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
        for (int i = 0; i < data.Length-1; i+=2)
        {
            ex = new string[2] { data[i], data[i + 1] };
            Q.Add(ex);
        }
        blockPanel.SetActive(false);

    }

    public void GameStart()
    {
        blockPanel2.SetActive(false);
        PracticeEndPanel.SetActive(false);
        GameManager.state = 10;
        playpanel.SetActive(true);
        playpanel.GetComponent<NBackplay>().GameStart(GameManager.Level, TotalStage, Q);
    }

    public void PracticeGameStart(){
        blockPanel2.SetActive(false);
        PracticeEndPanel.SetActive(false);
        playpanel.SetActive(true);
        playpanel.GetComponent<NBackplay>().GameStart(1, 10, practiceQuestion());
        GameManager.state = 9;
    }
    public void PracticeGameEnd(){
        blockPanel2.SetActive(true);
        playpanel.SetActive(false);
        PracticeEndPanel.SetActive(true);
        GameManager.state = 12;
    }

    public void GameEnd(){
        playpanel.SetActive(false);
        blockPanel2.SetActive(true);
        endpanel.SetActive(true);
        endpanel.GetComponent<NBackend>().EndSetting();
        GameManager.state = 13;
    }

    public void gotohome()
    {
        SceneManager.LoadScene("Main");
        GameManager.state = 3;
    }

    public void retry(){
        PracticeEndPanel.SetActive(false);
        blockPanel2.SetActive(false);
        endpanel.SetActive(false);
        selectpanel.SetActive(true);
        ScreenSetting(GameManager.Level);
        GameManager.state = 8;
    }

    public void nextLevel(){
        if(GameManager.Level<3){
            GameManager.Level +=1;
        }
        retry();

    }
}
