using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NBackend : MonoBehaviour
{
    public int[,] data;
    private float totalscore;
    public GameObject play, rankObj, rankbase;
    public Text scoreObj;
    public Text timeObj;
    private string saveUrl, rankUrl;
    private string Lan_1, Lan_2, id, Subject, Game, date, question, answer, input, correct, reactionTime;
    private int time, score, totalstage;
    public List<string[]> rank = new List<string[]>(); //문제
    
    // Start is called before the first frame update
    void Start()
    {
        totalstage = play.GetComponent<NBackplay>().TotalStage;
        saveUrl = "faulty337.cafe24.com/datasave.php";
        rankUrl = "faulty337.cafe24.com/RankGet.php";
        date = System.DateTime.Now.ToString("MM/dd/yyyy");
        
        answer = extract(play.GetComponent<NBackplay>().Answer);
        print(answer);
        input = extract(play.GetComponent<NBackplay>().input);
        print(input);
        reactionTime = extract(play.GetComponent<NBackplay>().RTime);
        print(reactionTime);
        correct = CorrectResult(play.GetComponent<NBackplay>().Answer,play.GetComponent<NBackplay>().input);
        print(correct);
        question = QuestionResult(play.GetComponent<NBackplay>().Q);
        print(question);
        // question = extract(play.GetComponent<NBackplay>().Q[]);
        StartCoroutine(DataSave());
        timeObj.text = System.Math.Truncate(play.GetComponent<NBackplay>().totalTime).ToString() + " s";
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
        form.AddField("answer", answer);
        form.AddField("input", input);
        form.AddField("correct", correct);
        form.AddField("reactionTime", reactionTime);
        form.AddField("totaltime", System.Math.Truncate(play.GetComponent<NBackplay>().totalTime).ToString());
        form.AddField("totalscore", System.Math.Truncate(totalscore).ToString());
        WWW webRequest = new WWW(saveUrl, form);
        yield return webRequest;
        StartCoroutine(Rankget());
    }
    
    public string CorrectResult(string[] ans, string[] input){
        string result = "";
        for(int i=0; i<totalstage; i++){
            if(ans[i] == input[i]){
                result += ",correct";
                score++;
            }else{
                result += ",incorrect";
            }
        }
        totalscore = score * (100/totalstage);
        scoreObj.text = System.Math.Truncate(totalscore).ToString() + " %";
        return result;
    }

    public string QuestionResult(string[,] Q){
        string result = "";
        for(int i = 0; i < totalstage; i++){
            print(Q[i,1]);
            result+= ","+Q[i, 1];
        }
        return result;
        
    }

    public string extract(string[] data)
    {
        string result = "";
        for(int i = 0; totalstage > i; i++)
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
            print(data[i] + " | "+data[i + 1]);
            rank.Add(ex);
        }

        for(int i = 0; i < rank.Count; i++){
            var rankdata = Instantiate(rankObj);
            rankdata.SetActive(true);
            rankdata.transform.SetParent(rankbase.transform);
            rankdata.GetComponent<Rankscript>().RankSetting(i+1, rank[i][0], rank[i][1]);
        }

    }
}