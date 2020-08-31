using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WMEnd : MonoBehaviour
{
    public GameObject play, Rank_1, Rank_2, Rank_3, Rank_4, Rank_5;
    private List<GameObject> ranklist = new List<GameObject>();
    public List<string> rank = new List<string>();
    public Text Time;
    public Button nextButton;
    private int touch, totalCard;
    private float TotalTime;
    // Start is called before the first frame update

    private string saveUrl, rankUrl, date, question;
    void Start()
    {
        
    }

    public void EndSetting(){
        rankUrl = "faulty337.cafe24.com/RankGet.php";
        totalCard = play.GetComponent<WMplay>().totalCard;
        saveUrl = "faulty337.cafe24.com/datasave.php";
        ranklist.Add(Rank_1);
        ranklist.Add(Rank_2);
        ranklist.Add(Rank_3);
        ranklist.Add(Rank_4);
        ranklist.Add(Rank_5);
        date = System.DateTime.Now.ToString("MM/dd/yyyy");
        question = extract(play.GetComponent<WMplay>().question);
        TotalTime = Mathf.Round(play.GetComponent<WMplay>().totaltime*10)*0.1f;
        Time.text = this.TotalTime.ToString() + "초";
        if(GameManager.Level >= 3)
        {
            nextButton.interactable = false;
        }
        StartCoroutine(DataSave());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DataSave()
    {
        WWWForm form = new WWWForm();
        form.AddField("game", GameManager.Game);
        form.AddField("date", date);
        form.AddField("id", GameManager.ID);
        form.AddField("Lan_1", GameManager.Lan_1);
        form.AddField("Lan_2", GameManager.Lan_2);
        form.AddField("subject", GameManager.Subject);
        form.AddField("level", GameManager.Level);
        form.AddField("question", question);
        form.AddField("totaltime", TotalTime.ToString());
        print(TotalTime);
        WWW webRequest = new WWW(saveUrl, form);
        yield return webRequest;
        print(webRequest.text);
        StartCoroutine(Rankget());
    }

    public string extract(string[] data)
    {
        string result = "";
        for(int i = 0; totalCard > i; i++)
        {
            result += "," + data[i];
        }

        return result;

    }
    IEnumerator Rankget()
    {
        
        WWWForm form = new WWWForm();
        form.AddField("id", GameManager.ID);
        form.AddField("gamename", GameManager.Game);
        WWW web = new WWW(rankUrl, form);
        do
        {
            yield return null;
        }
        while (!web.isDone);
        print(web.text);
        if (web.error != null)
        {
            Debug.LogError("web.error=" + web.error);
            yield break;
        }
        string[] data = web.text.Split(',');
        rank.Clear();
        // for (int i = 0; i < data.Length-1; i+=2)
        // {
        //     ex = new string[2] { data[i], data[i + 1] };
        //     rank.Add(ex);
        // }
        for (int i = 0; i < data.Length; i++)
        {
            if(data[i] != ""){
                rank.Add(data[i]);
            }
            
        }

        for(int i = 0; i < rank.Count; i++){
            // ranklist[i].SetActive(true);
            ranklist[i].GetComponent<MatchingCardScript>().RankSetting(i+1, rank[i]);
        }


    }
}
