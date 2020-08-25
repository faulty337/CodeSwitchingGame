using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StroopManager : MonoBehaviour
{

    private string getUrl;
    public GameObject selectPanel, playPanel, endPanel, blockPanel2;
    public List<string[]> Data = new List<string[]>();
    public Text Description;
    // Start is called before the first frame update
    private string level;
    void Start()
    {
        getUrl = "faulty337.cafe24.com/dataget.php";
        selectPanel.SetActive(true);
        endPanel.SetActive(false);
        playPanel.SetActive(false);
        StartCoroutine(DataGet());
        switch(GameManager.Level){
            case 1:
                level = "1단계에서는 제시되는 모든 단어의 색깔을 고르세요.";
                break;
            case 2:
                level = "2단계에서는 "+GameManager.Lan_2+" 단어 색깔만 고르세요. "+GameManager.Lan_1+" 단어는 PASS를 누르세요.";
                break;
            case 3:
                level = "3단계에서는 "+GameManager.Lan_1+" 단어 색깔만 고르세요. "+GameManager.Lan_2+" 단어는 PASS를 누르세요.";
                break;
            default:
                level = "1단계에서는 제시되는 모든 단어의 색깔을 고르세요.";
                break;
        }
        Description.text = "단어가 어떤 색깔로 \"쓰여져\" 있는지 고르세요.\n "+level+"\n\n게임설명을 \"꼭\" 보세요.";
    }

    // Update is called once per frame
    void Update()
    {
        
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
        playPanel.SetActive(true);
        playPanel.GetComponent<StroopPlay>().Start();
    }

    public void gameEnd(){
        playPanel.SetActive(false);
        endPanel.SetActive(true);
        endPanel.GetComponent<StroopEnd>().Start();
    }
    public void gotohome()
    {
        SceneManager.LoadScene("Main");
        GameManager.state = 1;
    }
    public void retry(){
        blockPanel2.SetActive(false);
        endPanel.SetActive(false);
        selectPanel.SetActive(true);
    }

    public void nextLevel(){
        if(GameManager.Level>3){
            GameManager.Level +=1;
        }
        retry();

    }

}
