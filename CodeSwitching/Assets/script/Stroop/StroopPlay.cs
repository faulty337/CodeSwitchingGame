using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StroopPlay : MonoBehaviour
{
    public Text Question;
    public GameObject manager, blockPanel;
    public string[] color = {"빨강", "노랑", "초록", "파랑"};
    public string[] colorstr = {"빨간", "노란", "초록", "파란"};
    public string[] colorNum;
    public int[,] QuestionIndex;
    public int stage, TotalStage;
    private int level;
    public List<string[]> Data = new List<string[]>();
    public string[] reactionTime, Q, input, Answer;
    public float time, Qtime, totalTime;
    public bool start = false;
    // Start is called before the first frame update
    public void Start()
    {
        
    }

    public void StartSetting(){
        blockPanel.SetActive(true);
        
        StartCoroutine(GameSetting());
        
    }
    IEnumerator GameSetting(){
        colorNum[0] = "<color=#bf2836>";
        colorNum[0] = "<color=#f3c500>";
        colorNum[0] = "<color=#0c7b3f>";
        colorNum[0] = "<color=#004e9e>";
        TotalStage = 40;
        QuestionIndex = new int[TotalStage+1, 2];
        Question.text = "";
        totalTime = 0.0f;
        time = 0.0f;
        start = false;
        stage = 0;
        level = GameManager.Level;
        //{"<color=#bf2836>", "<color=#f3c500>", "<color=#0c7b3f>", "<color=#004e9e>"};
        Data = manager.GetComponent<StroopManager>().Data.ConvertAll(s => s);
        
        reactionTime = new string[TotalStage+1];
        Q = new string[TotalStage+1];
        input = new string[TotalStage+1];
        Answer = new string[TotalStage+1];
        Qtime = 2.0f;
        for(int i = 0; i<TotalStage; i++){
            QuestionMaking(i);
        }

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;
        if(start){
            time += Time.deltaTime;
            if(time > Qtime){
                NextQuestion();
                time = 0.0f;
            }
        }else{
            if(totalTime > 1.0f){
                start = true;
                blockPanel.SetActive(false);
                FirstQuestion();
                totalTime = 0.0f;
            }
        }

    }

    public void ButtonIndex(int index){
        input[stage] = color[index];
        reactionTime[stage] = time.ToString();
        time = 0.0f;
        NextQuestion();
    }

    public void NextQuestion(){
        stage++;
        Question.text = colorNum[QuestionIndex[stage, 0]]+colorstr[QuestionIndex[stage, 1]]+Q[stage]+"</color>";
        if(stage >= TotalStage){
            manager.GetComponent<StroopManager>().gameEnd();
        }
    }
    public void QuestionMaking(int st){
        int ran = Random.Range(0,Data.Count);
        int ran2 = Random.Range(0,2);
        int fake = Random.Range(0,4);
        int ans = Random.Range(0,4);
        QuestionIndex[st, 0] = fake;
        QuestionIndex[st, 1] = ans;
       
        input[st] = "Pass";
        Q[st] = Data[ran][ran2];
        switch (level){
            case 1 :
                Answer[st] = color[ans];
                break;
            case 2 :
                if(ran2 == 1)
                    Answer[st] = color[ans];
                else
                    Answer[st] = "Pass";
                break;
            case 3 :
                if(ran2 == 0)
                    Answer[st] = color[ans];
                else
                    Answer[st] = "Pass";
                break;
        }
    }
    public void FirstQuestion(){
        Question.text = colorNum[QuestionIndex[stage, 0]]+colorstr[QuestionIndex[stage, 1]]+Q[stage]+"</color>";
        if(stage >= TotalStage){
            manager.GetComponent<StroopManager>().gameEnd();
        }
    }

}
