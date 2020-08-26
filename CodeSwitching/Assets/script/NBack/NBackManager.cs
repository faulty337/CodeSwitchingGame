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

    public GameObject playpanel, endpanel, selectpanel, blockPanel, blockPanel2;
    public string getUrl;
    // Start is called before the first frame update
    void Start()
    {
        Q = new List<string[]>();
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
                level = "1단계에서는 바로 전에";
                break;
            case 2:
                level = "2단계에서는 두번 전에";
                break;
            case 3:
                level = "1단계에서는 세번 전에";
                break;
            default:
                level = "1단계에서는 바로 전에";
                break;
        }
        Description.text = "뜻이 같은 단어가 나오면 \"YES\" \n\n"+level+" 제시된 단어와 지금 제시된 단어가 뜻이 같을 때, YES를 누르세요. 그렇지 않은 경우엔 모두 NO를 누르세요.\n\n게임설명을 \"꼭\" 보세요.";
    }

    IEnumerator DataGet()
    {
        blockPanel.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("input_Subject", GameManager.Subject);//
        WWW web = new WWW(getUrl, form);
        do
        {
            yield return null;
        }
        while (!web.isDone);
        blockPanel.SetActive(false);
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

    }

    public void GameStart()
    {
        playpanel.SetActive(true);
        playpanel.GetComponent<NBackplay>().GameStart();
    }
    public void GameEnd(){
        playpanel.SetActive(false);
        blockPanel2.SetActive(true);
        endpanel.SetActive(true);
        endpanel.GetComponent<NBackend>().EndSetting();
    }

    public void gotohome()
    {
        SceneManager.LoadScene("Main");
        GameManager.state = 1;
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
