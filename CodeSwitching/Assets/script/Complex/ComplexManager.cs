using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComplexManager : MonoBehaviour
{
    public int TotalStage; //총 스테이지 길이
    public GameObject panel;
    public List<string[]> Data; //문제
    public int[] data;  //사용자 입력
    public int level;
    public Text Description;

    private string leveltext; 

    public GameObject playpanel, endpanel, selectpanel, blockPanel, blockPanel2, PracticeEndPanel, WordNotePanel;
    public string getUrl;
    // Start is called before the first frame update
    void Start()
    {
        Data = new List<string[]>();
        getUrl = "faulty337.cafe24.com/dataget.php";
        StartCoroutine(DataGet());
        data = new int[TotalStage];
        selectpanel.SetActive(true);
        playpanel.SetActive(false);
        endpanel.SetActive(false);
        blockPanel.SetActive(false);
        blockPanel2.SetActive(false);
        ScreenSetting();
        LevelSetting(GameManager.Level);
    }
    
    void ScreenSetting(){
        switch(GameManager.Level){
            case 1:
                leveltext = "총 3개";
                break;
            case 2:
                leveltext = "총 5개";
                break;
            case 3:
                leveltext = "총 7개";
                break;
            default:
                leveltext = "총 3개";
                break;
        }
        Description.text = "\"기억하기\"와 \"의미판단\"하기 두가지 과제를 같이 진행합니다. \n1. "+GameManager.Lan_1+" 단어가 제시되면 기억하세요. \n2. "+GameManager.Lan_2+" 단어가 제시되면 의미판단을 해야 합니다(의미상 가까운 단어 선택).\n3.<기억(RECALL)> 단계에서, 기억하고 있는 "+GameManager.Lan_1+"단어를 고르세요.\n\n"+GameManager.Level+"단계에서는 한번에 "+leveltext+" 단어를 기억하고, "+leveltext+" 단어의 의미판단을 수행해야 합니다.\n게임설명을 \"꼭\" 보세요.";
    }

    void LevelSetting(int level){
        switch(level){
            case 1:
                this.level = 3;
                break;
            case 2:
                this.level = 5;
                break;
            case 3:
                this.level = 7;
                break;
            default:
                this.level = 3;
                break;
        }
    }

    IEnumerator DataGet()
    {
        blockPanel.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("input_Subject", GameManager.Subject);
        form.AddField("game", GameManager.Game);
        form.AddField("Lan1", GameManager.Lan_1);//
        form.AddField("Lan2", GameManager.Lan_2);//
        // form.AddField("input_Subject", "School");
        // form.AddField("game", "Complex");
        // form.AddField("Lan1", "한국어");//
        // form.AddField("Lan2", "영어");//
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
        for (int i = 0; i < data.Length-3; i+=4)
        {
            ex = new string[] { data[i], data[i + 1], data[i+2], data[i+3]};
            Data.Add(ex);
            
        }
        blockPanel.SetActive(false);

    }
    public void PracticeGameStart(){
        blockPanel2.SetActive(false);
        PracticeEndPanel.SetActive(false);
        playpanel.SetActive(true);
        playpanel.GetComponent<ComplexPlay>().StartSetting(5, 3, practiceQuestion());
        GameManager.state = 9;
    }
    public void PracticeGameEnd(){
        blockPanel2.SetActive(true);
        playpanel.SetActive(false);
        PracticeEndPanel.SetActive(true);
        GameManager.state = 12;
    }

    public void GameStart()
    {
        blockPanel2.SetActive(false);
        PracticeEndPanel.SetActive(false);
        playpanel.SetActive(true);
        playpanel.GetComponent<ComplexPlay>().StartSetting(level*5, level, Data);
        GameManager.state = 10;
    }
    public void GameEnd(){
        playpanel.SetActive(false);
        blockPanel2.SetActive(true);
        endpanel.SetActive(true);
        endpanel.GetComponent<ComplexEnd>().EndSetting(level);
        GameManager.state = 13;
    }

    public void gotohome()
    {
        SceneManager.LoadScene("Main");
        GameManager.state = 3;
    }

    public void retry(){
        WordNotePanel.SetActive(false);
        PracticeEndPanel.SetActive(false);
        blockPanel2.SetActive(false);
        endpanel.SetActive(false);
        selectpanel.SetActive(true);
        LevelSetting(GameManager.Level);
        ScreenSetting();
        GameManager.state = 8;
    }

    public void nextLevel(){
        if(GameManager.Level<3){
            GameManager.Level +=1;
        }
        retry();

    }
    public void WordNotePopup(){
        blockPanel2.SetActive(true);
        WordNotePanel.SetActive(true);
        WordNotePanel.GetComponent<Wordlist>().WordSet(Data, GameManager.Subject);
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
