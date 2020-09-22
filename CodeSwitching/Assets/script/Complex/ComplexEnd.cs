using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComplexEnd : MonoBehaviour
{
public int[,] data;
    public GameObject play, rankbase, Rank_1, Rank_2, Rank_3, Rank_4, Rank_5;
    public GameObject NextButton;
    private List<GameObject> ranklist = new List<GameObject>();
    public Text SequenceObj, WordObj, levelObj;
    public Text timeObj;
    private string saveUrl, rankUrl;
    private string Lan_1, Lan_2, id, Subject, Game, date, question, AcWord, input, AcSequence;
    private int time, totalstage, totalscore, level;
    public List<string[]> rank = new List<string[]>(); //문제
    
    // Start is called before the first frame update
    public void Start()
    {
        // EndSetting();
    }

    public void EndSetting(int level){
        this.level = level;
        totalstage = play.GetComponent<ComplexPlay>().totalStage;
        saveUrl = "faulty337.cafe24.com/datasave.php";
        rankUrl = "faulty337.cafe24.com/RankGet.php";
        date = System.DateTime.Now.ToString("MM/dd/yyyy");
        ranklist.Add(Rank_1);
        ranklist.Add(Rank_2);
        ranklist.Add(Rank_3);
        ranklist.Add(Rank_4);
        ranklist.Add(Rank_5);
        // print(answer);
        input = extract(play.GetComponent<ComplexPlay>().Input);
        question = extract(play.GetComponent<ComplexPlay>().Q);
        CorrectResult(play.GetComponent<ComplexPlay>().Q, play.GetComponent<ComplexPlay>().Input);
        // print(question);
        // question = extract(play.GetComponent<NBackplay>().Q[]);
        levelObj.text = "["+GameManager.Level.ToString()+"단계]";
        StartCoroutine(DataSave());
        timeObj.text = System.Math.Truncate(play.GetComponent<ComplexPlay>().totalTime).ToString() + " s";
        if(GameManager.Level >= 3){
            NextButton.gameObject.SetActive(false);
        }else{
            NextButton.gameObject.SetActive(true);
        }
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
        form.AddField("input", input);
        form.AddField("AcWord", AcWord);
        form.AddField("AcSequence", AcSequence);
        form.AddField("totaltime", System.Math.Truncate(play.GetComponent<ComplexPlay>().totalTime).ToString());
        // form.AddField("totalscore", totalscore);
        WWW webRequest = new WWW(saveUrl, form);
        yield return webRequest;
        StartCoroutine(Rankget());
    }
    
    public string CorrectResult(string[] Q, string[] input){
        int sc = 0;
        int word = 0;
        string result = "";
        for(int i = 0; i < totalstage; i++){
            print("Q : " + Q[i]);
            print("input : " + input[i]);
        }
        print(input.Length + "  " + Q.Length);
        for(int i=0; i<totalstage; i++){
            print("i : " +i);
            for(int j = level*(i/level); j < level*((i/level)+1); j++){
                print("j : " +j);
                if(input[i] == Q[j]){
                    if(i == System.Array.IndexOf(Q, input[i])){
                        sc++;
                    }
                    word++;
                    break;
                }
            }
        }
        AcWord = ((int) (word * (100/(float)totalstage))).ToString();
        AcSequence = ((int)(sc * (100/(float)totalstage))).ToString();
        // totalscore = (int)score;
        // System.Math.Truncate(score);
        SequenceObj.text = AcSequence+ " %";
        WordObj.text = AcWord + " %";
        return result;
    }

    public string QuestionResult(string[] Q){
        string result = "";
        for(int i = 0; i < totalstage; i++){
            result+= ","+Q[i];
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
        form.AddField("Level", GameManager.Level);
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
