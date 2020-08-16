using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimonManager : MonoBehaviour
{

    public List<string[]> Data = new List<string[]>();
    public GameObject SelectPanel, PlayPanel, EndPanel;
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
        Description.text = GameManager.Lan_1+" 단어가 제시되면, [왼쪽(LEFT)] 버튼을 누르고, "+GameManager.Lan_2+" 단어가 제시되면 [오른쪽(RIGHT)] 버튼을 누르세요. \n\n게임설명을 \"꼭\" 보세요.";
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
        PlayPanel.SetActive(true);
    }

    public void gameEnd(){
        PlayPanel.SetActive(false);
        EndPanel.SetActive(true);
    }

    public void gotohome(){
        GameManager.state = 2;
        SceneManager.LoadScene("Main");
    }
}
