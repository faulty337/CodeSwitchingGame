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
    public int N;
    public Text Description;

    private string level; 

    public GameObject playpanel, endpanel, selectpanel, blockPanel, blockPanel2;
    public string getUrl;
    // Start is called before the first frame update
    void Start()
    {
        Data = new List<string[]>();
        getUrl = "faulty337.cafe24.com/dataget.php";
        StartCoroutine(DataGet());
        N = 2;
        TotalStage = 40;
        data = new int[TotalStage];
        selectpanel.SetActive(true);
        playpanel.SetActive(false);
        endpanel.SetActive(false);
        blockPanel.SetActive(false);
        blockPanel2.SetActive(false);
        switch(GameManager.Level){
            case 1:
                level = "총 3개";
                break;
            case 2:
                level = "총 5개";
                break;
            case 3:
                level = "총 7개";
                break;
            default:
                level = "총 3개";
                break;
        }
        Description.text = "\"기억하기\"와 \"의미판단\"하기 두가지 과제를 같이 진행합니다. \n1. "+GameManager.Lan_1+" 단어가 제시되면 기억하세요. \n2. "+GameManager.Lan_2+" 단어가 제시되면 의미판단을 해야 합니다(의미상 가까운 단어 선택).\n3.<기억(RECALL)> 단계에서, 깅거하고 있는 "+GameManager.Lan_1+"단어를 고르세요.\n\n"+GameManager.Level+"단계에서는 한번에 총 "+level+" 단어를 기억하고, 총 "+level+" 단어의 의미판단을 수행해야 합니다.\n\n게임설명을 \"꼭\" 보세요.";
    }

    IEnumerator DataGet()
    {
        blockPanel.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("input_Subject", "School");//GameManager.Subject
        form.AddField("game", "Complex");
        form.AddField("Lan1", "한국어");//GameManager.Lan_1
        form.AddField("Lan2", "영어");//GameManager.Lan_2
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
        for (int i = 0; i < data.Length-2; i+=3)
        {
            ex = new string[3] { data[i], data[i + 1], data[i+2]};
            Data.Add(ex);
            
        }
        blockPanel.SetActive(false);

    }

    public void GameStart()
    {
        playpanel.SetActive(true);
        playpanel.GetComponent<ComplexPlay>().StartSetting(25, 5);
    }
    public void GameEnd(){
        playpanel.SetActive(false);
        blockPanel2.SetActive(true);
        endpanel.SetActive(true);
        // endpanel.GetComponent<ComplexPlay>().EndSetting();
    }

    public void gotohome()
    {
        SceneManager.LoadScene("Main");
        GameManager.state = 3;
    }

    public void retry(){
        blockPanel2.SetActive(false);
        endpanel.SetActive(false);
        selectpanel.SetActive(true);
    }

    public void nextLevel(){
        if(GameManager.Level>3){
            GameManager.Level +=1;
        }
        retry();

    }
}
