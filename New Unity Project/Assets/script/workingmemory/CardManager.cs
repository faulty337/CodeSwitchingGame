using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    public int state = 1; //0 => 시작ㄴㄴ, 1 => 게임시작은 했지만 카드 펼치기ㄴㄴ, 2 => 게임시작했고 카드 하나를 펼침, 3 => 게임 끝
    public GameObject PlayPanel, SelectPanel, EndPanel, blockpanel;
    public int CardIndex;
    public List<string[]> Data = new List<string[]>();
    public string getUrl;


    void Start()
    {
        getUrl = "faulty337.cafe24.com/dataget.php";
        blockpanel.SetActive(false);
        SelectPanel.SetActive(true);
        PlayPanel.SetActive(false);
        EndPanel.SetActive(false);
        StartCoroutine(DataGet());
    }

    public void GameStart()
    {
        PlayPanel.SetActive(true);
        SelectPanel.SetActive(false);
        state = 1;
    }

    IEnumerator DataGet()
    {


        WWWForm form = new WWWForm();
        form.AddField("input_Subject", "Office"); //GameManager.Subject
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
        int QIndex = 0;
        string[] ex;
        string[] data = web.text.Split(',');
        for (int i = 0; i < data.Length - 1; i += 2)
        {
            ex = new string[2] { data[i], data[i + 1] };
            Data.Add(ex);
        }

    }
}
