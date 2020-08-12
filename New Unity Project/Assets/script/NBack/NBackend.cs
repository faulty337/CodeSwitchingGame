using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NBackend : MonoBehaviour
{
    public int[,] data;
    public int totalscore;
    public GameObject play;
    public Text scoreObj;
    public Text timeObj;
    public string saveUrl;
    private string Len_1, Len_2, id, Subject, Game, date, question, answer, input, correct, reactionTime;
    private int time, score, totalstage;
    
    // Start is called before the first frame update
    void Start()
    {
        totalstage = play.GetComponent<NBackplay>().TotalStage;
        saveUrl = "faulty337.cafe24.com/datasave.php";
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
    }

    IEnumerator DataSave()
    {
        WWWForm form = new WWWForm();
        form.AddField("game", GameManager.Game+"Data");
        form.AddField("date", date);
        form.AddField("id", GameManager.ID);
        form.AddField("Len_1", GameManager.Len_1);
        form.AddField("Len_2", GameManager.Len_2);
        form.AddField("subject", GameManager.Subject);
        form.AddField("level", GameManager.Level);
        form.AddField("question", question);
        form.AddField("answer", answer);
        form.AddField("input", input);
        form.AddField("correct", correct);
        form.AddField("reactionTime", reactionTime);
        WWW webRequest = new WWW(saveUrl, form);
        yield return webRequest;
        print(webRequest.text);
    }
    
    public string CorrectResult(string[] ans, string[] input){
        string result = "";
        for(int i=0; i<totalstage; i++){
            if(ans[i] == input[i]){
                
                result += ",correct";
            }else{
                result += ",incorrect";
            }
        }
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