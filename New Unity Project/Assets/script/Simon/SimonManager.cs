using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonManager : MonoBehaviour
{

    public List<string[]> Data = new List<string[]>();
    public GameObject SelectPanel, PlayPanel, EndPanel;
    private string getUrl = "faulty337.cafe24.com/dataget.php";
    // Start is called before the first frame update
    void Start()
    {
        SelectPanel.SetActive(true);
        PlayPanel.SetActive(false);
        EndPanel.SetActive(false);
        StartCoroutine(DataGet());
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
        int QIndex = 0;
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
}
