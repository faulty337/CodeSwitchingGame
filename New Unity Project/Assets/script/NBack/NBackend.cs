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
    private string Len_1, Len_2, id, Subject, Game, date, question, answer, input, result;
    private int time, score;
    // Start is called before the first frame update
    void Start()
    {
        saveUrl = "faulty337.cafe24.com/datasave.php";
        id = GameManager.ID;
        Len_1 = GameManager.Len_1;
        Len_2 = GameManager.Len_2;
        Subject = GameManager.Subject;
        Game = GameManager.Game;
        date = System.DateTime.Now.ToString("MM/dd/yyyy");
        print("date : ");
        time = (int)play.GetComponent<NBackplay>().time;

        data = (int[,])play.GetComponent<NBackplay>().data.Clone();
        for (int i = 0; data.GetLength(0) > i; i++)
        {
            if (data[i, 1] == data[i, 2])
                data[i, 3] = 1;
            else
                data[i, 3] = 2;
        }
        for (int i= 0; data.GetLength(0) > i; i++)
        {
            if(data[i, 3] == 1)
            {
                score++;
            }
        }
        question = extract(data, 0);
        answer = extract(data, 1);
        input = extract(data, 2);
        result = extract(data, 3);
        scoreObj.GetComponent<Text>().text = "정  확  도  " + score;
        timeObj.GetComponent<Text>().text = "소요시간  " + time;
        string aa = "";
        for (int i = 0; data.GetLength(0) > i; i++)
        {
            for (int j = 0; data.GetLength(1) > j; j++)
            {
                aa += data[i, j] + ",";
            }
            print(aa);
            aa = "";
        }
        StartCoroutine(DataSave());
    }

    IEnumerator DataSave()
    {
        WWWForm form = new WWWForm();
        form.AddField("game", GameManager.Game+"Data");
        form.AddField("id", id);
        form.AddField("subject", Subject);
        form.AddField("level", GameManager.Level);
        form.AddField("data", date);
        form.AddField("score", score);
        form.AddField("time", time);
        form.AddField("question", question);
        form.AddField("answer", answer);
        print("answer : " + answer);
        form.AddField("input", input);
        print("input : " + input);
        form.AddField("result_id", result);
        form.AddField("Len_1", Len_1);
        form.AddField("Len_2", Len_2);

        WWW webRequest = new WWW(saveUrl, form);
        yield return webRequest;
        print(webRequest.text);
    }
    

    public string extract(int[,] data, int index)
    {
        string result = data[0, index]+"";
        for(int i = 1; data.GetLength(0) > i; i++)
        {
            result += ", " + data[i, index];
        }

        return result;

    }
}