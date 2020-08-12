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

    public GameObject playpanel, endpanel, selectpanel, blockPanel;
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

    }

    IEnumerator DataGet()
    {
        blockPanel.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("input_Subject", "Office");//GameManager.Subject
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
        int QIndex = 0;
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
    }
    public void GameEnd(){
        playpanel.SetActive(false);
        endpanel.SetActive(true);
    }

    public void gotohome()
    {
        SceneManager.LoadScene("Main");
    }
}
