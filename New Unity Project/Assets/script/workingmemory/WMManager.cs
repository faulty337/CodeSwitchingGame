using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WMManager : MonoBehaviour
{

    public GameObject PlayPanel, SelectPanel, EndPanel, blockpanel;
    public List<string[]> Data = new List<string[]>();
    public string getUrl;
    public int failCount, touchCount;
    public float playtime;


    void Start()
    {
        getUrl = "faulty337.cafe24.com/dataget.php";
        blockpanel.SetActive(false);
        PlayPanel.SetActive(false);
        EndPanel.SetActive(false);
        SelectPanel.SetActive(true);
        StartCoroutine(DataGet());
    }

    public void GameStart()
    {
        PlayPanel.SetActive(true);
        SelectPanel.SetActive(false);
    }

    public void GameEnd(int fail, int touch, float time)
    {
        failCount = fail;
        touchCount = touch;
        playtime = time;
        PlayPanel.SetActive(false);
        EndPanel.SetActive(true);
        EndPanel.GetComponent<WMEnd>().Results(failCount, touchCount, playtime);
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

    public void gotohome()
    {
        SceneManager.LoadScene("Main");
    }

    public void nextLevel()
    {
        if(GameManager.Level > 3)
        {
            GameManager.Level = +1;
            PlayPanel.SetActive(true);
        }
    }
}
