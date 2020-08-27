using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StroopEnd : MonoBehaviour
{
    public int[,] data;
    public GameObject play, Rank_1, Rank_2, Rank_3, Rank_4, Rank_5;
    private List<GameObject> ranklist = new List<GameObject>();
    public Text scoreObj;
    public Text timeObj;
    public string saveUrl;
    private string Len_1, Len_2, id, Subject, Game, date, question, answer, input, correct, reactionTime, rankUrl;
    private int time, totalstage, score, totalscore;
    public List<string[]> rank = new List<string[]>(); //문제
    
    // Start is called before the first frame update
    public void Start()
    {
        
    }
    public void EndSetting(){
        totalstage = play.GetComponent<StroopPlay>().TotalStage;
        saveUrl = "faulty337.cafe24.com/datasave.php";
        rankUrl = "faulty337.cafe24.com/RankGet.php";
        date = System.DateTime.Now.ToString("MM/dd/yyyy");
        ranklist.Add(Rank_1);
        ranklist.Add(Rank_2);
        ranklist.Add(Rank_3);
        ranklist.Add(Rank_4);
        ranklist.Add(Rank_5);
        answer = extract(play.GetComponent<StroopPlay>().Answer);
        // print(answer);
        input = extract(play.GetComponent<StroopPlay>().input);
        // print(input);
        reactionTime = extract(play.GetComponent<StroopPlay>().reactionTime);
        // print(reactionTime);
        correct = CorrectResult(play.GetComponent<StroopPlay>().Answer,play.GetComponent<StroopPlay>().input);
        // print(correct);
        question = extract(play.GetComponent<StroopPlay>().Q);
        // print(question);
        timeObj.text = System.Math.Truncate(play.GetComponent<StroopPlay>().totalTime).ToString();
        // question = extract(play.GetComponent<NBackplay>().Q[]);
        StartCoroutine(DataSave());
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
        form.AddField("totaltime", System.Math.Truncate(play.GetComponent<StroopPlay>().totalTime).ToString());
        form.AddField("totalscore", totalscore);
        WWW webRequest = new WWW(saveUrl, form);
        yield return webRequest;
        // print(webRequest.text);
        StartCoroutine(Rankget());
    }
    
    public string CorrectResult(string[] ans, string[] input){
        string result = "";
        int sc = 0;
        for(int i=0; i<totalstage; i++){
            if(ans[i] == input[i]){
                result += ",correct";
                sc++;
            }else{
                result += ",incorrect";
            }
        }
        float score = sc * (100/totalstage);
        totalscore = System.Convert.ToInt32(score);
        // System.Math.Truncate(score);
        scoreObj.text = totalscore.ToString() + " %";
        return result;
    }

    public string QuestionResult(string[,] Q){
        string result = "";
        for(int i = 0; i < totalstage; i++){
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
        rank.Clear();
        for (int i = 0; i < data.Length-1; i+=2)
        {
            ex = new string[2] { data[i], data[i + 1] };
            rank.Add(ex);
        }
        
        for(int i = 0; i < rank.Count; i++){
            // ranklist[i].SetActive(true);
            ranklist[i].GetComponent<Rankscript>().RankSetting(i+1, rank[i][0], rank[i][1]);
        }

    }

}
