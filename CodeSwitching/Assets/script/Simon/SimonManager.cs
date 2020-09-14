using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimonManager : MonoBehaviour
{

    public List<string[]> Data = new List<string[]>();
    public GameObject SelectPanel, PlayPanel, EndPanel, blockPanel2;
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
            ex = new string[2] {data[i + 1], data[i] };
            Data.Add(ex);
        }
    }
    public void gameStart()
    {
        PlayPanel.SetActive(true);
        PlayPanel.GetComponent<Simonplay>().GameStart(GameManager.Level, 40);
    }

    public void gameEnd(){
        PlayPanel.SetActive(false);
        EndPanel.SetActive(true);
        blockPanel2.SetActive(true);
        
        EndPanel.GetComponent<Simonend>().EndSetting();
    }

    public void gotohome(){
        
        SceneManager.LoadScene("Main");
        GameManager.state = 3;
    }
    public void retry(){
        blockPanel2.SetActive(false);
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
}
