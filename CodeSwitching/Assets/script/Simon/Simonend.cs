using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simonend : MonoBehaviour
{
    public int totalscore;
    public GameObject play;
    public Text scoreObj;
    public Text timeObj;
    private string saveUrl;
    private string Len_1, Len_2, id, Subject, Game, date, question, answer, input, correct, reactionTime, position, Compatible;
    private int time, totalstage;
    private float score;
    
    // Start is called before the first frame update
    void Start()
    {
        totalstage = play.GetComponent<Simonplay>().TotalStage;
        saveUrl = "faulty337.cafe24.com/datasave.php";
        date = System.DateTime.Now.ToString("MM/dd/yyyy");
        
        question = extract(play.GetComponent<Simonplay>().Q);
        print(question);
        Compatible = extract(play.GetComponent<Simonplay>().Compatible);
        print(Compatible);
        answer = extract(play.GetComponent<Simonplay>().Answer);
        print(answer);
        position = extract(play.GetComponent<Simonplay>().position);
        print(position);
        input = extract(play.GetComponent<Simonplay>().input);
        print(input);
        correct = CorrectResult(play.GetComponent<Simonplay>().Answer,play.GetComponent<Simonplay>().input);
        print(correct);
        reactionTime = extract(play.GetComponent<Simonplay>().reactionTime);
        print(reactionTime);
        

        
        
        // timeObj.text = play.GetComponent<Simonplay>().totalTime.ToString();
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
        form.AddField("compatible", Compatible);
        form.AddField("answer", answer);
        form.AddField("position", position);
        form.AddField("input", input);
        form.AddField("correct", correct);
        form.AddField("reactionTime", reactionTime);
        WWW webRequest = new WWW(saveUrl, form);
        yield return webRequest;
        print(webRequest.text);
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
        scoreObj.text = (sc*(100/40)).ToString();
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
}
